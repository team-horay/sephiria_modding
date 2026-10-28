# Kostüm Modları

Hiç kod yazmadan, yalnızca PNG ve JSON dosyalarıyla bir kostüm ekleyelim. Bu tür bir mod aşağıdaki iki şeyi ekleyebilir:

- **Yeni kostümler**: Sıfırdan yeni bir kostüm oluşturun. Görünümünü, niteliklerini, başlangıç eşyalarını ve daha fazlasını ayarlayabilirsiniz.
- **Mevcut kostümler için görünümler**: Pembe Tavşan gibi mevcut bir kostüme bir görünüm (skin) ekleyin.

Bu sayfa [`examples/costume-sample`](../../examples/costume-sample) örneğini temel alır. Mod yapısını kavramak için indirin.

## Mod Yapısı

```
costume-sample/
├── metadata.json
├── thumbnail.png
├── Costumes/                  ← kostüm klasörü
│   └── WhiteCat/              ← bir klasör = bir kostüm
│       ├── costume.json
│       ├── icon.png
│       ├── idle.png
│       ├── move.png
│       ├── idle_back.png
│       ├── move_back.png
│       ├── attack.png
│       └── ...                ← diğer hareket PNG'leri
└── Localization/              ← kostüm adı ve açıklama metni
    ├── ko-KR.json
    ├── en-US.json
    └── ...
```


## metadata.json

Örnekten yola çıkarak kendi modunuzu oluştururken `metadata.json` içindeki `modId` ve `author` değerlerini değiştirin. (Ayrıntılar için [`metadata.json`](metadata.md) sayfasına bakın.)

```json
{
  "info": {
    "title": { "ko-KR": "하얀 고양이 코스튬", "en-US": "White Cat Costume" },
    "description": { "ko-KR": "하얀 고양이 코스튬을 추가합니다.", "en-US": "Adds the White Cat costume." }
  },
  "tags": ["Costume"],
  "mod": {
    "schemaVersion": 1,
    "modId": "yourname.costume-whitecat",
    "version": "1.0.0",
    "author": "Your Name",
    "multiplayer": { "installation": "everyone" },
    "modules": [
      { "type": "costume", "path": "Costumes" },
      { "type": "localization", "path": "Localization" }
    ]
  }
}
```


## Sprite Animasyonları

- Hepsini dahil ederseniz bir kostüm epey çok animasyon gerektirir.
- Bu ilk kostümünüzse yalnızca `idle, move, airborne, fall` ile basit bir tane yapmayı deneyin.

| Hareket | Açıklama | Zorunlu |
|---|---|---|
| `idle` | Beklemede | Zorunlu |
| `move` | Hareket | Zorunlu |
| `airborne` | Havada | İsteğe bağlı |
| `fall` | Yere yığılma | İsteğe bağlı |
| `attack` | Saldırı | İsteğe bağlı |
| `greatsword_lower` | Döner savurma | İsteğe bağlı |
| `greatsword_sweep` | Dönüş | İsteğe bağlı |
| `hitfeedback` | Hasar alma | İsteğe bağlı |

### Arkaya Dönük Animasyonlar

- Daha doğal görünen bir kostüm için arkaya dönük sprite'lar çizebilirsiniz. Bu isteğe bağlıdır.
- Arkaya dönük sprite'lar eklemeye karar verirseniz `idle_back, move_back` hareketlerini dahil etmeniz zorunludur.
- Arkaya dönük animasyonlar şöyle tanımlanır.

| Hareket | Açıklama |
|---|---|
| `idle_back` | Beklemede (arka) |
| `move_back` | Hareket (arka) |
| `attack_back` | Saldırı (arka) |
| `greatsword_upper` | Döner savurma (arka) |

## Çizimleri Hazırlama

- Her hareket için, o hareketin karelerinin yatay olarak dizildiği bir PNG sprite sayfası yapın.
- Görselin yüksekliği kare yüksekliğine eşittir; genişlik ise kare genişliği × kare sayısıdır. Kareler arasında boşluk bırakmayın.
- Her bir görseli aynı boyutta yapmanızı öneririz.
- Her sprite'ın 20×16 olduğu Beyaz Kedi örneğinde görsel boyutları şöyledir.

| Dosya | Boyut | Kare |
|---|---|---|
| `idle.png` | 120×16 | 6 |
| `move.png` | 160×16 | 8 |
| `attack.png` | 60×16 | 3 |
| `fall.png` | 20×16 | 1 |

*Örnek görsel*


### Yeni Kostümün Bilgilerini Yazma

Bir `costume.json` dosyası yazarak kendi kostümünüzü tanımlayın.

Örnek `Costumes/WhiteCat/costume.json`:

