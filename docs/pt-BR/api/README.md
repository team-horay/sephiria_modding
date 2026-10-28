<!-- Arquivo gerado automaticamente. Tools/ModApiDocGen no repositório do jogo o constrói a partir dos comentários /// no código-fonte. Não o edite diretamente; corrija os comentários no código-fonte e gere novamente. -->
# Referência da API

Uma lista dos tipos e membros públicos disponíveis para mods em DLL. Configuração do projeto, pontos de entrada, ciclo de vida e regras de multijogador são tratados em [Mods em DLL](../dll-mods.md).

Gerado em 2026-10-26 para a versão **1.1.0** do jogo. Este documento é gerado novamente sempre que o jogo é atualizado.

| Documento | Tipos | Conteúdo |
|---|---|---|
| [HorayModAPI](HorayModAPI.md) | `HorayModAPI`, `HorayModLocalizationContext` | O ponto de entrada estático que o jogo expõe aos mods. |
| [Ponto de entrada e contexto](ModBase.md) | `WorkshopModBase`, `WorkshopModContext`, `HorayModBase`, `WorkshopDataPackage`, `WorkshopModDefinition`, `WorkshopModuleDefinition`, `WorkshopDependency`, `WorkshopModuleType`, `WorkshopInstallation` | As classes base que a classe de entrada de um mod herda, além dos tipos de contexto e manifesto que o loader anexa. |
| [Opções de mod](ModOptions.md) | `ModOptions`, `ModOptionsPage`, `ModOptionDefinition`, `ModOption<T>`, `ModToggleOption`, `ModSelectionOption`, `ModSliderOption`, `ModOptionText`, `ModOptionKind`, `ModOptionsRegistry` | Configurações de mod por dispositivo e uma API para adicionar interruptores, caixas de seleção e controles deslizantes à tela de opções do jogo. |
| [Atribuição de surgimento por sala](RoomSpawn.md) | `RoomSpawnAssignContext`, `ERoomSpawnKind` | O contexto passado pelo evento `HorayModAPI.OnAssignRoomSpawnPhases`. |
| [Atributos especiais](SpecialStats.md) | `SpecialStatContext`, `SpecialStatValue`, `SpecialStatCategoryEntity`, `SpecialStatCategoryVisibility`, `SpecialStatDisplaySettings`, `SpecialStatValueSource`, `SpecialStatLabelColor`, `SpecialStatValueProvider` | Tipos de exibição de atributos registrados com `RegisterSpecialStatCategory` e `RegisterSpecialStat` em `HorayModAPI.OnLoadSpecialStatDatabase`. |
| [UI](UI.md) | `UIWidgetFactory`, `UIWidgetTextStyle`, `DialoguePage`, `DialogueChoice`, `DialogueSession`, `DialogueCloseReason`, `ItemInteractionOptions`, `ItemInteractionSession`, `ItemInteractionItem`, `ItemInteractionCloseReason`, `ItemInteractionItemSource` | Tipos de diálogo, interação com itens e widgets usados por `HorayModAPI.TryShowDialogue`, `TryShowItemInteraction` e `TryGetUIWidgets`. |

## Visão geral dos eventos da HorayModAPI

Listados na ordem em que são declarados no código-fonte. Os eventos de banco de dados disparam novamente toda vez que um mundo é carregado.

