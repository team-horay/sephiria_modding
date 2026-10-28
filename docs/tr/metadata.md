# metadata.json

Mod klasörünüzün kökündeki `metadata.json` dosyası, modunuzun hem **ne** olduğunu hem de **nasıl** çalıştığını tanımlayan temel dosyadır.


## Genel Yapı

```json
{
  "info": {
    "title": "New Artifact Pack",
    "description": "Adds 12 artifacts.",
    "preview": "thumbnail.png"
  },
  "mod": {
    "schemaVersion": 1,
    "modId": "yourname.artifact-pack",
    "version": "0.9.1",
    "author": "Your Name",
    "multiplayer": { "installation": "everyone" },
    "modules": [
      { "type": "dll", "path": "bin/ArtifactPack.dll", "entryClass": "ArtifactPack.Entry" },
      { "type": "localization", "path": "Localization" }
    ],
    "libraries": [
      "bin/Helper.dll"
    ],
    "dependencies": [
      { "modId": "someone.shared-runtime", "version": "2.5.3", "workshopId": "123456789" }
    ]
  },
  "tags": ["Item"]
}
```

En üst düzeyde üç şey tanımlanır.

| Anahtar | Rol | Zorunlu |
|---|---|---|
| `info` | Steam Atölyesi'nde ve oyun içi panelde gösterilen başlık, açıklama ve önizleme | Zorunlu |
| `tags` | Atölye etiketleri | İsteğe bağlı |
| `mod` | Modun nasıl çalıştığı | Zorunlu |

## info

### title

- Modun adı. Boş bırakılırsa ad olarak `modId` kullanılır.
- Modun adını her dil için ayrı tanımlamak üzere bir dil haritası kullanabilirsiniz.
	- Kendi dilinize ek olarak İngilizce eklemenizi öneririz.
- Desteklenen dil kodları: `ko-KR`, `en-US`, `ja-JP`, `zh-CN`, `zh-TW`, `de-DE`, `fr-FR`, `es-ES`, `it-IT`, `pl-PL`, `pt-BR`, `ru-RU`, `sv-SE`, `th-TH`, `tr-TR`

```json
"info": {
  "title": { "en-US": "New Artifact Pack", "ko-KR": "새 아티팩트 팩" }
}
```

### description

- Modun açıklaması.
- `title` gibi, açıklamayı da dil başına tanımlayabilirsiniz.

```json
"info": {
  "description": { "en-US": "Adds 12 artifacts.", "ko-KR": "아티팩트 12종을 추가합니다." }
}
```

### preview

- Atölye'ye yüklemek için bir önizleme görseli gereklidir. (PNG veya JPG, 1 MB altında)
- Önizleme görselinin göreli yolunu girin.
- Atlanırsa oyun, kök klasörde sırasıyla `thumbnail.png`, `preview.png`, `thumbnail.jpg` ve `preview.jpg` dosyalarını arar.


## mod

### schemaVersion

- Geçerli sürüm 1'dir.
- Modların çalışma biçiminde büyük değişiklikler olursa bu sayının ileride artabileceğini unutmayın.

### modId

- Modunuzu tanımlayan benzersiz bir dize. Küçük İngilizce harfler, rakamlar, nokta, tire ve alt çizgi kullanın. En fazla 64 karakter.
- **Benzersiz bir ad seçin.** `modId` değeriniz başka bir modunkiyle çakışırsa mod doğru çalışmaz. Başına yapımcı adınızı eklemek çakışmaları önlemede büyük ölçüde yardımcı olur.
(örn. `horay.artifact-pack`)
- **Yayınladıktan sonra `modId` değerini değiştirmeyin.** Başkaları `dependencies` kullanarak modunuzun üzerine alt modlar inşa edebilir ve üst modu `modId` ile tanırlar.


### version

- Modunuzun sürümünü tanımlayan bir dize.
- Başka bir mod `dependencies.version` ile tam bir sürüm istediğinde karşılaştırılan değer budur.
- **Modunuzu her güncellediğinizde sürümü yükseltin.** Çok oyunculu bir lobiye katılırken oyun, sunucu sahibi ile katılımcıların aynı mod sürümlerine sahip olup olmadığını denetler. İçeriği değiştirip sürümü aynı bırakırsanız, farklı mod içeriğine sahip oyuncular aynı lobiye düşebilir; bu da içerik eşitlemesinin başarısız olmasına veya oyunun ilerleyememesine yol açabilir.
- Oyunun kullandığı 1.0.0 biçimini öneririz.

