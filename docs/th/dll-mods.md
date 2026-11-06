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
- ไม่รองรับโหลดเดอร์ภายนอก เช่น BepInEx และ MelonLoader ม็อดที่ทำงานเหมือนโหลดเดอร์ภายนอก (คือค้นหาและรัน DLL ของปลั๊กอินจากนอก Workshop) จะมีคำเตือนแนบมา และจะเปิดใช้งานได้ก็ต่อเมื่อผู้ใช้ยอมรับเท่านั้น ดูรายละเอียดที่ [ม็อดที่รันโค้ดจากนอก Workshop](#ม็อดที่รันโค้ดจากนอก-workshop)


## DLL ช่วยเหลือ

หากม็อดของคุณใช้ไลบรารีอื่น (เช่น แพ็กเกจจากบุคคลที่สามนอกเหนือจาก Newtonsoft.Json) ให้วาง DLL ของไลบรารีเหล่านั้นในโฟลเดอร์ม็อดและประกาศใน `libraries` ของ `metadata.json` DLL ที่ไม่ได้ประกาศจะไม่ถูกใช้ในการค้นหา (resolve) การอ้างอิง

```json
"libraries": [ "bin/MyHelper.dll" ]
```

- อย่าใส่ DLL ที่มาพร้อมกับเกมอยู่แล้ว (Unity, Mirror, Newtonsoft.Json, 0Harmony ฯลฯ) สำเนาของเกมจะโหลดก่อน ดังนั้นสำเนาที่ม็อดของคุณใส่มาจะถูกเพิกเฉยหรือเกิดการชนกัน
- ไม่สามารถใช้ DLL แบบเนทีฟ (C/C++) ได้ จะโหลดเฉพาะแอสเซมบลีแบบ managed เท่านั้น
- หากแอสเซมบลีชื่อเดียวกันถูกโหลดโดยม็อดอื่นไปแล้ว สำหรับ DLL ใน `modules` เกมจะไม่เลือกตัวใดตัวหนึ่ง แต่จะ**ระงับการรัน** ส่วน DLL ใน `libraries` เกมจะใช้ตัวที่โหลดไว้แล้วและเพิกเฉยสำเนาที่คุณใส่มา ซึ่งอาจทำงานผิดพลาดได้หากเวอร์ชันต่างกัน การแยกไลบรารีที่ใช้ร่วมกันออกเป็นม็อดต่างหากแล้วเชื่อมผ่าน `dependencies` จะปลอดภัยกว่า

### การใช้ไลบรารีของม็อดอื่น

หากม็อดหลายตัวใช้โค้ดเดียวกัน ให้นำโค้ดนั้นไปไว้ในม็อดหลักตัวเดียว แล้วให้ม็อดย่อยเชื่อมกับม็อดหลักผ่าน `dependencies`

1. ม็อดหลักประกาศโค้ดที่ใช้ร่วมกันไว้ใน `modules` หรือ `libraries` และใส่ `Library` ใน `tags`
2. ม็อดย่อยระบุ `modId` ของม็อดหลักไว้ใน `dependencies` และบิลด์โดยอ้างอิง DLL ของม็อดหลักด้วย `Private=false` **อย่าคัดลอก DLL ของม็อดหลักไปใส่ในม็อดย่อย**
3. เกมจะโหลดม็อดหลักก่อน ดังนั้นการอ้างอิงของม็อดย่อยจะถูกแก้ไปยังแอสเซมบลีของม็อดหลักที่โหลดไว้แล้ว

```json
"dependencies": [
  { "modId": "someone.shared-runtime", "workshopId": "123456789" }
]
```

- ม็อดย่อยก็ต้องมีโมดูล `dll` และ `entryClass` ของตัวเองด้วย โครงสร้างที่ม็อดหลักไปค้นหาและโหลด DLL ของม็อดย่อยเองไม่ได้รับการรองรับ ให้ทำโดยให้ม็อดย่อยเรียกฟังก์ชันลงทะเบียนของม็อดหลักจาก `OnModLoaded` ของตัวเองแทน
- หากม็อดหลักเปลี่ยนชนิดหรือสมาชิกสาธารณะในการอัปเดต ม็อดย่อยอาจใช้งานไม่ได้ ควรระวังไม่ลบหรือเปลี่ยน API สาธารณะที่มีอยู่ของไลบรารีที่ใช้ร่วมกัน

### อย่าสร้าง DLL ระหว่างรันไทม์!

โค้ดทั้งหมดที่ม็อดรันต้องอยู่ในไอเทม Workshop ที่ผ่านการตรวจสอบแล้ว หากไฟล์ DLL ในม็อดเปลี่ยนไประหว่างที่ม็อดทำงาน การโหลดม็อดนั้นจะถูกจำกัด ให้ใส่ DLL ทั้งหมดไว้ในไอเทม Workshop ตามสภาพเดิม โดยไม่มีการบีบอัดหรือสตรีมมิงใด ๆ กล่าวคือ กรณีต่อไปนี้อาจละเมิดระบบตรวจสอบ ให้ใส่ DLL ทั้งหมดที่ต้องใช้ไว้ในม็อดตั้งแต่ตอนอัปโหลด

- ม็อดดาวน์โหลด DLL จากภายนอกระหว่างทำงานแล้วโหลดแอสเซมบลี
- ม็อดสร้างและรัน DLL ของตัวเองระหว่างทำงาน (รวมถึงการเขียนทับ DLL อื่นด้วย Mono.Cecil หรือเครื่องมือคล้ายกันแล้วนำมารัน)
- ม็อดแตก DLL ที่บรรจุไว้ภายในด้วยรูปแบบการบีบอัดของตัวเองแล้วนำมาใช้

### ม็อดที่รันโค้ดจากนอก Workshop

ม็อดที่ค้นหาและรัน DLL ที่ผู้ใช้วางไว้**นอกโฟลเดอร์ม็อด** เช่น ในโฟลเดอร์ติดตั้งเกมหรือ `AppData` (เช่น ม็อดที่ทำให้เข้ากันได้กับปลั๊กอินของโหลดเดอร์อื่น) จะไม่ถูกปฏิเสธ แต่ระบบตรวจสอบจะแนบ**คำเตือน**ไว้

- เมื่อผู้ใช้เปิดใช้งานม็อดนั้น จะมีหน้าต่างคำเตือนปรากฏขึ้น และม็อดจะเปิดใช้งานได้ก็ต่อเมื่อผู้ใช้ยอมรับ เมื่อม็อดได้รับการอัปเดต ระบบจะถามอีกครั้ง
- DLL ภายนอกที่รันด้วยวิธีนี้ไม่ผ่านการตรวจสอบ และเราไม่รองรับปัญหาที่เกิดจาก DLL เหล่านั้น
- การที่ม็อดดาวน์โหลด DLL ดังกล่าวเองระหว่างทำงานเข้าข่ายข้อห้ามข้างต้น

หากต้องการใช้โค้ดของม็อดอื่น อย่าโหลด DLL จากโฟลเดอร์ของม็อดนั้นโดยตรง ให้เชื่อมผ่าน `dependencies` ตามที่อธิบายไว้ใน [การใช้ไลบรารีของม็อดอื่น](#การใช้ไลบรารีของม็อดอื่น) DLL ที่เกมไม่ได้โหลดเองจะข้ามลำดับการโหลด การตรวจสอบชื่อชนกัน และการตรวจสอบ dependencies จึงไม่ได้รับการรองรับ

หากจำเป็นต้องแก้ไข DLL (เช่น แปลง IL ให้ตรงกับ Harmony API อื่น) ให้ทำก่อนอัปโหลด แล้วใส่ DLL ที่แปลงแล้วไว้ในม็อดของคุณ


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
- เมื่อคุณเปลี่ยนโค้ด ต้องส่งม็อดเข้าระบบตรวจสอบอีกครั้ง ดูที่ [การเผยแพร่บน Workshop](publishing.md#การตรวจสอบโค้ด)


## ข้อควรพิจารณาสำหรับโหมดผู้เล่นหลายคน

- ม็อดที่มีโค้ดส่วนใหญ่จะตั้งค่า `multiplayer.installation` เป็น `everyone`
- หากม็อดของคุณเปลี่ยนสถานะเกมเฉพาะในอีเวนต์ `Serverside` และค่าที่เปลี่ยนเป็นสถานะเกมที่ถูกซิงก์อยู่แล้ว (เช่น `customStats`) ทำให้ผู้เข้าร่วมไม่ต้องมีโค้ดหรือการลงทะเบียนใด ๆ คุณสามารถประกาศเป็น `host_only` ได้ ผู้เข้าร่วมจะไม่ต้องดาวน์โหลดม็อด ม็อดที่ลงทะเบียนไอเทมใหม่ ใช้ `RegisterNetworkPrefab` หรือเปลี่ยนสิ่งใดในอีเวนต์ `Clientside` ไม่สามารถเป็น `host_only` ได้
- ออบเจ็กต์ที่เกิดผ่านเครือข่าย (ยูนิต กระสุน ฯลฯ) ต้องลงทะเบียนด้วย `RegisterNetworkPrefab` เพื่อให้ถูกสร้างในฝั่งผู้เข้าร่วมด้วย หากคุณส่ง `stableKey` ID เดิมจะถูกคงไว้แม้เวอร์ชันม็อดของคุณจะเปลี่ยน
- โฮสต์และผู้เข้าร่วมต้องมี**ลำดับการโหลด**ม็อดเหมือนกันด้วย หลีกเลี่ยงโค้ดที่ผลลัพธ์ขึ้นอยู่กับลำดับนั้น


## การซิงก์เครือข่าย (SyncVar, RPC, Command)

ระบบผู้เล่นหลายคนของ Sephiria สร้างขึ้นด้วยไลบรารีเครือข่ายโอเพนซอร์ส 'Mirror' ใน Mirror คุณสามารถใช้งานระบบสื่อสารได้อย่างง่ายดายโดยประกาศแอตทริบิวต์ต่อไปนี้

- `[SyncVar]`: แอตทริบิวต์ที่ติดกับตัวแปรสมาชิกของคลาส ใช้ซิงก์ค่าตัวแปรระหว่างเซิร์ฟเวอร์กับไคลเอนต์
- `[ClientRpc]`, `[TargetRpc]`: แอตทริบิวต์ที่ติดกับฟังก์ชัน ใช้ขอให้เซิร์ฟเวอร์สั่งให้ไคลเอนต์รันฟังก์ชัน
- `[Command]`: แอตทริบิวต์ที่ติดกับฟังก์ชัน ใช้ขอให้ไคลเอนต์สั่งให้เซิร์ฟเวอร์รันฟังก์ชัน

ในสภาพแวดล้อมของม็อด คุณไม่สามารถใช้แอตทริบิวต์ข้างต้นได้ตามปกติ แอตทริบิวต์เหล่านี้จะทำงานได้ก็ต่อเมื่อ Weaver ของ Mirror แทรกโค้ดส่งข้อมูลที่ตรงกับแต่ละแอตทริบิวต์ในตอนคอมไพล์ แต่ DLL ที่สร้างจากภายนอกไม่ผ่าน Weaver ดังนั้นแม้บิลด์จะสำเร็จ แอตทริบิวต์ข้างต้นก็ไม่ทำงานเลย บั๊กลักษณะนี้จะดูปกติบนหน้าจอของโฮสต์ และมีปัญหาเฉพาะบนหน้าจอของผู้เข้าร่วม จึงตรวจพบได้ยากหากทดสอบคนเดียว (และต่อให้บังคับให้ผ่าน Weaver ก็ยังอาจมีบั๊ก เช่น แฮชของฟังก์ชันชนกัน)


### `SyncVar<T>`, `ModRpc`

เพื่อแก้ปัญหานี้ Sephiria จึงมีโค้ดสื่อสารสำหรับม็อดที่ใช้แทนแอตทริบิวต์ข้างต้น คือ `SyncVar<T>` และ `ModRpc`

| สิ่งที่ต้องการทำ | สิ่งที่ควรใช้ |
|---|---|
| ซิงก์ค่าให้ตรงกันอยู่เสมอ (รวมถึงผู้เข้าร่วมที่เข้ามาทีหลัง) | `SyncVar<T>` |
| เรียกจากเซิร์ฟเวอร์ → ไคลเอนต์ทั้งหมด | `ModRpc.Send` |
| เรียกจากเซิร์ฟเวอร์ → ไคลเอนต์ที่ระบุ | `ModRpc.SendTo` |
| เรียกจากไคลเอนต์ → เซิร์ฟเวอร์ | `ModRpc.SendCommand` |
| เปลี่ยนค่าที่เกมซิงก์อยู่แล้ว | พร็อพเพอร์ตี้ `Network<FieldName>` |

- ฟีเจอร์นี้ใช้ได้ตั้งแต่เกมเวอร์ชัน 1.1.2 เป็นต้นไป
- ม็อดที่ใช้ฟีเจอร์นี้ให้ประกาศ `multiplayer.installation` เป็น `everyone`


### การเตรียมคอมโพเนนต์

`SyncVar<T>` และ `ModRpc` ใช้ได้เฉพาะในคอมโพเนนต์ที่สืบทอดจาก `NetworkBehaviour` เท่านั้น คอมโพเนนต์นี้ต้องติดอยู่กับออบเจ็กต์เดียวกันทั้งบนโฮสต์และผู้เข้าร่วม **ในลำดับเดียวกัน** หากนำไปติดด้วย `AddComponent` กับออบเจ็กต์ที่เกิดขึ้นแล้ว คอมโพเนนต์นั้นจะไม่ถูกนับเป็นเป้าหมายของการซิงก์ ดังนั้นต้องติดไว้ให้เรียบร้อยก่อนลงทะเบียนเป็น network prefab

```csharp
private static GameObject prefab;

// สมัครรับ HorayModAPI.OnAllDatabasesReady ใน OnModLoaded
private static void RegisterPrefab()
{
    if (prefab) return;

    // วางไว้ใต้ออบเจ็กต์แม่ที่ปิดใช้งาน Awake ของ prefab จะไม่ทำงาน และสำเนาที่ Instantiate จะเริ่มต้นแบบเปิดใช้งาน
    var holder = new GameObject("MyMod_Prefabs");
    holder.SetActive(false);
    Object.DontDestroyOnLoad(holder);

    prefab = new GameObject("FlameOrb");
    prefab.transform.SetParent(holder.transform);
    prefab.AddComponent<NetworkIdentity>();
    prefab.AddComponent<FlameOrb>();
    HorayModAPI.RegisterNetworkPrefab(prefab, "yourname.my-mod:flame-orb");
}

// สร้าง (spawn) บนเซิร์ฟเวอร์ หากส่งอาร์กิวเมนต์ตัวที่สอง ผู้เล่นคนนั้นจะเป็นเจ้าของ
private static void SpawnFor(PlayerAvatar player)
{
    var orb = Object.Instantiate(prefab);
    NetworkServer.Spawn(orb, player.connectionToClient);
}
```

### `SyncVar<T>`: การซิงก์ค่า

```csharp
public class FlameOrb : NetworkBehaviour
{
    public readonly SyncVar<int> stack = new SyncVar<int>(0);

    public FlameOrb()
    {
        // เนื่องจากไม่มี Weaver จึงต้องลงทะเบียนเองในคอนสตรักเตอร์ ลำดับของฟิลด์ต้องเหมือนกันเสมอ
        InitSyncObject(stack);
    }

    private void Awake()
    {
        stack.Callback += (oldValue, newValue) => UpdateVisual(newValue);
    }

    private void UpdateVisual(int value) { }
}
```

- เปลี่ยนค่า**บนเซิร์ฟเวอร์** เช่น `stack.Value = 3` ค่าที่ผู้เข้าร่วมเปลี่ยนจะไม่ถูกส่งไปยังเซิร์ฟเวอร์
- ผู้เข้าร่วมที่เข้ามาทีหลังก็จะได้รับค่าปัจจุบันตอนเข้าร่วม
- `Callback` จะถูกเรียกบนโฮสต์เมื่อกำหนดค่า และบนผู้เข้าร่วมเมื่อได้รับค่า หากค่าที่ได้รับตอนเข้าร่วมต่างจากค่าเริ่มต้น ก็จะถูกเรียกในตอนนั้นด้วย Exception ที่เกิดในคอลแบ็กจะถูกบันทึกลงล็อก และการซิงก์ยังดำเนินต่อไป
- ใช้ชนิดข้อมูลพื้นฐาน เช่น `int`, `float`, `string`, `Vector3` ได้ สำหรับ struct ที่ม็อดสร้างขึ้นเอง โค้ดซีเรียลไลซ์จะไม่ถูกสร้างให้อัตโนมัติ ให้แยกเป็นหลายฟิลด์แทน
- เมื่อต้องการอ้างอิงออบเจ็กต์อื่น ให้ส่ง `netId` ผ่าน `SyncVar<uint>` แทนการใช้ `SyncVar<GameObject>` เพราะฝั่งที่รับอาจยังไม่ได้สร้างออบเจ็กต์นั้น
- จำนวน SyncVar ที่ลงทะเบียนได้ต่อหนึ่งคอมโพเนนต์คือ 64 รายการ รวมของที่สืบทอดมาด้วย

### `ModRpc`: การเรียกฟังก์ชัน

ลงทะเบียนฟังก์ชันหนึ่งครั้งตอนโหลดม็อด ชื่อที่ใช้ตอนเรียกคือ `modId:functionName`

```csharp
protected override void OnModLoaded()
{
    ModRpc.RegisterRpc<FlameOrb>("yourname.my-mod", "explode", (orb, r) => orb.PlayExplosion(r.ReadVector3()));
    ModRpc.RegisterCommand<FlameOrb>("yourname.my-mod", "ignite", (orb, r, sender) => orb.ServerIgnite(r.ReadInt(), sender));
}
```

```csharp
// เซิร์ฟเวอร์ → ไคลเอนต์ทั้งหมดที่มองเห็นออบเจ็กต์นี้
ModRpc.Send(this, "yourname.my-mod:explode", w => w.WriteVector3(position));

// เซิร์ฟเวอร์ → ไคลเอนต์หนึ่งราย หากส่ง null เป็นการเชื่อมต่อ จะส่งไปยังเจ้าของออบเจ็กต์
ModRpc.SendTo(connectionToClient, this, "yourname.my-mod:explode", w => w.WriteVector3(position));

// ไคลเอนต์ → เซิร์ฟเวอร์
ModRpc.SendCommand(this, "yourname.my-mod:ignite", w => w.WriteInt(slot));
```

- อ่านตามลำดับที่เขียน หากลำดับหรือชนิดข้อมูลของการเขียนและการอ่านไม่ตรงกัน ค่าจะเพี้ยนหรือเกิด Exception
- โดยค่าเริ่มต้น `RegisterCommand` เรียกได้เฉพาะ**เจ้าของออบเจ็กต์**เท่านั้น หากต้องการรับจากออบเจ็กต์ที่ไม่มีเจ้าของ ให้ส่ง `requiresAuthority: false` ในกรณีนี้ผู้เข้าร่วมคนใดก็เรียกได้ จึงต้องระวังเรื่องการโกง
- หากส่งผิดทิศทางจากที่ลงทะเบียนไว้ หรือส่งด้วยชื่อที่ไม่ได้ลงทะเบียน จะมีเพียงล็อกข้อผิดพลาดและจะไม่มีการส่งข้อมูล
- หากฝั่งที่รับไม่มีฟังก์ชันนั้นหรือชนิดของคอมโพเนนต์ต่างกัน จะบันทึกเพียงล็อกคำเตือนแล้วเพิกเฉย Exception ที่เกิดในตัวจัดการก็จะถูกบันทึกลงล็อกและเกมดำเนินต่อไป
- ช่องทางเริ่มต้นคือการส่งแบบเชื่อถือได้ (reliable) หากเป็นเอฟเฟกต์ที่ส่งถี่ ๆ คุณสามารถส่งอาร์กิวเมนต์ `channelId: Channels.Unreliable` ได้

### สิ่งที่ต้องปฏิบัติตามอย่างเคร่งครัด

- ไคลเอนต์สามารถส่งค่าใดก็ได้ผ่าน Command ให้ตรวจสอบหมายเลขช่อง จำนวน เป้าหมาย ฯลฯ ในตัวจัดการฝั่งเซิร์ฟเวอร์
- ผู้เข้าร่วมที่เข้ามาทีหลังจะไม่ได้รับ RPC ที่ส่งก่อนเข้าร่วม สถานะที่ต้องคงไว้ให้ส่งผ่าน `SyncVar<T>` และใช้ RPC กับเรื่องที่เกิดขึ้นแล้วผ่านไป เช่น เสียงและเอฟเฟกต์
- **RPC อาจมาถึงก่อน SyncVar** หากอ่าน `stack.Value` ในตัวจัดการ RPC อาจได้ค่าเก่า หากต้องการค่าใหม่ ให้ส่งมาพร้อมเป็นอาร์กิวเมนต์ของ RPC หรือจัดการใน `Callback`

### การเปลี่ยนค่าในโค้ดของเกม Sephiria

หากกำหนดค่าตรง ๆ ให้ฟิลด์ `[SyncVar]` ของคลาสในเกม ค่านั้นจะไม่ถูกส่งไปยังผู้เข้าร่วม ให้ใช้พร็อพเพอร์ตี้ที่มี `Network` นำหน้า ตัวพิมพ์เล็กใหญ่เหมือนชื่อฟิลด์ทุกประการ

```csharp
unit.maxHp = 500;         // เปลี่ยนเฉพาะค่าบนโฮสต์
unit.NetworkmaxHp = 500;  // ส่งไปยังผู้เข้าร่วมด้วย
```

ก่อนที่ออบเจ็กต์จะเกิด (prefab หรือทันทีหลัง `Instantiate`) การกำหนดค่าตรง ๆ ก็จะถูกส่งไปพร้อมกันตอนเกิด

### การทดสอบ

ปัญหาข้างต้นจะไม่ปรากฏเมื่อโฮสต์อยู่คนเดียว ก่อนเผยแพร่ ให้ใช้คอมพิวเตอร์สองเครื่องตรวจสอบหน้าจอทั้งฝั่งโฮสต์และผู้เข้าร่วม และทดสอบไปถึงกรณีที่เปลี่ยนค่าแล้วมีคนเข้าร่วมทีหลังด้วย หากเปิดแพ็กเกจในเครื่อง (`WorkshopLocal`) คนอื่นจะเข้าห้องไม่ได้ ดังนั้นการทดสอบผู้เล่นหลายคนให้ทำโดยสมัครรับไอเทมที่อัปโหลดขึ้น Workshop เป็น Unlisted ไอเทมที่อัปโหลดครั้งแรกจะเป็น Unlisted โดยอัตโนมัติ


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
