# VR Escape Room

Unity ile geliştirilmiş, tek odalık bir VR kaçış odası bulmacası. VR geliştirmeyi öğrenmek için yaptığım bir proje. Renk bulmacasını çözüp şifreyi ortaya çıkar, kasayı aç, anahtarı al ve süre bitmeden kapıdan çık.

*(For the English description: [README.md](README.md))*

## Oynanış

https://github.com/user-attachments/assets/1e3e0abc-7e60-4ec9-bdf3-099e69513c30

## Proje hakkında

Bu, VR geliştirmeye ilk adımım. Amacım sadece tutorial izlemek yerine gerçekten bir şey inşa ederek Unity'nin component sistemini, fizik/trigger etkileşimlerini ve XR Interaction Toolkit'i öğrenmekti.

**Bulmaca akışı:**
1. Renkli topa tekrar tekrar tıklayarak hedef renge gelene kadar renkleri değiştir, hedef renge gelince topun arkasındaki duvarda bir şifre ortaya çıkar.
2. Şifreyi keypad'e gir.
3. Kasa açılır, içindeki anahtarı al.
4. Anahtarı kapıya götürerek kilidini aç.
5. Süre dolmadan kapıdan geçip oyunu kazan.

## Özellikler

- Renk eşleştirme bulmacası (hedef renge gelene kadar topa tıklayarak renkleri değiştirme)
- Doğru/yanlış geri bildirimli 3 haneli şifreli keypad (panel yeşil/kırmızı yanıp sönüyor)
- Odanın çeşitli yerlerine dağılmış, ipucu içeren okunabilir notlar
- Şifre girilince açılan, içinde anahtar saklı bir kasa
- Anahtar götürülünce kilidi açılıp yukarı kayan bir kapı
- Geri sayım sayacı ve süreni gösteren bir kazanma ekranı
- Sahneyi sıfırlayan restart butonu
- Etkileşimler için ses efektleri (tuş tıklaması, anahtarı alma, doğru/yanlış şifre, kapı açılma, kazanma sesi)
- Önemli noktaların etrafında birkaç nokta ışıkla desteklenmiş karanlık, atmosferik aydınlatma

## Kontroller (Editör testi)

VR gözlüğüm yok ve macOS kullanıyorum, bu yüzden gerçek XR kontrolcüleriyle test edemedim. Yine de oynanışı test edip geliştirebilmek için `EditorTestCamera.cs` adında, Unity Editor içinde VR tarzı etkileşimi simüle eden basit bir birinci şahıs kontrolcüsü yazdım:

- **WASD**: hareket
- **Sağ tık + fare**: etrafa bakma
- **Sol tık**: objelerle etkileşim (keypad tuşları, notlar, renkli top, anahtar)

Gerçek XR Interaction Toolkit kurulumu da projede mevcut, ileride bir VR gözlüğüne erişimim olduğunda kullanılmak üzere.

## Kullanılan teknolojiler

- Unity 6 (6000.3.10f1)
- Universal Render Pipeline (URP)
- XR Interaction Toolkit
- Unity Input System
- TextMeshPro

## Yapay zeka yardımı hakkında

Bu projede kodların büyük çoğunluğunda ve sahne/materyal düzenlemelerinde yapay zekadan yardım aldım. Unity ve VR geliştirmeyi bu proje üzerinden öğreniyordum; yapay zeka arayüzü anlamamda, script'lerin ve component'lerin nasıl bağlandığını kavramamda yardımcı oldu, ayrıca kendi başıma yapamadığım birçok uygulama detayını ve küçük sahne içi ayarlamaları hallettim. Yine de her adımı takip ettim, her script'in ne yaptığını ve neden öyle yaptığını anlıyorum. Bulmaca mantığı, oyun akışı ve hangi mekaniklerin olacağı gibi tasarım kararları bana ait.

## Bilinen sınırlamalar

- Gerçek VR donanımında hiç test edilmedi, sadece yukarıda anlatılan editör test kamerasıyla test edildi.
- macOS üzerinde geliştirildi ve test edildi.
- Bu ilk projem olduğu için bazı şeyler (klasör yapısı, birkaç sabit değer) daha gelişmiş bir projeye göre daha ham durumda.

## Projeyi çalıştırma

1. Projeyi Unity **6000.3.10f1** (veya yakın bir sürüm) ile aç.
2. `Assets/Scenes/SampleScene.unity` sahnesini aç.
3. Play'e bas. Yukarıdaki kontrollerle hareket edip etkileşime geçebilirsin.