### author

Kim olduğunuzu herkese duyurun!

### multiplayer / installation

Çok oyunculu bir oyundaki oyuncuların bu modu paylaşması gerekip gerekmediğini tanımlar. İzin verilen değerler:

| Değer | Açıklama |
|---|---|
| `local_only` | Yalnızca kendi ekranınızı ve arayüzünüzü etkiler (yerelleştirme modları ve basit arayüz modları bu gruba girer) |
| `everyone` | Odadaki herkesin aynı modu etkinleştirmiş olması gerekir (içerik ekleyen veya değiştiren her mod bu gruba girer) |
| `host_only` | Yalnızca sunucu sahibinin etkinleştirmesi gerekir; diğer katılımcılarda değişiklik gerektirmeden çalışır (yalnızca sunucu tarafı hesaplamayı etkileyen çok oyunculu modlar bu gruba girer) |

`installation` değerini `local_only` veya `host_only` olarak bildirir ama modunuz katılımcıların da ihtiyaç duyduğu içeriğe dokunursa (yeni eşya veya birim kaydetme, ağ nesneleri, katılımcıların istemcilerinde de çalışan kod), aynı lobideki oyuncular farklı oyunlar oynuyor olabilir ya da içerik eşitlemesi başarısız olup oyunun normal ilerleyişini engelleyebilir. Modunuzun gerçekte ne yaptığını mutlaka kontrol edin.


### modules

- Modüller modunuzun ne yaptığını tanımlar; bu da burayı **en önemli bölüm** yapar.
- Mevcut modüller: `localization, dll, costume, data`.
- Aşağıdaki örnekte olduğu gibi, tek bir pakette birden fazla modülü bir arada kullanabilirsiniz.
- Tek bir modül bile tanınmazsa paketin tamamı "desteklenmiyor" olarak işaretlenir ve mod yükleme sırasında atlanır.

```json
"modules": [
  { "type": "localization", "path": "Localization" },
  { "type": "dll", "path": "bin/ArtifactPack.dll", "entryClass": "ArtifactPack.Entry" },
  { "type": "costume", "path": "Costumes" },
  { "type": "data", "path": "Packs", "consumer": "someone.shared-runtime" }
]
```

#### type: localization modülü

```json
"modules": [
  { "type": "localization", "path": "Localization" }
]
```

- `localization` modülü, dil dosyaları içeren modlar içindir.
- `path` değerini dil dosyalarını içeren klasör olarak ayarlayın.
- `path` klasörünün içine `ko-KR.json` ve `en-US.json` gibi **dil koduyla adlandırılmış JSON dosyaları** koyun. Oyunun kullandığı dil kodları ve dosya biçimi için [Yerelleştirme Modları](localization-mods.md) sayfasına bakın.

#### type: dll modülü

```json
"modules": [
  { "type": "dll", "path": "bin/ArtifactPack.dll", "entryClass": "ArtifactPack.Entry" }
]
```

- `dll` modülü, C# koduyla oyuna bağlanmanızı sağlar.
- `path` değerini DLL dosyasının yolu olarak ayarlayın.
- `entryClass` değerini, DLL içindeki giriş noktası sınıfının ad alanı dahil tam adı olarak ayarlayın. Oyun, DLL içinde giriş noktası aramaz; bu yüzden **mutlaka belirtmelisiniz**. (İç içe sınıflar için `Ns.Outer+Inner` kullanın.)
- Aynı `entryClass` değerini iki farklı modülde kullanmayın.
- Yalnızca Mono derlemelerinde çalışır.
- Ayrıntılar için [DLL Modları](dll-mods.md) sayfasına bakın.

#### type: costume modülü

```json
"modules": [
  { "type": "costume", "path": "Costumes" }
]
```

- Hiç C# kodu yazmadan kendi kostümlerinizi oluşturmanızı sağlar.
- `path` değerini kostüm dosyalarınızı içeren klasör olarak ayarlayın.
- Çok oyunculu modda herkesin bu modu kurmuş olması gerekir.
- Ayrıntılar için [Kostüm Modları](costume-mods.md) sayfasına bakın.


#### type: data modülü

