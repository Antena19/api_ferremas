using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ferremas.Api.DTOs;
using Ferremas.Api.Modelos;
using MercadoPago.Client.Common;
using MercadoPago.Client.Preference;
using MercadoPago.Config;
using MercadoPago.Resource.Preference;
using Microsoft.Extensions.Configuration;

namespace Ferremas.Api.Services
{
    public class MercadoPagoService
    {
        private readonly string _accessToken;
        private readonly string _publicKey;
        private readonly string _urlRetorno;

        public MercadoPagoService(IConfiguration configuration)
        {
            _accessToken = configuration["MercadoPago:AccessToken"];
            _publicKey = configuration["MercadoPago:PublicKey"];
            _urlRetorno = configuration["MercadoPago:UrlRetorno"];

            // Configuración global de Mercado Pago
            MercadoPagoConfig.AccessToken = _accessToken;
        }

        public async Task<MercadoPagoResponseDTO> CrearPreferenciaPago(Pedido pedido, int pagoId)
        {
            var client = new PreferenceClient();

            // Crear items para la preferencia
            var items = new List<PreferenceItemRequest>();

            // Agregar el pedido como item
            items.Add(new PreferenceItemRequest
            {
                Id = pedido.Id.ToString(),
                Title = $"Pedido #{pedido.Id}",
                Description = $"Pago de pedido #{pedido.Id}",
                Quantity = 1,
                CurrencyId = "CLP", // Cambiar según tu país
                UnitPrice = pedido.Total
            });

            // Configurar URLs de retorno
            var backUrls = new PreferenceBackUrlsRequest
            {
                Success = $"{_urlRetorno}/success?pago_id={pagoId}",
                Failure = $"{_urlRetorno}/failure?pago_id={pagoId}",
                Pending = $"{_urlRetorno}/pending?pago_id={pagoId}"
            };

            // Crear preferencia
            var preferenceRequest = new PreferenceRequest
            {
                Items = items,
                BackUrls = backUrls,
                AutoReturn = "approved",
                ExternalReference = pagoId.ToString()
            };

            // Enviar solicitud a Mercado Pago
            var preference = await client.CreateAsync(preferenceRequest);

            // Retornar datos relevantes
            return new MercadoPagoResponseDTO
            {
                PreferenceId = preference.Id,
                InitPoint = preference.InitPoint,
                SandboxInitPoint = preference.SandboxInitPoint,
                PublicKey = _publicKey
            };
        }
    }

    // DTOs específicos para Mercado Pago
    public class MercadoPagoResponseDTO
    {
        public string PreferenceId { get; set; }
        public string InitPoint { get; set; }  // URL para producción
        public string SandboxInitPoint { get; set; }  // URL para testing
        public string PublicKey { get; set; }
    }
}
