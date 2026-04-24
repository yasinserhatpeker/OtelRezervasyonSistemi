# 🏨 Otel Rezervasyon Yönetim Sistemi (MVC)

Bu proje, .NET 8 MVC mimarisi kullanılarak geliştirilmiş kapsamlı bir otel yönetim sistemidir.  
Müşteri kayıtları, oda yönetimi ve rezervasyon süreçlerini uçtan uca yönetmeyi sağlar.

---

## 🚀 Öne Çıkan Özellikler

### 🔹 1:1 İlişki Mantığı
Veri tutarlılığını korumak adına:
- Bir müşteri aynı anda sadece **bir odaya**
- Bir oda ise aynı anda sadece **bir müşteriye**  
rezerve edilebilir.

---

### 🔹 Akıllı Filtreleme
Rezervasyon ekranında:
- Halihazırda rezervasyonu bulunan müşteriler
- Dolu olan odalar  

otomatik olarak listeden gizlenir.

---

### 🔹 Güçlü Hata Yönetimi
- Veritabanı kısıtlamaları (**Unique Index**)
- Form doğrulamaları (**Validation**)  

sayesinde hatalı veri girişi engellenmiştir.

---

### 🔹 Modern Arayüz
- **Bootstrap 5**
- **Bootstrap Icons**

kullanılarak responsive ve kullanıcı dostu bir arayüz sağlanmıştır.

---

### 🔹 Docker Desteği
- **Azure SQL Edge** imajı sayesinde  
- Windows ve macOS (Apple Silicon M1/M2/M3) cihazlarda sorunsuz çalışır.

---

## 🛠️ Kullanılan Teknolojiler

### Backend
- .NET 8 MVC
- Entity Framework Core

### Veritabanı
- MS SQL Server (Azure SQL Edge)

### Frontend
- Bootstrap 5
- Razor Pages
- JavaScript / jQuery

### Konteynerleştirme
- Docker
- Docker Compose

---

## 📦 Kurulum ve Çalıştırma

Projeyi çalıştırmak için aşağıdaki adımları takip edin:

### 1️⃣ Veritabanını Başlatın (Docker)

```bash
docker-compose up -d
