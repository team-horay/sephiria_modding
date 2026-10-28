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
3. Modunuzu seçin.
4. Kod (DLL) içeren modlar kod incelemesinden geçer.
5. Kod içermeyen modlar Atölye'ye hemen yüklenebilir.
6. Yüklenen Atölye sayfasına gidin, abone olmak gibi son bir test yapın, ardından görünürlüğü "Herkese Açık" olarak değiştirin.

## Kod İncelemesi

- İnceleme sistemi mod klasörünüzdeki her dosyayı denetler. DLL'lerin derlemesi çözülür ve ağ iletişimi, dosya erişimi, karartma ve kötü amaçlı davranış açısından denetlenir; görsel, ses ve JSON gibi veri dosyaları ise geçerli biçimde olup olmadıkları açısından denetlenir.
- Sorun bulunmazsa mod otomatik olarak onaylanır; şüpheli bir şey görülürse bir operatör elle denetler. Açıkça kötü amaçlı modlar otomatik olarak reddedilir.
- İnceleme, otomatik analiz ile operatör denetimini birleştirir ve hiçbir inceleme sisteminin her riski yakalayamayacağının farkındayız. Yine de bu ön inceleme ve onay süreciyle kötü amaçlı modların yayılmasını azaltmayı hedefliyoruz. İş birliğiniz için teşekkür ederiz.
- Bir modun kod içerip içermediği dosya adına göre değil, dosya **içeriğine** göre belirlenir. `dll` modülü olmasa bile, `libraries` içeren ya da uzantısı değiştirilmiş bir DLL veya EXE barındıran bir mod incelemeye tabidir.
- Kod incelemesinden geçen modların klasörü sıkıştırıldığında 20 MB veya daha küçük olmalıdır.

## Onay Sertifikası (Yalnızca Kod İçeren Modlar)

- Kod içeren modlar bir onay sertifikası alır.
- Bu sertifika, modun dosyalarının onaylanan sürümle birebir aynı olduğunu belgeler.
- Modu indiren oyuncuların modun kodunu çalıştırabilmesi için DLL modlarının bu sertifikasyondan geçmesi gerekir.
- Sertifikasyondan sonra tek bir dosya bile değişirse sertifika geçersiz olur ve modun yeniden incelenmesi gerekir.

## Görünürlük ve Herkese Açık Yapma

- İlk yüklediğinizde görünürlük `Unlisted` (Listelenmemiş) olarak ayarlanır.
- Herkese açık yapmadan önce kendiniz abone olun, kurulan kopyanın düzgün çalıştığını kontrol edin, ardından "Herkese Açık" olarak değiştirin.
- Sayfa açıklamasını, ekran görüntülerini, diğer dillerdeki açıklamaları ve benzerlerini Steam Atölyesi öğe sayfasından düzenleyebilirsiniz.

## Modunuzu Güncelleme

Modunuzu yayınladığınız şekilde güncelleyebilirsiniz.

- DLL modlarının yeni bir onay sertifikasına ihtiyacı vardır.
- `modId` değerini değiştirmeyin. Mod farklı bir mod olarak ele alınır; bu da bağımlılıkları ve daha fazlasını bozabilir.
- **Her güncellemede `metadata.json` içindeki `version` değerini mutlaka yükseltin.** Mod sürümleri çok oyunculu bir lobiye katılırken karşılaştırılır; içerik değişip sürüm değişmediyse, farklı mod içeriğine sahip oyuncular aynı oyuna düşüp sorun yaşayabilir.

## Yükleme Bileti

İnceleme isteyip yükleme yaptıktan sonra aşağıdaki dosyalar mod klasörünüze eklenir. İnceleme onay bilgilerinizi içerirler; bu yüzden **silmeyin.** Mod klasörünüzü git ile yönetiyorsanız bu üç dosyayı da commit'lemek en güvenlisidir. Bilgisayar değiştirir veya biçimlendirme yaparsanız, aynı Atölye öğesini güncellemeye devam etmek için bu dosyalara ihtiyacınız olacak.

| Dosya | İçerik |
|---|---|
| `uploadticket.json` | İnceleme bileti ve Atölye öğe kimliği. Bu dosya olmadan oyun, klasörü hiç yayınlanmamış yeni bir mod olarak ele alır. |
| `mod-guard-manifest.json`, `mod-guard.sig.json` | İnceleme onay sertifikası: onaylanan dosyaların listesi ve bir imza. Düzenlenmeleri onayı geçersiz kılar. |

Bu dosyaları kaybederseniz nasıl kurtaracağınız için [SSS](faq.md) sayfasına bakın.

## Onayın Geri Alınması

Yayınladıktan sonra bir kural ihlali tespit edilirse operatör onayı geri alabilir. Onayı geri alınan bir mod kurulu olsa bile yüklenmez ve panelde "İnceleme onayı geri alındı" olarak gösterilir.


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
