<!-- ไฟล์ที่สร้างอัตโนมัติ Tools/ModApiDocGen ในที่เก็บของเกมสร้างจากคอมเมนต์ /// ในซอร์ส อย่าแก้ไขโดยตรง ให้แก้คอมเมนต์ในซอร์สแล้วสร้างใหม่ -->
# จุดเริ่มต้นและคอนเท็กซ์

คลาสฐานที่คลาสจุดเริ่มต้นของม็อดสืบทอด รวมถึงชนิดคอนเท็กซ์และ manifest ที่โหลดเดอร์แนบมา ม็อดใหม่ควรสืบทอดจาก `WorkshopModBase`

สร้างเมื่อ 2026-10-26 สำหรับเกมเวอร์ชัน **1.1.0** [ดัชนีอ้างอิง](README.md)

| ชนิด | คำอธิบาย |
|---|---|
| [`WorkshopModBase`](#workshopmodbase) | คลาสฐานจุดเริ่มต้นสำหรับม็อด Workshop |
| [`WorkshopModContext`](#workshopmodcontext) | คอนเท็กซ์แบบอ่านอย่างเดียวที่มอบให้โมดูล DLL ของ Workshop |
| [`HorayModBase`](#horaymodbase) | คลาสฐานสำหรับคลาสจุดเริ่มต้นของม็อด DLL |
| [`WorkshopDataPackage`](#workshopdatapackage) | แพ็กเกจโมดูลข้อมูลหนึ่งชุดสำหรับให้ม็อดผู้บริโภค (consumer) อ่าน |
| [`WorkshopModDefinition`](#workshopmoddefinition) | ข้อกำหนดการรันที่ผ่านการตรวจสอบแล้ว |
| [`WorkshopModuleDefinition`](#workshopmoduledefinition) | หนึ่งรายการใน `modules[]` ของ manifest |
| [`WorkshopDependency`](#workshopdependency) | หนึ่งรายการใน `dependencies[]` ของ manifest |
| [`WorkshopModuleType`](#workshopmoduletype) | ค่า `modules[].type` ของ manifest |
| [`WorkshopInstallation`](#workshopinstallation) | การประกาศของผู้สร้างว่าผู้เข้าร่วมคนอื่นต้องติดตั้งม็อดหรือไม่ (`multiplayer.installation` ของ manifest) |


## WorkshopModBase

```csharp
public abstract class WorkshopModBase : HorayModBase
```

คลาสฐานจุดเริ่มต้นสำหรับม็อด Workshop

### คุณสมบัติ

#### Context

```csharp
public WorkshopModContext Context { get; }
```

ใช้ได้ตั้งแต่ช่วงที่ OnModLoaded ถูกเรียก

### เมธอด

#### GetDataPackages

```csharp
protected IReadOnlyList<WorkshopDataPackage> GetDataPackages()
```

แพ็กเกจโมดูลข้อมูลที่ระบุม็อดนี้เป็น consumer เรียงตามลำดับการโหลด ให้เรียกหลังจากการเปิดใช้งานเสร็จสิ้น (หลังอีเวนต์ฐานข้อมูล) คืนค่ารายการว่างหากไม่มีคอนเท็กซ์ (เช่น เส้นทาง AddOns)


## WorkshopModContext

```csharp
public sealed class WorkshopModContext
```

คอนเท็กซ์แบบอ่านอย่างเดียวที่มอบให้โมดูล DLL ของ Workshop เส้นทางติดตั้งอาจแตกต่างกันได้ ม็อดจึงใช้ค่าเหล่านี้เพื่อค้นหาไฟล์ของตัวเอง

### คุณสมบัติ

#### PackageRoot

```csharp
public string PackageRoot { get; }
```

เส้นทางสัมบูรณ์ของรากแพ็กเกจ (โฟลเดอร์ติดตั้ง)


#### FileId

```csharp
public ulong FileId { get; }
```

ID ไฟล์ของ Steam Workshop แพ็กเกจในเครื่องจะได้รับ ID เฉพาะที่สร้างจากเส้นทางของมัน


#### Definition

```csharp
public WorkshopModDefinition Definition { get; }
```

ข้อกำหนดการรันที่ผ่านการตรวจสอบแล้ว


#### Module

```csharp
public WorkshopModuleDefinition Module { get; }
```

รายการโมดูล dll ที่สร้างอินสแตนซ์นี้


#### Options

```csharp
public ModOptions Options { get; }
```

ตัวเลือกต่ออุปกรณ์สำหรับ modId นี้ ใช้ร่วมกันระหว่างโมดูล DLL ของแพ็กเกจ และระหว่างสำเนาในเครื่องกับสำเนาที่สมัครรับ

### คอนสตรักเตอร์

#### WorkshopModContext

```csharp
public WorkshopModContext(string packageRoot, ulong fileId, WorkshopModDefinition definition, WorkshopModuleDefinition module)
```

*ยังไม่มีคำอธิบาย*

### เมธอด

#### ResolvePath

```csharp
public string ResolvePath(string relativePath)
```

แปลงเส้นทางสัมพัทธ์จากรากแพ็กเกจเป็นเส้นทางสัมบูรณ์ เส้นทางที่หลุดออกนอกราก หรือเส้นทางสัมบูรณ์ จะถูกปฏิเสธ (โยนข้อยกเว้น)


## HorayModBase

```csharp
public class HorayModBase
```

คลาสฐานสำหรับคลาสจุดเริ่มต้นของม็อด DLL

### ค่าคงที่และฟิลด์

#### metadata

```csharp
public Metadata metadata
```

metadata ที่ได้รับใน `LoadMod`

### เมธอด

#### LoadMod

```csharp
public void LoadMod(Metadata metadata)
```

ถูกเรียกโดยโหลดเดอร์ เก็บ metadata แล้วเรียก `OnModLoaded`


#### OnModLoaded

```csharp
protected virtual void OnModLoaded()
```

จุดเริ่มต้นการทำงานของม็อด ถูกเรียกก่อนฐานข้อมูลเริ่มต้น ดังนั้นให้สมัครรับอีเวนต์ของ `HorayModAPI` ที่นี่


#### UnloadMod

```csharp
public void UnloadMod()
```

เรียก `OnModUnloaded`


#### OnModUnloaded

```csharp
protected virtual void OnModUnloaded()
```

จุดยกเลิกการโหลดของม็อด ใส่การล้างข้อมูล เช่น การยกเลิกการสมัครรับอีเวนต์ ไว้ที่นี่

### ชนิดซ้อน

#### HorayModBase.Metadata

```csharp
public struct Metadata
```

ข้อมูลระบุตัวตนของม็อดที่โหลดเดอร์ส่งให้ `LoadMod`

##### ค่าคงที่และฟิลด์

###### modName

```csharp
public string modName
```

ชื่อที่แสดง


###### modVersion

```csharp
public string modVersion
```

เวอร์ชัน


###### modAuthor

```csharp
public string modAuthor
```

ผู้สร้าง


## WorkshopDataPackage

```csharp
public sealed class WorkshopDataPackage
```

แพ็กเกจโมดูลข้อมูลหนึ่งชุดสำหรับให้ม็อดผู้บริโภค (consumer) อ่าน รับได้ด้วย `WorkshopModBase.GetDataPackages()`

### คุณสมบัติ

#### ModId

```csharp
public string ModId { get; }
```

modId ของแพ็กเกจ


#### Version

```csharp
public string Version { get; }
```

เวอร์ชันของแพ็กเกจ


#### FileId

```csharp
public ulong FileId { get; }
```

ID ไฟล์ของ Steam Workshop


#### DisplayName

```csharp
public string DisplayName { get; }
```

ชื่อที่แสดง (info.title หรือ modId หากไม่มี)


#### PackageRoot

```csharp
public string PackageRoot { get; }
```

เส้นทางสัมบูรณ์ของรากแพ็กเกจ


#### Modules

```csharp
public IReadOnlyList<WorkshopModuleDefinition> Modules { get; }
```

โมดูลข้อมูลที่มีเป้าหมายเป็น consumer นี้ เรียงตามลำดับการประกาศ


#### Folders

```csharp
public IReadOnlyList<string> Folders { get; }
```

เส้นทางสัมบูรณ์ของโฟลเดอร์โมดูล เรียงตามลำดับการประกาศ เหมือนกับ Modules[i].FullPath

### เมธอด

#### Collect

```csharp
public static IReadOnlyList<WorkshopDataPackage> Collect(string consumerModId, IEnumerable<WorkshopModActivation> activations)
```

เลือกแพ็กเกจที่มีโมดูลข้อมูลสำหรับ `consumerModId` จากบันทึกการเปิดใช้งาน (ตามลำดับการโหลด) รวมเฉพาะสถานะที่ dependency ครบถ้วนเท่านั้น (Active, AlreadyActive, NoDllModules) ข้อมูลจากแพ็กเกจที่ล้มเหลวหรือถูกใช้เพียงบางส่วนจะไม่ถูกส่งต่อ


## WorkshopModDefinition

```csharp
public sealed class WorkshopModDefinition
```

ข้อกำหนดการรันที่ผ่านการตรวจสอบแล้ว เปลี่ยนแปลงไม่ได้

### คุณสมบัติ

#### PackageRoot

```csharp
public string PackageRoot { get; }
```

เส้นทางสัมบูรณ์ของรากแพ็กเกจ


#### ModId

```csharp
public string ModId { get; }
```

modId ของ manifest


#### Version

```csharp
public string Version { get; }
```

เวอร์ชันของ manifest


#### Author

```csharp
public string Author { get; }
```

ผู้สร้างของ manifest เป็น null หากไม่มี


#### DisplayName

```csharp
public string DisplayName { get; }
```

info.title หรือ modId หากไม่มี


#### Installation

```csharp
public WorkshopInstallation Installation { get; }
```

ข้อกำหนดการติดตั้งสำหรับโหมดผู้เล่นหลายคน


#### MultiplayerDeclared

```csharp
public bool MultiplayerDeclared { get; }
```

ออบเจ็กต์ multiplayer ถูกประกาศไว้อย่างชัดเจนหรือไม่ หากเป็น false Installation จะเก็บค่าเริ่มต้นที่ผ่านการปรับมาตรฐานแล้ว


#### Modules

```csharp
public IReadOnlyList<WorkshopModuleDefinition> Modules { get; }
```

รายการโมดูล เรียงตามลำดับการประกาศ


#### LibraryPaths

```csharp
public IReadOnlyList<string> LibraryPaths { get; }
```

เส้นทางสัมบูรณ์ของ DLL แบบ managed เสริม DLL เหล่านี้จะไม่ได้รับ callback ตอนเริ่มต้น


#### Dependencies

```csharp
public IReadOnlyList<WorkshopDependency> Dependencies { get; }
```

รายการม็อดที่ม็อดนี้พึ่งพา


#### HasDll

```csharp
public bool HasDll { get; }
```

ได้มาจากการที่ modules มีชนิด dll หรือไม่ ไม่มีแฟล็กแยกต่างหาก


#### HasLocalization

```csharp
public bool HasLocalization { get; }
```

modules มีชนิด localization หรือไม่


#### HasCostume

```csharp
public bool HasCostume { get; }
```

modules มีชนิด costume หรือไม่


#### HasData

```csharp
public bool HasData { get; }
```

modules มีชนิด data หรือไม่

### คอนสตรักเตอร์

#### WorkshopModDefinition

```csharp
public WorkshopModDefinition(string packageRoot, string modId, string version, string author, string displayName, WorkshopInstallation installation, bool multiplayerDeclared, IReadOnlyList<WorkshopModuleDefinition> modules, IReadOnlyList<string> libraryPaths, IReadOnlyList<WorkshopDependency> dependencies)
```

*ยังไม่มีคำอธิบาย*

### เมธอด

#### DataModulesFor

```csharp
public IReadOnlyList<WorkshopModuleDefinition> DataModulesFor(string consumerModId)
```

โมดูลข้อมูลที่ `consumerModId` ได้รับ เรียงตามลำดับการประกาศ รายการว่างหากไม่มี โมดูลที่ระบุ consumer จะส่งไปยังม็อดนั้นเท่านั้น ส่วนโมดูลที่ละไว้จะส่งไปยังแพ็กเกจนี้เองและทุกม็อดที่พึ่งพามันโดยตรง


## WorkshopModuleDefinition

```csharp
public sealed class WorkshopModuleDefinition
```

หนึ่งรายการใน `modules[]` ของ manifest

### คุณสมบัติ

#### Type

```csharp
public WorkshopModuleType Type { get; }
```

ชนิดของโมดูล


#### RelativePath

```csharp
public string RelativePath { get; }
```

เส้นทางสัมพัทธ์จากรากแพ็กเกจ (ตัวคั่นถูกปรับมาตรฐานเป็น '/')


#### FullPath

```csharp
public string FullPath { get; }
```

เส้นทางสัมบูรณ์ที่ผ่านการตรวจสอบแล้ว


#### EntryClass

```csharp
public string EntryClass { get; }
```

ชื่อเต็มของคลาสจุดเริ่มต้นของโมดูล dll เป็น null สำหรับ localization


#### Consumer

```csharp
public string Consumer { get; }
```

modId ของม็อดที่อ่านโมดูลข้อมูล หากละไว้ (null) จะเป็นแพ็กเกจนี้เองรวมถึงทุกม็อดที่พึ่งพาโดยตรง เป็น null เสมอสำหรับโมดูลที่ไม่ใช่ data

### คอนสตรักเตอร์

#### WorkshopModuleDefinition

```csharp
public WorkshopModuleDefinition(WorkshopModuleType type, string relativePath, string fullPath, string entryClass)
```

สร้างโดยตัวตรวจสอบ


#### WorkshopModuleDefinition

```csharp
public WorkshopModuleDefinition(WorkshopModuleType type, string relativePath, string fullPath, string entryClass, string consumer)
```

สร้างโดยตัวตรวจสอบ


## WorkshopDependency

```csharp
public sealed class WorkshopDependency
```

หนึ่งรายการใน `dependencies[]` ของ manifest

### คุณสมบัติ

#### ModId

```csharp
public string ModId { get; }
```

modId ของม็อดที่ถูกพึ่งพา


#### Version

```csharp
public string Version { get; }
```

null หมายถึงเวอร์ชันใดก็ได้ หากตั้งค่าไว้ จะยอมรับเฉพาะเวอร์ชันที่ตรงกันทุกประการ (v1)


#### WorkshopId

```csharp
public ulong? WorkshopId { get; }
```

คำใบ้ว่าจะดาวน์โหลดจากที่ใด ต้องตรวจสอบกับ modId/เวอร์ชันจริงหลังติดตั้ง ค่านี้เพียงอย่างเดียวไม่ได้พิสูจน์ว่าม็อดที่ดาวน์โหลดมาเป็นม็อดที่ถูกต้อง

### คอนสตรักเตอร์

#### WorkshopDependency

```csharp
public WorkshopDependency(string modId, string version, ulong? workshopId)
```

สร้างโดยตัวตรวจสอบ


## WorkshopModuleType

```csharp
public enum WorkshopModuleType
```

ค่า `modules[].type` ของ manifest

| ค่า | คำอธิบาย |
|---|---|
| `Dll` | โมดูล DLL แบบ managed |
| `Localization` | โมดูลโฟลเดอร์แปลภาษา |
| `Costume` | โฟลเดอร์แพ็กเกจชุดแต่งกายแบบไม่ต้องใช้โค้ด (costume.json/skin.json + PNG) |
| `Data` | โฟลเดอร์ข้อมูลอเนกประสงค์ที่ม็อดอื่น (consumer) อ่าน เกมไม่ตีความเนื้อหาข้างใน |


## WorkshopInstallation

```csharp
public enum WorkshopInstallation
```

การประกาศของผู้สร้างว่าผู้เข้าร่วมคนอื่นต้องติดตั้งม็อดหรือไม่ (`multiplayer.installation` ของ manifest)

| ค่า | คำอธิบาย |
|---|---|
| `Unknown` | ไม่ได้ประกาศ (รวมในการเปรียบเทียบการตั้งค่าล็อบบี้เหมือน Everyone) |
| `LocalOnly` | ทำงานเฉพาะบนอุปกรณ์ของผู้ใช้เอง ไม่รวมในการเปรียบเทียบการตั้งค่าล็อบบี้ |
| `Everyone` | ทุกคนในห้องต้องติดตั้งแพ็กเกจเดียวกัน |
| `HostOnly` | เฉพาะโฮสต์ที่ต้องติดตั้ง |
