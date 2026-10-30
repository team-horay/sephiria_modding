# Atölye'de Yayınlama

Bu sayfa modunuzu Steam Atölyesi'ne nasıl yükleyeceğinizi, inceleme sisteminin nasıl çalıştığını ve çok oyunculu modda modların oyuncular arasında nasıl eşitlendiğini açıklar.


## Yüklemeden Önce

- Bir modu Atölye'ye yüklemek için önizleme görseli (PNG veya JPG, 1 MB altında) gereklidir. Bunu `info.preview` ile ayarlayın veya mod klasörünüze bir `thumbnail.png` koyun. Ayrıntılar için [metadata.json Referansı](metadata.md#preview) sayfasına bakın.
- `info.title` en fazla 128, `info.description` en fazla 7999 karakter olabilir. Bunları birden fazla dilde yazdıysanız Atölye sayfasında `en-US` sürümü kullanılır.
- Atölye kategorilerini `tags` içine yazın. Kullanılabilir kategoriler için [metadata.json Referansı](metadata.md) sayfasına bakın.
- Klasördeki her dosya incelemeye tabidir; bu yüzden mod klasörünüzde oyun DLL'lerinin kopyaları, `.pdb` dosyaları veya ara derleme çıktıları bulunmadığından emin olun.

## Yayınlama Adımları

1. Sephiria'yı başlatın.
2. Başlık ekranında **Steam Atölyesi** panelini açın ve yaptığınız modu bulun.
3. Modunuzu seçin ve **Atölye'ye yükle** düğmesine basın.
4. Kod içermeyen modlarda Atölye'ye yükleme son adımdır.
5. Kod (DLL) içeren modlar, Atölye'ye yükleme tamamlandığında Atölye öğe kimliğiyle birlikte otomatik olarak **inceleme sistemine** gönderilir.
6. Yüklenen Atölye sayfasına gidin, abone olmak gibi son bir test yapın, ardından görünürlüğü "Herkese Açık" olarak değiştirin.

Atölye'ye yükleme tamamlandığı halde yalnızca inceleme gönderimi başarısız olduysa (örneğin bir ağ hatası nedeniyle), aynı modda **Atölye'ye yükle** düğmesine yeniden basın. İçerik değişmediyse mod Atölye'ye yeniden yüklenmez; yalnızca gönderim yeniden yapılır. Gönderim, Atölye öğesini oluşturan Steam hesabıyla yapılmalıdır.

## Kod İncelemesi

- İnceleme sistemi DLL'lerin derlemesini çözer ve bunları ağ iletişimi, dosya erişimi, karartma ve kötü amaçlı davranış açısından denetler. İnceleme, otomatik analiz ile operatör denetimini birleştirir ve hiçbir inceleme sisteminin her riski yakalayamayacağının farkındayız. Yine de bu inceleme süreciyle kötü amaçlı modların yayılmasını azaltmayı hedefliyoruz. İş birliğiniz için teşekkür ederiz.
- Kod (DLL) içeren her mod **mod inceleme sistemine de gönderilmelidir.** Bunun için her zaman oyundaki **Atölye'ye yükle** düğmesiyle yükleyin.
- Atölye'ye yüklenmiş ancak inceleme sistemine gönderilmemiş modlar genellikle oyuncular tarafından çalıştırılamaz.
- Modu indiren oyuncular, modun Atölye öğe kimliğini ve kurulu DLL'leri kullanarak inceleme durumunu sorgular.
  - **Gönderildi / Onaylandı**: Mod çalışır. İnceleme tamamlanmadan önce de çalışır.
  - **Gönderilmedi / Reddedildi**: Mod çalışmaz ve nedeni mod panelinde gösterilir.
- İnceleme sırasında bir sorun bulunursa mod yayınlandıktan sonra bile reddedilebilir. Reddedilen bir modun nedenini paneldeki **Reddedilme nedenini görüntüle** ile görebilirsiniz. Reddedilen DLL'ler olduğu gibi yeniden yayınlanamaz; sorunu düzeltip yeniden yükleyin.
- `dll` modülü olmasa bile, `libraries` içeren ya da uzantısı değiştirilmiş bir DLL veya EXE barındıran bir mod incelemeye tabidir.
- Kod incelemesinden geçen modların klasörü sıkıştırıldığında 20 MB veya daha küçük olmalıdır.

### İnceleme Başarısızlığının Nedenleri

