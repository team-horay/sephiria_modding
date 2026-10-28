<!-- 자동 생성 파일입니다. 게임 저장소의 Tools/ModApiDocGen이 소스의 /// 주석에서 만듭니다. 직접 고치지 말고 소스 주석을 고친 뒤 다시 생성하세요. -->
# API 레퍼런스

DLL 모드에서 쓸 수 있는 공개 타입과 멤버의 목록입니다. 프로젝트 설정, 진입점, 생명주기, 멀티플레이 규칙은 [DLL 모드](../dll-mods.md)에 있습니다.

게임 버전 **1.1.0** 기준, 2026-10-26 생성. 게임이 업데이트되면 이 문서도 다시 생성됩니다.

| 문서 | 타입 | 내용 |
|---|---|---|
| [HorayModAPI](HorayModAPI.md) | `HorayModAPI`, `HorayModLocalizationContext` | 게임이 모드에 공개하는 정적 진입점입니다. |
| [진입점과 컨텍스트](ModBase.md) | `WorkshopModBase`, `WorkshopModContext`, `HorayModBase`, `WorkshopDataPackage`, `WorkshopModDefinition`, `WorkshopModuleDefinition`, `WorkshopDependency`, `WorkshopModuleType`, `WorkshopInstallation` | 모드 진입 클래스가 상속하는 베이스 클래스와, 로더가 붙여 주는 컨텍스트·매니페스트 타입입니다. |
| [모드 옵션](ModOptions.md) | `ModOptions`, `ModOptionsPage`, `ModOptionDefinition`, `ModOption<T>`, `ModToggleOption`, `ModSelectionOption`, `ModSliderOption`, `ModOptionText`, `ModOptionKind`, `ModOptionsRegistry` | 기기별로 저장되는 모드 설정값과, 게임 옵션 화면에 토글·선택 상자·슬라이더를 추가하는 API입니다. |
| [방 스폰 배정](RoomSpawn.md) | `RoomSpawnAssignContext`, `ERoomSpawnKind` | `HorayModAPI.OnAssignRoomSpawnPhases` 이벤트가 넘기는 컨텍스트입니다. |
| [전문 능력치](SpecialStats.md) | `SpecialStatContext`, `SpecialStatValue`, `SpecialStatCategoryEntity`, `SpecialStatCategoryVisibility`, `SpecialStatDisplaySettings`, `SpecialStatValueSource`, `SpecialStatLabelColor`, `SpecialStatValueProvider` | `HorayModAPI.OnLoadSpecialStatDatabase`에서 `RegisterSpecialStatCategory`·`RegisterSpecialStat`으로 등록하는 스탯 표시 타입입니다. |
| [UI](UI.md) | `UIWidgetFactory`, `UIWidgetTextStyle`, `DialoguePage`, `DialogueChoice`, `DialogueSession`, `DialogueCloseReason`, `ItemInteractionOptions`, `ItemInteractionSession`, `ItemInteractionItem`, `ItemInteractionCloseReason`, `ItemInteractionItemSource` | `HorayModAPI.TryShowDialogue`, `TryShowItemInteraction`, `TryGetUIWidgets`가 쓰는 대화·아이템 상호작용·위젯 타입입니다. |

## HorayModAPI 이벤트 한눈에 보기

소스에 선언된 순서입니다. 데이터베이스 이벤트는 월드를 로드할 때마다 다시 발생합니다.

