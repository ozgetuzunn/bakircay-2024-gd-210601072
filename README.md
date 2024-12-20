﻿# 🎮 **3D Yemek Eşleştirme**

Unity üzerinde yapılan obje sürükle ve bırak mekaniği ile çalışan ve yemek konsepti verilmiş, birkaç level içeren basic yapıda bir projedir.

---

## 📌 **Proje Genel Bakış**

- **Tutorial ve 3 Farklı Sahne:** Oyun, oyuncuya rehberlik eden bir tutorial bölümü ve farklı zorluk seviyelerinde 3 ayrı sahneden oluşmaktadır.
- **Dotween** animasyon paketi kullanılarak animasyon efektleri uygulandı.  
- **Score ve Can Bilgisi:** Oyuncunun skor ve can değerleri oyun boyunca takip edilmekte ve sahne geçişlerinde **DontDestroyOnLoad** ile taşınmaktadır.    
- **Obje Paketi:** Oyunda kullanılan objeler için **Kenney Food Kit** asset paketi tercih edilmiştir.  
- **Feedback Sistemleri:**  
  - **Başarı Ses Efekti:** Oyuncunun başarılı bir yerleştirme yapması durumunda bir başarı sesi çalınır.  
  - **Başarısızlık Ses Efekti:** Yanlış bir yerleştirme yapıldığında başarısızlık ses efekti çalınır.  
  - **Ekran Titremesi:** Hata durumunda oyuncuya görsel bir geri bildirim sağlanır.  
  - **Particle Effectler:** Obje sürükleme ve bırakma sırasında küçük partikül efektleri eklenmiştir.  
- **Prefab ve Prefab Variant Kullanımı:** Oyundaki objeler için düzenli bir yapı sağlamak amacıyla prefab ve prefab variant teknikleri kullanılmıştır.  
- **Zorluk Seviyeleri:**  
  - Oyun **Tutorial >> Level 1 >> Level 2 >> Level 3** zorluk seviyesinde ilerler.  
  - **Level 3:** Hareketli yerleştirme alanları içermektedir ve zamana karşı yarış eklenmiştir.  
- **UI Yönetimi:** Birkaç temel UI objesi ve yönetimi uygulandı.  
- **Level Geçiş ve Scene Management:**  
  - Oyuncular başarılı bir şekilde bir levelı tamamladıktan sonra bir sonraki levele geçebilir.  
  - Oyuncular aynı levelı tekrar oynayabilir.

---

## 🎮 **Oynanış ve Özellikler**

### **Oynanış Akışı**
- Oyuncular, tutorial sahnesinde oyunun temel mekaniklerini öğrenir.
- İlk iki seviyede sabit yerleştirme alanları bulunurken, son seviyede hareketli yerleştirme alanları ve zamana karşı yarış eklenmiştir.

---

### **Ekran Görüntüleri**

Aşağıda oyunun oynanışına dair kısa bir ekran görüntüsü bulunmaktadır:

![Tutorial Sahnesi](pngs/tutorial.jpg)
![Level 1](pngs/level1.jpg)
![Level 2](pngs/level2.jpg)
![Level 3](pngs/level3.jpg)

---

## 📋 **UI ve Yönetimi**

- Oyunda birkaç temel **UI objesi** kullanılmıştır. Örneğin:  
  - Skor ve can göstergeleri.  
  - Tutorial rehberi.  
- **UI Management** yapısı, farklı sahnelerde UI öğelerinin tutarlı çalışmasını sağlar.

---

## 📦 **Obje Paketi**

Projede kullanılan objeler, Unity'nin **Kenney Food Kit** asset paketinden alınmıştır. 

**Obje Paketi Görüntüsü:**  
![Kenney Food Kit](pngs/assetpackage.jpg)

---

## 🕹️ **Ek Teknik Detaylar**

- **Ses Efektleri:**  
  - Başarı ve başarısızlık durumlarında ses efektleri oyuna entegre edilmiştir.  
- **Hareketli Yerleştirme Alanları:**  
  - **Level 3'te**, yerleştirme alanları hareketlidir ve oyuncular zamana karşı yarışır.  
- **Prefab ve Prefab Variant:**  
  - Objeler düzenli bir yapı sağlamak amacıyla prefab ve prefab variant teknikleri ile organize edilmiştir.  
- **Scene Management:**  
  - Oyuncular tamamlanan seviyeden sonraki seviyeye geçebilir veya aynı seviyeyi tekrar oynayabilir.

---

## ▶️ **Kurulum ve Çalıştırma**

1. Unity 2022.3.31f sürümü indirin
2. Ana sahne olarak **TutorialScene**'i yükleyin.
3. Oyunu "Play" butonuna basarak başlatın. Ardından EndScreen gelene kadar devam edin!

---