```json
{
  "schemaVersion": 1,
  "id": "whitecat",
  "name": "Costume_WhiteCat_Name",
  "flavorText": "Costume_WhiteCat_FlavorText",
  "icon": "icon.png",

  "frameSize": [20, 16],
  "pivot": [10, 2],
  "pixelsPerUnit": 16,

  "animations": {
    "idle":             { "file": "idle.png", "fps": 12 },
    "idle_back":        { "file": "idle_back.png", "fps": 12 },
    "move":             { "file": "move.png", "fps": 4, "footstepFrames": [3, 7] },
    "move_back":        { "file": "move_back.png", "fps": 4, "footstepFrames": [3, 7] },
    "attack":           { "file": "attack.png", "fps": 20, "loop": false },
    "attack_back":      { "file": "attack_back.png", "fps": 20, "loop": false },
    "greatsword_lower": { "file": "greatsword_lower.png", "fps": 20, "loop": false },
    "greatsword_upper": { "file": "greatsword_upper.png", "fps": 20, "loop": false },
    "greatsword_sweep": { "file": "greatsword_sweep.png", "fps": 30, "loop": false },
    "airborne":         { "file": "airborne.png", "fps": 12 },
    "fall":             { "file": "fall.png", "fps": 1 },
    "hitfeedback":      { "file": "hitfeedback.png", "fps": 20, "loop": false }
  },

  "stats": ["DEFENSE/-10", "EVASION/1200"],
  "startingItems": [],
  "onlyDefaultWeapon": false
}
```

