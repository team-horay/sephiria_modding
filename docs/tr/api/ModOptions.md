<!-- Otomatik üretilen dosya. Oyun deposundaki Tools/ModApiDocGen, kaynaktaki /// yorumlarından oluşturur. Doğrudan düzenlemeyin; kaynak yorumlarını düzeltip yeniden üretin. -->
# Mod Seçenekleri

Cihaz başına kaydedilen mod ayarları ve oyunun seçenekler ekranına açma/kapama düğmeleri, seçim kutuları ve kaydırıcılar eklemek için API. `Context.Options` veya `HorayModAPI.GetModOptions(modId)` ile alın.

Oyun sürümü **1.1.0** için 2026-10-26 tarihinde üretildi. [Referans dizini](README.md)

| Tür | Açıklama |
|---|---|
| [`ModOptions`](#modoptions) | Her modun ayarlayabileceği seçenek değerleri. |
| [`ModOptionsPage`](#modoptionspage) | Seçenekler ekranında bir modun sayfası. |
| [`ModOptionDefinition`](#modoptiondefinition) | Bir seçenekler ekranı öğesinin ortak tanımı. |
| [`ModOption<T>`](#modoptiont) | Bir değer tutan seçenek öğesi. |
| [`ModToggleOption`](#modtoggleoption) | Açık/kapalı seçeneği. |
| [`ModSelectionOption`](#modselectionoption) | Seçim kutusu seçeneği. |
| [`ModSliderOption`](#modslideroption) | Kaydırıcı seçeneği. |
| [`ModOptionText`](#modoptiontext) |  |
| [`ModOptionKind`](#modoptionkind) | Seçenekler ekranındaki satır türü. |
| [`ModOptionsRegistry`](#modoptionsregistry) | Seçenekler ekranının okuduğu mod sayfalarının listesi. |


## ModOptions

```csharp
public sealed class ModOptions
```

Her modun ayarlayabileceği seçenek değerleri.

### Özellikler

#### ModId

```csharp
public string ModId { get; }
```

Bu seçenek kümesinin modId değeri


#### IsAvailable

```csharp
public bool IsAvailable { get; }
```

Depolamanın başlatılıp başlatılmadığı


#### DisplayName

```csharp
public ModOptionText DisplayName { get; set; }
```

Seçenekler ekranında gösterilen mod adı. Steam Atölyesi modları için varsayılan olarak bildirimdeki görünen addır.

### Yöntemler

#### GetBool

```csharp
public bool GetBool(string key, bool fallback = false)
```

Kaydedilmiş bool değeri. Yoksa veya türü farklıysa fallback döndürür.


#### GetInt

```csharp
public int GetInt(string key, int fallback = 0)
```

Kaydedilmiş int değeri. Yoksa veya türü farklıysa fallback döndürür.


#### GetLong

```csharp
public long GetLong(string key, long fallback = 0)
```

Kaydedilmiş long değeri. Yoksa veya türü farklıysa fallback döndürür.


#### GetFloat

```csharp
public float GetFloat(string key, float fallback = 0)
```

Kaydedilmiş float değeri. Yoksa veya türü farklıysa fallback döndürür.


#### GetString

```csharp
public string GetString(string key, string fallback = "")
```

Kaydedilmiş string değeri. Yoksa veya türü farklıysa fallback döndürür.


#### SetBool

```csharp
public void SetBool(string key, bool value)
```

Belleğe bir bool değeri yazar.


#### SetInt

```csharp
public void SetInt(string key, int value)
```

Belleğe bir int değeri yazar.


#### SetLong

```csharp
public void SetLong(string key, long value)
```

Belleğe bir long değeri yazar.


#### SetFloat

```csharp
public void SetFloat(string key, float value)
```

Belleğe bir float değeri yazar.


#### SetString

```csharp
public void SetString(string key, string value)
```

Belleğe bir string değeri yazar. null kabul etmez.


#### HasKey

```csharp
public bool HasKey(string key)
```

key altında kayıtlı bir değer olup olmadığı


#### Remove

```csharp
public bool Remove(string key)
```

Kaydedilmiş değeri kaldırır; sonraki okumalar fallback döndürür.


#### Reset

```csharp
public int Reset()
```

Bu modun tüm seçeneklerini temizler. Kaldırılan sayısını döndürür.


#### Save

```csharp
public bool Save()
```

Seçenekler dosyasını (DeviceOptions) kaydeder. Set*/Remove/Reset yalnızca belleği değiştirir; dosyaya yazmak için bunu çağırın. Depolama yoksa veya yazma başarısız olursa false döndürür.


#### AddToggle

```csharp
public ModToggleOption AddToggle(string key, bool defaultValue = false, ModOptionText label = default, Action<bool> onChanged = null)
```

Seçenekler ekranında bu modun sayfasına bir açma/kapama düğmesi ekler. Aynı anahtara sahip öğenin yerini alır.


#### AddSelectionBox

```csharp
public ModSelectionOption AddSelectionBox(string key, string[] choices, int defaultValue = 0, ModOptionText label = default, Action<int> onChanged = null)
```

Bir seçim kutusu ekler. Değer, choices içindeki bir dizindir.


#### AddSelectionBox

```csharp
public ModSelectionOption AddSelectionBox(string key, LocalizedString[] choices, int defaultValue = 0, ModOptionText label = default, Action<int> onChanged = null)
```

Seçenekleri yerelleştirme anahtarı olarak verilen bir seçim kutusu ekler.


#### AddSlider

```csharp
public ModSliderOption AddSlider(string key, float min = 0, float max = 1, float defaultValue = 0, float step = 0, ModOptionText label = default, Action<float> onChanged = null)
```

Bir kaydırıcı ekler. step 0 ise değer süreklidir.


#### UnregisterUI

```csharp
public void UnregisterUI()
```

Bu modun tüm seçenekler ekranı öğelerini kaldırır. Kaydedilmiş değerler korunur.


## ModOptionsPage

```csharp
public sealed class ModOptionsPage
```

Seçenekler ekranında bir modun sayfası. Yalnızca arayüz tanımlarını tutar; kaydedilmiş değerler ayrı saklanır.

### Özellikler

#### Options

```csharp
public ModOptions Options { get; }
```

Bu sayfanın mod seçenekleri


#### ModId

```csharp
public string ModId { get; }
```

`Options.ModId`


#### DisplayName

```csharp
public ModOptionText DisplayName { get; }
```

Seçenekler ekranında gösterilen mod adı


#### Items

```csharp
public IReadOnlyList<ModOptionDefinition> Items { get; }
```

Kayıt sırasına göre öğeler


## ModOptionDefinition

```csharp
public abstract class ModOptionDefinition
```

Bir seçenekler ekranı öğesinin ortak tanımı. `ModOptions.Add*` tarafından oluşturulur.

### Özellikler

#### Options

```csharp
public ModOptions Options { get; }
```

Bu öğenin ait olduğu mod seçenekleri


#### Key

```csharp
public string Key { get; }
```

Depolama anahtarı


#### Label

```csharp
public ModOptionText Label { get; }
```

Görünen ad. Boşsa anahtar gösterilir.


#### Kind

```csharp
public abstract ModOptionKind Kind { get; }
```

Satır türü

### Yapıcılar

#### ModOptionDefinition

```csharp
protected ModOptionDefinition(ModOptions options, string key, ModOptionText label)
```

key değerini doğrular.


## ModOption<T>

```csharp
public abstract class ModOption<T> : ModOptionDefinition
```

Bir değer tutan seçenek öğesi. Değeri koddan okuyup değiştirmek için tanıtıcıyı saklayın.

### Olaylar

#### ValueChanged

```csharp
public event Action<T> ValueChanged
```

Değer değiştikten sonra yeni değerle çağrılır.

### Özellikler

#### DefaultValue

```csharp
public T DefaultValue { get; }
```

Kayıt sırasında verilen varsayılan değer


#### Value

```csharp
public T Value { get; set; }
```

Geçerli değer. Ayarlamak değeri belleğe kaydeder ve değiştiyse `ValueChanged` tetikler.

### Yapıcılar

#### ModOption

```csharp
protected ModOption(ModOptions options, string key, T defaultValue, ModOptionText label, Action<T> onChanged)
```

onChanged, `ValueChanged` olayının ilk abonesi olur.

### Yöntemler

#### Read

```csharp
protected abstract T Read()
```

Değeri depolamadan okur.


#### Write

```csharp
protected abstract void Write(T value)
```

Değeri depolama belleğine yazar.


#### Normalize

```csharp
protected virtual T Normalize(T value)
```

Yazmadan önce değeri izin verilen aralığa sıkıştırır.


## ModToggleOption

```csharp
public sealed class ModToggleOption : ModOption<bool>
```

Açık/kapalı seçeneği.

### Özellikler

#### Kind

```csharp
public override ModOptionKind Kind { get; }
```

Her zaman Toggle

### Yöntemler

#### Read

```csharp
protected override bool Read()
```

bool değerini okur.


#### Write

```csharp
protected override void Write(bool value)
```

bool değerini yazar.


## ModSelectionOption

```csharp
public sealed class ModSelectionOption : ModOption<int>
```

Seçim kutusu seçeneği. Değer, `Choices` içindeki bir dizindir.

### Özellikler

#### Kind

```csharp
public override ModOptionKind Kind { get; }
```

Her zaman SelectionBox


#### Choices

```csharp
public IReadOnlyList<ModOptionText> Choices { get; }
```

Seçeneklerin listesi

### Yöntemler

#### Read

```csharp
protected override int Read()
```

Kaydedilmiş dizin. Aralık dışındaysa varsayılan değer.


#### Normalize

```csharp
protected override int Normalize(int value)
```

Seçeneklerin aralığına sıkıştırır.


#### Write

```csharp
protected override void Write(int value)
```

Dizini yazar.


## ModSliderOption

```csharp
public sealed class ModSliderOption : ModOption<float>
```

Kaydırıcı seçeneği.

### Özellikler

#### Kind

```csharp
public override ModOptionKind Kind { get; }
```

Her zaman Slider


#### Min

```csharp
public float Min { get; }
```

En küçük değer


#### Max

```csharp
public float Max { get; }
```

En büyük değer


#### Step

```csharp
public float Step { get; }
```

Adım boyutu. 0 ise sürekli.

### Yöntemler

#### Read

```csharp
protected override float Read()
```

Kaydedilmiş değer, normalleştirilmiş


#### Normalize

```csharp
protected override float Normalize(float value)
```

Aralığa sıkıştırır ve adıma hizalar.


#### Write

```csharp
protected override void Write(float value)
```

float değerini yazar.


## ModOptionText

```csharp
public readonly struct ModOptionText
```

*Henüz açıklama yok.*

### Yöntemler

#### implicit operator ModOptionText

```csharp
public static implicit operator ModOptionText(string text)
```

Dizeyi olduğu gibi gösterir.


#### implicit operator ModOptionText

```csharp
public static implicit operator ModOptionText(LocalizedString text)
```

LocalizedString'in anahtarını gösterim anında çevirir.


## ModOptionKind

```csharp
public enum ModOptionKind
```

Seçenekler ekranındaki satır türü.

| Değer | Açıklama |
|---|---|
| `Toggle` | Açık/kapalı |
| `SelectionBox` | Seçim kutusu |
| `Slider` | Kaydırıcı |


## ModOptionsRegistry

```csharp
public static class ModOptionsRegistry
```

Seçenekler ekranının okuduğu mod sayfalarının listesi.

### Yöntemler

#### GetPages

```csharp
public static IReadOnlyList<ModOptionsPage> GetPages()
```

Gösterilecek sayfaları modId'ye göre sıralanmış olarak döndürür.
