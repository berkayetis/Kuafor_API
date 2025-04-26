using KuaforRandevu.Application.Validators;
using Core.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Cache.Contracts;
using Infrastructure.Cache.Service;
using Infrastructure.Implementations;
using KuaforRandevu.Core.Interfaces;
using Serilog.Events;
using Serilog;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
public static class ConfigrationExtensions
{
    /// <summary>
    /// Redis tabanlı IDistributedCache ve IRedisCacheService'i DI konteynerine kaydeder.
    /// </summary>
    public static void ConfigrationRedisCache(this IServiceCollection services, IConfiguration configuration)
    {
        // IRedisCacheService implementasyonunu scoped olarak kaydet
        services.AddScoped<IRedisCacheService, RedisCacheService>();

        // Redis bağlantı dizesini "Redis:Configuration" altından al
        var redisConfig = configuration.GetSection("Redis")["Configuration"];
        Console.WriteLine($"Redis Config: {redisConfig}");
        // StackExchange.Redis tabanlı IDistributedCache'i ekle
        services.AddStackExchangeRedisCache(options =>
        {
            // Redis sunucu adresi: localhost:6379 veya konfigürasyondaki değer
            options.Configuration = redisConfig;

            // Cache key'lerinizin önüne koyulacak isim
            options.InstanceName = configuration.GetSection("Redis")["InstanceName"] ?? "KuaforRdv_";
            Console.WriteLine($"Redis InstanceName: {options.InstanceName}");

        });
    }
    public static void ConfigrationAllServices(this IServiceCollection services)
    {
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        services.AddScoped<IAppointmentService, AppointmentService>();
        services.AddScoped<IStylistRepository, StylistRepository>();
        services.AddScoped<IStylistService, StylistService>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<ICustomerService, CustomerService>();
    }
    public static void ConfigrationValidators(this IServiceCollection services)
    {
        // Model binding pipeline’ına otomatik validation ekle
        services.AddFluentValidationAutoValidation();
        // İstersen client‑side adaptörleri de aç
        services.AddFluentValidationClientsideAdapters();

        // Validator’ları kaydet
        services.AddValidatorsFromAssemblyContaining<CreateAppointmentValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateCustomerValidator>();
    }
    public static void ConfigrationLogger(this IServiceCollection services, WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console()                                         // Konsola log yazar
            .WriteTo.File(
                path: "logs/log-.txt",
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 7)                            // Haftalik donen dosyalar
            .CreateLogger();

        builder.Host.UseSerilog();
    }
    public static void ConfigrationDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        // DbContext
        // Burada sadece tek bir DbContext kaydı var, kullandığı connection string’i ortam ayarlarından alacak:
        services.AddDbContext<AppointmentContext>(opts =>
        {
            var conn = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrWhiteSpace(conn))
                throw new InvalidOperationException("DefaultConnection ayarı bulunamadı.");

            // 2) "Host=" ile başlıyorsa PostgreSQL, aksi halde SQL Server
            if (conn.StartsWith("Host=", StringComparison.OrdinalIgnoreCase))
            {
                // --- Burada EXPLICIT conn parametresini veriyoruz! ---
                opts.UseNpgsql(
                    conn,
                    npgsqlOptions => npgsqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(10),
                        errorCodesToAdd: null
                    )
                );
            }
            else
            {
                opts.UseSqlServer(
                    conn,
                    sqlOptions => sqlOptions.EnableRetryOnFailure()
                );
            }
        });
    }
}