| Alan | Zorunlu | Açıklama |
|---|---|---|
| `schemaVersion` | Evet | Biçim tanımlayıcısı. `1` tam sayısını girin. |
| `id` | Evet | Küçük İngilizce harfler, rakamlar, alt çizgi ve tire kullanın, 1–31 karakter. Çakışmaları önlemek için oyun içinde `<modId>:<id>` olarak kaydedilir. |
| `name` | Evet | Kostüm adının **yerelleştirme anahtarı**. Aşağıdaki [Yerelleştirme Dosyalarını Hazırlama](#yerelleştirme-dosyalarını-hazırlama) bölümüne bakın. |
| `flavorText` | Hayır | Kostüm açıklamasının **yerelleştirme anahtarı**. Aşağıdaki [Yerelleştirme Dosyalarını Hazırlama](#yerelleştirme-dosyalarını-hazırlama) bölümüne bakın. |
| `icon` | Evet | Kostüm önizleme simgesi PNG'sinin yolu. |
| `face` | Hayır | Çok oyunculu HUD için yüz PNG'sinin yolu. |
| `pixelsPerUnit` | Hayır | Daha ayrıntılı çizim kullanmak için bunu değiştirin. Daha yüksek değerler çizimi daha ince ayrıntıyla işler. Varsayılan PPU 16'dır. |
| `frameSize` | Hayır | Her sprite'ın `[genişlik, yükseklik]` değeri. Varsayılan: `[16, 16]`. |
| `pivot` | Hayır | Her sprite'ın pivot noktası. Sol alt köşe `[0, 0]`'dır. Varsayılan: `[genişlik/2, 0]`. |
| `animations` | Evet | Her hareketin çizimi. [animations](#animations) bölümüne bakın. |
| `stats` | Hayır | Ayarlanacak niteliklerin listesi. [Nitelik Listesi](stats.md) sayfasına bakın. |
| `startingItems` | Hayır | Başlangıç eşyası kimliklerinin dizisi. [Eşya Listesi](item-table.md) sayfasına bakın. |
| `body` | Hayır | Vuruş kutusunu ayarlar. [body](#body) bölümüne bakın. |
| `skins` | Hayır | Aynı kostüm için ek görünümler. [skins](#skins) bölümüne bakın. |

### animations

```json
  "animations": {
    "idle":      { "file": "idle.png", "fps": 12 },
    "move":      { "file": "move.png", "fps": 4, "footstepFrames": [3, 7] },
    "attack":    { "file": "attack.png", "fps": 20, "loop": false },
    "fall":      { "file": "fall.png", "fps": 1 },
    "hitfeedback": { "file": "hitfeedback.png", "fps": 20, "loop": false, "frameSize": [24, 16], "pivot": [12, 2] }
  }
```

- Yukarıda hazırladığınız animasyonları dört özellikle tanımlayın: `{ "file", "fps", "loop", "footstepFrames" }`.
- Her hareket için `frameSize` ve `pivot` değerlerini ayrı ayrı da ayarlayabilirsiniz.
- Son satırdaki gibi yalnızca tek bir hareketi farklı boyutta çizdiyseniz, `frameSize` ve `pivot` değerlerini o harekette ayarlayın.

#### Özellikler

| Alan | Zorunlu | Açıklama |
|---|---|---|
| `file` | Evet | Oynatılacak dosya adı. |
| `fps` | Hayır | Animasyon oynatma hızı. (Varsayılan: 12) |
| `loop` | Hayır | Animasyonun döngüye girip girmeyeceği. (Varsayılan: true; saldırı animasyonları için false) |
| `footstepFrames` | Hayır | Sıfırdan başlayan kare numaraları. Bu kareler gösterildiğinde bir ayak sesi çalar. |


### body

```json
"body": { "hitboxRadius": 4, "hitboxOffset": [0, 4], "movementRadius": 0.25, "bodyScale": 1 }
```

- Karakterin hareket ve vuruş çarpışma alanlarını tanımlar.
- Yerleşik kostümler bu değerleri varsayılan olarak kullanır.
- Özel bir nedeniniz yoksa bunu atlamanızı öneririz.

#### Özellikler

| Alan | Zorunlu | Açıklama |
|---|---|---|
| `hitboxRadius` | Hayır | Vuruş kutusu dairesinin yarıçapı, piksel cinsinden, 0,5–64. Varsayılan `4`. |
| `hitboxOffset` | Hayır | Vuruş kutusu dairesinin merkezi, `[x, y]`, pivota göre piksel cinsinden; pozitif x sağ, pozitif y yukarıdır, ±64. Varsayılan `[0, 4]`. |
| `movementRadius` | Hayır | Duvarlar ve engellerle çarpışan hareket çarpışma dairesinin yarıçapı, birim cinsinden (16 piksel = 1), 0,05–2. Varsayılan `0.25`. |
| `bodyScale` | Hayır | Silahın tutulduğu omuz konumunun ölçeği, 0,1–4. Varsayılan `1`. Büyük bir kostümde silah gövdeye gömülüyorsa yükseltin. |



### skins

```json
"skins": [
  { "id": "winter", "name": "Skin_WhiteCat_Winter_Name", "folder": "skins/Winter", "icon": "icon.png",
    "animations": { "idle": "idle.png", "move": "move.png" } }
]
```

- Aynı kostüme ek görünümler ekler.
- Animasyonlar yukarıdakiyle aynı biçimi izler.
- `frameSize` veya `pivot` değerini atlarsanız kostümün kendi değerleri kullanılır.




## Yerelleştirme Dosyalarını Hazırlama

`Localization/ko-KR.json`:

```json
{
  "Costume_WhiteCat_Name": "하얀 고양이",
  "Costume_WhiteCat_FlavorText": "줄무늬가 인상적입니다."
}
```

`Localization/en-US.json`:

```json
{
  "Costume_WhiteCat_Name": "White Cat",
  "Costume_WhiteCat_FlavorText": "The stripes are rather impressive."
}
```

- `name` ve `flavorText` alanlarına metin yerine bir **yerelleştirme anahtarı** yazın ve metni aynı modun `Localization/` klasörüne, her dil için bir dosya olacak şekilde koyun.
- Anahtarlarda boşluk olmadan İngilizce harfler, rakamlar, alt çizgi ve tire kullanın. Modunuzun adını dahil etmek diğer modlarla çakışmaları önlemeye yardımcı olur.
- Bir anahtar **hiçbir dil dosyasında yoksa** kostüm yüklenmez. En az bir dil sağlamanız gerekir.
- Daha fazla kişinin kostümünüzün keyfini çıkarabilmesi için İngilizce (`en-US`) de eklemenizi öneririz.
- `metadata.json` içindeki `modules` alanında bir `localization` modülü bildirmeyi de unutmayın.



## Kontrol Edin

1. Mod klasörünüzü `WorkshopLocal/` içine koyun ve oyunu başlatın.
2. Bir oyun başlatın, köyde kostüm seçimini açın (**Kostümü değiştir**) ve yeni kostümünüzün eklendiğini kontrol edin.
3. Kostümün içeriğini değiştirirseniz oyunu yeniden başlatmanız gerekir.



## Ek: Mevcut Bir Kostüme Yalnızca Skin Ekleme

`costume.json` yerine `skin.json` yaparak mevcut bir kostüm için görünüm oluşturabilirsiniz.

```
Costumes/
└── PinkRabbit_Pajama/
    ├── skin.json
    ├── icon.png
    ├── idle.png
    └── move.png
```

```json
{
  "schemaVersion": 1,
  "costume": "PinkRabbit",
  "id": "pajama",
  "name": "Skin_PinkRabbit_Pajama_Name",
  "icon": "icon.png",
  "animations": { "idle": "idle.png", "move": "move.png" }
}
```

- `costume`, hedef kostümün kimliğidir. Yerleşik bir kostüm kimliği (`PinkRabbit` gibi) ya da başka bir modun kostümü için mod adını içeren çalışma zamanı kimliğini kullanın. (Beyaz Kedi örneği için `yourname.costume-whitecat:whitecat`.)
- `stats`, `startingItems`, `body`, `skins` ve `flavorText` mevcut kostümden olduğu gibi gelir; bu yüzden bunları ayarlayamazsınız.
- Yerleşik bir kostümün varsayılan görünümünü geçersiz kılmak desteklenmez.
