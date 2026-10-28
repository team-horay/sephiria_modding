<!-- ไฟล์ที่สร้างอัตโนมัติ Tools/ModApiDocGen ในที่เก็บของเกมสร้างจากคอมเมนต์ /// ในซอร์ส อย่าแก้ไขโดยตรง ให้แก้คอมเมนต์ในซอร์สแล้วสร้างใหม่ -->
# ค่าสถานะพิเศษ

ชนิดสำหรับการแสดงค่าสถานะที่ลงทะเบียนด้วย `RegisterSpecialStatCategory` และ `RegisterSpecialStat` ใน `HorayModAPI.OnLoadSpecialStatDatabase`

สร้างเมื่อ 2026-10-26 สำหรับเกมเวอร์ชัน **1.1.0** [ดัชนีอ้างอิง](README.md)

| ชนิด | คำอธิบาย |
|---|---|
| [`SpecialStatContext`](#specialstatcontext) | ข้อมูลอวตารเป้าหมายที่ส่งให้ฟังก์ชันอ่านค่า |
| [`SpecialStatValue`](#specialstatvalue) | ค่าของแถวค่าสถานะพิเศษหนึ่งแถว |
| [`SpecialStatCategoryEntity`](#specialstatcategoryentity) | ข้อมูลของหมวดหมู่ค่าสถานะพิเศษใน UI ค่าสถานะ |
| [`SpecialStatCategoryVisibility`](#specialstatcategoryvisibility) | ระบุว่าหมวดหมู่จะแสดงเมื่อใด |
| [`SpecialStatDisplaySettings`](#specialstatdisplaysettings) | ข้อมูลรายละเอียดสำหรับ `StatusEntity.specialStat` |
| [`SpecialStatValueSource`](#specialstatvaluesource) | ระบุว่าค่าแสดงผลของค่าสถานะพิเศษถูกอ่านจากที่ใด |
| [`SpecialStatLabelColor`](#specialstatlabelcolor) | สีของชื่อในแถวค่าสถานะพิเศษ |
| [`SpecialStatValueProvider`](#specialstatvalueprovider) | แอสเซตที่อ่านเฉพาะค่าแสดงผล โดยไม่ต้องมี StatusInstance |


## SpecialStatContext

```csharp
public sealed class SpecialStatContext
```

ข้อมูลอวตารเป้าหมายที่ส่งให้ฟังก์ชันอ่านค่า

### คุณสมบัติ

#### Avatar

```csharp
public UnitAvatar Avatar { get; }
```

ยูนิตเป้าหมาย


#### Hooker

```csharp
public AvatarStatsHooker Hooker { get; }
```

AvatarStatsHooker ของเป้าหมาย เป็น null หากไม่มี


#### Weapons

```csharp
public WeaponControllerSimple Weapons { get; }
```

ตัวควบคุมอาวุธของเป้าหมาย เป็น null หากไม่มี

### คอนสตรักเตอร์

#### SpecialStatContext

```csharp
public SpecialStatContext(UnitAvatar avatar, AvatarStatsHooker hooker = null, WeaponControllerSimple weapons = null)
```

หากละ hooker หรือ weapons ไว้ จะค้นหาจาก avatar


## SpecialStatValue

```csharp
public struct SpecialStatValue
```

ค่าของแถวค่าสถานะพิเศษหนึ่งแถว สร้างค่าตัวเลขด้วย `Number`

### ค่าคงที่และฟิลด์

#### text

```csharp
public string text
```

สตริงที่แสดง


#### hasStat

```csharp
public bool hasStat
```

มีค่าหรือไม่


#### sign

```csharp
public AvatarStatsHooker.EStatValueSign sign
```

สีเพิ่ม/ลด


#### hasNumericValue

```csharp
public bool hasNumericValue
```

`numericValue` ใช้ได้หรือไม่


#### numericValue

```csharp
public double numericValue
```

ตัวเลขสำหรับแสดงผล


#### numberFormat

```csharp
public string numberFormat
```

รูปแบบการแสดงตัวเลข


#### symbolKey

```csharp
public string symbolKey
```

คีย์แปลภาษาของสัญลักษณ์หน่วย


#### symbol

```csharp
public string symbol
```

สตริงสัญลักษณ์หน่วย


#### tooltipValues

```csharp
public Loc.KeywordValue[] tooltipValues
```

ค่าแทนที่คีย์เวิร์ดสำหรับทูลทิป

### เมธอด

#### Number

```csharp
public static SpecialStatValue Number(double number, bool hasStat, AvatarStatsHooker.EStatValueSign sign, string symbol = "", string format = "0.#", string symbolKey = "")
```

สร้างค่าตัวเลข


## SpecialStatCategoryEntity

```csharp
public class SpecialStatCategoryEntity : ScriptableObject
```

ข้อมูลของหมวดหมู่ค่าสถานะพิเศษใน UI ค่าสถานะ

### ค่าคงที่และฟิลด์

#### id

```csharp
public string id
```

ตัวระบุ


#### enabled

```csharp
public bool enabled
```

แสดงหมวดหมู่หรือไม่


#### displayName

```csharp
public LocalizedString displayName
```

คีย์แปลภาษาของชื่อเรื่อง


#### fallbackName

```csharp
public string fallbackName
```

ชื่อเรื่องที่ใช้เมื่อไม่มีคำแปล


#### headerColor

```csharp
public Color headerColor
```

สีของชื่อเรื่อง


#### order

```csharp
public int order
```

ลำดับการจัดเรียง


#### visibility

```csharp
public SpecialStatCategoryVisibility visibility
```

เงื่อนไขการแสดงผล


#### requireWeapon

```csharp
public bool requireWeapon
```

แสดงเฉพาะเมื่อสวมใส่อาวุธที่ระบุหรือไม่


#### weaponType

```csharp
public EWeaponType weaponType
```

ชนิดอาวุธที่ต้องมีเมื่อตั้งค่า `requireWeapon`

### คุณสมบัติ

#### DisplayName

```csharp
public string DisplayName { get; }
```

ชื่อเรื่องที่แปลแล้ว


## SpecialStatCategoryVisibility

```csharp
public enum SpecialStatCategoryVisibility
```

ระบุว่าหมวดหมู่จะแสดงเมื่อใด

| ค่า | คำอธิบาย |
|---|---|
| `Always` | เสมอ |
| `WhenAnyStatHasValue` | เฉพาะเมื่อมีอย่างน้อยหนึ่งแถวที่มีค่า |


## SpecialStatDisplaySettings

```csharp
public class SpecialStatDisplaySettings
```

ข้อมูลรายละเอียดสำหรับ `StatusEntity.specialStat` กำหนดวิธีแสดงค่าสถานะนี้ใน UI ค่าสถานะและ UI ประวัติการเล่น

### ค่าคงที่และฟิลด์

#### enabled

```csharp
public bool enabled
```

แสดงในแผงหรือไม่


#### category

```csharp
public SpecialStatCategoryEntity category
```

หมวดหมู่ที่สังกัด (จำเป็น)


#### order

```csharp
public int order
```

ลำดับการจัดเรียงภายในหมวดหมู่


#### showWhenZero

```csharp
public bool showWhenZero
```

แสดงแถวแม้ไม่มีค่าหรือไม่


#### labelOverride

```csharp
public LocalizedString labelOverride
```

คีย์แปลภาษาของชื่อที่ใช้เฉพาะในแผงค่าสถานะพิเศษ


#### fallbackLabel

```csharp
public string fallbackLabel
```

ชื่อที่ใช้เมื่อไม่มีคำแปล


#### labelColorMode

```csharp
public SpecialStatLabelColor labelColorMode
```

ตัวเลือกสีของชื่อ


#### labelColor

```csharp
public Color labelColor
```

สีของชื่อเมื่อเป็น `SpecialStatLabelColor.Override`


#### valueSource

```csharp
public SpecialStatValueSource valueSource
```

วิธีอ่านค่า


#### valueId

```csharp
public string valueId
```

ID สำหรับค้นหา StatHook หรือคีย์ค่าสถานะกำหนดเอง หากว่าง จะใช้ StatusEntity.id


#### valueProvider

```csharp
public SpecialStatValueProvider valueProvider
```

แอสเซตที่อ่านค่าเมื่อเป็น `SpecialStatValueSource.Provider`


#### divideForDisplay

```csharp
public float divideForDisplay
```

ตัวหารสำหรับการแสดงผล


#### displayOffset

```csharp
public float displayOffset
```

ออฟเซ็ตที่บวกเพิ่มสำหรับการแสดงผล


#### numberFormat

```csharp
public string numberFormat
```

รูปแบบการแสดงตัวเลข


#### includeSign

```csharp
public bool includeSign
```

แสดงเครื่องหมายหรือไม่


## SpecialStatValueSource

```csharp
public enum SpecialStatValueSource
```

ระบุว่าค่าแสดงผลของค่าสถานะพิเศษถูกอ่านจากที่ใด

| ค่า | คำอธิบาย |
|---|---|
| `StatHook` | ผลลัพธ์ของ `AvatarStatsHooker.HookStat(valueId)` |
| `CustomStat` | ค่าของ `UnitAvatar.GetCustomStat(valueId)` |
| `Provider` | ผลลัพธ์ `Read` ของแอสเซต `valueProvider` |


## SpecialStatLabelColor

```csharp
public enum SpecialStatLabelColor
```

สีของชื่อในแถวค่าสถานะพิเศษ

| ค่า | คำอธิบาย |
|---|---|
| `Default` | สีขาว |
| `Keyword` | สีของคีย์เวิร์ดที่เกี่ยวข้อง |
| `Override` | ค่าของ `labelColor` |


## SpecialStatValueProvider

```csharp
public abstract class SpecialStatValueProvider : ScriptableObject
```

แอสเซตที่อ่านเฉพาะค่าแสดงผล โดยไม่ต้องมี StatusInstance

### เมธอด

#### Read

```csharp
public abstract SpecialStatValue Read(SpecialStatContext context, StatusEntity status)
```

อ่านค่าแสดงผลปัจจุบันของ status
