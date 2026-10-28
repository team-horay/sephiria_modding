<!-- Otomatik üretilen dosya. Oyun deposundaki Tools/ModApiDocGen, kaynaktaki /// yorumlarından oluşturur. Doğrudan düzenlemeyin; kaynak yorumlarını düzeltip yeniden üretin. -->
# API Referansı

DLL modlarının kullanabileceği herkese açık türlerin ve üyelerin listesi. Proje kurulumu, giriş noktaları, yaşam döngüsü ve çok oyunculu kuralları [DLL Modları](../dll-mods.md) sayfasında ele alınmıştır.

Oyun sürümü **1.1.0** için 2026-10-26 tarihinde üretildi. Bu belge oyun her güncellendiğinde yeniden üretilir.

| Belge | Türler | İçerik |
|---|---|---|
| [HorayModAPI](HorayModAPI.md) | `HorayModAPI`, `HorayModLocalizationContext` | Oyunun modlara sunduğu statik giriş noktası. |
| [Giriş Noktası ve Bağlam](ModBase.md) | `WorkshopModBase`, `WorkshopModContext`, `HorayModBase`, `WorkshopDataPackage`, `WorkshopModDefinition`, `WorkshopModuleDefinition`, `WorkshopDependency`, `WorkshopModuleType`, `WorkshopInstallation` | Bir modun giriş sınıfının devraldığı temel sınıflar ile yükleyicinin iliştirdiği bağlam ve bildirim türleri. |
| [Mod Seçenekleri](ModOptions.md) | `ModOptions`, `ModOptionsPage`, `ModOptionDefinition`, `ModOption<T>`, `ModToggleOption`, `ModSelectionOption`, `ModSliderOption`, `ModOptionText`, `ModOptionKind`, `ModOptionsRegistry` | Cihaz başına mod ayarları ve oyunun seçenekler ekranına açma/kapama düğmeleri, seçim kutuları ve kaydırıcılar eklemek için bir API. |
| [Oda Doğuş Ataması](RoomSpawn.md) | `RoomSpawnAssignContext`, `ERoomSpawnKind` | `HorayModAPI.OnAssignRoomSpawnPhases` olayının geçirdiği bağlam. |
| [Özel Nitelikler](SpecialStats.md) | `SpecialStatContext`, `SpecialStatValue`, `SpecialStatCategoryEntity`, `SpecialStatCategoryVisibility`, `SpecialStatDisplaySettings`, `SpecialStatValueSource`, `SpecialStatLabelColor`, `SpecialStatValueProvider` | `HorayModAPI.OnLoadSpecialStatDatabase` içinde `RegisterSpecialStatCategory` ve `RegisterSpecialStat` ile kaydedilen nitelik gösterim türleri. |
| [Arayüz](UI.md) | `UIWidgetFactory`, `UIWidgetTextStyle`, `DialoguePage`, `DialogueChoice`, `DialogueSession`, `DialogueCloseReason`, `ItemInteractionOptions`, `ItemInteractionSession`, `ItemInteractionItem`, `ItemInteractionCloseReason`, `ItemInteractionItemSource` | `HorayModAPI.TryShowDialogue`, `TryShowItemInteraction` ve `TryGetUIWidgets` tarafından kullanılan diyalog, eşya etkileşimi ve widget türleri. |

## HorayModAPI Olaylarına Genel Bakış

Kaynakta bildirildikleri sırayla listelenmiştir. Veritabanı olayları bir dünya her yüklendiğinde yeniden tetiklenir.

