<!-- Arquivo gerado automaticamente. Tools/ModApiDocGen no repositório do jogo o constrói a partir dos comentários /// no código-fonte. Não o edite diretamente; corrija os comentários no código-fonte e gere novamente. -->
# HorayModAPI

O ponto de entrada estático que o jogo expõe aos mods. Inscreva-se nos eventos de carregamento de banco de dados, sessão e surgimento, e registre prefabs de rede e Atributos especiais. Inscreva-se nos eventos apenas uma vez, em `OnModLoaded`. Para o ciclo de vida e as regras de multijogador, consulte [Mods em DLL](../dll-mods.md).

Gerado em 2026-10-26 para a versão **1.1.0** do jogo. [Índice da referência](README.md)

| Tipo | Descrição |
|---|---|
| [`HorayModAPI`](#horaymodapi) | O ponto de entrada para mods. |
| [`HorayModLocalizationContext`](#horaymodlocalizationcontext) | Um auxiliar para registrar textos de localização de mods. |


## HorayModAPI

```csharp
public static class HorayModAPI
```

O ponto de entrada para mods.

Exemplo:

```csharp
HorayModAPI.OnLoadMiracleDatabase += () => {
    MiracleDatabase.Register(myCustomMiraclePrefab);
    MiracleDatabase.Modify("Scholar", m => m.categories = new[] { "Lake" });
};
```

### Constantes e campos

#### WORK_IN_PROGRESS_README

```csharp
public const string WORK_IN_PROGRESS_README = "The MOD System is still under development. Most features are not work properly yet."
```

*Ainda sem descrição.*


#### WORK_IN_PROGRESS_README_URL

```csharp
public const string WORK_IN_PROGRESS_README_URL = "https://teamhoray.com/mod-api"
```

*Ainda sem descrição.*


#### NAME

```csharp
public const string NAME = "HorayMod"
```

*Ainda sem descrição.*

### Eventos

#### OnLoadMiracleDatabase

```csharp
public static event Action OnLoadMiracleDatabase
```

Dispara logo depois que o banco de dados de milagres é inicializado. Registre ou modifique milagres com `MiracleDatabase.Register`/`Modify`.


#### OnLoadWeaponDatabase

```csharp
public static event Action OnLoadWeaponDatabase
```

Dispara logo depois que o banco de dados de armas é inicializado. Registre ou modifique armas com `WeaponDatabase.Register`/`Modify`.


#### OnLoadStatusDatabase

```csharp
public static event Action OnLoadStatusDatabase
```

Dispara logo depois que o banco de dados de status (efeitos de atributo) é inicializado. Registre ou modifique status com `StatusDatabase.Register`/`Modify`.


#### OnLoadSpecialStatDatabase

```csharp
public static event Action OnLoadSpecialStatDatabase
```

Dispara logo depois que as categorias e os status de atributos especiais são carregados. Registre entradas de exibição com `RegisterSpecialStatCategory`/`RegisterSpecialStat`.


#### OnLoadItemDatabase

```csharp
public static event Action OnLoadItemDatabase
```

Dispara logo depois que o banco de dados de itens é inicializado. Registre ou modifique itens com `ItemDatabase.Register`/`Modify`.


#### OnLoadKeywordDatabase

```csharp
public static event Action OnLoadKeywordDatabase
```

Dispara logo depois que o banco de dados de palavras-chave é inicializado. Registre ou modifique palavras-chave com `KeywordDatabase.Register`/`Modify`.


#### OnLoadPassiveDatabase

```csharp
public static event Action OnLoadPassiveDatabase
```

Dispara logo depois que o banco de dados de passivas é inicializado. Registre ou modifique passivas com `PassiveDatabase.Register`/`Modify`.


#### OnLoadCostumeDatabase

```csharp
public static event Action OnLoadCostumeDatabase
```

Dispara logo depois que o banco de dados de trajes é inicializado. Registre ou modifique trajes com `CostumeDatabase.Register`/`RegisterSkin`/`Modify`.


#### OnLoadPropDatabase

```csharp
public static event Action OnLoadPropDatabase
```

Dispara logo depois que o banco de dados de props é inicializado. Registre props com `PropDatabase.Register`.


#### OnLoadUnitDatabase

```csharp
public static event Action OnLoadUnitDatabase
```

Dispara logo depois que o banco de dados de unidades (entidades de surgimento de inimigos e NPCs) é inicializado.


#### OnLoadSocialIDDatabase

```csharp
public static event Action OnLoadSocialIDDatabase
```

Dispara logo depois que o banco de dados de papéis de NPC é inicializado.


#### OnLoadCustomGameDatabase

```csharp
public static event Action OnLoadCustomGameDatabase
```

Dispara logo depois que o banco de dados de jogo personalizado é inicializado.


#### OnAllDatabasesReady

```csharp
public static event Action OnAllDatabasesReady
```

Dispara logo depois que todos os bancos de dados terminam de inicializar.


#### OnTitleMenuReady

```csharp
public static event Action<UI_TitleLobby> OnTitleMenuReady
```

Chamado depois que o menu de título é carregado.


#### OnStartGameServerside

```csharp
public static event Action OnStartGameServerside
```

Chamado quando o jogo começa. (lado do servidor) Começar significa clicar em um slot de save na tela de título para entrar no jogo. Os dados da sessão, como o jogador e a localização atual, ainda não foram carregados.


#### OnStartGameClientside

```csharp
public static event Action OnStartGameClientside
```

Chamado quando o jogo começa. (lado do cliente) Começar significa clicar em um slot de save na tela de título para entrar no jogo. Os dados da sessão, como o jogador e a localização atual, ainda não foram carregados.


#### OnStartSessionServerside

```csharp
public static event Action<bool> OnStartSessionServerside
```

Chamado quando uma sessão de jogo começa. (lado do servidor) Uma sessão é uma unidade de jogo. Quando o jogador morre, aquela sessão termina e uma nova começa.


#### OnStartSessionClientside

```csharp
public static event Action<bool> OnStartSessionClientside
```

Chamado quando uma sessão de jogo começa. (lado do cliente) Uma sessão é uma unidade de jogo. Quando o jogador morre, aquela sessão termina e uma nova começa.


#### OnLocalizationReady

```csharp
public static event Action<HorayModLocalizationContext> OnLocalizationReady
```

Chamado depois que os arquivos de localização são carregados.


#### OnFloorAllocatedServerside

```csharp
public static event Action<string, string, FloorGenerator> OnFloorAllocatedServerside
```

Chamado quando um andar é alocado. (lado do servidor) Todo local em Sephiria é definido como um andar. Quando um jogador se move entre locais, os passos a seguir são executados em ordem. 1. Alocação do andar: cria e carrega o novo andar. 2. Movimento: move o jogador para o ponto solicitado no andar totalmente carregado. 3. Remoção do andar: remove o local em que o jogador estava antes, se estiver vazio (= nenhum jogador permanece lá).
param1: GUID do andar  
param2: Nome do andar  
param3: Instância do FloorGenerator


#### OnFloorAllocatedClientside

```csharp
public static event Action<string, string, FloorGenerator> OnFloorAllocatedClientside
```

Chamado quando um andar é alocado. (lado do cliente) Todo local em Sephiria é definido como um andar. Quando um jogador se move entre locais, os passos a seguir são executados em ordem. 1. Alocação do andar: cria e carrega o novo andar. 2. Movimento: move o jogador para o ponto solicitado no andar totalmente carregado. 3. Remoção do andar: remove o local em que o jogador estava antes, se estiver vazio (= nenhum jogador permanece lá).
param1: GUID do andar  
param2: Nome do andar  
param3: Instância do FloorGenerator


#### OnAssignRoomSpawnPhases

```csharp
public static event Action<RoomSpawnAssignContext> OnAssignRoomSpawnPhases
```

Chamado depois que a composição aleatória de inimigos de um estágio é decidida. (lado do servidor) Disparado logo depois que o gerador de andares escolhe as fases de monstros de uma sala de batalha (ou de uma zona de batalha), antes que o spawner as receba. Substitua ou edite ctx.Phases para mudar o que surge ali. O jogo não impõe balanceamento aqui; consulte RoomSpawnAssignContext para as regras (RNG determinístico via ctx.Seed, nunca mexa em ctx.Sources, as entidades precisam estar registradas no UnitDatabase).


#### OnPlayerAvatarSpawnedServerside

```csharp
public static event Action<PlayerAvatar> OnPlayerAvatarSpawnedServerside
```

Chamado quando um jogador surge. (lado do servidor) Você pode registrar callbacks relacionados ao jogador dentro deste hook. Disparado no fim de PlayerAvatar.OnStartServer, depois que os atributos base são aplicados. Traje, andar e conteúdo do inventário ainda podem ser preenchidos mais tarde no mesmo quadro ou nos quadros seguintes. Use-o para se inscrever nos eventos de instância do próprio avatar (OnDamagedServerside, OnAttackUnit, OnKillUnit, OnDie, OnAddedBuff, OnEnteredFloorServerside, ...). As inscrições no avatar morrem junto com o avatar, então não é preciso cancelá-las.


#### OnPlayerAvatarSpawnedClientside

```csharp
public static event Action<PlayerAvatar> OnPlayerAvatarSpawnedClientside
```

Chamado quando o avatar de um jogador surge. (lado do cliente) Disparado uma vez por avatar no fim do seu OnStartClient; também dispara para jogadores remotos. Verifique avatar.isLocalPlayer / avatar.isOwned para distinguir o jogador local. As SyncVars já estão aplicadas, mas o andar e o conteúdo do inventário podem chegar depois.


#### OnUnitSpawnedServerside

```csharp
public static event Action<UnitAvatar> OnUnitSpawnedServerside
```

Dispara quando uma unidade (jogador, inimigo, invocação ou companheiro) surge no servidor. Apenas no servidor (anfitrião).


#### OnUnitDespawnedServerside

```csharp
public static event Action<UnitAvatar> OnUnitDespawnedServerside
```

Dispara logo antes de uma unidade ser removida do servidor. Apenas no servidor (anfitrião).


#### OnPlayerEvade

```csharp
public static event Action<PlayerAvatar, DamageInstance> OnPlayerEvade
```

Dispara quando um jogador esquiva de um ataque. (lado do servidor)


#### OnGetDebuff

```csharp
public static event Func<CharacterDebuff, UnitAvatar, CharacterDebuff> OnGetDebuff
```

Permite trocar o prefab do debuff logo antes de um debuff ser aplicado a uma unidade. O handler recebe (prefab atual, conjurador) e retorna um prefab substituto; retornar null o mantém inalterado.


#### GridInventoryStartPermission

```csharp
public static event Action<GridInventory, PlayerAvatar> GridInventoryStartPermission
```

Dispara quando o servidor assume a permissão de escrita do inventário e começa a remover todos os efeitos de itens.


#### GridInventoryEndPermission

```csharp
public static event Action<GridInventory, PlayerAvatar> GridInventoryEndPermission
```

Dispara logo depois que o servidor devolve a permissão de escrita e reaplica os efeitos de itens.

### Métodos

#### RegisterNetworkPrefab

```csharp
public static bool RegisterNetworkPrefab(GameObject prefab)
```

Registra um objeto (prefab) que pode ser criado pela rede. O objeto precisa ter um componente NetworkIdentity.

O servidor (anfitrião) e os clientes precisam manter, cada um, uma lista de objetos que podem usar a rede, e essa lista precisa ser idêntica para todos.

Exemplo:

```csharp
HorayModAPI.OnAllDatabasesReady += () => {
    HorayModAPI.RegisterNetworkPrefab(myNewCustomPrefab);
};
```

Retorna: true se o registro foi bem-sucedido


#### RegisterNetworkPrefab

```csharp
public static bool RegisterNetworkPrefab(GameObject prefab, string stableKey)
```

Registra um prefab criado em tempo de execução (como um clone de um modelo embutido no jogo) como objeto que pode ser criado pela rede, usando um assetId fixo calculado a partir de `stableKey`. Use o nome da unidade do mod (`<modId>:<id>`) como chave.

Retorna: true se o registro foi bem-sucedido


#### RegisterNetworkPrefabs

```csharp
public static int RegisterNetworkPrefabs(IEnumerable<GameObject> prefabs)
```

Registra vários prefabs como objetos que podem ser criados pela rede de uma só vez.

Retorna: o número de prefabs registrados com sucesso


#### UnregisterNetworkPrefab

```csharp
public static bool UnregisterNetworkPrefab(GameObject prefab)
```

Cancela o registro de um prefab de rede registrado.

Retorna: true se foi removido


#### RegisterSpecialStatCategory

```csharp
public static bool RegisterSpecialStatCategory(SpecialStatCategoryEntity category)
```

Registra uma categoria de Atributos especiais na UI de atributos.

Retorna: true se foi registrada


#### RegisterSpecialStat

```csharp
public static bool RegisterSpecialStat(StatusEntity status, Func<SpecialStatContext, SpecialStatValue> reader = null)
```

Registra um StatusEntity como entrada em uma categoria de Atributos especiais na UI de atributos. Se você passar `reader`, o valor é lido por essa função em vez das configurações do asset.

Retorna: true se foi registrado


#### GetModOptions

```csharp
public static ModOptions GetModOptions(string modId)
```

Obtém as opções por dispositivo vinculadas ao modId. Mods da Oficina podem usar `Context.Options` em vez disso.


#### TryShowItemInteraction

```csharp
public static bool TryShowItemInteraction(ItemInteractionOptions options, out ItemInteractionSession session)
```

Abre um painel de interação com itens usado junto com o inventário. Retorna false se a UI estiver ocupada.


#### TryGetUIWidgets

```csharp
public static bool TryGetUIWidgets(out UIWidgetFactory widgets)
```

Obtém uma fábrica de widgets de UI no estilo do jogo. Anexe-os sob um Canvas de UI existente.


#### TryShowDialogue

```csharp
public static bool TryShowDialogue(Transform anchor, DialoguePage page, out DialogueSession session)
```

Abre uma caixa de diálogo no mundo. Retorna false se a UI estiver ocupada; exiba a próxima página com `session.Show`.


#### TryShowDialogue

```csharp
public static bool TryShowDialogue(Transform anchor, DialoguePage page, out DialogueSession session, Vector3? offset)
```

Abre uma caixa de diálogo com um deslocamento no espaço do mundo. Se null, o deslocamento padrão do prefab é usado.


#### TryShowDialogue

```csharp
public static bool TryShowDialogue(Transform anchor, DialoguePage page, out DialogueSession session, Vector3? offset = null, Vector2? cameraOffset = null, bool focusCamera = true)
```

Abre uma caixa de diálogo com deslocamentos separados para o diálogo e para a câmera. O foco da câmera se aplica apenas localmente.


## HorayModLocalizationContext

```csharp
public class HorayModLocalizationContext
```

Um auxiliar para registrar textos de localização de mods.

### Métodos

#### AddText

```csharp
public void AddText(string language, string key, string value)
```

Registra um texto para um idioma específico.


#### AddText

```csharp
public void AddText(string key, Dictionary<string, string> texts)
```

Registra textos para vários idiomas de uma só vez
texts: { "ko-KR" : "불의 검", "en-US" : "Fire Sword", ... }
