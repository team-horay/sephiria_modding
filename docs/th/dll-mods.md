# ม็อด DLL

โมดูล DLL ช่วยให้คุณรันโค้ดที่เขียนด้วย C# ภายในเกมได้ สมัครรับอีเวนต์จาก `HorayModAPI` เพื่อเชื่อมต่อกับผู้เล่น ไอเทม UI และอื่น ๆ
เอกสารนี้ครอบคลุมตั้งแต่การสร้างโปรเจกต์ไปจนถึงการทำให้ DLL แรกของคุณทำงานในเกม สำหรับรายการอีเวนต์และฟังก์ชันลงทะเบียนทั้งหมด ดูที่ [อ้างอิง API](api/README.md)


## ความต้องการ

- [.NET SDK](https://dotnet.microsoft.com/download) 8 ขึ้นไป
- โปรแกรมแก้ไข C#


## การสร้างโปรเจกต์

ดูตัวอย่างได้ที่ [`examples/dll-sample`](../../examples/dll-sample) เอกสารนี้ใช้ตัวอย่างดังกล่าวเป็นข้อมูลอ้างอิง

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>netstandard2.1</TargetFramework>
    <LangVersion>9.0</LangVersion>
    <AssemblyName>PhysicalDamagePlus</AssemblyName>
    <RootNamespace>PhysicalDamagePlus</RootNamespace>
    <Nullable>disable</Nullable>
    <ImplicitUsings>disable</ImplicitUsings>
    <GenerateDependencyFile>false</GenerateDependencyFile>
    <AppendTargetFrameworkToOutputPath>false</AppendTargetFrameworkToOutputPath>
    <!-- โฟลเดอร์ติดตั้งเกม เปลี่ยนให้ตรงกับเครื่องของคุณ หรือส่งผ่าน dotnet build -p:GameDir=... -->
    <GameDir Condition="'$(GameDir)' == ''">C:\Program Files (x86)\Steam\steamapps\common\Sephiria</GameDir>
    <OutDir>$(GameDir)\WorkshopLocal\physical-damage-plus\bin\</OutDir>
  </PropertyGroup>

  <ItemGroup>
    <Reference Include="Assembly-CSharp">
      <HintPath>$(GameDir)\Sephiria_Data\Managed\Assembly-CSharp.dll</HintPath>
      <Private>false</Private>
    </Reference>
    <Reference Include="UnityEngine.CoreModule">
      <HintPath>$(GameDir)\Sephiria_Data\Managed\UnityEngine.CoreModule.dll</HintPath>
      <Private>false</Private>
    </Reference>
    <!-- เพิ่ม Mirror หากทำงานกับออบเจ็กต์เครือข่าย เช่น PlayerAvatar, UnityEngine.UI หากสร้าง UI และ 0Harmony หากทำแพตช์ ด้วยวิธีเดียวกัน -->
    <Reference Include="Mirror">
      <HintPath>$(GameDir)\Sephiria_Data\Managed\Mirror.dll</HintPath>
      <Private>false</Private>
    </Reference>
  </ItemGroup>
</Project>
```

1. **กำหนดเป้าหมายเป็น `netstandard2.1`** และตั้งเวอร์ชันภาษาเป็น C# 9
2. **อ้างอิง DLL ของเกม แต่อย่าคัดลอก** ชี้การอ้างอิงไปยังไฟล์ใน `<โฟลเดอร์ติดตั้งเกม>/Sephiria_Data/Managed/` และตั้งค่าทั้งหมดเป็น `Private=false` หาก DLL ของเกมถูกคัดลอกเข้าไปในโฟลเดอร์ม็อด แอสเซมบลีเดียวกันจะถูกโหลดสองครั้ง ซึ่งทำให้เกิดข้อผิดพลาดที่หาสาเหตุยาก และยังเพิ่มไฟล์ที่ต้องตรวจสอบโดยไม่จำเป็น
3. `Assembly-CSharp.dll` มีโค้ดส่วนใหญ่ที่ขับเคลื่อนเกม


## โครงสร้างโค้ด


```csharp
using UnityEngine;

namespace PhysicalDamagePlus
{
    public class Entry : WorkshopModBase
    {
        public const int Bonus = 5;

        // ถูกเรียกครั้งเดียวต่อโปรเซส สมัครรับอีเวนต์ที่นี่ และทำเพียงครั้งเดียว
        protected override void OnModLoaded()
        {
            Debug.Log($"[PhysicalDamagePlus] Loaded v{metadata.modVersion} | folder={Context.PackageRoot}");

            HorayModAPI.OnPlayerAvatarSpawnedServerside += HandlePlayerSpawnedServer;
            HorayModAPI.OnPlayerAvatarSpawnedClientside += HandlePlayerSpawnedClient;
        }

        protected override void OnModUnloaded()
        {
            HorayModAPI.OnPlayerAvatarSpawnedServerside -= HandlePlayerSpawnedServer;
            HorayModAPI.OnPlayerAvatarSpawnedClientside -= HandlePlayerSpawnedClient;
        }

        // ถูกเรียกเฉพาะบนโฮสต์ (เซิร์ฟเวอร์) เปลี่ยนสถานะเกมที่นี่
        private static void HandlePlayerSpawnedServer(PlayerAvatar player)
        {
            if (player == null)
                return;
            player.AddCustomStat(ECustomStat.PhysicalDamage, Bonus);
        }

        // ถูกเรียกบนผู้เข้าร่วมทุกคน ค่าที่เซิร์ฟเวอร์เพิ่มจะมาถึงผ่านการซิงก์
        private static void HandlePlayerSpawnedClient(PlayerAvatar player)
        {
            if (player == null)
                return;
            Debug.Log($"[PhysicalDamagePlus] PhysicalDamage={player.GetCustomStat(ECustomStat.PhysicalDamage)}");
        }
    }
}
```

```json
"modules": [
  { "type": "dll", "path": "bin/PhysicalDamagePlus.dll", "entryClass": "PhysicalDamagePlus.Entry" }
]
```

### WorkshopModBase

คลาสของคุณจะสืบทอดสิ่งต่อไปนี้จากคลาสฐาน

| สมาชิก | คำอธิบาย |
|---|---|
| `metadata` | `modName`, `modVersion`, `modAuthor` ถูกเติมด้วยค่าจาก `metadata.json` |
| `Context` | `PackageRoot` (เส้นทางสัมบูรณ์ของโฟลเดอร์ม็อด), `FileId` (ID ไอเทม Steam Workshop สำหรับแพ็กเกจในเครื่องจะเป็นค่าแยกต่างหากที่ไม่ซ้ำกับ Steam ID), `Definition` (manifest ที่ผ่านการตรวจสอบแล้ว), `ResolvePath(relativePath)` ใช้เพื่อค้นหาไฟล์ภายในโฟลเดอร์ม็อดของคุณ เส้นทางติดตั้งต่างกันในทุกเครื่องของผู้ใช้ จึงห้ามเขียนตายตัวเด็ดขาด |
| `GetDataPackages()` | รายการแพ็กเกจโมดูล `data` ที่ระบุม็อดนี้เป็น `consumer` ให้เรียกหลังจากการเปิดใช้งานเสร็จสมบูรณ์ (ภายในตัวจัดการ `OnLoad*Database` หรือหลัง `OnAllDatabasesReady`) |

`Context` ใช้ได้ตั้งแต่ช่วงที่ `OnModLoaded` ถูกเรียก ในคอนสตรักเตอร์ค่านี้ยังเป็น null


## วงจรชีวิต

ม็อด DLL จะถูกเปิดใช้งาน**เมื่อคุณเริ่มเกมและเข้าสู่โลก** ยังไม่มีโค้ดใดทำงานบนหน้าจอไตเติล ลำดับการเริ่มต้นเป็นดังนี้

1. ซีนโลกโหลด และ `GameDataLoader` เริ่มทำงาน
2. โมดูล DLL ในแผนการโหลดถูกโหลดตามลำดับ แต่ละคลาสจุดเริ่มต้นถูกสร้างอินสแตนซ์ และ `OnModLoaded` ถูกเรียก
3. ขณะที่ฐานข้อมูลเริ่มต้น อีเวนต์ เช่น `OnLoadItemDatabase` และ `OnLoadUnitDatabase` จะทำงานทีละตัว
4. เมื่อทุกอย่างเสร็จสิ้น `OnAllDatabasesReady` จะถูกเรียก

## การสมัครรับอีเวนต์

คลาส `HorayModAPI` มีอีเวนต์ที่กำหนดไว้ล่วงหน้าซึ่งช่วยให้คุณเชื่อมต่อกับสถานการณ์ต่าง ๆ ได้มากมาย

- สมัครรับอีเวนต์**เพียงครั้งเดียว**ใน `OnModLoaded` อีเวนต์ฐานข้อมูลจะทำงานอีกครั้งทุกครั้งที่โหลด ดังนั้นให้ทำสิ่งที่ "ต้องเกิดขึ้นทุกครั้ง" (การลงทะเบียนไอเทม ฯลฯ) ภายในตัวจัดการ
- จัดการการสมัครรับและยกเลิกการสมัครรับอย่างระมัดระวัง ยกเลิกการสมัครรับ (-=) อีเวนต์ที่คุณสมัครไว้ใน `OnModUnloaded`

รายการอีเวนต์ทั้งหมดอยู่ใน [อ้างอิง API](api/README.md) ตารางด้านล่างแสดงอีเวนต์ที่แนะนำสำหรับแต่ละสิ่งที่คุณอาจต้องการทำ

| สิ่งที่ต้องการทำ | อีเวนต์ |
|---|---|
| เพิ่มไอเทมหรืออาร์ติแฟกต์ | `OnLoadItemDatabase` |
| เพิ่มอาวุธ ปาฏิหาริย์ แพสซีฟ ชุดแต่งกาย หรือพร็อป | `OnLoadWeaponDatabase`, `OnLoadMiracleDatabase`, `OnLoadPassiveDatabase`, `OnLoadCostumeDatabase`, `OnLoadPropDatabase` |
| เพิ่มยูนิตศัตรูหรือ NPC | `OnLoadUnitDatabase` + `RegisterNetworkPrefab(prefab, stableKey)` |
| เพิ่มนักเดินทาง (NPC ในหมู่บ้าน) | `OnLoadSocialIDDatabase` |
| เพิ่มการแสดงค่าสถานะ | `OnLoadSpecialStatDatabase` + `RegisterSpecialStat` |
| ลงทะเบียนข้อความแปลภาษาจากโค้ด | `OnLocalizationReady` (`AddText`) |
| เพิ่มปุ่มในเมนูไตเติล | `OnTitleMenuReady` |
| เริ่มเกมหรือเซสชัน | `OnStartGameServerside/Clientside`, `OnStartSessionServerside/Clientside` |
| เมื่อชั้นถูกสร้าง | `OnFloorAllocatedServerside/Clientside`, `OnAssignRoomSpawnPhases` |
| ผู้เล่นหรือยูนิตเกิด | `OnPlayerAvatarSpawned*`, `OnUnitSpawnedServerside`, `OnUnitDespawnedServerside` |
| เชื่อมต่อกับการตรวจสอบการต่อสู้ | `OnPlayerEvade`, `OnGetDebuff` |
| สิทธิ์ในการจัดการช่องเก็บของ | `GridInventoryStartPermission`, `GridInventoryEndPermission` |


## การหลีกเลี่ยงการชนกัน

เอนทิตีที่ม็อดของคุณเพิ่มจะถูกตั้งชื่อในรูปแบบ `<modId>:<id>` เช่น `yourname.artifact-pack:flame-ring` กฎนี้ป้องกันการชนกับม็อดอื่น และเป็นวิธีที่ใช้แยกแยะเนื้อหาของม็อดในประวัติการเล่นและสถิติ


## ฝั่งเซิร์ฟเวอร์ ฝั่งไคลเอนต์

- Sephiria ถูกสร้างขึ้นโดยมีโหมดผู้เล่นหลายคนเป็นแกนหลัก อีเวนต์จำนวนมากจึงมีเวอร์ชันฝั่งเซิร์ฟเวอร์และฝั่งไคลเอนต์แยกกัน
- โฮสต์จะเรียกทั้งสองฝั่ง ส่วนผู้เข้าร่วมคนอื่นจะเรียกเฉพาะ Clientside
- ตามหลักแล้ว ให้ทำสิ่งที่เปลี่ยนสถานะเกม (การเกิด การดรอป ความเสียหาย) เฉพาะใน Serverside และทำสิ่งที่เปลี่ยนเฉพาะการแสดงผลใน Clientside


## แพตช์ Harmony

เมื่อคุณต้องการเปลี่ยนสิ่งที่อีเวนต์เข้าไม่ถึง คุณสามารถใช้ Harmony ได้
ตั้งแต่แพตช์ 1.1.0 เกม**มี Harmony 2.4.3 ในตัว**แล้ว เพียงเพิ่มการอ้างอิงเท่านั้น

```xml
<Reference Include="0Harmony">
  <HintPath>$(GameDir)\Sephiria_Data\Managed\0Harmony.dll</HintPath>
  <Private>false</Private>
</Reference>
```

```csharp
protected override void OnModLoaded()
{
    var harmony = new HarmonyLib.Harmony("yourname.first-mod");
    harmony.PatchAll();
}
```

- **อย่ารวม `0Harmony.dll` ไปกับม็อดของคุณ** สำเนาที่รวมไปจะถูกเพิกเฉย และแม้คุณจะใส่เวอร์ชันอื่น ม็อดของคุณก็จะไม่ทำงานบนเวอร์ชันนั้น
- การใช้ `modId` ของคุณตามเดิมเป็น Harmony ID ช่วยป้องกันการชนกับผู้อื่น
- แพตช์พังเมื่อเกมอัปเดตได้ง่ายกว่า API อย่างเป็นทางการมาก ทำเท่าที่ทำได้ด้วยอีเวนต์ และใช้แพตช์เป็นทางเลือกสุดท้าย
- ไม่รองรับโหลดเดอร์ภายนอก เช่น BepInEx และ MelonLoader


## DLL ช่วยเหลือ

หากม็อดของคุณใช้ไลบรารีอื่น (เช่น แพ็กเกจจากบุคคลที่สามนอกเหนือจาก Newtonsoft.Json) ให้วาง DLL ของไลบรารีเหล่านั้นในโฟลเดอร์ม็อดและประกาศใน `libraries` ของ `metadata.json` DLL ที่ไม่ได้ประกาศจะไม่ถูกใช้ในการค้นหา (resolve) การอ้างอิง

```json
"libraries": [ "bin/MyHelper.dll" ]
```

- อย่าใส่ DLL ที่มาพร้อมกับเกมอยู่แล้ว (Unity, Mirror, Newtonsoft.Json, 0Harmony ฯลฯ) สำเนาของเกมจะโหลดก่อน ดังนั้นสำเนาที่ม็อดของคุณใส่มาจะถูกเพิกเฉยหรือเกิดการชนกัน
- ไม่สามารถใช้ DLL แบบเนทีฟ (C/C++) ได้ จะโหลดเฉพาะแอสเซมบลีแบบ managed เท่านั้น
- หากแอสเซมบลีชื่อเดียวกันถูกโหลดโดยม็อดอื่นไปแล้ว เกมจะไม่เลือกตัวใดตัวหนึ่ง แต่จะ**ระงับการรัน** การแยกไลบรารีที่ใช้ร่วมกันออกเป็นม็อดต่างหากแล้วเชื่อมผ่าน `dependencies` จะปลอดภัยกว่า


## การบิลด์และการทดสอบ

```bash
dotnet build -c Release
```

- เมื่อบิลด์สำเร็จและ DLL ปรากฏใน `WorkshopLocal/<mod>/bin/` ให้เปิดเกม ตรวจสอบว่าม็อดของคุณแสดงอยู่ใต้ "ม็อดของฉัน" ในแผง Steam Workshop บนหน้าจอไตเติล แล้วเริ่มเกม
- หากคุณเก็บ `.pdb` ไว้คู่กับไฟล์บิลด์ สแต็กเทรซในล็อกจะมีหมายเลขบรรทัด ให้เอาออกเมื่อเผยแพร่


## การอัปเดตเกมและความเข้ากันได้

เมื่อเกมอัปเดต `Assembly-CSharp.dll` จะเปลี่ยนไป ม็อดที่ใช้เฉพาะอีเวนต์อย่างเป็นทางการมักจะยังทำงานได้ แต่ม็อดที่แตะต้องคลาสภายในโดยตรงหรือแพตช์ด้วย Harmony อาจพังได้

- หลังอัปเดต ให้บิลด์ใหม่โดยอ้างอิงโฟลเดอร์ `Managed/` ใหม่แล้วทดสอบ
- หากคุณระบุ**เวอร์ชันเกมที่รองรับ**ไว้ในคำอธิบายหน้า Workshop ผู้ใช้จะหาสาเหตุได้อย่างรวดเร็ว เวอร์ชันเกมอาจถูกบันทึกโดยอัตโนมัติใน metadata ของไอเทม Workshop เมื่อคุณเผยแพร่ด้วย
- หากคุณเปลี่ยนโค้ด ม็อดต้องผ่านการตรวจสอบอีกครั้งก่อนจึงจะเผยแพร่ได้ ดูที่ [การเผยแพร่บน Workshop](publishing.md)


## ข้อควรพิจารณาสำหรับโหมดผู้เล่นหลายคน

- ม็อดที่มีโค้ดส่วนใหญ่จะตั้งค่า `multiplayer.installation` เป็น `everyone`
- หากม็อดของคุณเปลี่ยนสถานะเกมเฉพาะในอีเวนต์ `Serverside` และค่าที่เปลี่ยนเป็นสถานะเกมที่ถูกซิงก์อยู่แล้ว (เช่น `customStats`) ทำให้ผู้เข้าร่วมไม่ต้องมีโค้ดหรือการลงทะเบียนใด ๆ คุณสามารถประกาศเป็น `host_only` ได้ ผู้เข้าร่วมจะไม่ต้องดาวน์โหลดม็อด ม็อดที่ลงทะเบียนไอเทมใหม่ ใช้ `RegisterNetworkPrefab` หรือเปลี่ยนสิ่งใดในอีเวนต์ `Clientside` ไม่สามารถเป็น `host_only` ได้
- ออบเจ็กต์ที่เกิดผ่านเครือข่าย (ยูนิต กระสุน ฯลฯ) ต้องลงทะเบียนด้วย `RegisterNetworkPrefab` เพื่อให้ถูกสร้างในฝั่งผู้เข้าร่วมด้วย หากคุณส่ง `stableKey` ID เดิมจะถูกคงไว้แม้เวอร์ชันม็อดของคุณจะเปลี่ยน
- โฮสต์และผู้เข้าร่วมต้องมี**ลำดับการโหลด**ม็อดเหมือนกันด้วย หลีกเลี่ยงโค้ดที่ผลลัพธ์ขึ้นอยู่กับลำดับนั้น


## การแก้ไขปัญหา

| อาการ | สิ่งที่ต้องตรวจสอบ |
|---|---|
| สรุปสถานะแจ้งว่าหาคลาสจุดเริ่มต้นไม่พบ | namespace หรือชื่อคลาสใน `entryClass` ไม่ตรงกับโค้ดของคุณ คลาสซ้อนเขียนเป็น `Outer+Inner` |
| สรุปสถานะแสดงข้อผิดพลาดชนิดของคลาสจุดเริ่มต้น | คลาสจุดเริ่มต้นไม่ได้สืบทอดจาก `WorkshopModBase` (หรือ `HorayModBase`) ไม่ใช่ public เป็น abstract หรือไม่มีคอนสตรักเตอร์ public ที่ไม่มีพารามิเตอร์ |
| สรุปสถานะแจ้งว่าม็อดถูกระงับเนื่องจากชื่อแอสเซมบลีชนกัน | ม็อดอื่น (รวมถึง AddOns) ใช้ `AssemblyName` เดียวกัน เปลี่ยนชื่อของคุณ |
| สรุปสถานะแสดงว่าโหลดไลบรารีล้มเหลว | DLL ที่อยู่ใน `libraries` หายไปหรือไม่ใช่แอสเซมบลีแบบ managed |
| `FileNotFoundException` ในล็อก | DLL ที่อ้างอิงไม่อยู่ในโฟลเดอร์ม็อดหรือไม่ได้ประกาศใน `libraries` หรือ DLL ของเกมถูกคัดลอกด้วย `Private=true` และทั้งสองชนกัน |
| เกิด Exception ทันทีหลังเข้าสู่โลก | มี Exception ในตัวจัดการอีเวนต์ฐานข้อมูล ค้นหา namespace ของม็อดคุณในสแต็กเทรซของล็อก |
| เอฟเฟกต์ซ้อนทับกันหลังกลับไปหน้าไตเติลแล้วเข้าใหม่ | คุณสมัครรับอีเวนต์ซ้ำภายในตัวจัดการ สมัครรับเพียงครั้งเดียวใน `OnModLoaded` |


## ตัวอย่าง

[`examples/dll-sample`](../../examples/dll-sample) คือม็อดขนาดเล็กที่สุดที่เพิ่มความเสียหายกายภาพ 5 หน่วยเมื่อผู้เล่นเกิด โฟลเดอร์ `package/` มี DLL ที่บิลด์ไว้แล้ว ดังนั้นการคัดลอกโฟลเดอร์นั้นไปไว้ใน `WorkshopLocal/` จะทำให้ใช้งานได้ทันทีโดยไม่ต้องบิลด์ หากต้องการเปลี่ยนเป็นม็อดของคุณเอง คุณเพียงเปลี่ยน `AssemblyName`, `modId`, `entryClass` และเส้นทางเอาต์พุต เริ่มเกมแล้วเปิดหน้าต่างค่าสถานะเพื่อดูว่าทำงานเป็น ความเสียหายกายภาพ +5
