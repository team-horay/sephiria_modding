<!-- ไฟล์ที่สร้างอัตโนมัติ Tools/ModApiDocGen ในที่เก็บของเกมสร้างจากคอมเมนต์ /// ในซอร์ส อย่าแก้ไขโดยตรง ให้แก้คอมเมนต์ในซอร์สแล้วสร้างใหม่ -->
# การกำหนดการเกิดในห้อง

คอนเท็กซ์ที่ส่งโดยอีเวนต์ `HorayModAPI.OnAssignRoomSpawnPhases` เชื่อมต่อในจังหวะที่ตัวสร้างชั้นกำหนดเฟสมอนสเตอร์ให้กับห้อง

สร้างเมื่อ 2026-10-26 สำหรับเกมเวอร์ชัน **1.1.0** [ดัชนีอ้างอิง](README.md)

| ชนิด | คำอธิบาย |
|---|---|
| [`RoomSpawnAssignContext`](#roomspawnassigncontext) | คอนเท็กซ์ที่ถูกเรียกเมื่อ FloorGenerator กำหนดศัตรูแบบสุ่ม |
| [`ERoomSpawnKind`](#eroomspawnkind) | ชนิดของห้องที่กำลังถูกกำหนด |


## RoomSpawnAssignContext

```csharp
public sealed class RoomSpawnAssignContext
```

คอนเท็กซ์ที่ถูกเรียกเมื่อ FloorGenerator กำหนดศัตรูแบบสุ่ม ถูกเรียกเฉพาะบนเซิร์ฟเวอร์ (โฮสต์) เท่านั้น

กฎ
- `Phases` คือสำเนาเชิงลึกของตารางด่าน คุณสามารถแก้ไขหรือแทนที่ทั้งหมดได้ หากปล่อยเป็น null จะกลับไปใช้สำเนาต้นฉบับ  
- `Sources` คือแอสเซตด่านต้นฉบับ อ่านอย่างเดียว (การแก้ไขจะทำให้แอสเซตในเอดิเตอร์เปลี่ยนแปลง)  
ในการกำหนดห้อง `Phases` (สำเนา) จะมี containedFactions ว่าง (GeneratePhaseData ไม่คัดลอกส่วนนี้) ให้อ่าน factions จาก Sources
- สำหรับการสุ่ม ให้สร้าง `new System.Random(ctx.Seed)` จาก `Seed` การใช้ UnityEngine.Random หรือ RNG ของชั้นจะทำให้ชั้นต่างออกไปเมื่อสร้างใหม่หรือเข้าร่วมอีกครั้ง  
- เอนทิตีใน spawnData ที่ม็อดเพิ่มเข้ามาต้องลงทะเบียนใน UnitDatabase เพื่อให้การกู้คืนเซฟและการเกิดฝั่งไคลเอนต์ทำงานได้

### คุณสมบัติ

#### Generator

```csharp
public FloorGenerator Generator { get; }
```

ชั้นที่กำลังทำการกำหนดนี้


#### Stage

```csharp
public StageEntity Stage { get; }
```

ด่านของชั้น เป็น null หากไม่พบ


#### Floor

```csharp
public FloorData Floor { get; }
```

ข้อมูลชั้น (ซีด ความยาก ชื่อด่าน) เป็น null หากไม่มีข้อมูลเซิร์ฟเวอร์


#### Difficulty

```csharp
public int Difficulty { get; }
```

ความยากสุดท้ายที่ใช้เลือกพูล (รวมจำนวนผู้เล่นและ DIFFICULTY_UP)


#### Kind

```csharp
public ERoomSpawnKind Kind { get; }
```

ชนิดของห้องที่กำลังถูกกำหนด


#### AssignIndex

```csharp
public int AssignIndex { get; }
```

ดัชนีของการกำหนดนี้บนชั้น (เริ่มจาก 0) เริ่มที่ 0 สำหรับแต่ละอินสแตนซ์ของตัวสร้าง


#### Seed

```csharp
public int Seed { get; }
```

Hash(ซีดของชั้น, ดัชนีการกำหนด) ซีดสำหรับ RNG ของม็อด


#### Sources

```csharp
public IReadOnlyList<MonsterSpawnPhases> Sources { get; }
```

แอสเซตด่านต้นฉบับ (อ่านอย่างเดียว) หนึ่งรายการสำหรับห้อง และ phaseCount รายการสำหรับโซนต่อสู้


#### Source

```csharp
public MonsterSpawnPhases Source { get; }
```

ต้นฉบับรายการแรก Sources[0]


#### Phases

```csharp
public MonsterSpawnPhases Phases { get; set; }
```

สำเนาที่จะถูกกำหนด แทนที่ได้

### คอนสตรักเตอร์

#### RoomSpawnAssignContext

```csharp
public RoomSpawnAssignContext(FloorGenerator generator, StageEntity stage, FloorData floor, int difficulty, ERoomSpawnKind kind, int assignIndex, int seed, IReadOnlyList<MonsterSpawnPhases> sources, MonsterSpawnPhases phases)
```

*ยังไม่มีคำอธิบาย*


## ERoomSpawnKind

```csharp
public enum ERoomSpawnKind
```

ชนิดของห้องที่กำลังถูกกำหนด

| ค่า | คำอธิบาย |
|---|---|
| `Normal` | ห้องต่อสู้ปกติ (ซุ่มโจมตี) |
| `Weak` | ห้องต่อสู้แบบอ่อน (พูลความยาก ≤ 1 ไม่มีซุ่มโจมตี) |
| `BattleZone` | โซนต่อสู้ (รวมหลายเฟสเข้าด้วยกัน แล้วรันฮุกครั้งเดียว) |
