<!-- ไฟล์ที่สร้างอัตโนมัติ Tools/ModApiDocGen ในที่เก็บของเกมสร้างจากคอมเมนต์ /// ในซอร์ส อย่าแก้ไขโดยตรง ให้แก้คอมเมนต์ในซอร์สแล้วสร้างใหม่ -->
# อ้างอิง API

รายการชนิดและสมาชิกสาธารณะที่ม็อด DLL ใช้ได้ การตั้งค่าโปรเจกต์ จุดเริ่มต้น วงจรชีวิต และกฎของโหมดผู้เล่นหลายคนอยู่ใน [ม็อด DLL](../dll-mods.md)

สร้างเมื่อ 2026-10-26 สำหรับเกมเวอร์ชัน **1.1.0** เอกสารนี้จะถูกสร้างใหม่ทุกครั้งที่เกมอัปเดต

| เอกสาร | ชนิด | เนื้อหา |
|---|---|---|
| [HorayModAPI](HorayModAPI.md) | `HorayModAPI`, `HorayModLocalizationContext` | จุดเริ่มต้นแบบ static ที่เกมเปิดให้ม็อดใช้ |
| [จุดเริ่มต้นและคอนเท็กซ์](ModBase.md) | `WorkshopModBase`, `WorkshopModContext`, `HorayModBase`, `WorkshopDataPackage`, `WorkshopModDefinition`, `WorkshopModuleDefinition`, `WorkshopDependency`, `WorkshopModuleType`, `WorkshopInstallation` | คลาสฐานที่คลาสจุดเริ่มต้นของม็อดสืบทอด รวมถึงชนิดคอนเท็กซ์และ manifest ที่โหลดเดอร์แนบมา |
| [ตัวเลือกม็อด](ModOptions.md) | `ModOptions`, `ModOptionsPage`, `ModOptionDefinition`, `ModOption<T>`, `ModToggleOption`, `ModSelectionOption`, `ModSliderOption`, `ModOptionText`, `ModOptionKind`, `ModOptionsRegistry` | การตั้งค่าม็อดต่ออุปกรณ์ และ API สำหรับเพิ่มสวิตช์ กล่องเลือก และแถบเลื่อนในหน้าจอตัวเลือกของเกม |
| [การกำหนดการเกิดในห้อง](RoomSpawn.md) | `RoomSpawnAssignContext`, `ERoomSpawnKind` | คอนเท็กซ์ที่ส่งโดยอีเวนต์ `HorayModAPI.OnAssignRoomSpawnPhases` |
| [ค่าสถานะพิเศษ](SpecialStats.md) | `SpecialStatContext`, `SpecialStatValue`, `SpecialStatCategoryEntity`, `SpecialStatCategoryVisibility`, `SpecialStatDisplaySettings`, `SpecialStatValueSource`, `SpecialStatLabelColor`, `SpecialStatValueProvider` | ชนิดสำหรับการแสดงค่าสถานะที่ลงทะเบียนด้วย `RegisterSpecialStatCategory` และ `RegisterSpecialStat` ใน `HorayModAPI.OnLoadSpecialStatDatabase` |
| [UI](UI.md) | `UIWidgetFactory`, `UIWidgetTextStyle`, `DialoguePage`, `DialogueChoice`, `DialogueSession`, `DialogueCloseReason`, `ItemInteractionOptions`, `ItemInteractionSession`, `ItemInteractionItem`, `ItemInteractionCloseReason`, `ItemInteractionItemSource` | ชนิดสำหรับบทสนทนา การโต้ตอบไอเทม และวิดเจ็ตที่ใช้โดย `HorayModAPI.TryShowDialogue`, `TryShowItemInteraction` และ `TryGetUIWidgets` |

## ภาพรวมอีเวนต์ของ HorayModAPI

เรียงตามลำดับที่ประกาศในซอร์ส อีเวนต์ฐานข้อมูลจะทำงานอีกครั้งทุกครั้งที่โลกโหลด