```json
"modules": [
  { "type": "data", "path": "Packs", "consumer": "someone.shared-runtime" }
]
```

- Modlar için genel amaçlı bir veri modülü. Olağan durumlarda kullanılmaz; esas olarak modlar arası bağımlılıklar için kullanılır.
- `consumer`, bu veri modülünü hangi modun kullanacağını belirtir, ancak genellikle atlanır. Atlanırsa `dependencies` içinde listelenen her mod bu veriyi okuyabilir.
- Birden fazla üst modunuz varsa ve veri yalnızca bunlardan biri içinse, o modun `modId` değerini `consumer` alanına yazın. Bu değer `dependencies` içinde de bulunmalıdır.

### libraries

```json
"libraries": [
  "bin/Helper.dll",
  { "path": "bin/Newtonsoft.Json.dll" }
]
```

- `dll` modülünüzün başvurduğu yardımcı derlemelerin listesi.
- Oyun, başvuruları **yalnızca burada bildirilen dosyalar** üzerinden çözümler. Bir DLL'i klasöre koymak, bulunması için yeterli değildir.
- Girdiler bir dize ya da `{ "path": "..." }` nesnesi olabilir.
- Her girdi `.dll` ile bitmelidir ve `modules` içindeki bir DLL ile çakışırsa doğru çalışmaz.
- Kütüphaneler giriş noktası olarak ele alınmaz. İçlerinden biri bir `WorkshopModBase` sınıfı içerse bile çağrılmaz.

### dependencies

```json
"dependencies": [
  { "modId": "someone.shared-runtime", "version": "2.5.3", "workshopId": "123456789" }
]
```

- Üst mod bir "içerik oluşturma" modu ise, bir alt mod onun üzerine yeni içerik ekleyebilir.
- Bu durumda `dependencies` kullanarak başka bir moda bağımlılık bildirebilirsiniz.
- Bir üst mod belirtildiğinde modunuz o üst moddan önce yüklenemez.
- Üst mod eksikse, devre dışıysa veya yüklenemezse, tüm alt modlarının yüklenmesi iptal edilir.
- Bir bağımlılık aşağıdaki üç alana sahip olabilir.

| Alan | Zorunlu | Kural |
|---|---|---|
| `modId` | Evet | Üzerine inşa ettiğiniz modun `modId` değeri |
| `version` | Hayır | **Yalnızca birebir eşleşen bir sürüm dizesine ihtiyacınız olduğunda kullanın.** Sürüm farklıysa yükleme atlanır; bu yüzden genellikle boş bırakmak daha güvenlidir. |
| `workshopId` | Hayır | Kullanıcıların gerekli modu bulup abone olmasını sağlar. |


## tags

- Atölye'ye yüklerken kullanılan kategoriler.
- Bir modun birden fazla etiketi olabilir. (Hem eşya hem kostüm ekleyen hepsi bir arada bir mod için `"tags": ["Item", "Costume"]` kullanın.)

| Kategori | İçerik |
|---|---|
| `Item` | Eşyalar (eserler, tabletler, iksirler vb.), kombolar |
| `Costume` | Kostümler |
| `Weapon` | Silahlar |
| `NPC` | Gezginler, tüccarlar vb. |
| `Enemy` | Yeni düşmanlar, bosslar vb. |
| `Gameplay` | Oynanış değişiklikleri |
| `QoL` | Arayüz, kullanım kolaylığı ve erişilebilirlik iyileştirmeleri |
| `Library` | Diğer modların bağımlı olduğu çalışma zamanları, çerçeveler, veri paketleri vb. |
| `Localization` | Çeviriler |

## Yol Kuralları
 
- `modules[].path`, `libraries` ve `info.preview` içindeki yolların tümü aynı kurallara uyar.
- Yalnızca mod klasörüne göreli yollara izin verilir. (Mutlak yollar, sürücü harfleri ve `..` reddedilir.)
- Ayırıcı olarak `/` kullanın.
- Sembolik bağlantılardan veya kavşaklardan (junction) geçen yollar reddedilir.
- **Büyük/küçük harf gerçek adla birebir eşleşmelidir.** Windows'ta `localization` yazmak klasörü yine açar, ancak oyun bunu `Localization` ile farklı sayıp reddeder. (Bu kural Steam Deck'te (Linux) aynı davranışı garanti eder.)