|Mesaj|Neden|
|---|---|
|İnceleme sisteminde başvuru kaydı yok.|Mod Atölye'ye yüklenmiş ancak inceleme sistemine gönderilmemiş. **Atölye'ye yükle** düğmesiyle yeniden gönderin.|
|İnceleme sistemi bu modun onayını reddetti.|Operasyon ekibi modu bir nedenle reddetti. Reddedilme nedenini kontrol edin.|
|İnceleme sunucusundan bilgi alınamıyor.|Abonenin internet bağlantısı kesilmiş olabilir ya da inceleme sunucusunda bir sorun olabilir.|
|Bu mod önceki inceleme sistemine ait.|İnceleme sunucusu yükseltildi ve modun mevcut doğrulama verileri sıfırlandı. Yeniden gönderin.|
|Modda yetkisiz bir DLL algılandı.|Mod çalışırken moddaki DLL dosyalarının kümesi değişirse o modun yüklenmesi kısıtlanır. Yani şunlar inceleme sistemini ihlal edebilir: mod çalışırken dışarıdan bir DLL indirip derlemeyi yükler; mod çalışırken kendi DLL'ini oluşturup çalıştırır; mod, içinde kendi sıkıştırma yöntemiyle paketlenmiş bir DLL'i çıkarıp kullanır.|

## Görünürlük ve Herkese Açık Yapma

- İlk yüklediğinizde görünürlük `Unlisted` (Listelenmemiş) olarak ayarlanır.
- Herkese açık yapmadan önce kendiniz abone olun, kurulan kopyanın düzgün çalıştığını kontrol edin, ardından "Herkese Açık" olarak değiştirin.
- Sayfa açıklamasını, ekran görüntülerini, diğer dillerdeki açıklamaları ve benzerlerini Steam Atölyesi öğe sayfasından düzenleyebilirsiniz.

## Modunuzu Güncelleme

Modunuzu yayınladığınız şekilde güncelleyebilirsiniz.

- DLL'leri değiştiren bir güncelleme, yüklemeden sonra otomatik olarak yeniden gönderilir ve yeniden incelemeden geçer. Hiçbir DLL'i değiştirmediyseniz inceleme durumu aynı kalır.
- `modId` değerini değiştirmeyin. Mod farklı bir mod olarak ele alınır; bu da bağımlılıkları ve daha fazlasını bozabilir.
- **Her güncellemede `metadata.json` içindeki `version` değerini mutlaka yükseltin.** Mod sürümleri çok oyunculu bir lobiye katılırken karşılaştırılır; içerik değişip sürüm değişmediyse, farklı mod içeriğine sahip oyuncular aynı oyuna düşüp sorun yaşayabilir.

## Yükleme Bileti

Yükleme yaptıktan sonra mod klasörünüze `uploadticket.json` eklenir. Atölye öğe kimliğinizi ve inceleme gönderim bilgilerinizi içerir; bu yüzden **silmeyin.** Mod klasörünüzü git ile yönetiyorsanız bu dosyayı da commit'lemek en güvenlisidir. Bilgisayar değiştirir veya biçimlendirme yaparsanız, aynı Atölye öğesini güncellemeye devam etmek için bu dosyaya ihtiyacınız olacak. Bu dosya olmadan oyun, klasörü hiç yayınlanmamış yeni bir mod olarak ele alır.

İnceleme sisteminin 1.1.1 öncesinde kullandığı `mod-guard-manifest.json` ve `mod-guard.sig.json` artık kullanılmıyor. Kalmış olsalar bile Atölye'ye yüklenmezler; silebilirsiniz.

Dosyayı kaybederseniz nasıl kurtaracağınız için [SSS](faq.md) sayfasına bakın.


## Çok Oyunculu Kuralları

### Çok Oyunculu Lobi Kurulumu

Sunucu sahibi çok oyunculu bir lobi oluşturduğunda, **sunucu sahibinin etkinleştirdiği mod listesi** lobiye kaydedilir.

- `installation` değeri `host_only` olan modlar: katılımcıların kurması gerekmez; yalnızca listede gösterilir.
- `multiplayer.installation` değeri `everyone` olan (veya bildirilmemiş) modlar: katılımcıların da kurması gerekir.
- Kostüm modları ve veri modları: katılımcıların da kurması gerekir.

### Katılma

Bir oyuncunun mevcut mod yapılandırması lobininkinden farklıysa şu adımlardan geçer: indir → etkinleştir → oyunu yeniden başlat ve tekrar katılmayı dene.

### WorkshopLocal'a Kurulu Test Modları

`WorkshopLocal/` içinden yerel modların yüklendiği bir oyunda çok oyunculu mod kullanılamaz. Çok oyunculu modu test etmek için modunuzu Atölye'ye yükleyin, yerel klasörü boşaltın ve abone olunan kopyayı kullanın. Öğe `Unlisted` durumundayken bile bağlantı üzerinden abone olabilirsiniz.