| อีเวนต์ | ลายเซ็น | คำอธิบาย |
|---|---|---|
| [`OnLoadMiracleDatabase`](HorayModAPI.md#onloadmiracledatabase) | `Action OnLoadMiracleDatabase` | ทำงานทันทีหลังฐานข้อมูลปาฏิหาริย์เริ่มต้นเสร็จ |
| [`OnLoadWeaponDatabase`](HorayModAPI.md#onloadweapondatabase) | `Action OnLoadWeaponDatabase` | ทำงานทันทีหลังฐานข้อมูลอาวุธเริ่มต้นเสร็จ |
| [`OnLoadStatusDatabase`](HorayModAPI.md#onloadstatusdatabase) | `Action OnLoadStatusDatabase` | ทำงานทันทีหลังฐานข้อมูลสถานะ (เอฟเฟกต์ค่าสถานะ) เริ่มต้นเสร็จ |
| [`OnLoadSpecialStatDatabase`](HorayModAPI.md#onloadspecialstatdatabase) | `Action OnLoadSpecialStatDatabase` | ทำงานทันทีหลังหมวดหมู่และสถานะของค่าสถานะพิเศษโหลดเสร็จ |
| [`OnLoadItemDatabase`](HorayModAPI.md#onloaditemdatabase) | `Action OnLoadItemDatabase` | ทำงานทันทีหลังฐานข้อมูลไอเทมเริ่มต้นเสร็จ |
| [`OnLoadKeywordDatabase`](HorayModAPI.md#onloadkeyworddatabase) | `Action OnLoadKeywordDatabase` | ทำงานทันทีหลังฐานข้อมูลคีย์เวิร์ดเริ่มต้นเสร็จ |
| [`OnLoadPassiveDatabase`](HorayModAPI.md#onloadpassivedatabase) | `Action OnLoadPassiveDatabase` | ทำงานทันทีหลังฐานข้อมูลแพสซีฟเริ่มต้นเสร็จ |
| [`OnLoadCostumeDatabase`](HorayModAPI.md#onloadcostumedatabase) | `Action OnLoadCostumeDatabase` | ทำงานทันทีหลังฐานข้อมูลชุดแต่งกายเริ่มต้นเสร็จ |
| [`OnLoadPropDatabase`](HorayModAPI.md#onloadpropdatabase) | `Action OnLoadPropDatabase` | ทำงานทันทีหลังฐานข้อมูลพร็อปเริ่มต้นเสร็จ |
| [`OnLoadUnitDatabase`](HorayModAPI.md#onloadunitdatabase) | `Action OnLoadUnitDatabase` | ทำงานทันทีหลังฐานข้อมูลยูนิต (เอนทิตีการเกิดของศัตรูและ NPC) เริ่มต้นเสร็จ |
| [`OnLoadSocialIDDatabase`](HorayModAPI.md#onloadsocialiddatabase) | `Action OnLoadSocialIDDatabase` | ทำงานทันทีหลังฐานข้อมูลบทบาท NPC เริ่มต้นเสร็จ |
| [`OnLoadCustomGameDatabase`](HorayModAPI.md#onloadcustomgamedatabase) | `Action OnLoadCustomGameDatabase` | ทำงานทันทีหลังฐานข้อมูลเกมกำหนดเองเริ่มต้นเสร็จ |
| [`OnAllDatabasesReady`](HorayModAPI.md#onalldatabasesready) | `Action OnAllDatabasesReady` | ทำงานทันทีหลังฐานข้อมูลทั้งหมดเริ่มต้นเสร็จ |
| [`OnTitleMenuReady`](HorayModAPI.md#ontitlemenuready) | `Action<UI_TitleLobby> OnTitleMenuReady` | ถูกเรียกหลังเมนูไตเติลโหลดเสร็จ |
| [`OnStartGameServerside`](HorayModAPI.md#onstartgameserverside) | `Action OnStartGameServerside` | ถูกเรียกเมื่อเกมเริ่ม |
| [`OnStartGameClientside`](HorayModAPI.md#onstartgameclientside) | `Action OnStartGameClientside` | ถูกเรียกเมื่อเกมเริ่ม |
| [`OnStartSessionServerside`](HorayModAPI.md#onstartsessionserverside) | `Action<bool> OnStartSessionServerside` | ถูกเรียกเมื่อเซสชันเกมเริ่ม |
| [`OnStartSessionClientside`](HorayModAPI.md#onstartsessionclientside) | `Action<bool> OnStartSessionClientside` | ถูกเรียกเมื่อเซสชันเกมเริ่ม |
| [`OnLocalizationReady`](HorayModAPI.md#onlocalizationready) | `Action<HorayModLocalizationContext> OnLocalizationReady` | ถูกเรียกหลังไฟล์แปลภาษาโหลดเสร็จ |
| [`OnFloorAllocatedServerside`](HorayModAPI.md#onfloorallocatedserverside) | `Action<string, string, FloorGenerator> OnFloorAllocatedServerside` | ถูกเรียกเมื่อมีการจัดสรรชั้น |
| [`OnFloorAllocatedClientside`](HorayModAPI.md#onfloorallocatedclientside) | `Action<string, string, FloorGenerator> OnFloorAllocatedClientside` | ถูกเรียกเมื่อมีการจัดสรรชั้น |
| [`OnAssignRoomSpawnPhases`](HorayModAPI.md#onassignroomspawnphases) | `Action<RoomSpawnAssignContext> OnAssignRoomSpawnPhases` | ถูกเรียกหลังองค์ประกอบศัตรูแบบสุ่มของด่านถูกกำหนดแล้ว |
| [`OnPlayerAvatarSpawnedServerside`](HorayModAPI.md#onplayeravatarspawnedserverside) | `Action<PlayerAvatar> OnPlayerAvatarSpawnedServerside` | ถูกเรียกเมื่อผู้เล่นเกิด |
| [`OnPlayerAvatarSpawnedClientside`](HorayModAPI.md#onplayeravatarspawnedclientside) | `Action<PlayerAvatar> OnPlayerAvatarSpawnedClientside` | ถูกเรียกเมื่ออวตารของผู้เล่นเกิด |
| [`OnUnitSpawnedServerside`](HorayModAPI.md#onunitspawnedserverside) | `Action<UnitAvatar> OnUnitSpawnedServerside` | ทำงานเมื่อยูนิต (ผู้เล่น ศัตรู สิ่งอัญเชิญ หรือสหาย) เกิดบนเซิร์ฟเวอร์ |
| [`OnUnitDespawnedServerside`](HorayModAPI.md#onunitdespawnedserverside) | `Action<UnitAvatar> OnUnitDespawnedServerside` | ทำงานทันทีก่อนยูนิตถูกนำออกจากเซิร์ฟเวอร์ |
| [`OnPlayerEvade`](HorayModAPI.md#onplayerevade) | `Action<PlayerAvatar, DamageInstance> OnPlayerEvade` | ทำงานเมื่อผู้เล่นหลบการโจมตี |
| [`OnGetDebuff`](HorayModAPI.md#ongetdebuff) | `Func<CharacterDebuff, UnitAvatar, CharacterDebuff> OnGetDebuff` | ให้คุณสลับ prefab ของดีบัฟได้ทันทีก่อนดีบัฟถูกใช้กับยูนิต |
| [`GridInventoryStartPermission`](HorayModAPI.md#gridinventorystartpermission) | `Action<GridInventory, PlayerAvatar> GridInventoryStartPermission` | ทำงานเมื่อเซิร์ฟเวอร์รับสิทธิ์เขียนช่องเก็บของและเริ่มถอดเอฟเฟกต์ไอเทมทั้งหมด |
| [`GridInventoryEndPermission`](HorayModAPI.md#gridinventoryendpermission) | `Action<GridInventory, PlayerAvatar> GridInventoryEndPermission` | ทำงานทันทีหลังเซิร์ฟเวอร์คืนสิทธิ์เขียนและใช้เอฟเฟกต์ไอเทมอีกครั้ง |
