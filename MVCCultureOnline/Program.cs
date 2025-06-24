using CultureOnline.Application.Profiles;
using CultureOnline.Application.Services.Implementations;
using CultureOnline.Application.Services.Interfaces;
using CultureOnline.Infraestructure.Data;
using CultureOnline.Infraestructure.Repository.Implementations;
using CultureOnline.Infraestructure.Repository.Interfaces;
using Libreria.Web.Middleware;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using System.Text;
using Microsoft.AspNetCore.Builder;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configurar D.I.
//Repository 
builder.Services.AddTransient<IRepositoryAutor, RepositoryAutor>();
builder.Services.AddTransient<IRepositoryCategoria, RepositoryCategoria>();
builder.Services.AddTransient<IRepositoryProducto, RepositoryProducto>();
//Services 
builder.Services.AddTransient<IServiceAutor, ServiceAutor>();
builder.Services.AddTransient<IServiceCategoria, ServiceCategoria>();
builder.Services.AddTransient<IServiceProducto, ServiceProducto>();

//Configurar Automapper 
builder.Services.AddAutoMapper(config =>
{
config.AddProfile<AutorProfile>();
config.AddProfile<CategoriaProfile>();
config.AddProfile<ProductoProfile>();
config.AddProfile<UsuarioProfile>();
config.AddProfile<RolUsuarioProfile>();
config.AddProfile<DetallePedidoProfile>();
config.AddProfile<EtiquetaProfile>();
config.AddProfile<PagoProfile>();
config.AddProfile<PedidoProfile>();
config.AddProfile<ProductoImagenesProfile>();
config.AddProfile<PromocionProfile>();
config.AddProfile<ReseñaProfile>();
config.AddProfile<DetalleCarritoProfile>();
config.AddProfile<CarritoProfile>();
config.AddProfile<GeneroProductoProfile>();
config.AddProfile<TipoPromocionProfile>();
config.AddProfile<ProductoEtiquetaProfile>();
});

// Configuar Conexión a la Base de Datos SQL 
builder.Services.AddDbContext<CultureOnlineContext>(options =>
{
    // it read appsettings.json file 

    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerDataBase"));
    if (builder.Environment.IsDevelopment())
        options.EnableSensitiveDataLogging();
});


//Configuración Serilog 
// Logger. P.E. Verbose = muestra SQl Statement 
var logger = new LoggerConfiguration()
// Limitar la información de depuración 
.MinimumLevel.Override("Microsoft", LogEventLevel.Error)
.Enrich.FromLogContext() 
// Log LogEventLevel.Verbose muestra mucha información, pero no es necesaria 
//solo para el proceso de depuración 
.WriteTo.Console(LogEventLevel.Information) 
.WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level == 
LogEventLevel.Information).WriteTo.File(@"Logs\Info-.log", shared: true, encoding:
Encoding.ASCII, rollingInterval: RollingInterval.Day)) .WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level ==
LogEventLevel.Debug).WriteTo.File(@"Logs\Debug-.log", shared: true, encoding:
System.Text.Encoding.ASCII, rollingInterval: RollingInterval.Day))
.WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level ==
LogEventLevel.Warning).WriteTo.File(@"Logs\Warning-.log", shared: true, encoding:
System.Text.Encoding.ASCII, rollingInterval: RollingInterval.Day))
.WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level ==
LogEventLevel.Error).WriteTo.File(@"Logs\Error-.log", shared: true, encoding: Encoding.ASCII,
rollingInterval: RollingInterval.Day))
.WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level ==
LogEventLevel.Fatal).WriteTo.File(@"Logs\Fatal-.log", shared: true, encoding: Encoding.ASCII,
rollingInterval: RollingInterval.Day))
.CreateLogger();
builder.Host.UseSerilog(logger);




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

}
else
{
    // Error control Middleware 
    app.UseMiddleware<ErrorHandlingMiddleware>();
} //Activar soporte a la solicitud de registro con SERILOG 
app.UseSerilogRequestLogging();

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();
// Activar Antiforgery  
app.UseAntiforgery(); 

app.MapStaticAssets();
app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