| Olay | İmza | Açıklama |
|---|---|---|
| [`OnLoadMiracleDatabase`](HorayModAPI.md#onloadmiracledatabase) | `Action OnLoadMiracleDatabase` | Mucize veritabanı başlatıldıktan hemen sonra tetiklenir. |
| [`OnLoadWeaponDatabase`](HorayModAPI.md#onloadweapondatabase) | `Action OnLoadWeaponDatabase` | Silah veritabanı başlatıldıktan hemen sonra tetiklenir. |
| [`OnLoadStatusDatabase`](HorayModAPI.md#onloadstatusdatabase) | `Action OnLoadStatusDatabase` | Durum (nitelik etkisi) veritabanı başlatıldıktan hemen sonra tetiklenir. |
| [`OnLoadSpecialStatDatabase`](HorayModAPI.md#onloadspecialstatdatabase) | `Action OnLoadSpecialStatDatabase` | Özel nitelik kategorileri ve durumları yüklendikten hemen sonra tetiklenir. |
| [`OnLoadItemDatabase`](HorayModAPI.md#onloaditemdatabase) | `Action OnLoadItemDatabase` | Eşya veritabanı başlatıldıktan hemen sonra tetiklenir. |
| [`OnLoadKeywordDatabase`](HorayModAPI.md#onloadkeyworddatabase) | `Action OnLoadKeywordDatabase` | Anahtar sözcük veritabanı başlatıldıktan hemen sonra tetiklenir. |
| [`OnLoadPassiveDatabase`](HorayModAPI.md#onloadpassivedatabase) | `Action OnLoadPassiveDatabase` | Pasif veritabanı başlatıldıktan hemen sonra tetiklenir. |
| [`OnLoadCostumeDatabase`](HorayModAPI.md#onloadcostumedatabase) | `Action OnLoadCostumeDatabase` | Kostüm veritabanı başlatıldıktan hemen sonra tetiklenir. |
| [`OnLoadPropDatabase`](HorayModAPI.md#onloadpropdatabase) | `Action OnLoadPropDatabase` | Prop veritabanı başlatıldıktan hemen sonra tetiklenir. |
| [`OnLoadUnitDatabase`](HorayModAPI.md#onloadunitdatabase) | `Action OnLoadUnitDatabase` | Birim veritabanı (düşman ve NPC doğuş varlıkları) başlatıldıktan hemen sonra tetiklenir. |
| [`OnLoadSocialIDDatabase`](HorayModAPI.md#onloadsocialiddatabase) | `Action OnLoadSocialIDDatabase` | NPC rolü veritabanı başlatıldıktan hemen sonra tetiklenir. |
| [`OnLoadCustomGameDatabase`](HorayModAPI.md#onloadcustomgamedatabase) | `Action OnLoadCustomGameDatabase` | Özel oyun veritabanı başlatıldıktan hemen sonra tetiklenir. |
| [`OnAllDatabasesReady`](HorayModAPI.md#onalldatabasesready) | `Action OnAllDatabasesReady` | Tüm veritabanları başlatmayı bitirdikten hemen sonra tetiklenir. |
| [`OnTitleMenuReady`](HorayModAPI.md#ontitlemenuready) | `Action<UI_TitleLobby> OnTitleMenuReady` | Başlık menüsü yüklendikten sonra çağrılır. |
| [`OnStartGameServerside`](HorayModAPI.md#onstartgameserverside) | `Action OnStartGameServerside` | Oyun başladığında çağrılır. |
| [`OnStartGameClientside`](HorayModAPI.md#onstartgameclientside) | `Action OnStartGameClientside` | Oyun başladığında çağrılır. |
| [`OnStartSessionServerside`](HorayModAPI.md#onstartsessionserverside) | `Action<bool> OnStartSessionServerside` | Bir oyun oturumu başladığında çağrılır. |
| [`OnStartSessionClientside`](HorayModAPI.md#onstartsessionclientside) | `Action<bool> OnStartSessionClientside` | Bir oyun oturumu başladığında çağrılır. |
| [`OnLocalizationReady`](HorayModAPI.md#onlocalizationready) | `Action<HorayModLocalizationContext> OnLocalizationReady` | Yerelleştirme dosyaları yüklendikten sonra çağrılır. |
| [`OnFloorAllocatedServerside`](HorayModAPI.md#onfloorallocatedserverside) | `Action<string, string, FloorGenerator> OnFloorAllocatedServerside` | Bir kat tahsis edildiğinde çağrılır. |
| [`OnFloorAllocatedClientside`](HorayModAPI.md#onfloorallocatedclientside) | `Action<string, string, FloorGenerator> OnFloorAllocatedClientside` | Bir kat tahsis edildiğinde çağrılır. |
| [`OnAssignRoomSpawnPhases`](HorayModAPI.md#onassignroomspawnphases) | `Action<RoomSpawnAssignContext> OnAssignRoomSpawnPhases` | Bir sahnenin rastgele düşman bileşimi belirlendikten sonra çağrılır. |
| [`OnPlayerAvatarSpawnedServerside`](HorayModAPI.md#onplayeravatarspawnedserverside) | `Action<PlayerAvatar> OnPlayerAvatarSpawnedServerside` | Bir oyuncu doğduğunda çağrılır. |
| [`OnPlayerAvatarSpawnedClientside`](HorayModAPI.md#onplayeravatarspawnedclientside) | `Action<PlayerAvatar> OnPlayerAvatarSpawnedClientside` | Bir oyuncu avatarı doğduğunda çağrılır. |
| [`OnUnitSpawnedServerside`](HorayModAPI.md#onunitspawnedserverside) | `Action<UnitAvatar> OnUnitSpawnedServerside` | Sunucuda bir birim (oyuncu, düşman, çağrılan varlık veya yoldaş) doğduğunda tetiklenir. |
| [`OnUnitDespawnedServerside`](HorayModAPI.md#onunitdespawnedserverside) | `Action<UnitAvatar> OnUnitDespawnedServerside` | Bir birim sunucudan kaldırılmadan hemen önce tetiklenir. |
| [`OnPlayerEvade`](HorayModAPI.md#onplayerevade) | `Action<PlayerAvatar, DamageInstance> OnPlayerEvade` | Bir oyuncu bir saldırıdan kaçındığında tetiklenir. |
| [`OnGetDebuff`](HorayModAPI.md#ongetdebuff) | `Func<CharacterDebuff, UnitAvatar, CharacterDebuff> OnGetDebuff` | Bir birime zayıflatma uygulanmadan hemen önce zayıflatma prefab'ını değiştirmenizi sağlar. |
| [`GridInventoryStartPermission`](HorayModAPI.md#gridinventorystartpermission) | `Action<GridInventory, PlayerAvatar> GridInventoryStartPermission` | Sunucu envanter yazma iznini alıp tüm eşya etkilerini kaldırmaya başladığında tetiklenir. |
| [`GridInventoryEndPermission`](HorayModAPI.md#gridinventoryendpermission) | `Action<GridInventory, PlayerAvatar> GridInventoryEndPermission` | Sunucu yazma iznini geri verip eşya etkilerini yeniden uyguladıktan hemen sonra tetiklenir. |
