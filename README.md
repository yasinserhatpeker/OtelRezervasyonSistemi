🏨 Otel Rezervasyon Yönetim Sistemi (MVC)
Bu proje, bir üniversite dönem ödevi kapsamında .NET 8 MVC mimarisi kullanılarak geliştirilmiştir. Projede müşteri yönetimi, oda yönetimi ve rezervasyon işlemleri modern web standartlarına uygun olarak gerçekleştirilmektedir.

🚀 Öne Çıkan Özellikler
Bire-Bir (1:1) İlişki Mantığı: Veri tutarlılığını korumak adına, bir müşteri aynı anda sadece bir odaya, bir oda ise aynı anda sadece bir müşteriye rezerve edilebilir.

Akıllı Filtreleme: Rezervasyon oluşturma ekranında, halihazırda dolu olan odalar ve aktif rezervasyonu bulunan müşteriler otomatik olarak listeden gizlenir.

Güçlü Hata Yönetimi: Hem veritabanı seviyesinde (Unique Index) hem de uygulama seviyesinde (Try-Catch & Validation) hata kontrolleri yapılmıştır.

Modern Arayüz: Tasarımda Bootstrap ve Bootstrap Icons kullanılarak responsive bir deneyim sağlanmıştır.

Docker Entegrasyonu: Veritabanı kurulumu ile uğraşmamak için Azure SQL Edge (Apple Silicon/M mimarisi uyumlu) Docker desteği eklenmiştir.

🛠️ Kullanılan Teknolojiler
Backend: .NET 8 MVC, Entity Framework Core

Veritabanı: MS SQL Server (Azure SQL Edge)

Frontend: Bootstrap 5, Razor Pages, JavaScript/jQuery

Konteynerleştirme: Docker & Docker Compose

📦 Kurulum ve Çalıştırma
Projeyi kendi bilgisayarınızda çalıştırmak için aşağıdaki adımları sırasıyla takip edin:

1. Veritabanını Başlatın
Proje dizininde terminali açın ve Docker container'ını ayağa kaldırın:

Bash
docker-compose up -d
2. Veritabanı Şemasını Oluşturun
Migration dosyalarını kullanarak tabloları oluşturun:

Bash
dotnet ef database update
3. Uygulamayı Çalıştırın
Uygulamayı başlatın:

Bash
dotnet run
Uygulama ayağa kalktığında terminalde belirtilen adresi (örn: http://localhost:5194) tarayıcınızda açabilirsiniz.

📊 Veritabanı Şeması
Proje 3 temel tablodan oluşmaktadır:

Odalar: Oteldeki oda bilgilerini ve fiyatlarını tutar.

Müşteriler: Konaklayan kişilerin kimlik ve iletişim bilgilerini tutar.

Rezervasyonlar: Müşteri ve oda eşleşmelerini tarih bilgisiyle tutar (Unique Constraint uygulanmıştır).
