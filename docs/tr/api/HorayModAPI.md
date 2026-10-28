<!-- Otomatik üretilen dosya. Oyun deposundaki Tools/ModApiDocGen, kaynaktaki /// yorumlarından oluşturur. Doğrudan düzenlemeyin; kaynak yorumlarını düzeltip yeniden üretin. -->
# HorayModAPI

Oyunun modlara sunduğu statik giriş noktası. Veritabanı yükleme, oturum ve doğuş olaylarına abone olun; ağ prefab'larını ve Özel Nitelikleri kaydedin. Olaylara yalnızca bir kez, `OnModLoaded` içinde abone olun. Yaşam döngüsü ve çok oyunculu kuralları için [DLL Modları](../dll-mods.md) sayfasına bakın.

Oyun sürümü **1.1.0** için 2026-10-26 tarihinde üretildi. [Referans dizini](README.md)

| Tür | Açıklama |
|---|---|
| [`HorayModAPI`](#horaymodapi) | Modlar için giriş noktası. |
| [`HorayModLocalizationContext`](#horaymodlocalizationcontext) | Mod yerelleştirme metinlerini kaydetmek için bir yardımcı. |


## HorayModAPI

```csharp
public static class HorayModAPI
```

Modlar için giriş noktası.

Örnek:

```csharp
HorayModAPI.OnLoadMiracleDatabase += () => {
    MiracleDatabase.Register(myCustomMiraclePrefab);
    MiracleDatabase.Modify("Scholar", m => m.categories = new[] { "Lake" });
};
```

### Sabitler ve Alanlar

#### WORK_IN_PROGRESS_README

```csharp
public const string WORK_IN_PROGRESS_README = "The MOD System is still under development. Most features are not work properly yet."
```

*Henüz açıklama yok.*


#### WORK_IN_PROGRESS_README_URL

```csharp
public const string WORK_IN_PROGRESS_README_URL = "https://teamhoray.com/mod-api"
```

*Henüz açıklama yok.*


#### NAME

```csharp
public const string NAME = "HorayMod"
```

*Henüz açıklama yok.*

### Olaylar

#### OnLoadMiracleDatabase

```csharp
public static event Action OnLoadMiracleDatabase
```

Mucize veritabanı başlatıldıktan hemen sonra tetiklenir. Mucizeleri `MiracleDatabase.Register`/`Modify` ile kaydedin veya değiştirin.


#### OnLoadWeaponDatabase

```csharp
public static event Action OnLoadWeaponDatabase
```

Silah veritabanı başlatıldıktan hemen sonra tetiklenir. Silahları `WeaponDatabase.Register`/`Modify` ile kaydedin veya değiştirin.


#### OnLoadStatusDatabase

```csharp
public static event Action OnLoadStatusDatabase
```

Durum (nitelik etkisi) veritabanı başlatıldıktan hemen sonra tetiklenir. Durumları `StatusDatabase.Register`/`Modify` ile kaydedin veya değiştirin.


#### OnLoadSpecialStatDatabase

```csharp
public static event Action OnLoadSpecialStatDatabase
```

Özel nitelik kategorileri ve durumları yüklendikten hemen sonra tetiklenir. Gösterim girdilerini `RegisterSpecialStatCategory`/`RegisterSpecialStat` ile kaydedin.


#### OnLoadItemDatabase

```csharp
public static event Action OnLoadItemDatabase
```

Eşya veritabanı başlatıldıktan hemen sonra tetiklenir. Eşyaları `ItemDatabase.Register`/`Modify` ile kaydedin veya değiştirin.


#### OnLoadKeywordDatabase

```csharp
public static event Action OnLoadKeywordDatabase
```

Anahtar sözcük veritabanı başlatıldıktan hemen sonra tetiklenir. Anahtar sözcükleri `KeywordDatabase.Register`/`Modify` ile kaydedin veya değiştirin.


#### OnLoadPassiveDatabase

```csharp
public static event Action OnLoadPassiveDatabase
```

Pasif veritabanı başlatıldıktan hemen sonra tetiklenir. Pasifleri `PassiveDatabase.Register`/`Modify` ile kaydedin veya değiştirin.


#### OnLoadCostumeDatabase

```csharp
public static event Action OnLoadCostumeDatabase
```

Kostüm veritabanı başlatıldıktan hemen sonra tetiklenir. Kostümleri `CostumeDatabase.Register`/`RegisterSkin`/`Modify` ile kaydedin veya değiştirin.


#### OnLoadPropDatabase

```csharp
public static event Action OnLoadPropDatabase
```

Prop veritabanı başlatıldıktan hemen sonra tetiklenir. Prop'ları `PropDatabase.Register` ile kaydedin.


#### OnLoadUnitDatabase

```csharp
public static event Action OnLoadUnitDatabase
```

Birim veritabanı (düşman ve NPC doğuş varlıkları) başlatıldıktan hemen sonra tetiklenir.


#### OnLoadSocialIDDatabase

```csharp
public static event Action OnLoadSocialIDDatabase
```

NPC rolü veritabanı başlatıldıktan hemen sonra tetiklenir.


#### OnLoadCustomGameDatabase

```csharp
public static event Action OnLoadCustomGameDatabase
```

Özel oyun veritabanı başlatıldıktan hemen sonra tetiklenir.


#### OnAllDatabasesReady

```csharp
public static event Action OnAllDatabasesReady
```

Tüm veritabanları başlatmayı bitirdikten hemen sonra tetiklenir.


#### OnTitleMenuReady

```csharp
public static event Action<UI_TitleLobby> OnTitleMenuReady
```

Başlık menüsü yüklendikten sonra çağrılır.


#### OnStartGameServerside

```csharp
public static event Action OnStartGameServerside
```

Oyun başladığında çağrılır. (sunucu tarafı) Başlamak, başlık ekranında bir kayıt yuvasına tıklayarak oyuna girmek anlamına gelir. Oyuncu ve mevcut konum gibi oturum verileri henüz yüklenmemiştir.


#### OnStartGameClientside

```csharp
public static event Action OnStartGameClientside
```

Oyun başladığında çağrılır. (istemci tarafı) Başlamak, başlık ekranında bir kayıt yuvasına tıklayarak oyuna girmek anlamına gelir. Oyuncu ve mevcut konum gibi oturum verileri henüz yüklenmemiştir.


#### OnStartSessionServerside

```csharp
public static event Action<bool> OnStartSessionServerside
```

Bir oyun oturumu başladığında çağrılır. (sunucu tarafı) Oturum, bir oyun birimidir. Oyuncu öldüğünde o oturum biter ve yenisi başlar.


#### OnStartSessionClientside

```csharp
public static event Action<bool> OnStartSessionClientside
```

Bir oyun oturumu başladığında çağrılır. (istemci tarafı) Oturum, bir oyun birimidir. Oyuncu öldüğünde o oturum biter ve yenisi başlar.


#### OnLocalizationReady

```csharp
public static event Action<HorayModLocalizationContext> OnLocalizationReady
```

Yerelleştirme dosyaları yüklendikten sonra çağrılır.


#### OnFloorAllocatedServerside

```csharp
public static event Action<string, string, FloorGenerator> OnFloorAllocatedServerside
```

Bir kat tahsis edildiğinde çağrılır. (sunucu tarafı) Sephiria'daki her konum bir kat olarak tanımlanır. Bir oyuncu konumlar arasında hareket ettiğinde aşağıdaki adımlar sırayla çalışır. 1. Kat tahsisi: yeni katı oluşturur ve yükler. 2. Taşıma: oyuncuyu tamamen yüklenmiş kattaki istenen noktaya taşır. 3. Kat kaldırma: oyuncunun daha önce bulunduğu konumu, boşsa (= orada oyuncu kalmadıysa) kaldırır.
param1: Kat GUID'i  
param2: Kat Adı  
param3: FloorGenerator örneği


#### OnFloorAllocatedClientside

```csharp
public static event Action<string, string, FloorGenerator> OnFloorAllocatedClientside
```

Bir kat tahsis edildiğinde çağrılır. (istemci tarafı) Sephiria'daki her konum bir kat olarak tanımlanır. Bir oyuncu konumlar arasında hareket ettiğinde aşağıdaki adımlar sırayla çalışır. 1. Kat tahsisi: yeni katı oluşturur ve yükler. 2. Taşıma: oyuncuyu tamamen yüklenmiş kattaki istenen noktaya taşır. 3. Kat kaldırma: oyuncunun daha önce bulunduğu konumu, boşsa (= orada oyuncu kalmadıysa) kaldırır.
param1: Kat GUID'i  
param2: Kat Adı  
param3: FloorGenerator örneği


#### OnAssignRoomSpawnPhases

```csharp
public static event Action<RoomSpawnAssignContext> OnAssignRoomSpawnPhases
```

Bir sahnenin rastgele düşman bileşimi belirlendikten sonra çağrılır. (sunucu tarafı) Kat üreteci bir savaş odası (veya savaş bölgesi) için canavar evrelerini seçtikten hemen sonra, doğurucu bunları almadan önce tetiklenir. Orada neyin doğacağını değiştirmek için ctx.Phases'i değiştirin veya düzenleyin. Oyun burada denge zorlamaz; kurallar için RoomSpawnAssignContext'e bakın (ctx.Seed ile belirlenimci RNG, ctx.Sources'a asla dokunmayın, varlıklar UnitDatabase'de kayıtlı olmalı).


#### OnPlayerAvatarSpawnedServerside

```csharp
public static event Action<PlayerAvatar> OnPlayerAvatarSpawnedServerside
```

Bir oyuncu doğduğunda çağrılır. (sunucu tarafı) Bu kancanın içinde oyuncuyla ilgili geri çağrıları kaydedebilirsiniz. PlayerAvatar.OnStartServer'ın sonunda, temel nitelikler uygulandıktan sonra tetiklenir. Kostüm, kat ve envanter içeriği aynı karede daha sonra veya sonraki karelerde doldurulabilir. Avatarın kendi örnek olaylarına (OnDamagedServerside, OnAttackUnit, OnKillUnit, OnDie, OnAddedBuff, OnEnteredFloorServerside, ...) abone olmak için bunu kullanın. Avatar üzerindeki abonelikler avatarla birlikte sona erer; bu yüzden onlar için abonelikten çıkmak gerekmez.


#### OnPlayerAvatarSpawnedClientside

```csharp
public static event Action<PlayerAvatar> OnPlayerAvatarSpawnedClientside
```

Bir oyuncu avatarı doğduğunda çağrılır. (istemci tarafı) Her avatar için OnStartClient'ının sonunda bir kez tetiklenir; uzak oyuncular için de tetiklenir. Yerel oyuncuyu ayırt etmek için avatar.isLocalPlayer / avatar.isOwned değerlerini kontrol edin. SyncVar'lar zaten uygulanmıştır, ancak kat ve envanter içeriği daha sonra gelebilir.


#### OnUnitSpawnedServerside

```csharp
public static event Action<UnitAvatar> OnUnitSpawnedServerside
```

Sunucuda bir birim (oyuncu, düşman, çağrılan varlık veya yoldaş) doğduğunda tetiklenir. Yalnızca sunucu (sunucu sahibi).


#### OnUnitDespawnedServerside

```csharp
public static event Action<UnitAvatar> OnUnitDespawnedServerside
```

Bir birim sunucudan kaldırılmadan hemen önce tetiklenir. Yalnızca sunucu (sunucu sahibi).


#### OnPlayerEvade

```csharp
public static event Action<PlayerAvatar, DamageInstance> OnPlayerEvade
```

Bir oyuncu bir saldırıdan kaçındığında tetiklenir. (sunucu tarafı)


#### OnGetDebuff

```csharp
public static event Func<CharacterDebuff, UnitAvatar, CharacterDebuff> OnGetDebuff
```

Bir birime zayıflatma uygulanmadan hemen önce zayıflatma prefab'ını değiştirmenizi sağlar. İşleyici (mevcut prefab, uygulayan) alır ve yerine geçecek bir prefab döndürür; null döndürmek değişiklik yapmaz.


#### GridInventoryStartPermission

```csharp
public static event Action<GridInventory, PlayerAvatar> GridInventoryStartPermission
```

Sunucu envanter yazma iznini alıp tüm eşya etkilerini kaldırmaya başladığında tetiklenir.


#### GridInventoryEndPermission

```csharp
public static event Action<GridInventory, PlayerAvatar> GridInventoryEndPermission
```

Sunucu yazma iznini geri verip eşya etkilerini yeniden uyguladıktan hemen sonra tetiklenir.

### Yöntemler

#### RegisterNetworkPrefab

```csharp
public static bool RegisterNetworkPrefab(GameObject prefab)
```

Ağ üzerinden doğabilen bir nesneyi (prefab) kaydeder. Nesnenin bir NetworkIdentity bileşeni olmalıdır.

Sunucu (sunucu sahibi) ve istemcilerin her biri ağ kullanabilen nesnelerin bir listesini tutmalıdır ve bu liste herkeste aynı olmalıdır.

Örnek:

```csharp
HorayModAPI.OnAllDatabasesReady += () => {
    HorayModAPI.RegisterNetworkPrefab(myNewCustomPrefab);
};
```

Döndürür: kayıt başarılıysa true


#### RegisterNetworkPrefab

```csharp
public static bool RegisterNetworkPrefab(GameObject prefab, string stableKey)
```

Çalışma zamanında oluşturulan bir prefab'ı (yerleşik bir şablonun kopyası gibi), `stableKey` değerinden hesaplanan sabit bir assetId kullanarak ağ üzerinden doğabilen bir nesne olarak kaydeder. Anahtar olarak mod birim adını (`<modId>:<id>`) kullanın.

Döndürür: kayıt başarılıysa true


#### RegisterNetworkPrefabs

```csharp
public static int RegisterNetworkPrefabs(IEnumerable<GameObject> prefabs)
```

Birden fazla prefab'ı tek seferde ağ üzerinden doğabilen nesneler olarak kaydeder.

Döndürür: başarıyla kaydedilen prefab sayısı


#### UnregisterNetworkPrefab

```csharp
public static bool UnregisterNetworkPrefab(GameObject prefab)
```

Kayıtlı bir ağ prefab'ının kaydını siler.

Döndürür: kaldırıldıysa true


#### RegisterSpecialStatCategory

```csharp
public static bool RegisterSpecialStatCategory(SpecialStatCategoryEntity category)
```

Nitelik arayüzüne bir Özel Nitelikler kategorisi kaydeder.

Döndürür: kaydedildiyse true


#### RegisterSpecialStat

```csharp
public static bool RegisterSpecialStat(StatusEntity status, Func<SpecialStatContext, SpecialStatValue> reader = null)
```

Bir StatusEntity'yi nitelik arayüzündeki bir Özel Nitelikler kategorisine girdi olarak kaydeder. `reader` geçirirseniz değer, varlık ayarları yerine o işlev üzerinden okunur.

Döndürür: kaydedildiyse true


#### GetModOptions

```csharp
public static ModOptions GetModOptions(string modId)
```

modId'ye bağlı cihaz başına seçenekleri alır. Atölye modları bunun yerine `Context.Options` kullanabilir.


#### TryShowItemInteraction

```csharp
public static bool TryShowItemInteraction(ItemInteractionOptions options, out ItemInteractionSession session)
```

Envanterle birlikte kullanılan bir eşya etkileşim panelini açar. Arayüz meşgulse false döndürür.


#### TryGetUIWidgets

```csharp
public static bool TryGetUIWidgets(out UIWidgetFactory widgets)
```

Oyun stilinde arayüz widget'ları için bir fabrika alır. Bunları mevcut bir UI Canvas'ın altına iliştirin.


#### TryShowDialogue

```csharp
public static bool TryShowDialogue(Transform anchor, DialoguePage page, out DialogueSession session)
```

Bir dünya diyalog kutusu açar. Arayüz meşgulse false döndürür; sonraki sayfayı `session.Show` ile gösterin.


#### TryShowDialogue

```csharp
public static bool TryShowDialogue(Transform anchor, DialoguePage page, out DialogueSession session, Vector3? offset)
```

Dünya uzayında bir ofsetle diyalog kutusu açar. null ise prefab'ın varsayılan ofseti kullanılır.


#### TryShowDialogue

```csharp
public static bool TryShowDialogue(Transform anchor, DialoguePage page, out DialogueSession session, Vector3? offset = null, Vector2? cameraOffset = null, bool focusCamera = true)
```

Ayrı diyalog ve kamera ofsetleriyle bir diyalog kutusu açar. Kamera odağı yalnızca yerel olarak uygulanır.


## HorayModLocalizationContext

```csharp
public class HorayModLocalizationContext
```

Mod yerelleştirme metinlerini kaydetmek için bir yardımcı.

### Yöntemler

#### AddText

```csharp
public void AddText(string language, string key, string value)
```

Belirli bir dil için metin kaydeder.


#### AddText

```csharp
public void AddText(string key, Dictionary<string, string> texts)
```

Birden fazla dil için metni tek seferde kaydeder
texts: { "ko-KR" : "불의 검", "en-US" : "Fire Sword", ... }
