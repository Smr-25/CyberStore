# CyberStore

CyberStore, **.NET 10** ile geliştirilmiş katmanlı mimariye sahip bir ASP.NET Core MVC e-ticaret örnek projesidir.

## İçindekiler
- [Genel Bakış](#genel-bakış)
- [Teknoloji Yığını](#teknoloji-yığını)
- [Mimari ve Proje Yapısı](#mimari-ve-proje-yapısı)
- [Özellikler](#özellikler)
- [Gereksinimler](#gereksinimler)
- [Hızlı Başlangıç](#hızlı-başlangıç)
- [Veritabanı ve Migration](#veritabanı-ve-migration)
- [Konfigürasyon](#konfigürasyon)
- [Kullanılabilir Sayfalar / Akışlar](#kullanılabilir-sayfalar--akışlar)
- [Geliştirici Komutları](#geliştirici-komutları)
- [Sorun Giderme](#sorun-giderme)
- [Mevcut Kısıtlar / Notlar](#mevcut-kısıtlar--notlar)

## Genel Bakış
Bu proje bir elektronik ürün mağazası senaryosunu simüle eder. Uygulama; ürün listeleme/arama, sepet yönetimi, istek listesi (wishlist), iletişim formu ve sipariş oluşturma akışlarını içerir.

Çözüm katmanlı olarak ayrılmıştır:
- **Web/UI katmanı** (ASP.NET Core MVC)
- **Application katmanı** (iş kuralları ve servisler)
- **Data katmanı** (EF Core, repository + unit of work)
- **Core katmanı** (entity modelleri)

## Teknoloji Yığını
- **.NET SDK:** 10.0.x
- **Framework:** ASP.NET Core MVC (net10.0)
- **ORM:** Entity Framework Core 10
- **Veritabanı sağlayıcı:** SQL Server (`Microsoft.EntityFrameworkCore.SqlServer`)
- **Pattern’ler:** Generic Repository, Unit of Work, Service Layer

## Mimari ve Proje Yapısı
Repository içindeki çözüm dizini: `CyberStore/`

```text
CyberStore.sln
├─ CyberStore/              -> ASP.NET Core MVC (UI, Controllers, Views)
├─ CyberStore.Application/  -> İş servisleri
├─ CyberStore.Data/         -> DbContext, Migration’lar, Repository’ler
└─ CyberStore.Core/         -> Entity modelleri
```

### Katmanların Sorumlulukları
- **CyberStore (Web):** HTTP isteklerini karşılar, controller/action akışını yönetir.
- **CyberStore.Application:** Uygulama servisleri ile use-case’leri çalıştırır.
- **CyberStore.Data:** Veritabanı erişimi ve EF Core konfigürasyonlarını içerir.
- **CyberStore.Core:** Domain entity’lerini tanımlar (`Product`, `Order`, `CartItem`, vb.).

## Özellikler
- Ürün listeleme
- Ürün arama
- Kategori bazlı filtreleme
- Sıralama (isim/fiyat/yenilik)
- Sayfalama
- Sepete ürün ekleme/çıkarma
- İstek listesine ürün ekleme/çıkarma
- İletişim mesajı gönderme
- Sepetten sipariş oluşturma
- Sipariş detay/istatü takibi
- Başlangıç (seed) verileri (kategori, marka, örnek ürünler)

## Gereksinimler
- **.NET 10 SDK** (ör. `10.0.201`)
- SQL Server erişimi (lokal veya uzak)

> Not: Uygulama başlangıcında `context.Database.Migrate()` çağrısı ile migration’ları otomatik uygulamayı dener.
## Hızlı Başlangıç
Aşağıdaki komutları repository kökünden çalıştırın:

```bash
cd CyberStore
dotnet restore
dotnet build CyberStore.sln
dotnet run --project CyberStore/CyberStore.csproj
```

Uygulama ayağa kalktıktan sonra terminalde görünen URL’den erişebilirsiniz (genelde `https://localhost:xxxx`).

## Veritabanı ve Migration
Projede migration dosyaları `CyberStore.Data/Migrations` altında bulunur.

### Connection string ayarlama
`CyberStore/appsettings.json` dosyasındaki `ConnectionStrings:DefaultConnection` değerini SQL Server bağlantınıza göre düzenleyin:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost,1433;Database=CyberStoreDb;User Id=sa;Password=YourStrongPassword123;TrustServerCertificate=True;"
}
```

### Migration komutları (manuel)
Çözüm klasöründe:

```bash
cd CyberStore

# Yeni migration oluşturma
dotnet ef migrations add <MigrationName> \
  --project CyberStore.Data/CyberStore.Data.csproj \
  --startup-project CyberStore/CyberStore.csproj

# Migration uygulama
dotnet ef database update \
  --project CyberStore.Data/CyberStore.Data.csproj \
  --startup-project CyberStore/CyberStore.csproj
```

## Konfigürasyon
- Ana ayarlar: `CyberStore/appsettings.json`
- Geliştirme ayarları: `CyberStore/appsettings.Development.json`
- Uygulama `appsettings.Mac.json` dosyasını opsiyonel olarak da yükler (`optional: true`).

İsterseniz kendi local override dosyanızı ekleyebilirsiniz:
- `CyberStore/appsettings.Mac.json`

## Kullanılabilir Sayfalar / Akışlar
- `GET /` -> Ana sayfa, ürün listesi, arama/filtreleme/sıralama
- `GET /Store/Details/{id}` -> Ürün detay
- `GET /Store/Cart` -> Sepet
- `POST /Store/AddToCart` -> Sepete ekleme
- `POST /Store/RemoveFromCart` -> Sepetten silme
- `GET /Store/Wishlist` -> İstek listesi
- `POST /Store/AddToWishlist` -> İstek listesine ekleme
- `POST /Store/RemoveFromWishlist` -> İstek listesinden silme
- `GET /Order/Create` -> Checkout formu
- `POST /Order/Create` -> Sipariş oluşturma
- `GET /Order/Details/{id}` -> Sipariş detay
- `GET /Order/History` -> Kullanıcı sipariş geçmişi
- `GET /Order/AllOrders` -> Tüm siparişler
- `POST /Order/UpdateStatus` -> Sipariş durum güncelleme
- `GET /Home/Contact` -> İletişim sayfası
- `POST /Home/SubmitContact` -> İletişim formu gönderimi

## Geliştirici Komutları
Çözüm klasöründe:

```bash
cd CyberStore

dotnet restore
dotnet build CyberStore.sln
dotnet test CyberStore.sln
```

## Sorun Giderme
1. **Veritabanı bağlantı hatası**
   - `DefaultConnection` değerini kontrol edin.
   - SQL Server instance’ının erişilebilir olduğundan emin olun.

2. **Migration uygulanmıyor**
   - `dotnet ef` aracının kurulu olduğundan emin olun.
   - Startup ve data project parametrelerini doğru verdiğinizi kontrol edin.

3. **HTTPS/sertifika problemleri**
   - Geliştirme sertifikasını yenileyin:
     ```bash
     dotnet dev-certs https --trust
     ```

## Mevcut Kısıtlar / Notlar
- Kimlik doğrulama/authorization entegrasyonu henüz yok; sepet/sipariş akışında demo kullanıcı (`demo-user-1`) kullanılıyor.
- `dotnet test` komutu çalışır ancak çözümde ayrı bir test projesi bulunmamaktadır.
- Uygulama SQL Server sağlayıcısı ile çalışacak şekilde konfigüre edilmiştir; bağlantı string’inin ortama göre düzenlenmesi gerekir.

---

Katkı vermek için bir branch açıp değişikliklerinizi PR olarak gönderebilirsiniz.
