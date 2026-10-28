# İlk Modunuz

Bu rehber, bir Sephiria modunun temel yapısını açıklar ve ilk modunuzu oluşturup oyun içinde kontrol etmenize adım adım eşlik eder.

## Gerekenler

- Yetkin bir metin düzenleyici (örneğin [Visual Studio Code](https://code.visualstudio.com/))

## Oyun Kurulum Klasörünü Bulun

Sephiria kurulum klasörünü açın.

- Steam kütüphanenizden
  - `Steam Kütüphanesi → Sephiria'ya sağ tıklayın → Yönet → Yerel dosyalara göz at`
- Windows (varsayılan kurulum yolu)
  - `C:\Program Files (x86)\Steam\steamapps\common\Sephiria`
- macOS (varsayılan kurulum yolu)
  - `~/Library/Application Support/Steam/steamapps/common/Sephiria` (`Sephiria.app` dosyasını içeren klasör)

## WorkshopLocal Klasörünü ve Kendi Mod Klasörünüzü Oluşturun

1. Oyun kurulum klasöründe bir `WorkshopLocal/` klasörü oluşturun.
2. Onun içinde kendi modunuz için bir klasör oluşturun.
   - Sephiria her klasörü bir mod olarak ele alır.
   - Burada oluşturulan modlar oyun içinde test edilebilir ve Steam Atölyesi'ne yüklenebilir.

Bu rehberde, başlık ekranındaki "Oyunu Başlat" düğmesinin metnini "Hello World!" olarak değiştiren `my-first-mod` adlı bir mod oluşturacaksınız. Tamamlanmış yapı şöyle görünür:

```
<oyun kurulum klasörü>/
├── Sephiria.exe
└── WorkshopLocal/
    ├── my-first-mod/          ← bir mod
    │   ├── metadata.json      ← modu tanımlayan temel dosya
    │   ├── thumbnail.png      ← mod önizleme görseli (Atölye'ye yüklemek için gerekli)
    │   └── Localization/      ← yerelleştirme modülünün okuduğu klasör
    │       └── en-US.json     ← değiştirilecek metin
    └── another-mod/           ← başka bir mod
        └── ...
```

## metadata.json Dosyası

- Mod klasörünüzde `metadata.json` dosyasını oluşturun ve aşağıdaki içeriği yapıştırın.
- `metadata.json`: modunuzu tanımlayan temel dosya.
- `yourname`: bunu kendi takma adınızla değiştirin.
- Dosyayı **UTF-8** olarak kaydedin. Not Defteri'nde "Farklı Kaydet" altında kodlama olarak UTF-8'i seçin.
- Her alanın anlamı için [metadata.json Referansı](metadata.md) sayfasına bakın.

```json
{
  "info": {
    "title": "My First Mod",
    "description": "Hello World!"
  },
  "mod": {
    "schemaVersion": 1,
    "modId": "yourname.my-first-mod",
    "version": "0.0.1",
    "author": "Your Name",
    "multiplayer": { "installation": "local_only" },
    "modules": [
      { "type": "localization", "path": "Localization" }
    ]
  }
}
```

## Dil Dosyasını Oluşturun

- Mod klasörünüzün içinde bir `Localization/` klasörü oluşturun.
- `Localization/` içinde, aşağıdaki içerikle bir `en-US.json` dosyası oluşturun.
- Test için dosyayı kendi dil kodunuzla da yeniden adlandırabilirsiniz.

```json
{
  "Title_GameStart": "Hello World!"
}
```

## Kontrol Edin

Bu noktada modun çalışıp çalışmadığını kontrol edebilirsiniz.

1. Sephiria'yı başlatın.
2. Oyun dili İngilizce olarak ayarlıyken başlık ekranına bakın: "Start Game" düğmesi artık "Hello World!" olarak görünür. `WorkshopLocal/` içindeki modlar varsayılan olarak etkindir, bu yüzden onları ayrıca açmanız gerekmez.
3. Atölye panelini açmak için başlık ekranındaki **Steam Atölyesi** düğmesine basın. "My First Mod", "Modlarım" bölümünde görünmelidir.

### Modum Uygulanmıyor!

- JSON söz diziminizin doğru olduğundan emin olun.
- `metadata.json` içindeki `path`, büyük/küçük harf dahil gerçek klasör adıyla birebir eşleşmelidir.
- `metadata.json` dosyasının UTF-8 olarak kodlandığından emin olun.
- Oyun dilinin, dosya adındaki dille eşleştiğinden emin olun.

## Sonraki Adımlar

1. [metadata.json Referansı](metadata.md) ile modların nasıl tanımlandığını öğrenin.
2. [Yerelleştirme Modları](localization-mods.md) ve [Kostüm Modları](costume-mods.md) nispeten kolay yapılır; bu yüzden onlarla başlayın.
3. Temel kodlama bilginiz varsa [DLL Modları](dll-mods.md) önerilir.
4. [Atölye'de Yayınlama](publishing.md) ile modunuzu Atölye'de yayınlayın.
