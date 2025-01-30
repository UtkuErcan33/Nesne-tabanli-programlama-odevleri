# HAL KAYIT OTOMASYONU

<div align="center">
    <img src="docs/firat_logo.png" alt="Fırat Üniversitesi Logo" width="200"/>
    <h2>FIRAT ÜNİVERSİTESİ</h2>
    <h3>Teknoloji Fakültesi</h3>
    <h3>Yazılım Mühendisliği Bölümü</h3>
    <br/>
    <h1>HAL KAYIT OTOMASYONU</h1>
    <h2>Proje Dokümantasyonu</h2>
    <br/>
    <p><strong>Hazırlayan:</strong> Utku Mustafa ERCAN</p>
    <p><strong>Öğrenci No:</strong> 225541070</p>
    <br/>
    <p><strong>2024</strong></p>
</div>

## İçindekiler

1. [Proje Hakkında](#proje-hakkında)
2. [Kullanılan Teknolojiler](#kullanılan-teknolojiler)
3. [Sistem Gereksinimleri](#sistem-gereksinimleri)
4. [Kurulum](#kurulum)
5. [Modüller](#modüller)
6. [Veritabanı Yapısı](#veritabanı-yapısı)
7. [Kullanım Kılavuzu](#kullanım-kılavuzu)

## Proje Hakkında

HAL Kayıt Otomasyonu, hal işletmelerinin günlük operasyonlarını yönetmek ve takip etmek için geliştirilmiş kapsamlı bir yazılım çözümüdür. Bu sistem, ürün girişi, müşteri yönetimi, satış takibi, stok kontrolü ve raporlama gibi temel işlevleri içermektedir.

## Kullanılan Teknolojiler

- **Programlama Dili:** C#
- **Framework:** .NET Framework
- **Veritabanı:** Microsoft SQL Server
- **Arayüz Bileşenleri:** DevExpress, Windows Forms
- **Geliştirme Ortamı:** Visual Studio

## Sistem Gereksinimleri

- Windows 10 veya üzeri işletim sistemi
- .NET Framework 4.7.2 veya üzeri
- Microsoft SQL Server 2019 veya üzeri
- Minimum 4GB RAM
- 500MB boş disk alanı

## Kurulum

1. Veritabanı kurulumu:
   - SQL Server Management Studio'yu açın
   - `giriş_kayıtları` veritabanını oluşturun
   - Verilen SQL scriptlerini çalıştırın

2. Uygulama kurulumu:
   - Setup dosyasını çalıştırın
   - Kurulum sihirbazını takip edin
   - Veritabanı bağlantı ayarlarını yapılandırın

## Modüller

### 1. Ekleme İşlemleri
- Ürün Ekleme
- Kasa Ekleme
- Plaka Ekleme
- Müstahsil Ekleme
- Müşteri Ekleme

### 2. Alım İşlemleri
- Müstahsil Alım
- Hal İçi Alım

### 3. Satış İşlemleri
- Sevkiyat
- Dükkan Önü Satış

### 4. Raporlama
- Geçmiş Alımlar
- Geçmiş Satışlar

## Veritabanı Yapısı

Sistem aşağıdaki ana tablolardan oluşmaktadır:

- **Ürün_Ekleme:** Ürün bilgileri
- **Kasa_Ekleme:** Kasa kayıtları
- **Müstahsil_Ekleme:** Müstahsil bilgileri
- **Müsteri_Ekleme:** Müşteri kayıtları
- **Alım_gecmisi:** Alım işlemleri
- **Satış_gecmisi:** Satış işlemleri

## Kullanım Kılavuzu

### Ana Ekran

Ana ekran, tüm modüllere erişim sağlayan bir menü sistemi içerir:

1. Sol menüden istenen modül seçilir
2. İlgili form açılır
3. Gerekli bilgiler girilerek işlem yapılır

### Ekleme İşlemleri

Yeni kayıt ekleme adımları:

1. İlgili ekleme formunu açın
2. Gerekli bilgileri doldurun
3. Kaydet butonuna tıklayın
4. İşlem sonucunu kontrol edin

### Alım/Satış İşlemleri

İşlem yapma adımları:

1. İlgili formu açın
2. Ürün ve müşteri bilgilerini seçin
3. Miktar ve fiyat bilgilerini girin
4. Toplam tutarı hesaplayın
5. İşlemi kaydedin

### Raporlama

Rapor görüntüleme adımları:

1. Geçmiş işlemler menüsünden rapor türünü seçin
2. Tarih aralığı belirleyin
3. Filtreleme kriterlerini ayarlayın
4. Raporu görüntüleyin