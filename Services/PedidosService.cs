using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ferremas.Api.DTOs;
using Ferremas.Api.Modelos;
using Ferremas.Api.Repositories;

namespace Ferremas.Api.Services
{
    public class PedidosService : IPedidosService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IProductoRepository _productoRepository;
        private readonly IClienteRepository _clienteRepository;

        public PedidosService(IPedidoRepository pedidoRepository,
                             IProductoRepository productoRepository,
                             IClienteRepository clienteRepository)
        {
            _pedidoRepository = pedidoRepository;
            _productoRepository = productoRepository;
            _clienteRepository = clienteRepository;
        }

        public async Task<IEnumerable<PedidoDTO>> GetAllPedidosAsync()
        {
            var pedidos = await _pedidoRepository.GetAllPedidosAsync();
            return pedidos.Select(p => MapPedidoToDTO(p));
        }

        public async Task<PedidoDTO> GetPedidoByIdAsync(int id)
        {
            var pedido = await _pedidoRepository.GetPedidoByIdAsync(id);
            if (pedido == null)
                return null;

            return MapPedidoToDTO(pedido);
        }

        public async Task<IEnumerable<PedidoDTO>> GetPedidosByClienteIdAsync(int clienteId)
        {
            var pedidos = await _pedidoRepository.GetPedidosByClienteIdAsync(clienteId);
            return pedidos.Select(p => MapPedidoToDTO(p));
        }

        public async Task<PedidoDTO> CreatePedidoAsync(PedidoCreateDTO pedidoCreateDTO)
        {
            // Verificar que el cliente existe
            var cliente = await _clienteRepository.ObtenerPorIdAsync(pedidoCreateDTO.ClienteId);
            if (cliente == null)
                throw new KeyNotFoundException($"No se encontró el cliente con ID {pedidoCreateDTO.ClienteId}");

            // Crear el nuevo pedido
            var nuevoPedido = new Pedido
            {
                ClienteId = pedidoCreateDTO.ClienteId,
                Fecha = DateTime.Now,
                Estado = "PENDIENTE",
                Total = 0, // Se calculará después
                Items = new List<PedidoItem>()
            };

            // Procesar cada item del pedido
            decimal totalPedido = 0;
            foreach (var itemDTO in pedidoCreateDTO.Items)
            {
                var producto = await _productoRepository.ObtenerPorIdAsync(itemDTO.ProductoId);
                if (producto == null)
                    throw new KeyNotFoundException($"No se encontró el producto con ID {itemDTO.ProductoId}");

                var subtotal = producto.Precio * itemDTO.Cantidad;
                totalPedido += subtotal;

                var pedidoItem = new PedidoItem
                {
                    ProductoId = itemDTO.ProductoId,
                    Cantidad = itemDTO.Cantidad,
                    PrecioUnitario = producto.Precio,
                    Subtotal = subtotal
                };

                nuevoPedido.Items.Add(pedidoItem);
            }

            nuevoPedido.Total = totalPedido;

            // Guardar el pedido en la base de datos
            var pedidoCreado = await _pedidoRepository.CreatePedidoAsync(nuevoPedido);
            return MapPedidoToDTO(pedidoCreado);
        }

        public async Task<PedidoDTO> UpdatePedidoAsync(int id, PedidoUpdateDTO pedidoUpdateDTO)
        {
            var pedidoExistente = await _pedidoRepository.GetPedidoByIdAsync(id);
            if (pedidoExistente == null)
                return null;

            // Actualizar el estado del pedido
            pedidoExistente.Estado = pedidoUpdateDTO.Estado;

            // Si hay items para actualizar
            if (pedidoUpdateDTO.Items != null && pedidoUpdateDTO.Items.Count > 0)
            {
                // Recalcular el total del pedido
                decimal totalPedido = 0;
                var itemsExistentes = pedidoExistente.Items.ToList();
                var itemsActualizados = new List<PedidoItem>();

                foreach (var itemDTO in pedidoUpdateDTO.Items)
                {
                    PedidoItem item;

                    // Si es un item existente, actualizar
                    if (itemDTO.Id.HasValue)
                    {
                        item = itemsExistentes.FirstOrDefault(i => i.Id == itemDTO.Id.Value);
                        if (item == null)
                            throw new KeyNotFoundException($"No se encontró el item con ID {itemDTO.Id.Value}");

                        var producto = await _productoRepository.ObtenerPorIdAsync(itemDTO.ProductoId);
                        if (producto == null)
                            throw new KeyNotFoundException($"No se encontró el producto con ID {itemDTO.ProductoId}");

                        item.ProductoId = itemDTO.ProductoId;
                        item.Cantidad = itemDTO.Cantidad;
                        item.PrecioUnitario = producto.Precio;
                        item.Subtotal = producto.Precio * itemDTO.Cantidad;
                    }
                    // Si es un nuevo item, agregar
                    else
                    {
                        var producto = await _productoRepository.ObtenerPorIdAsync(itemDTO.ProductoId);
                        if (producto == null)
                            throw new KeyNotFoundException($"No se encontró el producto con ID {itemDTO.ProductoId}");

                        item = new PedidoItem
                        {
                            PedidoId = id,
                            ProductoId = itemDTO.ProductoId,
                            Cantidad = itemDTO.Cantidad,
                            PrecioUnitario = producto.Precio,
                            Subtotal = producto.Precio * itemDTO.Cantidad
                        };
                        pedidoExistente.Items.Add(item);
                    }

                    itemsActualizados.Add(item);
                    totalPedido += item.Subtotal;
                }

                // Eliminar items que no están en la actualización
                var itemsAEliminar = itemsExistentes
                    .Where(i => !itemsActualizados.Any(ia => ia.Id == i.Id))
                    .ToList();

                foreach (var item in itemsAEliminar)
                {
                    pedidoExistente.Items.Remove(item);
                }

                pedidoExistente.Total = totalPedido;
            }

            // Actualizar el pedido en la base de datos
            var pedidoActualizado = await _pedidoRepository.UpdatePedidoAsync(id, pedidoExistente);
            return MapPedidoToDTO(pedidoActualizado);
        }

        public async Task<PedidoDTO> UpdatePedidoEstadoAsync(int id, string estado)
        {
            var pedidoActualizado = await _pedidoRepository.UpdatePedidoEstadoAsync(id, estado);
            if (pedidoActualizado == null)
                return null;

            return MapPedidoToDTO(pedidoActualizado);
        }

        public async Task<bool> DeletePedidoAsync(int id)
        {
            return await _pedidoRepository.DeletePedidoAsync(id);
        }

        private PedidoDTO MapPedidoToDTO(Pedido pedido)
        {
            return new PedidoDTO
            {
                Id = pedido.Id,
                Fecha = pedido.Fecha,
                ClienteId = pedido.ClienteId,
                ClienteNombre = pedido.Cliente?.Nombre,
                Estado = pedido.Estado,
                Total = pedido.Total,
                Items = pedido.Items?.Select(i => new PedidoItemResponseDTO
                {
                    Id = i.Id,
                    ProductoId = i.ProductoId,
                    ProductoNombre = i.Producto?.Nombre,
                    Cantidad = i.Cantidad,
                    PrecioUnitario = i.PrecioUnitario,
                    Subtotal = i.Subtotal
                }).ToList()
            };
        }
    }
}