| 이벤트 | 시그니처 | 설명 |
|---|---|---|
| [`OnLoadMiracleDatabase`](HorayModAPI.md#onloadmiracledatabase) | `Action OnLoadMiracleDatabase` | 기적 데이터베이스가 초기화된 직후 발생합니다. |
| [`OnLoadWeaponDatabase`](HorayModAPI.md#onloadweapondatabase) | `Action OnLoadWeaponDatabase` | 무기 데이터베이스가 초기화된 직후 발생합니다. |
| [`OnLoadStatusDatabase`](HorayModAPI.md#onloadstatusdatabase) | `Action OnLoadStatusDatabase` | 상태(스탯 효과) 데이터베이스가 초기화된 직후 발생합니다. |
| [`OnLoadSpecialStatDatabase`](HorayModAPI.md#onloadspecialstatdatabase) | `Action OnLoadSpecialStatDatabase` | 전문 능력치 카테고리와 상태가 적재된 직후 발생합니다. |
| [`OnLoadItemDatabase`](HorayModAPI.md#onloaditemdatabase) | `Action OnLoadItemDatabase` | 아이템 데이터베이스가 초기화된 직후 발생합니다. |
| [`OnLoadKeywordDatabase`](HorayModAPI.md#onloadkeyworddatabase) | `Action OnLoadKeywordDatabase` | 키워드 데이터베이스가 초기화된 직후 발생합니다. |
| [`OnLoadPassiveDatabase`](HorayModAPI.md#onloadpassivedatabase) | `Action OnLoadPassiveDatabase` | 패시브 데이터베이스가 초기화된 직후 발생합니다. |
| [`OnLoadCostumeDatabase`](HorayModAPI.md#onloadcostumedatabase) | `Action OnLoadCostumeDatabase` | 코스튬 데이터베이스가 초기화된 직후 발생합니다. |
| [`OnLoadPropDatabase`](HorayModAPI.md#onloadpropdatabase) | `Action OnLoadPropDatabase` | 프롭 데이터베이스가 초기화된 직후 발생합니다. |
| [`OnLoadUnitDatabase`](HorayModAPI.md#onloadunitdatabase) | `Action OnLoadUnitDatabase` | 유닛 데이터베이스(적·NPC 스폰 엔티티)가 초기화된 직후 발생합니다. |
| [`OnLoadSocialIDDatabase`](HorayModAPI.md#onloadsocialiddatabase) | `Action OnLoadSocialIDDatabase` | NPC 역할 데이터베이스가 초기화된 직후 발생합니다. |
| [`OnLoadCustomGameDatabase`](HorayModAPI.md#onloadcustomgamedatabase) | `Action OnLoadCustomGameDatabase` | 사용자 지정 게임 데이터베이스가 초기화된 직후 발생합니다. |
| [`OnAllDatabasesReady`](HorayModAPI.md#onalldatabasesready) | `Action OnAllDatabasesReady` | 모든 데이터베이스 초기화가 끝난 직후 발생합니다. |
| [`OnTitleMenuReady`](HorayModAPI.md#ontitlemenuready) | `Action<UI_TitleLobby> OnTitleMenuReady` | 타이틀 메뉴가 로드된 이후 호출됩니다. |
| [`OnStartGameServerside`](HorayModAPI.md#onstartgameserverside) | `Action OnStartGameServerside` | 게임이 시작될 때 호출됩니다. |
| [`OnStartGameClientside`](HorayModAPI.md#onstartgameclientside) | `Action OnStartGameClientside` | 게임이 시작될 때 호출됩니다. |
| [`OnStartSessionServerside`](HorayModAPI.md#onstartsessionserverside) | `Action<bool> OnStartSessionServerside` | 게임 세션이 시작될 때 호출됩니다. |
| [`OnStartSessionClientside`](HorayModAPI.md#onstartsessionclientside) | `Action<bool> OnStartSessionClientside` | 게임 세션이 시작될 때 호출됩니다. |
| [`OnLocalizationReady`](HorayModAPI.md#onlocalizationready) | `Action<HorayModLocalizationContext> OnLocalizationReady` | 번역 파일이 로드된 후 호출됩니다. |
| [`OnFloorAllocatedServerside`](HorayModAPI.md#onfloorallocatedserverside) | `Action<string, string, FloorGenerator> OnFloorAllocatedServerside` | 층 할당시 호출됩니다. |
| [`OnFloorAllocatedClientside`](HorayModAPI.md#onfloorallocatedclientside) | `Action<string, string, FloorGenerator> OnFloorAllocatedClientside` | 층 할당시 호출됩니다. |
| [`OnAssignRoomSpawnPhases`](HorayModAPI.md#onassignroomspawnphases) | `Action<RoomSpawnAssignContext> OnAssignRoomSpawnPhases` | 스테이지의 랜덤 적이 어떤 구성으로 결정된 이후 호출됩니다. |
| [`OnPlayerAvatarSpawnedServerside`](HorayModAPI.md#onplayeravatarspawnedserverside) | `Action<PlayerAvatar> OnPlayerAvatarSpawnedServerside` | 플레이어가 스폰될 때 호출됩니다. |
| [`OnPlayerAvatarSpawnedClientside`](HorayModAPI.md#onplayeravatarspawnedclientside) | `Action<PlayerAvatar> OnPlayerAvatarSpawnedClientside` | 플레이어 아바타가 스폰될 때 호출됩니다. |
| [`OnUnitSpawnedServerside`](HorayModAPI.md#onunitspawnedserverside) | `Action<UnitAvatar> OnUnitSpawnedServerside` | 유닛(플레이어·적·소환수·동료)이 서버에 스폰됐을 때 발생합니다. |
| [`OnUnitDespawnedServerside`](HorayModAPI.md#onunitdespawnedserverside) | `Action<UnitAvatar> OnUnitDespawnedServerside` | 유닛이 서버에서 제거되기 직전에 발생합니다. |
| [`OnPlayerEvade`](HorayModAPI.md#onplayerevade) | `Action<PlayerAvatar, DamageInstance> OnPlayerEvade` | 플레이어가 공격을 회피했을 때 발생합니다. |
| [`OnGetDebuff`](HorayModAPI.md#ongetdebuff) | `Func<CharacterDebuff, UnitAvatar, CharacterDebuff> OnGetDebuff` | 유닛에 디버프가 적용되기 직전, 적용할 디버프 프리팹을 바꿀 수 있습니다. |
| [`GridInventoryStartPermission`](HorayModAPI.md#gridinventorystartpermission) | `Action<GridInventory, PlayerAvatar> GridInventoryStartPermission` | 서버가 인벤토리 쓰기 권한을 얻어 아이템 효과를 전부 제거하기 시작할 때 발생합니다. |
| [`GridInventoryEndPermission`](HorayModAPI.md#gridinventoryendpermission) | `Action<GridInventory, PlayerAvatar> GridInventoryEndPermission` | 서버가 쓰기 권한을 반납하며 아이템 효과를 다시 반영한 직후 발생합니다. |
