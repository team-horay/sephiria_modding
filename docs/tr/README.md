[한국어](../../README.md) | [English](../../README.en.md) | [日本語](../ja/README.md) | [简体中文](../zh-CN/README.md) | [繁體中文](../zh-TW/README.md) | [Deutsch](../de/README.md) | [Français](../fr/README.md) | [Español](../es/README.md) | [Italiano](../it/README.md) | [Polski](../pl/README.md) | [Português (Brasil)](../pt-BR/README.md) | [Русский](../ru/README.md) | [Svenska](../sv/README.md) | [ไทย](../th/README.md) | **Türkçe**

# Sephiria Mod Rehberi

Sephiria mod topluluğuna katıldığınız için teşekkür ederiz.

Burası [Sephiria](https://store.steampowered.com/app/2436940) için mod rehberlerinin, örneklerin ve şablonların resmi deposudur. Modlar [Steam Atölyesi](https://steamcommunity.com/app/2436940/workshop/) üzerinden paylaşılabilir.



## Kurallar

Oyunun herkes için güvenli kalması adına lütfen aşağıdaki kurallara uyun. İhlaller, mod yayınlama veya oyunu oynama konusunda kısıtlamalarla sonuçlanabilir.

1. **Kötü amaçlı davranış yasaktır**: Kötü amaçlı yazılım, arka kapı, tuş kaydedici, kripto madencisi ve kullanıcının cihazını, hesabını veya verilerini tehlikeye atan her türlü özellik yasaktır.
2. **Ticari olmayan kullanım**: Tüm modlar ticari olmayan amaçlarla yapılmalı ve dağıtılmalıdır.
3. **Hak ihlali ve zararlı içerik yasaktır**: Başkalarının telif, gizlilik veya görüntü haklarını ihlal eden, cinsel içerik barındıran ya da nefreti, tacizi veya gerçek dünyadaki şiddeti teşvik eden içerikler yasaktır.
4. **Oyunu bozmak yasaktır**: Oyunun kasıtlı olarak hatalı çalışmasına yol açmayın ve diğer oyuncuların normal oyun deneyimine müdahale etmeyin.
5. **Veri toplamak yasaktır**: Kullanıcı bilgilerini toplamayın ve dışarıya göndermeyin.
6. **Erişim denetimlerini atlatmak yasaktır**: Oyunun veya dağıtım platformunun satın alma, sahiplik ya da erişim denetimlerini usulsüz şekilde atlatmayın.
7. **Mod incelemesi**: Kod (DLL) içeren modlar, kural ihlali olup olmadığını denetlemek için onaydan önce analiz ve derleme çözme (decompile) dahil bir incelemeden geçer. Onay biraz zaman alabilir. İnceleme sonuçlarını veya onay işaretlerini sahte olarak üretmeyin. Dosyalar incelenebilir durumda sunulmalıdır; karartma (obfuscation) yasaktır.
8. **Oyun varlıklarının kullanımı**: Oyunun kodunu, çizimlerini, seslerini ve diğer varlıklarını mod yapmak için gereken ölçüde çözümlemek ve analiz etmek serbesttir. Ancak çıkarılan oyun varlıklarını olduğu gibi yeniden dağıtmayın.
9. **Taklit ve kurcalama yasaktır**: Başkalarını taklit etmeyin; başkalarının yaptığı modları izinleri olmadan değiştirip yeniden dağıtmayın.



## Başlamadan Önce

- Kod (DLL) içeren modlar, Atölye'ye yüklenebilmeleri için önce **incelemeden** geçer. İnceleme esas olarak kodda kötü amaçlı davranış olup olmadığını denetler. Çeviri ve kostüm gibi kod içermeyen modlar inceleme olmadan hemen yüklenebilir. İnceleme sisteminin okuyamadığı modların (karartılmış dosyalar, C/C++ ile derlenmiş DLL'ler vb.) reddedileceğini unutmayın.
- Çok oyunculu modda, odadaki her katılımcının aynı mod setini etkin hale getirmiş olması gerekir. (Çeviri gibi kişisel arayüz modları bu kuraldan muaftır.)
- Modları etkinleştirdikten sonra sıralarını değiştirmek veya onları yeniden devre dışı bırakmak için oyunu yeniden başlatmanız gerekir.
- Abone olunan modlar varsayılan olarak devre dışıdır. Listeden kendiniz etkinleştirebilirsiniz.



## Belgeler

| Belge | İçerik |
|---|---|
| [İlk Modunuz](getting-started.md) | Geliştirme klasörü kurma, modu yerel olarak kontrol etme, günlükleri okuma |
| [metadata.json Referansı](metadata.md) | Her alanın anlamı, bağımlılık ve sürüm kuralları, modül bildirimleri |
| [Yerelleştirme Modları](localization-mods.md) | Dil dosyası yapısı, anahtarları bulma, etiketleri ve yer tutucuları koruma kuralları |
| [Kostüm Modları](costume-mods.md) | Kendi kostümünüzü yapma |
| [DLL Modları](dll-mods.md) | Proje kurulumu, giriş noktası, `HorayModAPI` olayları, hata ayıklama |
| [Atölye'de Yayınlama](publishing.md) | Atölye'ye yükleme, kod modu incelemesi, çok oyunculu kuralları |
| [API Referansı](api/README.md) | `HorayModAPI`, `WorkshopModBase`, mod seçenekleri ve arayüz gibi herkese açık türler ve üyeler. Oyun kaynağından otomatik üretilmiştir |
| [Ek & SSS](faq.md) |  |


## Lisans

Bu depodaki kod (örnekler ve şablonlar) [MIT Lisansı](../../LICENSE) ile lisanslanmıştır.
Belgeler serbestçe alıntılanabilir, çevrilebilir ve yeniden dağıtılabilir; lütfen kaynak belirtin.
Sephiria oyununun kendisi ve varlıkları TEAM HORAY'in eserleridir ve bu lisansın kapsamında değildir.
