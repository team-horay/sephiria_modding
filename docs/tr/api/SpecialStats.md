<!-- Otomatik üretilen dosya. Oyun deposundaki Tools/ModApiDocGen, kaynaktaki /// yorumlarından oluşturur. Doğrudan düzenlemeyin; kaynak yorumlarını düzeltip yeniden üretin. -->
# Özel Nitelikler

`HorayModAPI.OnLoadSpecialStatDatabase` içinde `RegisterSpecialStatCategory` ve `RegisterSpecialStat` ile kaydedilen nitelik gösterim türleri.

Oyun sürümü **1.1.0** için 2026-10-26 tarihinde üretildi. [Referans dizini](README.md)

| Tür | Açıklama |
|---|---|
| [`SpecialStatContext`](#specialstatcontext) | Değer okuma işlevlerine geçirilen hedef avatar bilgisi. |
| [`SpecialStatValue`](#specialstatvalue) | Bir özel nitelik satırının değeri. |
| [`SpecialStatCategoryEntity`](#specialstatcategoryentity) | Nitelik arayüzündeki bir Özel Nitelikler kategorisinin verisi. |
| [`SpecialStatCategoryVisibility`](#specialstatcategoryvisibility) | Bir kategorinin ne zaman gösterileceğini belirler. |
| [`SpecialStatDisplaySettings`](#specialstatdisplaysettings) | `StatusEntity.specialStat` için ayrıntılı veri. |
| [`SpecialStatValueSource`](#specialstatvaluesource) | Bir özel niteliğin gösterim değerinin nereden okunacağını belirler. |
| [`SpecialStatLabelColor`](#specialstatlabelcolor) | Bir özel nitelik satırının adının rengi. |
| [`SpecialStatValueProvider`](#specialstatvalueprovider) | StatusInstance olmadan yalnızca gösterim değerini okuyan bir varlık. |


## SpecialStatContext

```csharp
public sealed class SpecialStatContext
```

Değer okuma işlevlerine geçirilen hedef avatar bilgisi.

### Özellikler

#### Avatar

```csharp
public UnitAvatar Avatar { get; }
```

Hedef birim


#### Hooker

```csharp
public AvatarStatsHooker Hooker { get; }
```

Hedefin AvatarStatsHooker'ı. Yoksa null.


#### Weapons

```csharp
public WeaponControllerSimple Weapons { get; }
```

Hedefin silah denetleyicisi. Yoksa null.

### Yapıcılar

#### SpecialStatContext

```csharp
public SpecialStatContext(UnitAvatar avatar, AvatarStatsHooker hooker = null, WeaponControllerSimple weapons = null)
```

hooker veya weapons atlanırsa avatar üzerinden bulunur.


## SpecialStatValue

```csharp
public struct SpecialStatValue
```

Bir özel nitelik satırının değeri. Sayısal değerleri `Number` ile oluşturun.

### Sabitler ve Alanlar

#### text

```csharp
public string text
```

Gösterim dizesi


#### hasStat

```csharp
public bool hasStat
```

Bir değer olup olmadığı


#### sign

```csharp
public AvatarStatsHooker.EStatValueSign sign
```

Artış/azalış rengi


#### hasNumericValue

```csharp
public bool hasNumericValue
```

`numericValue` değerinin geçerli olup olmadığı


#### numericValue

```csharp
public double numericValue
```

Gösterilecek sayı


#### numberFormat

```csharp
public string numberFormat
```

Sayı gösterim biçimi


#### symbolKey

```csharp
public string symbolKey
```

Birim simgesinin yerelleştirme anahtarı


#### symbol

```csharp
public string symbol
```

Birim simgesi dizesi


#### tooltipValues

```csharp
public Loc.KeywordValue[] tooltipValues
```

İpucu için anahtar sözcük değiştirme değerleri

### Yöntemler

#### Number

```csharp
public static SpecialStatValue Number(double number, bool hasStat, AvatarStatsHooker.EStatValueSign sign, string symbol = "", string format = "0.#", string symbolKey = "")
```

Sayısal bir değer oluşturur.


## SpecialStatCategoryEntity

```csharp
public class SpecialStatCategoryEntity : ScriptableObject
```

Nitelik arayüzündeki bir Özel Nitelikler kategorisinin verisi.

### Sabitler ve Alanlar

#### id

```csharp
public string id
```

Tanımlayıcı


#### enabled

```csharp
public bool enabled
```

Kategorinin gösterilip gösterilmeyeceği


#### displayName

```csharp
public LocalizedString displayName
```

Başlığın yerelleştirme anahtarı


#### fallbackName

```csharp
public string fallbackName
```

Çeviri yokken kullanılan başlık


#### headerColor

```csharp
public Color headerColor
```

Başlık rengi


#### order

```csharp
public int order
```

Sıralama düzeni


#### visibility

```csharp
public SpecialStatCategoryVisibility visibility
```

Gösterim koşulu


#### requireWeapon

```csharp
public bool requireWeapon
```

Yalnızca belirli bir silah kuşanıldığında gösterilip gösterilmeyeceği


#### weaponType

```csharp
public EWeaponType weaponType
```

`requireWeapon` ayarlandığında gereken silah türü

### Özellikler

#### DisplayName

```csharp
public string DisplayName { get; }
```

Çevrilmiş başlık


## SpecialStatCategoryVisibility

```csharp
public enum SpecialStatCategoryVisibility
```

Bir kategorinin ne zaman gösterileceğini belirler.

| Değer | Açıklama |
|---|---|
| `Always` | Her zaman |
| `WhenAnyStatHasValue` | Yalnızca en az bir satırın değeri olduğunda |


## SpecialStatDisplaySettings

```csharp
public class SpecialStatDisplaySettings
```

`StatusEntity.specialStat` için ayrıntılı veri. Bu niteliğin nitelik arayüzünde ve oyun geçmişi arayüzünde nasıl gösterileceğini belirler.

### Sabitler ve Alanlar

#### enabled

```csharp
public bool enabled
```

Panelde gösterilip gösterilmeyeceği


#### category

```csharp
public SpecialStatCategoryEntity category
```

Ait olduğu kategori (zorunlu)


#### order

```csharp
public int order
```

Kategori içindeki sıralama düzeni


#### showWhenZero

```csharp
public bool showWhenZero
```

Değer olmadığında bile satırın gösterilip gösterilmeyeceği


#### labelOverride

```csharp
public LocalizedString labelOverride
```

Yalnızca Özel Nitelikler panelinde kullanılan bir adın yerelleştirme anahtarı


#### fallbackLabel

```csharp
public string fallbackLabel
```

Çeviri yokken kullanılan ad


#### labelColorMode

```csharp
public SpecialStatLabelColor labelColorMode
```

Ad rengi seçimi


#### labelColor

```csharp
public Color labelColor
```

`SpecialStatLabelColor.Override` olduğunda ad rengi


#### valueSource

```csharp
public SpecialStatValueSource valueSource
```

Değerin nasıl okunacağı


#### valueId

```csharp
public string valueId
```

StatHook arama kimliği veya özel nitelik anahtarı. Boşsa StatusEntity.id kullanılır.


#### valueProvider

```csharp
public SpecialStatValueProvider valueProvider
```

`SpecialStatValueSource.Provider` olduğunda değeri okuyan varlık


#### divideForDisplay

```csharp
public float divideForDisplay
```

Gösterim için bölen


#### displayOffset

```csharp
public float displayOffset
```

Gösterim için eklenen ofset


#### numberFormat

```csharp
public string numberFormat
```

Sayı gösterim biçimi


#### includeSign

```csharp
public bool includeSign
```

İşaretin gösterilip gösterilmeyeceği


## SpecialStatValueSource

```csharp
public enum SpecialStatValueSource
```

Bir özel niteliğin gösterim değerinin nereden okunacağını belirler.

| Değer | Açıklama |
|---|---|
| `StatHook` | `AvatarStatsHooker.HookStat(valueId)` sonucu |
| `CustomStat` | `UnitAvatar.GetCustomStat(valueId)` değeri |
| `Provider` | `valueProvider` varlığının `Read` sonucu |


## SpecialStatLabelColor

```csharp
public enum SpecialStatLabelColor
```

Bir özel nitelik satırının adının rengi.

| Değer | Açıklama |
|---|---|
| `Default` | Beyaz |
| `Keyword` | İlgili anahtar sözcüğün rengi |
| `Override` | `labelColor` değeri |


## SpecialStatValueProvider

```csharp
public abstract class SpecialStatValueProvider : ScriptableObject
```

StatusInstance olmadan yalnızca gösterim değerini okuyan bir varlık.

### Yöntemler

#### Read

```csharp
public abstract SpecialStatValue Read(SpecialStatContext context, StatusEntity status)
```

status'un geçerli gösterim değerini okur.
