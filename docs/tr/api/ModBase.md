<!-- Otomatik üretilen dosya. Oyun deposundaki Tools/ModApiDocGen, kaynaktaki /// yorumlarından oluşturur. Doğrudan düzenlemeyin; kaynak yorumlarını düzeltip yeniden üretin. -->
# Giriş Noktası ve Bağlam

Bir modun giriş sınıfının devraldığı temel sınıflar ile yükleyicinin iliştirdiği bağlam ve bildirim türleri. Yeni modlar `WorkshopModBase` sınıfından türemelidir.

Oyun sürümü **1.1.0** için 2026-10-26 tarihinde üretildi. [Referans dizini](README.md)

| Tür | Açıklama |
|---|---|
| [`WorkshopModBase`](#workshopmodbase) | Atölye modları için giriş temeli. |
| [`WorkshopModContext`](#workshopmodcontext) | Bir Atölye DLL modülüne verilen salt okunur bağlam. |
| [`HorayModBase`](#horaymodbase) | DLL modu giriş sınıfları için temel. |
| [`WorkshopDataPackage`](#workshopdatapackage) | Tüketici bir modun okuyacağı bir veri modülü paketi. |
| [`WorkshopModDefinition`](#workshopmoddefinition) | Doğrulanmış bir yürütme tanımı. |
| [`WorkshopModuleDefinition`](#workshopmoduledefinition) | Bildirimin `modules[]` listesindeki bir girdi. |
| [`WorkshopDependency`](#workshopdependency) | Bildirimin `dependencies[]` listesindeki bir girdi. |
| [`WorkshopModuleType`](#workshopmoduletype) | Bir bildirim `modules[].type` değeri. |
| [`WorkshopInstallation`](#workshopinstallation) | Diğer katılımcıların modu kurması gerekip gerekmediğine dair yazarın bildirimi (bildirim `multiplayer.installation`). |


## WorkshopModBase

```csharp
public abstract class WorkshopModBase : HorayModBase
```

Atölye modları için giriş temeli.

### Özellikler

#### Context

```csharp
public WorkshopModContext Context { get; }
```

OnModLoaded çağrıldığı andan itibaren geçerlidir.

### Yöntemler

#### GetDataPackages

```csharp
protected IReadOnlyList<WorkshopDataPackage> GetDataPackages()
```

Tüketici olarak bu modu belirten veri modülü paketleri, yükleme sırasına göre. Etkinleştirme bittikten sonra (veritabanı olaylarından sonra) çağırın. Bağlam yoksa (örn. AddOns yolu) boş bir liste döndürür.


## WorkshopModContext

```csharp
public sealed class WorkshopModContext
```

Bir Atölye DLL modülüne verilen salt okunur bağlam. Kurulum yolu değişebildiğinden modlar kendi dosyalarını bulmak için bu değerleri kullanır.

### Özellikler

#### PackageRoot

```csharp
public string PackageRoot { get; }
```

Paket kökünün (kurulum klasörü) mutlak yolu.


#### FileId

```csharp
public ulong FileId { get; }
```

Steam Atölyesi dosya kimliği. Yerel paketler, yollarından türetilen benzersiz bir kimlik alır.


#### Definition

```csharp
public WorkshopModDefinition Definition { get; }
```

Doğrulanmış yürütme tanımı.


#### Module

```csharp
public WorkshopModuleDefinition Module { get; }
```

Bu örneği oluşturan dll modülü girdisi.


#### Options

```csharp
public ModOptions Options { get; }
```

Bu modId için cihaz başına seçenekler. Paketin DLL modülleri ile yerel ve abone olunan kopyaları tarafından paylaşılır.

### Yapıcılar

#### WorkshopModContext

```csharp
public WorkshopModContext(string packageRoot, ulong fileId, WorkshopModDefinition definition, WorkshopModuleDefinition module)
```

*Henüz açıklama yok.*

### Yöntemler

#### ResolvePath

```csharp
public string ResolvePath(string relativePath)
```

Paket köküne göreli bir yolu mutlak yola dönüştürür. Kökün dışına çıkan yollar veya mutlak yollar reddedilir (istisna fırlatır).


## HorayModBase

```csharp
public class HorayModBase
```

DLL modu giriş sınıfları için temel.

### Sabitler ve Alanlar

#### metadata

```csharp
public Metadata metadata
```

`LoadMod` içinde alınan meta veri.

### Yöntemler

#### LoadMod

```csharp
public void LoadMod(Metadata metadata)
```

Yükleyici tarafından çağrılır. Meta veriyi saklar, ardından `OnModLoaded` çağırır.


#### OnModLoaded

```csharp
protected virtual void OnModLoaded()
```

Modun başlatma noktası. Veritabanları başlatılmadan önce çağrılır; bu yüzden `HorayModAPI` olaylarına burada abone olun.


#### UnloadMod

```csharp
public void UnloadMod()
```

`OnModUnloaded` çağırır.


#### OnModUnloaded

```csharp
protected virtual void OnModUnloaded()
```

Modun kaldırma noktası. Olaylardan abonelikten çıkma gibi temizlik işlerini buraya koyun.

### İç İçe Türler

#### HorayModBase.Metadata

```csharp
public struct Metadata
```

Yükleyicinin `LoadMod` içine geçirdiği mod tanımlama bilgisi.

##### Sabitler ve Alanlar

###### modName

```csharp
public string modName
```

Görünen ad.


###### modVersion

```csharp
public string modVersion
```

Sürüm.


###### modAuthor

```csharp
public string modAuthor
```

Yazar.


## WorkshopDataPackage

```csharp
public sealed class WorkshopDataPackage
```

Tüketici bir modun okuyacağı bir veri modülü paketi. `WorkshopModBase.GetDataPackages()` ile alın.

### Özellikler

#### ModId

```csharp
public string ModId { get; }
```

Paketin modId değeri.


#### Version

```csharp
public string Version { get; }
```

Paketin sürümü.


#### FileId

```csharp
public ulong FileId { get; }
```

Steam Atölyesi dosya kimliği.


#### DisplayName

```csharp
public string DisplayName { get; }
```

Görünen ad (info.title, yoksa modId).


#### PackageRoot

```csharp
public string PackageRoot { get; }
```

Paket kökünün mutlak yolu.


#### Modules

```csharp
public IReadOnlyList<WorkshopModuleDefinition> Modules { get; }
```

Bu tüketiciyi hedefleyen veri modülleri, bildirim sırasına göre.


#### Folders

```csharp
public IReadOnlyList<string> Folders { get; }
```

Modül klasörlerinin mutlak yolları, bildirim sırasına göre. Modules[i].FullPath ile aynıdır.

### Yöntemler

#### Collect

```csharp
public static IReadOnlyList<WorkshopDataPackage> Collect(string consumerModId, IEnumerable<WorkshopModActivation> activations)
```

Etkinleştirme kayıtlarından (yükleme sırasına göre), `consumerModId` için veri modülü olan paketleri seçer. Yalnızca bağımlılıkların karşılandığı durumları (Active, AlreadyActive, NoDllModules) içerir. Başarısız olan veya kısmen uygulanan paketlerin verisi aktarılmaz.


## WorkshopModDefinition

```csharp
public sealed class WorkshopModDefinition
```

Doğrulanmış bir yürütme tanımı. Değişmezdir.

### Özellikler

#### PackageRoot

```csharp
public string PackageRoot { get; }
```

Paket kökünün mutlak yolu.


#### ModId

```csharp
public string ModId { get; }
```

Bildirimdeki modId.


#### Version

```csharp
public string Version { get; }
```

Bildirimdeki sürüm.


#### Author

```csharp
public string Author { get; }
```

Bildirimdeki yazar. Yoksa null.


#### DisplayName

```csharp
public string DisplayName { get; }
```

info.title, yoksa modId.


#### Installation

```csharp
public WorkshopInstallation Installation { get; }
```

Çok oyunculu kurulum gereksinimi.


#### MultiplayerDeclared

```csharp
public bool MultiplayerDeclared { get; }
```

multiplayer nesnesinin açıkça bildirilip bildirilmediği. false ise Installation normalleştirilmiş varsayılanı tutar.


#### Modules

```csharp
public IReadOnlyList<WorkshopModuleDefinition> Modules { get; }
```

Modül listesi, bildirim sırasına göre.


#### LibraryPaths

```csharp
public IReadOnlyList<string> LibraryPaths { get; }
```

Yardımcı yönetilen DLL'lerin mutlak yolları. Bunlar başlangıç geri çağrılarını almaz.


#### Dependencies

```csharp
public IReadOnlyList<WorkshopDependency> Dependencies { get; }
```

Bu modun bağımlı olduğu modların listesi.


#### HasDll

```csharp
public bool HasDll { get; }
```

modules içinde dll türü olup olmadığından türetilir. Ayrı bir bayrak yoktur.


#### HasLocalization

```csharp
public bool HasLocalization { get; }
```

modules içinde localization türü olup olmadığı.


#### HasCostume

```csharp
public bool HasCostume { get; }
```

modules içinde costume türü olup olmadığı.


#### HasData

```csharp
public bool HasData { get; }
```

modules içinde data türü olup olmadığı.

### Yapıcılar

#### WorkshopModDefinition

```csharp
public WorkshopModDefinition(string packageRoot, string modId, string version, string author, string displayName, WorkshopInstallation installation, bool multiplayerDeclared, IReadOnlyList<WorkshopModuleDefinition> modules, IReadOnlyList<string> libraryPaths, IReadOnlyList<WorkshopDependency> dependencies)
```

*Henüz açıklama yok.*

### Yöntemler

#### DataModulesFor

```csharp
public IReadOnlyList<WorkshopModuleDefinition> DataModulesFor(string consumerModId)
```

`consumerModId`'nin aldığı veri modülleri, bildirim sırasına göre. Yoksa boş liste. Bir tüketici belirten modül yalnızca o moda gider; belirtmeyen modül ise bu paketin kendisine ve ona doğrudan bağımlı her moda gider.


## WorkshopModuleDefinition

```csharp
public sealed class WorkshopModuleDefinition
```

Bildirimin `modules[]` listesindeki bir girdi.

### Özellikler

#### Type

```csharp
public WorkshopModuleType Type { get; }
```

Modül türü.


#### RelativePath

```csharp
public string RelativePath { get; }
```

Paket köküne göreli yol (ayırıcılar '/' olarak normalleştirilmiştir).


#### FullPath

```csharp
public string FullPath { get; }
```

Doğrulanmış mutlak yol.


#### EntryClass

```csharp
public string EntryClass { get; }
```

dll modülünün giriş sınıfının tam adı. localization için null.


#### Consumer

```csharp
public string Consumer { get; }
```

Veri modülünü okuyan modun modId değeri. Atlanırsa (null), paketin kendisi ve doğrudan bağımlı her moddur. Veri olmayan modüller için her zaman null.

### Yapıcılar

#### WorkshopModuleDefinition

```csharp
public WorkshopModuleDefinition(WorkshopModuleType type, string relativePath, string fullPath, string entryClass)
```

Doğrulayıcı tarafından oluşturulur.


#### WorkshopModuleDefinition

```csharp
public WorkshopModuleDefinition(WorkshopModuleType type, string relativePath, string fullPath, string entryClass, string consumer)
```

Doğrulayıcı tarafından oluşturulur.


## WorkshopDependency

```csharp
public sealed class WorkshopDependency
```

Bildirimin `dependencies[]` listesindeki bir girdi.

### Özellikler

#### ModId

```csharp
public string ModId { get; }
```

Bağımlı olunan modun modId değeri.


#### Version

```csharp
public string Version { get; }
```

null herhangi bir sürüm anlamına gelir. Ayarlanmışsa yalnızca birebir eşleşme kabul edilir (v1).


#### WorkshopId

```csharp
public ulong? WorkshopId { get; }
```

Nereden indirileceğine dair bir ipucu. Kurulumdan sonra gerçek modId/sürüm ile karşılaştırılmalıdır; tek başına indirilen modun doğru olduğunu kanıtlamaz.

### Yapıcılar

#### WorkshopDependency

```csharp
public WorkshopDependency(string modId, string version, ulong? workshopId)
```

Doğrulayıcı tarafından oluşturulur.


## WorkshopModuleType

```csharp
public enum WorkshopModuleType
```

Bir bildirim `modules[].type` değeri.

| Değer | Açıklama |
|---|---|
| `Dll` | Yönetilen DLL modülü |
| `Localization` | Yerelleştirme klasörü modülü |
| `Costume` | Kod içermeyen kostüm paketi klasörü (costume.json/skin.json + PNG) |
| `Data` | Başka bir modun (tüketicinin) okuduğu genel amaçlı veri klasörü; oyun içeriğini yorumlamaz |


## WorkshopInstallation

```csharp
public enum WorkshopInstallation
```

Diğer katılımcıların modu kurması gerekip gerekmediğine dair yazarın bildirimi (bildirim `multiplayer.installation`).

| Değer | Açıklama |
|---|---|
| `Unknown` | Bildirilmemiş (lobi kurulum karşılaştırmasına Everyone gibi dahil edilir) |
| `LocalOnly` | Yalnızca kullanıcının kendi cihazında çalışır; lobi kurulum karşılaştırmasının dışında tutulur |
| `Everyone` | Odadaki herkes aynı paketi kurmalıdır |
| `HostOnly` | Yalnızca sunucu sahibinin kurması gerekir |
