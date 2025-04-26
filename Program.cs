var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrar servicios de productos
builder.Services.AddScoped<Ferremas.Api.Repositories.IProductoRepository, Ferremas.Api.Repositories.ProductoRepository>();
builder.Services.AddScoped<Ferremas.Api.Services.IProductoService, Ferremas.Api.Services.ProductoService>();

// Registrar servicios clientes
builder.Services.AddScoped<Ferremas.Api.Repositories.IClienteRepository, Ferremas.Api.Repositories.ClienteRepository>();
builder.Services.AddScoped<Ferremas.Api.Services.IClienteService, Ferremas.Api.Services.ClienteService>();

// Registrar servicios de pedidos
builder.Services.AddScoped<Ferremas.Api.Repositories.IPedidoRepository, Ferremas.Api.Repositories.PedidoRepository>();
builder.Services.AddScoped<Ferremas.Api.Services.IPedidosService, Ferremas.Api.Services.PedidosService>();

// Registrar servicios de pagos
builder.Services.AddScoped<Ferremas.Api.Repositories.IPagoRepository, Ferremas.Api.Repositories.PagoRepository>();
builder.Services.AddScoped<Ferremas.Api.Services.IPagosService, Ferremas.Api.Services.PagosService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();