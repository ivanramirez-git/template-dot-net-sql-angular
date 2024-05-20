// Import Services
using Api.Services.CatProductoService;
using Api.Services.CatTipoClienteService;
using Api.Services.TblClienteService;
using Api.Services.TblDetalleFacturaService;
using Api.Services.TblFacturaService;

// Import Repository
using Api.Repository.Contract;
using Api.Repository.Implement.CatProductoRepository;
using Api.Repository.Implement.CatTipoClienteRepository;
using Api.Repository.Implement.TblClienteRepository;
using Api.Repository.Implement.TblDetalleFacturaRepository;
using Api.Repository.Implement.TblFacturaRepository;

// Import Models
using Api.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Inject Repository
builder.Services.AddScoped<ICatProductoRepository, CatProductoRepositoryImpl>();
builder.Services.AddScoped<ICatTipoClienteRepository, CatTipoClienteRepositoryImpl>();
builder.Services.AddScoped<ITblClienteRepository, TblClienteRepositoryImpl>();
builder.Services.AddScoped<ITblDetalleFacturaRepository, TblDetalleFacturaRepositoryImpl>();
builder.Services.AddScoped<ITblFacturaRepository, TblFacturaRepositoryImpl>();
// End Inject Repository

// Inject Services
builder.Services.AddScoped<ICatProductoService, CatProductoServiceImpl>();
builder.Services.AddScoped<ICatTipoClienteService, CatTipoClienteServiceImpl>();
builder.Services.AddScoped<ITblClienteService, TblClienteServiceImpl>();
builder.Services.AddScoped<ITblDetalleFacturaService, TblDetalleFacturaServiceImpl>();
builder.Services.AddScoped<ITblFacturaService, TblFacturaServiceImpl>();
// End Inject Services

var app = builder.Build();

app.UseCors(options =>
{
    options.AllowAnyOrigin(); // Puedes configurar aquí las políticas específicas de origen.
    options.AllowAnyMethod();
    options.AllowAnyHeader();
});


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
