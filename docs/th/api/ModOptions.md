<!-- ไฟล์ที่สร้างอัตโนมัติ Tools/ModApiDocGen ในที่เก็บของเกมสร้างจากคอมเมนต์ /// ในซอร์ส อย่าแก้ไขโดยตรง ให้แก้คอมเมนต์ในซอร์สแล้วสร้างใหม่ -->
# ตัวเลือกม็อด

API สำหรับการตั้งค่าของแต่ละม็อดที่บันทึกต่ออุปกรณ์ และสำหรับเพิ่มสวิตช์ กล่องเลือก และแถบเลื่อนในหน้าจอตัวเลือกของเกม รับได้จาก `Context.Options` หรือ `HorayModAPI.GetModOptions(modId)`

สร้างเมื่อ 2026-10-26 สำหรับเกมเวอร์ชัน **1.1.0** [ดัชนีอ้างอิง](README.md)

| ชนิด | คำอธิบาย |
|---|---|
| [`ModOptions`](#modoptions) | ค่าตัวเลือกที่แต่ละม็อดตั้งได้ |
| [`ModOptionsPage`](#modoptionspage) | หน้าของม็อดหนึ่งตัวบนหน้าจอตัวเลือก |
| [`ModOptionDefinition`](#modoptiondefinition) | นิยามร่วมของรายการบนหน้าจอตัวเลือก |
| [`ModOption<T>`](#modoptiont) | รายการตัวเลือกที่เก็บค่า |
| [`ModToggleOption`](#modtoggleoption) | ตัวเลือกแบบเปิด/ปิด |
| [`ModSelectionOption`](#modselectionoption) | ตัวเลือกแบบกล่องเลือก |
| [`ModSliderOption`](#modslideroption) | ตัวเลือกแบบแถบเลื่อน |
| [`ModOptionText`](#modoptiontext) |  |
| [`ModOptionKind`](#modoptionkind) | ชนิดของแถวบนหน้าจอตัวเลือก |
| [`ModOptionsRegistry`](#modoptionsregistry) | รายการหน้าของม็อดที่หน้าจอตัวเลือกอ่าน |


## ModOptions

```csharp
public sealed class ModOptions
```

ค่าตัวเลือกที่แต่ละม็อดตั้งได้

### คุณสมบัติ

#### ModId

```csharp
public string ModId { get; }
```

modId ของชุดตัวเลือกนี้


#### IsAvailable

```csharp
public bool IsAvailable { get; }
```

ที่เก็บข้อมูลเริ่มต้นแล้วหรือไม่


#### DisplayName

```csharp
public ModOptionText DisplayName { get; set; }
```

ชื่อม็อดที่แสดงบนหน้าจอตัวเลือก สำหรับม็อดจาก Steam Workshop ค่าเริ่มต้นคือชื่อที่แสดงของ manifest

### เมธอด

#### GetBool

```csharp
public bool GetBool(string key, bool fallback = false)
```

ค่า bool ที่บันทึกไว้ คืนค่า fallback หากไม่มีหรือเป็นชนิดอื่น


#### GetInt

```csharp
public int GetInt(string key, int fallback = 0)
```

ค่า int ที่บันทึกไว้ คืนค่า fallback หากไม่มีหรือเป็นชนิดอื่น


#### GetLong

```csharp
public long GetLong(string key, long fallback = 0)
```

ค่า long ที่บันทึกไว้ คืนค่า fallback หากไม่มีหรือเป็นชนิดอื่น


#### GetFloat

```csharp
public float GetFloat(string key, float fallback = 0)
```

ค่า float ที่บันทึกไว้ คืนค่า fallback หากไม่มีหรือเป็นชนิดอื่น


#### GetString

```csharp
public string GetString(string key, string fallback = "")
```

ค่า string ที่บันทึกไว้ คืนค่า fallback หากไม่มีหรือเป็นชนิดอื่น


#### SetBool

```csharp
public void SetBool(string key, bool value)
```

เขียนค่า bool ลงหน่วยความจำ


#### SetInt

```csharp
public void SetInt(string key, int value)
```

เขียนค่า int ลงหน่วยความจำ


#### SetLong

```csharp
public void SetLong(string key, long value)
```

เขียนค่า long ลงหน่วยความจำ


#### SetFloat

```csharp
public void SetFloat(string key, float value)
```

เขียนค่า float ลงหน่วยความจำ


#### SetString

```csharp
public void SetString(string key, string value)
```

เขียนค่า string ลงหน่วยความจำ ไม่รับ null


#### HasKey

```csharp
public bool HasKey(string key)
```

มีค่าบันทึกไว้ภายใต้ key หรือไม่


#### Remove

```csharp
public bool Remove(string key)
```

นำค่าที่บันทึกไว้ออก เพื่อให้การอ่านครั้งต่อไปคืนค่า fallback


#### Reset

```csharp
public int Reset()
```

ล้างตัวเลือกทั้งหมดของม็อดนี้ คืนค่าจำนวนที่ถูกนำออก


#### Save

```csharp
public bool Save()
```

บันทึกไฟล์ตัวเลือก (DeviceOptions) Set*/Remove/Reset เปลี่ยนเฉพาะหน่วยความจำ จึงต้องเรียกเมธอดนี้เพื่อเขียนลงไฟล์ คืนค่า false หากไม่มีที่เก็บข้อมูลหรือการเขียนล้มเหลว


#### AddToggle

```csharp
public ModToggleOption AddToggle(string key, bool defaultValue = false, ModOptionText label = default, Action<bool> onChanged = null)
```

เพิ่มสวิตช์ในหน้าของม็อดนี้บนหน้าจอตัวเลือก แทนที่รายการใด ๆ ที่มี key เดียวกัน


#### AddSelectionBox

```csharp
public ModSelectionOption AddSelectionBox(string key, string[] choices, int defaultValue = 0, ModOptionText label = default, Action<int> onChanged = null)
```

เพิ่มกล่องเลือก ค่าคือดัชนีใน choices


#### AddSelectionBox

```csharp
public ModSelectionOption AddSelectionBox(string key, LocalizedString[] choices, int defaultValue = 0, ModOptionText label = default, Action<int> onChanged = null)
```

เพิ่มกล่องเลือกที่ตัวเลือกถูกกำหนดเป็นคีย์แปลภาษา


#### AddSlider

```csharp
public ModSliderOption AddSlider(string key, float min = 0, float max = 1, float defaultValue = 0, float step = 0, ModOptionText label = default, Action<float> onChanged = null)
```

เพิ่มแถบเลื่อน หาก step เป็น 0 ค่าจะต่อเนื่อง


#### UnregisterUI

```csharp
public void UnregisterUI()
```

นำรายการบนหน้าจอตัวเลือกทั้งหมดของม็อดนี้ออก ค่าที่บันทึกไว้ยังคงอยู่


## ModOptionsPage

```csharp
public sealed class ModOptionsPage
```

หน้าของม็อดหนึ่งตัวบนหน้าจอตัวเลือก เก็บเฉพาะนิยาม UI เท่านั้น ค่าที่บันทึกไว้ถูกเก็บแยกต่างหาก

### คุณสมบัติ

#### Options

```csharp
public ModOptions Options { get; }
```

ตัวเลือกม็อดของหน้านี้


#### ModId

```csharp
public string ModId { get; }
```

`Options.ModId`


#### DisplayName

```csharp
public ModOptionText DisplayName { get; }
```

ชื่อม็อดที่แสดงบนหน้าจอตัวเลือก


#### Items

```csharp
public IReadOnlyList<ModOptionDefinition> Items { get; }
```

รายการเรียงตามลำดับการลงทะเบียน


## ModOptionDefinition

```csharp
public abstract class ModOptionDefinition
```

นิยามร่วมของรายการบนหน้าจอตัวเลือก สร้างโดย `ModOptions.Add*`

### คุณสมบัติ

#### Options

```csharp
public ModOptions Options { get; }
```

ตัวเลือกม็อดที่รายการนี้สังกัด


#### Key

```csharp
public string Key { get; }
```

คีย์ที่ใช้เก็บข้อมูล


#### Label

```csharp
public ModOptionText Label { get; }
```

ชื่อที่แสดง หากว่าง จะแสดง key แทน


#### Kind

```csharp
public abstract ModOptionKind Kind { get; }
```

ชนิดของแถว

### คอนสตรักเตอร์

#### ModOptionDefinition

```csharp
protected ModOptionDefinition(ModOptions options, string key, ModOptionText label)
```

ตรวจสอบความถูกต้องของ key


## ModOption<T>

```csharp
public abstract class ModOption<T> : ModOptionDefinition
```

รายการตัวเลือกที่เก็บค่า เก็บ handle ไว้เพื่ออ่านและเปลี่ยนค่าจากโค้ด

### อีเวนต์

#### ValueChanged

```csharp
public event Action<T> ValueChanged
```

ถูกเรียกพร้อมค่าใหม่หลังค่าเปลี่ยนแปลง

### คุณสมบัติ

#### DefaultValue

```csharp
public T DefaultValue { get; }
```

ค่าเริ่มต้นที่กำหนดตอนลงทะเบียน


#### Value

```csharp
public T Value { get; set; }
```

ค่าปัจจุบัน การตั้งค่าจะเก็บค่าลงหน่วยความจำและเรียก `ValueChanged` หากค่าเปลี่ยนไป

### คอนสตรักเตอร์

#### ModOption

```csharp
protected ModOption(ModOptions options, string key, T defaultValue, ModOptionText label, Action<T> onChanged)
```

onChanged จะกลายเป็นผู้สมัครรับรายแรกของ `ValueChanged`

### เมธอด

#### Read

```csharp
protected abstract T Read()
```

อ่านค่าจากที่เก็บข้อมูล


#### Write

```csharp
protected abstract void Write(T value)
```

เขียนค่าลงหน่วยความจำของที่เก็บข้อมูล


#### Normalize

```csharp
protected virtual T Normalize(T value)
```

จำกัดค่าให้อยู่ในช่วงที่อนุญาตก่อนเขียน


## ModToggleOption

```csharp
public sealed class ModToggleOption : ModOption<bool>
```

ตัวเลือกแบบเปิด/ปิด

### คุณสมบัติ

#### Kind

```csharp
public override ModOptionKind Kind { get; }
```

เป็น Toggle เสมอ

### เมธอด

#### Read

```csharp
protected override bool Read()
```

อ่านค่า bool


#### Write

```csharp
protected override void Write(bool value)
```

เขียนค่า bool


## ModSelectionOption

```csharp
public sealed class ModSelectionOption : ModOption<int>
```

ตัวเลือกแบบกล่องเลือก ค่าคือดัชนีใน `Choices`

### คุณสมบัติ

#### Kind

```csharp
public override ModOptionKind Kind { get; }
```

เป็น SelectionBox เสมอ


#### Choices

```csharp
public IReadOnlyList<ModOptionText> Choices { get; }
```

รายการตัวเลือก

### เมธอด

#### Read

```csharp
protected override int Read()
```

ดัชนีที่บันทึกไว้ เป็นค่าเริ่มต้นหากอยู่นอกช่วง


#### Normalize

```csharp
protected override int Normalize(int value)
```

จำกัดให้อยู่ในช่วงของตัวเลือก


#### Write

```csharp
protected override void Write(int value)
```

เขียนดัชนี


## ModSliderOption

```csharp
public sealed class ModSliderOption : ModOption<float>
```

ตัวเลือกแบบแถบเลื่อน

### คุณสมบัติ

#### Kind

```csharp
public override ModOptionKind Kind { get; }
```

เป็น Slider เสมอ


#### Min

```csharp
public float Min { get; }
```

ค่าต่ำสุด


#### Max

```csharp
public float Max { get; }
```

ค่าสูงสุด


#### Step

```csharp
public float Step { get; }
```

ขนาดขั้น ต่อเนื่องหากเป็น 0

### เมธอด

#### Read

```csharp
protected override float Read()
```

ค่าที่บันทึกไว้ ผ่านการปรับมาตรฐานแล้ว


#### Normalize

```csharp
protected override float Normalize(float value)
```

จำกัดให้อยู่ในช่วงและปัดให้ตรงขั้น


#### Write

```csharp
protected override void Write(float value)
```

เขียนค่า float


## ModOptionText

```csharp
public readonly struct ModOptionText
```

*ยังไม่มีคำอธิบาย*

### เมธอด

#### implicit operator ModOptionText

```csharp
public static implicit operator ModOptionText(string text)
```

แสดงสตริงตามเดิม


#### implicit operator ModOptionText

```csharp
public static implicit operator ModOptionText(LocalizedString text)
```

แปลคีย์ของ LocalizedString ในเวลาที่แสดงผล


## ModOptionKind

```csharp
public enum ModOptionKind
```

ชนิดของแถวบนหน้าจอตัวเลือก

| ค่า | คำอธิบาย |
|---|---|
| `Toggle` | เปิด/ปิด |
| `SelectionBox` | กล่องเลือก |
| `Slider` | แถบเลื่อน |


## ModOptionsRegistry

```csharp
public static class ModOptionsRegistry
```

รายการหน้าของม็อดที่หน้าจอตัวเลือกอ่าน

### เมธอด

#### GetPages

```csharp
public static IReadOnlyList<ModOptionsPage> GetPages()
```

คืนค่าหน้าที่จะแสดง เรียงตาม modId
