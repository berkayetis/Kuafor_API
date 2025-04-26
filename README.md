## 📖 Proje Hakkında

Basit bir kuaför randevu yönetimi API’si.  
Clean Architecture prensipleriyle katmanlara ayrılmış, Entity Framework Core + PostgreSQL veritabanı, Redis cache ve Docker desteği içerir.

- **API**: ASP.NET Core Web API  
- **Application**: Use-case (iş kuralları) katmanı  
- **Core**: Domain modelleri & repository arayüzleri  
- **Infrastructure**: EF Core DbContext, repository & cache implementasyonları  
- **Tests**: Birim ve entegrasyon testleri (eklenecek)

## 🏗️ Mimari
- **Dependency Inversion**: Dış katmanlar yalnızca arayüzlere bağımlıdır.  
- **DI/IoC**: `Program.cs` üzerinden `IServiceCollection` ile tüm servisler kaydedilir.  
- **FluentValidation**: Request doğrulamaları  
- **AutoMapper**: Dto ↔ Entity dönüşümleri  
- **Serilog**: Merkezi loglama  
- **Swagger**: Otomatik API dokümantasyonu  
