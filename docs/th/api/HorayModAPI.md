<!-- ไฟล์ที่สร้างอัตโนมัติ Tools/ModApiDocGen ในที่เก็บของเกมสร้างจากคอมเมนต์ /// ในซอร์ส อย่าแก้ไขโดยตรง ให้แก้คอมเมนต์ในซอร์สแล้วสร้างใหม่ -->
# HorayModAPI

จุดเริ่มต้นแบบ static ที่เกมเปิดให้ม็อดใช้ สมัครรับอีเวนต์การโหลดฐานข้อมูล เซสชัน และการเกิด รวมถึงลงทะเบียน prefab เครือข่ายและค่าสถานะพิเศษ สมัครรับอีเวนต์เพียงครั้งเดียวใน `OnModLoaded` สำหรับวงจรชีวิตและกฎของโหมดผู้เล่นหลายคน ดูที่ [ม็อด DLL](../dll-mods.md)

สร้างเมื่อ 2026-10-26 สำหรับเกมเวอร์ชัน **1.1.0** [ดัชนีอ้างอิง](README.md)

| ชนิด | คำอธิบาย |
|---|---|
| [`HorayModAPI`](#horaymodapi) | จุดเริ่มต้นสำหรับม็อด |
| [`HorayModLocalizationContext`](#horaymodlocalizationcontext) | ตัวช่วยสำหรับลงทะเบียนข้อความแปลภาษาของม็อด |


## HorayModAPI

```csharp
public static class HorayModAPI
```

จุดเริ่มต้นสำหรับม็อด

ตัวอย่าง:

```csharp
HorayModAPI.OnLoadMiracleDatabase += () => {
    MiracleDatabase.Register(myCustomMiraclePrefab);
    MiracleDatabase.Modify("Scholar", m => m.categories = new[] { "Lake" });
};
```

### ค่าคงที่และฟิลด์

#### WORK_IN_PROGRESS_README

```csharp
public const string WORK_IN_PROGRESS_README = "The MOD System is still under development. Most features are not work properly yet."
```

*ยังไม่มีคำอธิบาย*


#### WORK_IN_PROGRESS_README_URL

```csharp
public const string WORK_IN_PROGRESS_README_URL = "https://teamhoray.com/mod-api"
```

*ยังไม่มีคำอธิบาย*


#### NAME

```csharp
public const string NAME = "HorayMod"
```

*ยังไม่มีคำอธิบาย*

### อีเวนต์

#### OnLoadMiracleDatabase

```csharp
public static event Action OnLoadMiracleDatabase
```

ทำงานทันทีหลังฐานข้อมูลปาฏิหาริย์เริ่มต้นเสร็จ ลงทะเบียนหรือแก้ไขปาฏิหาริย์ด้วย `MiracleDatabase.Register`/`Modify`


#### OnLoadWeaponDatabase

```csharp
public static event Action OnLoadWeaponDatabase
```

ทำงานทันทีหลังฐานข้อมูลอาวุธเริ่มต้นเสร็จ ลงทะเบียนหรือแก้ไขอาวุธด้วย `WeaponDatabase.Register`/`Modify`


#### OnLoadStatusDatabase

```csharp
public static event Action OnLoadStatusDatabase
```

ทำงานทันทีหลังฐานข้อมูลสถานะ (เอฟเฟกต์ค่าสถานะ) เริ่มต้นเสร็จ ลงทะเบียนหรือแก้ไขสถานะด้วย `StatusDatabase.Register`/`Modify`


#### OnLoadSpecialStatDatabase

```csharp
public static event Action OnLoadSpecialStatDatabase
```

ทำงานทันทีหลังหมวดหมู่และสถานะของค่าสถานะพิเศษโหลดเสร็จ ลงทะเบียนรายการแสดงผลด้วย `RegisterSpecialStatCategory`/`RegisterSpecialStat`


#### OnLoadItemDatabase

```csharp
public static event Action OnLoadItemDatabase
```

ทำงานทันทีหลังฐานข้อมูลไอเทมเริ่มต้นเสร็จ ลงทะเบียนหรือแก้ไขไอเทมด้วย `ItemDatabase.Register`/`Modify`


#### OnLoadKeywordDatabase

```csharp
public static event Action OnLoadKeywordDatabase
```

ทำงานทันทีหลังฐานข้อมูลคีย์เวิร์ดเริ่มต้นเสร็จ ลงทะเบียนหรือแก้ไขคีย์เวิร์ดด้วย `KeywordDatabase.Register`/`Modify`


#### OnLoadPassiveDatabase

```csharp
public static event Action OnLoadPassiveDatabase
```

ทำงานทันทีหลังฐานข้อมูลแพสซีฟเริ่มต้นเสร็จ ลงทะเบียนหรือแก้ไขแพสซีฟด้วย `PassiveDatabase.Register`/`Modify`


#### OnLoadCostumeDatabase

```csharp
public static event Action OnLoadCostumeDatabase
```

ทำงานทันทีหลังฐานข้อมูลชุดแต่งกายเริ่มต้นเสร็จ ลงทะเบียนหรือแก้ไขชุดแต่งกายด้วย `CostumeDatabase.Register`/`RegisterSkin`/`Modify`


#### OnLoadPropDatabase

```csharp
public static event Action OnLoadPropDatabase
```

ทำงานทันทีหลังฐานข้อมูลพร็อปเริ่มต้นเสร็จ ลงทะเบียนพร็อปด้วย `PropDatabase.Register`


#### OnLoadUnitDatabase

```csharp
public static event Action OnLoadUnitDatabase
```

ทำงานทันทีหลังฐานข้อมูลยูนิต (เอนทิตีการเกิดของศัตรูและ NPC) เริ่มต้นเสร็จ


#### OnLoadSocialIDDatabase

```csharp
public static event Action OnLoadSocialIDDatabase
```

ทำงานทันทีหลังฐานข้อมูลบทบาท NPC เริ่มต้นเสร็จ


#### OnLoadCustomGameDatabase

```csharp
public static event Action OnLoadCustomGameDatabase
```

ทำงานทันทีหลังฐานข้อมูลเกมกำหนดเองเริ่มต้นเสร็จ


#### OnAllDatabasesReady

```csharp
public static event Action OnAllDatabasesReady
```

ทำงานทันทีหลังฐานข้อมูลทั้งหมดเริ่มต้นเสร็จ


#### OnTitleMenuReady

```csharp
public static event Action<UI_TitleLobby> OnTitleMenuReady
```

ถูกเรียกหลังเมนูไตเติลโหลดเสร็จ


#### OnStartGameServerside

```csharp
public static event Action OnStartGameServerside
```

ถูกเรียกเมื่อเกมเริ่ม (ฝั่งเซิร์ฟเวอร์) การเริ่มหมายถึงการคลิกช่องเซฟบนหน้าจอไตเติลเพื่อเข้าสู่เกม ข้อมูลเซสชัน เช่น ผู้เล่นและตำแหน่งปัจจุบัน ยังไม่ถูกโหลด


#### OnStartGameClientside

```csharp
public static event Action OnStartGameClientside
```

ถูกเรียกเมื่อเกมเริ่ม (ฝั่งไคลเอนต์) การเริ่มหมายถึงการคลิกช่องเซฟบนหน้าจอไตเติลเพื่อเข้าสู่เกม ข้อมูลเซสชัน เช่น ผู้เล่นและตำแหน่งปัจจุบัน ยังไม่ถูกโหลด


#### OnStartSessionServerside

```csharp
public static event Action<bool> OnStartSessionServerside
```

ถูกเรียกเมื่อเซสชันเกมเริ่ม (ฝั่งเซิร์ฟเวอร์) เซสชันคือหน่วยของการเล่น เมื่อผู้เล่นตาย เซสชันนั้นจะจบลงและเซสชันใหม่จะเริ่มขึ้น


#### OnStartSessionClientside

```csharp
public static event Action<bool> OnStartSessionClientside
```

ถูกเรียกเมื่อเซสชันเกมเริ่ม (ฝั่งไคลเอนต์) เซสชันคือหน่วยของการเล่น เมื่อผู้เล่นตาย เซสชันนั้นจะจบลงและเซสชันใหม่จะเริ่มขึ้น


#### OnLocalizationReady

```csharp
public static event Action<HorayModLocalizationContext> OnLocalizationReady
```

ถูกเรียกหลังไฟล์แปลภาษาโหลดเสร็จ


#### OnFloorAllocatedServerside

```csharp
public static event Action<string, string, FloorGenerator> OnFloorAllocatedServerside
```

ถูกเรียกเมื่อมีการจัดสรรชั้น (ฝั่งเซิร์ฟเวอร์) ทุกสถานที่ใน Sephiria ถูกนิยามเป็นชั้น เมื่อผู้เล่นย้ายระหว่างสถานที่ ขั้นตอนต่อไปนี้จะทำงานตามลำดับ 1. การจัดสรรชั้น: สร้างและโหลดชั้นใหม่ 2. การย้าย: ย้ายผู้เล่นไปยังจุดที่ร้องขอบนชั้นที่โหลดเสร็จสมบูรณ์แล้ว 3. การนำชั้นออก: นำสถานที่ที่ผู้เล่นเคยอยู่ก่อนหน้าออก หากว่างเปล่า (= ไม่มีผู้เล่นเหลืออยู่ที่นั่น)
param1: GUID ของชั้น  
param2: ชื่อชั้น  
param3: อินสแตนซ์ของ FloorGenerator


#### OnFloorAllocatedClientside

```csharp
public static event Action<string, string, FloorGenerator> OnFloorAllocatedClientside
```

ถูกเรียกเมื่อมีการจัดสรรชั้น (ฝั่งไคลเอนต์) ทุกสถานที่ใน Sephiria ถูกนิยามเป็นชั้น เมื่อผู้เล่นย้ายระหว่างสถานที่ ขั้นตอนต่อไปนี้จะทำงานตามลำดับ 1. การจัดสรรชั้น: สร้างและโหลดชั้นใหม่ 2. การย้าย: ย้ายผู้เล่นไปยังจุดที่ร้องขอบนชั้นที่โหลดเสร็จสมบูรณ์แล้ว 3. การนำชั้นออก: นำสถานที่ที่ผู้เล่นเคยอยู่ก่อนหน้าออก หากว่างเปล่า (= ไม่มีผู้เล่นเหลืออยู่ที่นั่น)
param1: GUID ของชั้น  
param2: ชื่อชั้น  
param3: อินสแตนซ์ของ FloorGenerator


#### OnAssignRoomSpawnPhases

```csharp
public static event Action<RoomSpawnAssignContext> OnAssignRoomSpawnPhases
```

ถูกเรียกหลังองค์ประกอบศัตรูแบบสุ่มของด่านถูกกำหนดแล้ว (ฝั่งเซิร์ฟเวอร์) ทำงานทันทีหลังตัวสร้างชั้นเลือกเฟสมอนสเตอร์สำหรับห้องต่อสู้ (หรือโซนต่อสู้) ก่อนที่ตัวสร้างการเกิดจะได้รับ แทนที่หรือแก้ไข ctx.Phases เพื่อเปลี่ยนสิ่งที่จะเกิดที่นั่น เกมไม่บังคับเรื่องความสมดุลที่จุดนี้ ดูกฎได้ที่ RoomSpawnAssignContext (RNG แบบกำหนดได้ผ่าน ctx.Seed ห้ามแตะ ctx.Sources เอนทิตีต้องลงทะเบียนใน UnitDatabase)


#### OnPlayerAvatarSpawnedServerside

```csharp
public static event Action<PlayerAvatar> OnPlayerAvatarSpawnedServerside
```

ถูกเรียกเมื่อผู้เล่นเกิด (ฝั่งเซิร์ฟเวอร์) คุณสามารถลงทะเบียน callback ที่เกี่ยวกับผู้เล่นภายในฮุกนี้ได้ ถูกเรียกที่ตอนท้ายของ PlayerAvatar.OnStartServer หลังค่าสถานะพื้นฐานถูกนำไปใช้แล้ว ชุดแต่งกาย ชั้น และเนื้อหาในช่องเก็บของอาจถูกเติมในภายหลังภายในเฟรมเดียวกันหรือเฟรมถัดไป ใช้จุดนี้เพื่อสมัครรับอีเวนต์อินสแตนซ์ของอวตารเอง (OnDamagedServerside, OnAttackUnit, OnKillUnit, OnDie, OnAddedBuff, OnEnteredFloorServerside, ...) การสมัครรับบนอวตารจะหายไปพร้อมอวตาร จึงไม่ต้องยกเลิกการสมัครรับสำหรับอีเวนต์เหล่านั้น


#### OnPlayerAvatarSpawnedClientside

```csharp
public static event Action<PlayerAvatar> OnPlayerAvatarSpawnedClientside
```

ถูกเรียกเมื่ออวตารของผู้เล่นเกิด (ฝั่งไคลเอนต์) ถูกเรียกครั้งเดียวต่ออวตารที่ตอนท้ายของ OnStartClient และทำงานสำหรับผู้เล่นระยะไกลด้วย ตรวจสอบ avatar.isLocalPlayer / avatar.isOwned เพื่อแยกแยะผู้เล่นในเครื่อง SyncVar ถูกนำไปใช้แล้ว แต่ชั้นและเนื้อหาในช่องเก็บของอาจมาถึงในภายหลัง


#### OnUnitSpawnedServerside

```csharp
public static event Action<UnitAvatar> OnUnitSpawnedServerside
```

ทำงานเมื่อยูนิต (ผู้เล่น ศัตรู สิ่งอัญเชิญ หรือสหาย) เกิดบนเซิร์ฟเวอร์ เฉพาะเซิร์ฟเวอร์ (โฮสต์) เท่านั้น


#### OnUnitDespawnedServerside

```csharp
public static event Action<UnitAvatar> OnUnitDespawnedServerside
```

ทำงานทันทีก่อนยูนิตถูกนำออกจากเซิร์ฟเวอร์ เฉพาะเซิร์ฟเวอร์ (โฮสต์) เท่านั้น


#### OnPlayerEvade

```csharp
public static event Action<PlayerAvatar, DamageInstance> OnPlayerEvade
```

ทำงานเมื่อผู้เล่นหลบการโจมตี (ฝั่งเซิร์ฟเวอร์)


#### OnGetDebuff

```csharp
public static event Func<CharacterDebuff, UnitAvatar, CharacterDebuff> OnGetDebuff
```

ให้คุณสลับ prefab ของดีบัฟได้ทันทีก่อนดีบัฟถูกใช้กับยูนิต ตัวจัดการจะได้รับ (prefab ปัจจุบัน, ผู้ร่าย) และคืนค่า prefab ทดแทน การคืนค่า null จะคงค่าเดิมไว้


#### GridInventoryStartPermission

```csharp
public static event Action<GridInventory, PlayerAvatar> GridInventoryStartPermission
```

ทำงานเมื่อเซิร์ฟเวอร์รับสิทธิ์เขียนช่องเก็บของและเริ่มถอดเอฟเฟกต์ไอเทมทั้งหมด


#### GridInventoryEndPermission

```csharp
public static event Action<GridInventory, PlayerAvatar> GridInventoryEndPermission
```

ทำงานทันทีหลังเซิร์ฟเวอร์คืนสิทธิ์เขียนและใช้เอฟเฟกต์ไอเทมอีกครั้ง

### เมธอด

#### RegisterNetworkPrefab

```csharp
public static bool RegisterNetworkPrefab(GameObject prefab)
```

ลงทะเบียนออบเจ็กต์ (prefab) ที่สามารถเกิดผ่านเครือข่ายได้ ออบเจ็กต์ต้องมีคอมโพเนนต์ NetworkIdentity

เซิร์ฟเวอร์ (โฮสต์) และไคลเอนต์ต่างต้องมีรายการออบเจ็กต์ที่ใช้เครือข่ายได้ และรายการนั้นต้องเหมือนกันสำหรับทุกคน

ตัวอย่าง:

```csharp
HorayModAPI.OnAllDatabasesReady += () => {
    HorayModAPI.RegisterNetworkPrefab(myNewCustomPrefab);
};
```

คืนค่า: true หากลงทะเบียนสำเร็จ


#### RegisterNetworkPrefab

```csharp
public static bool RegisterNetworkPrefab(GameObject prefab, string stableKey)
```

ลงทะเบียน prefab ที่สร้างขณะรันไทม์ (เช่น โคลนของเทมเพลตที่มีในเกม) เป็นออบเจ็กต์ที่เกิดผ่านเครือข่ายได้ โดยใช้ assetId คงที่ที่คำนวณจาก `stableKey` ใช้ชื่อยูนิตของม็อด (`<modId>:<id>`) เป็นคีย์

คืนค่า: true หากลงทะเบียนสำเร็จ


#### RegisterNetworkPrefabs

```csharp
public static int RegisterNetworkPrefabs(IEnumerable<GameObject> prefabs)
```

ลงทะเบียน prefab หลายตัวเป็นออบเจ็กต์ที่เกิดผ่านเครือข่ายได้ในครั้งเดียว

คืนค่า: จำนวน prefab ที่ลงทะเบียนสำเร็จ


#### UnregisterNetworkPrefab

```csharp
public static bool UnregisterNetworkPrefab(GameObject prefab)
```

ยกเลิกการลงทะเบียน prefab เครือข่ายที่ลงทะเบียนไว้

คืนค่า: true หากถูกนำออก


#### RegisterSpecialStatCategory

```csharp
public static bool RegisterSpecialStatCategory(SpecialStatCategoryEntity category)
```

ลงทะเบียนหมวดหมู่ค่าสถานะพิเศษใน UI ค่าสถานะ

คืนค่า: true หากลงทะเบียนแล้ว


#### RegisterSpecialStat

```csharp
public static bool RegisterSpecialStat(StatusEntity status, Func<SpecialStatContext, SpecialStatValue> reader = null)
```

ลงทะเบียน StatusEntity เป็นรายการในหมวดหมู่ค่าสถานะพิเศษใน UI ค่าสถานะ หากส่ง `reader` ค่าจะถูกอ่านผ่านฟังก์ชันนั้นแทนการตั้งค่าของแอสเซต

คืนค่า: true หากลงทะเบียนแล้ว


#### GetModOptions

```csharp
public static ModOptions GetModOptions(string modId)
```

รับตัวเลือกต่ออุปกรณ์ที่ผูกกับ modId ม็อดจาก Workshop สามารถใช้ `Context.Options` แทนได้


#### TryShowItemInteraction

```csharp
public static bool TryShowItemInteraction(ItemInteractionOptions options, out ItemInteractionSession session)
```

เปิดแผงการโต้ตอบไอเทมที่ใช้ควบคู่กับช่องเก็บของ คืนค่า false หาก UI ไม่ว่าง


#### TryGetUIWidgets

```csharp
public static bool TryGetUIWidgets(out UIWidgetFactory widgets)
```

รับแฟกทอรีสำหรับวิดเจ็ต UI ในสไตล์ของเกม แนบไว้ใต้ Canvas ของ UI ที่มีอยู่


#### TryShowDialogue

```csharp
public static bool TryShowDialogue(Transform anchor, DialoguePage page, out DialogueSession session)
```

เปิดกล่องบทสนทนาในโลก คืนค่า false หาก UI ไม่ว่าง แสดงหน้าถัดไปด้วย `session.Show`


#### TryShowDialogue

```csharp
public static bool TryShowDialogue(Transform anchor, DialoguePage page, out DialogueSession session, Vector3? offset)
```

เปิดกล่องบทสนทนาพร้อมออฟเซ็ตในพิกัดโลก หากเป็น null จะใช้ออฟเซ็ตเริ่มต้นของ prefab


#### TryShowDialogue

```csharp
public static bool TryShowDialogue(Transform anchor, DialoguePage page, out DialogueSession session, Vector3? offset = null, Vector2? cameraOffset = null, bool focusCamera = true)
```

เปิดกล่องบทสนทนาพร้อมออฟเซ็ตของบทสนทนาและกล้องแยกกัน การโฟกัสกล้องมีผลเฉพาะในเครื่องเท่านั้น


## HorayModLocalizationContext

```csharp
public class HorayModLocalizationContext
```

ตัวช่วยสำหรับลงทะเบียนข้อความแปลภาษาของม็อด

### เมธอด

#### AddText

```csharp
public void AddText(string language, string key, string value)
```

ลงทะเบียนข้อความสำหรับภาษาที่ระบุ


#### AddText

```csharp
public void AddText(string key, Dictionary<string, string> texts)
```

ลงทะเบียนข้อความสำหรับหลายภาษาในครั้งเดียว
texts: { "ko-KR" : "불의 검", "en-US" : "Fire Sword", ... }
