using KuaforRandevu.Application.Mapping;
using KuaforRandevu.Application.Validators;
using Core.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Cache.Contracts;
using Infrastructure.Cache.Service;
using Infrastructure.Data;
using Infrastructure.Implementations;
using KuaforRandevu.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;
using KuaforRandevu.Infrastructure.Implementations;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.ConfigrationDatabase(builder.Configuration);

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// MVC Controllers
builder.Services.AddControllers();

// Repository & Service kayıtları
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>(); // todo: configure extension
builder.Services.ConfigrationLogger(builder);
builder.Services.ConfigrationValidators();
builder.Services.ConfigrationAllServices();
builder.Services.ConfigrationRedisCache(builder.Configuration);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// middleware order
app.UseSerilogRequestLogging();
// Global exception handling: unhandled exception'ları capture edip loglar
/*app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var ex = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>()?.Error;
        if (ex != null)
        {
            Log.Error(ex, "Unhandled exception caught by global handler");
        }
        context.Response.StatusCode = 500;
        await context.Response.WriteAsJsonAsync(new { error = "Sunucu hatası" });
    });
});*/
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Controller endpoint’lerini ekle
app.MapControllers();

// Swagger UI
app.UseSwagger();
app.UseSwaggerUI();

// (2) Sadece Production veya Docker’da migration uygula
if (!app.Environment.IsDevelopment())
{
    // DbContext örneğini oluşturup migration’ları uygula
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppointmentContext>();
    db.Database.Migrate();
}
try
{
    Log.Information("Application starting up");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed");
}
finally
{
    Log.CloseAndFlush();                                       // Log buffer'larını temizle
}