| Evento | Assinatura | Descrição |
|---|---|---|
| [`OnLoadMiracleDatabase`](HorayModAPI.md#onloadmiracledatabase) | `Action OnLoadMiracleDatabase` | Dispara logo depois que o banco de dados de milagres é inicializado. |
| [`OnLoadWeaponDatabase`](HorayModAPI.md#onloadweapondatabase) | `Action OnLoadWeaponDatabase` | Dispara logo depois que o banco de dados de armas é inicializado. |
| [`OnLoadStatusDatabase`](HorayModAPI.md#onloadstatusdatabase) | `Action OnLoadStatusDatabase` | Dispara logo depois que o banco de dados de status (efeitos de atributo) é inicializado. |
| [`OnLoadSpecialStatDatabase`](HorayModAPI.md#onloadspecialstatdatabase) | `Action OnLoadSpecialStatDatabase` | Dispara logo depois que as categorias e os status de atributos especiais são carregados. |
| [`OnLoadItemDatabase`](HorayModAPI.md#onloaditemdatabase) | `Action OnLoadItemDatabase` | Dispara logo depois que o banco de dados de itens é inicializado. |
| [`OnLoadKeywordDatabase`](HorayModAPI.md#onloadkeyworddatabase) | `Action OnLoadKeywordDatabase` | Dispara logo depois que o banco de dados de palavras-chave é inicializado. |
| [`OnLoadPassiveDatabase`](HorayModAPI.md#onloadpassivedatabase) | `Action OnLoadPassiveDatabase` | Dispara logo depois que o banco de dados de passivas é inicializado. |
| [`OnLoadCostumeDatabase`](HorayModAPI.md#onloadcostumedatabase) | `Action OnLoadCostumeDatabase` | Dispara logo depois que o banco de dados de trajes é inicializado. |
| [`OnLoadPropDatabase`](HorayModAPI.md#onloadpropdatabase) | `Action OnLoadPropDatabase` | Dispara logo depois que o banco de dados de props é inicializado. |
| [`OnLoadUnitDatabase`](HorayModAPI.md#onloadunitdatabase) | `Action OnLoadUnitDatabase` | Dispara logo depois que o banco de dados de unidades (entidades de surgimento de inimigos e NPCs) é inicializado. |
| [`OnLoadSocialIDDatabase`](HorayModAPI.md#onloadsocialiddatabase) | `Action OnLoadSocialIDDatabase` | Dispara logo depois que o banco de dados de papéis de NPC é inicializado. |
| [`OnLoadCustomGameDatabase`](HorayModAPI.md#onloadcustomgamedatabase) | `Action OnLoadCustomGameDatabase` | Dispara logo depois que o banco de dados de jogo personalizado é inicializado. |
| [`OnAllDatabasesReady`](HorayModAPI.md#onalldatabasesready) | `Action OnAllDatabasesReady` | Dispara logo depois que todos os bancos de dados terminam de inicializar. |
| [`OnTitleMenuReady`](HorayModAPI.md#ontitlemenuready) | `Action<UI_TitleLobby> OnTitleMenuReady` | Chamado depois que o menu de título é carregado. |
| [`OnStartGameServerside`](HorayModAPI.md#onstartgameserverside) | `Action OnStartGameServerside` | Chamado quando o jogo começa. |
| [`OnStartGameClientside`](HorayModAPI.md#onstartgameclientside) | `Action OnStartGameClientside` | Chamado quando o jogo começa. |
| [`OnStartSessionServerside`](HorayModAPI.md#onstartsessionserverside) | `Action<bool> OnStartSessionServerside` | Chamado quando uma sessão de jogo começa. |
| [`OnStartSessionClientside`](HorayModAPI.md#onstartsessionclientside) | `Action<bool> OnStartSessionClientside` | Chamado quando uma sessão de jogo começa. |
| [`OnLocalizationReady`](HorayModAPI.md#onlocalizationready) | `Action<HorayModLocalizationContext> OnLocalizationReady` | Chamado depois que os arquivos de localização são carregados. |
| [`OnFloorAllocatedServerside`](HorayModAPI.md#onfloorallocatedserverside) | `Action<string, string, FloorGenerator> OnFloorAllocatedServerside` | Chamado quando um andar é alocado. |
| [`OnFloorAllocatedClientside`](HorayModAPI.md#onfloorallocatedclientside) | `Action<string, string, FloorGenerator> OnFloorAllocatedClientside` | Chamado quando um andar é alocado. |
| [`OnAssignRoomSpawnPhases`](HorayModAPI.md#onassignroomspawnphases) | `Action<RoomSpawnAssignContext> OnAssignRoomSpawnPhases` | Chamado depois que a composição aleatória de inimigos de um estágio é decidida. |
| [`OnPlayerAvatarSpawnedServerside`](HorayModAPI.md#onplayeravatarspawnedserverside) | `Action<PlayerAvatar> OnPlayerAvatarSpawnedServerside` | Chamado quando um jogador surge. |
| [`OnPlayerAvatarSpawnedClientside`](HorayModAPI.md#onplayeravatarspawnedclientside) | `Action<PlayerAvatar> OnPlayerAvatarSpawnedClientside` | Chamado quando o avatar de um jogador surge. |
| [`OnUnitSpawnedServerside`](HorayModAPI.md#onunitspawnedserverside) | `Action<UnitAvatar> OnUnitSpawnedServerside` | Dispara quando uma unidade (jogador, inimigo, invocação ou companheiro) surge no servidor. |
| [`OnUnitDespawnedServerside`](HorayModAPI.md#onunitdespawnedserverside) | `Action<UnitAvatar> OnUnitDespawnedServerside` | Dispara logo antes de uma unidade ser removida do servidor. |
| [`OnPlayerEvade`](HorayModAPI.md#onplayerevade) | `Action<PlayerAvatar, DamageInstance> OnPlayerEvade` | Dispara quando um jogador esquiva de um ataque. |
| [`OnGetDebuff`](HorayModAPI.md#ongetdebuff) | `Func<CharacterDebuff, UnitAvatar, CharacterDebuff> OnGetDebuff` | Permite trocar o prefab do debuff logo antes de um debuff ser aplicado a uma unidade. |
| [`GridInventoryStartPermission`](HorayModAPI.md#gridinventorystartpermission) | `Action<GridInventory, PlayerAvatar> GridInventoryStartPermission` | Dispara quando o servidor assume a permissão de escrita do inventário e começa a remover todos os efeitos de itens. |
| [`GridInventoryEndPermission`](HorayModAPI.md#gridinventoryendpermission) | `Action<GridInventory, PlayerAvatar> GridInventoryEndPermission` | Dispara logo depois que o servidor devolve a permissão de escrita e reaplica os efeitos de itens. |
