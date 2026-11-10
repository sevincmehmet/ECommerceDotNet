# ECommerceDotNet

**.NET 8 ile katmanlı mimaride yazılmış bir e-ticaret Web API'si: JWT kimlik doğrulama ve izin (permission) tabanlı yetkilendirme.**

Backend'e frontend'den geçiş sürecimde, katmanlı mimariyi, EF Core'u ve kimlik doğrulama/yetkilendirmeyi sıfırdan öğrenmek için yazdığım bir öğrenme projesi.

## Neler var?

- **Kimlik doğrulama:** Kayıt ve giriş (`/api/auth/register`, `/api/auth/login`), BCrypt ile şifre hash'leme, JWT erişim token'ı
- **İzin tabanlı yetkilendirme:** `[HasPermission("Product.FullControl")]` gibi özel bir attribute; `PermissionPolicyProvider` + `PermissionHandler` ile her izin dinamik bir policy'ye dönüşüyor. Rol tabanlı kontrol (`[Authorize(Roles = "Admin")]`) de destekleniyor.
- **Kullanıcı ve rol yönetimi:** Kullanıcıya rol ve izin atama, "benim profilim" uç noktaları
- **Ürün kataloğu:** Ürün, kategori (seviye ve üst kategori ile hiyerarşik) ve marka CRUD'u, ürün arama
- **Tutarlı API cevapları:** `IResult` / `IDataResult` ile başarı/hata sonuçları tek tip
- **İstek loglama:** Özel `LogMiddleware`
- **Veri modeli:** Sipariş, kargo, sepet, kupon, fatura, ödeme ve yorum varlıkları ile servis arayüzleri (uç noktaların bir kısmı henüz yazılmadı)

## Mimari

```
Core / Entities          Varlıklar (User, Product, Order, Cart...)
DataAccess               EF Core DbContext, repository'ler, migration'lar
Business                 Servis arayüzleri (Abstract) ve iş kuralları (Concrete), DTO'lar
Infrastructure           JWT, şifre hash'leme, izin altyapısı, Result tipleri
Presentation_API         ASP.NET Core Web API, controller'lar, middleware, Swagger
Presentation_Forms       Deneme amaçlı WinForms istemcisi
```

## Çalıştırma

Gereksinimler: .NET 8 SDK, SQL Server (Express yeterli).

1. Bağlantı cümlesini `DataAccess/Concrete/EntityFramework/ECommerceDbContext.cs` içinde kendi SQL Server'ına göre düzenle.
2. JWT imzalama anahtarını ortam değişkeni olarak ver (en az 64 karakter):
   ```bash
   export TokenOptions__SecurityKey="$(openssl rand -hex 64)"
   ```
3. Veritabanını oluştur ve API'yi başlat:
   ```bash
   dotnet ef database update --project DataAccess --startup-project Presentation_API
   dotnet run --project Presentation_API
   ```
4. Swagger arayüzünden uç noktaları dene.

## Teknolojiler

.NET 8 · ASP.NET Core Web API · Entity Framework Core (SQL Server) · JWT Bearer · BCrypt · Swagger

## Durum ve eksikler

Öğrenme projesi. Sipariş, sepet ve ödeme akışlarının veri modeli hazır ama uç noktaları tamamlanmadı; otomatik test yok. Bu konulardaki daha olgun çalışmam için: [Mini-ERP](https://github.com/sevincmehmet/mini-erp) (.NET Clean Architecture, multi-tenant, testli).
