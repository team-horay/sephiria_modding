<!-- Fichier généré automatiquement. Tools/ModApiDocGen dans le dépôt du jeu le construit à partir des commentaires /// du code source. Ne le modifiez pas directement ; corrigez les commentaires du code source et régénérez-le. -->
# HorayModAPI

Le point d'entrée statique que le jeu expose aux mods. Abonnez-vous aux événements de chargement de base de données, de session et d'apparition, et enregistrez des prefabs réseau et des stats spéciales. Abonnez-vous aux événements une seule fois, dans `OnModLoaded`. Pour le cycle de vie et les règles du multijoueur, consultez [Mods DLL](../dll-mods.md).

Généré le 2026-10-26 pour la version **1.1.0** du jeu. [Index de la référence](README.md)

| Type | Description |
|---|---|
| [`HorayModAPI`](#horaymodapi) | Le point d'entrée pour les mods. |
| [`HorayModLocalizationContext`](#horaymodlocalizationcontext) | Un utilitaire pour enregistrer les textes de localisation d'un mod. |


## HorayModAPI

```csharp
public static class HorayModAPI
```

Le point d'entrée pour les mods.

Exemple :

```csharp
HorayModAPI.OnLoadMiracleDatabase += () => {
    MiracleDatabase.Register(myCustomMiraclePrefab);
    MiracleDatabase.Modify("Scholar", m => m.categories = new[] { "Lake" });
};
```

### Constantes et champs

#### WORK_IN_PROGRESS_README

```csharp
public const string WORK_IN_PROGRESS_README = "The MOD System is still under development. Most features are not work properly yet."
```

*Pas encore de description.*


#### WORK_IN_PROGRESS_README_URL

```csharp
public const string WORK_IN_PROGRESS_README_URL = "https://teamhoray.com/mod-api"
```

*Pas encore de description.*


#### NAME

```csharp
public const string NAME = "HorayMod"
```

*Pas encore de description.*

### Événements

#### OnLoadMiracleDatabase

```csharp
public static event Action OnLoadMiracleDatabase
```

Se déclenche juste après l'initialisation de la base de données des miracles. Enregistrez ou modifiez des miracles avec `MiracleDatabase.Register`/`Modify`.


#### OnLoadWeaponDatabase

```csharp
public static event Action OnLoadWeaponDatabase
```

Se déclenche juste après l'initialisation de la base de données des armes. Enregistrez ou modifiez des armes avec `WeaponDatabase.Register`/`Modify`.


#### OnLoadStatusDatabase

```csharp
public static event Action OnLoadStatusDatabase
```

Se déclenche juste après l'initialisation de la base de données des statuts (effets de stats). Enregistrez ou modifiez des statuts avec `StatusDatabase.Register`/`Modify`.


#### OnLoadSpecialStatDatabase

```csharp
public static event Action OnLoadSpecialStatDatabase
```

Se déclenche juste après le chargement des catégories et statuts de stats spéciales. Enregistrez des entrées d'affichage avec `RegisterSpecialStatCategory`/`RegisterSpecialStat`.


#### OnLoadItemDatabase

```csharp
public static event Action OnLoadItemDatabase
```

Se déclenche juste après l'initialisation de la base de données des objets. Enregistrez ou modifiez des objets avec `ItemDatabase.Register`/`Modify`.


#### OnLoadKeywordDatabase

```csharp
public static event Action OnLoadKeywordDatabase
```

Se déclenche juste après l'initialisation de la base de données des mots-clés. Enregistrez ou modifiez des mots-clés avec `KeywordDatabase.Register`/`Modify`.


#### OnLoadPassiveDatabase

```csharp
public static event Action OnLoadPassiveDatabase
```

Se déclenche juste après l'initialisation de la base de données des passifs. Enregistrez ou modifiez des passifs avec `PassiveDatabase.Register`/`Modify`.


#### OnLoadCostumeDatabase

```csharp
public static event Action OnLoadCostumeDatabase
```

Se déclenche juste après l'initialisation de la base de données des tenues. Enregistrez ou modifiez des tenues avec `CostumeDatabase.Register`/`RegisterSkin`/`Modify`.


#### OnLoadPropDatabase

```csharp
public static event Action OnLoadPropDatabase
```

Se déclenche juste après l'initialisation de la base de données des props. Enregistrez des props avec `PropDatabase.Register`.


#### OnLoadUnitDatabase

```csharp
public static event Action OnLoadUnitDatabase
```

Se déclenche juste après l'initialisation de la base de données des unités (entités d'apparition d'ennemis et de PNJ).


#### OnLoadSocialIDDatabase

```csharp
public static event Action OnLoadSocialIDDatabase
```

Se déclenche juste après l'initialisation de la base de données des rôles de PNJ.


#### OnLoadCustomGameDatabase

```csharp
public static event Action OnLoadCustomGameDatabase
```

Se déclenche juste après l'initialisation de la base de données des parties personnalisées.


#### OnAllDatabasesReady

```csharp
public static event Action OnAllDatabasesReady
```

Se déclenche juste après la fin de l'initialisation de toutes les bases de données.


#### OnTitleMenuReady

```csharp
public static event Action<UI_TitleLobby> OnTitleMenuReady
```

Appelé après le chargement du menu titre.


#### OnStartGameServerside

```csharp
public static event Action OnStartGameServerside
```

Appelé au démarrage de la partie. (côté serveur) Démarrer signifie cliquer sur un emplacement de sauvegarde sur l'écran titre pour entrer dans le jeu. Les données de session, comme le joueur et l'emplacement actuel, ne sont pas encore chargées.


#### OnStartGameClientside

```csharp
public static event Action OnStartGameClientside
```

Appelé au démarrage de la partie. (côté client) Démarrer signifie cliquer sur un emplacement de sauvegarde sur l'écran titre pour entrer dans le jeu. Les données de session, comme le joueur et l'emplacement actuel, ne sont pas encore chargées.


#### OnStartSessionServerside

```csharp
public static event Action<bool> OnStartSessionServerside
```

Appelé au démarrage d'une session de jeu. (côté serveur) Une session est une unité de jeu. Lorsque le joueur meurt, cette session se termine et une nouvelle commence.


#### OnStartSessionClientside

```csharp
public static event Action<bool> OnStartSessionClientside
```

Appelé au démarrage d'une session de jeu. (côté client) Une session est une unité de jeu. Lorsque le joueur meurt, cette session se termine et une nouvelle commence.


#### OnLocalizationReady

```csharp
public static event Action<HorayModLocalizationContext> OnLocalizationReady
```

Appelé après le chargement des fichiers de localisation.


#### OnFloorAllocatedServerside

```csharp
public static event Action<string, string, FloorGenerator> OnFloorAllocatedServerside
```

Appelé lors de l'allocation d'un étage. (côté serveur) Chaque lieu de Sephiria est défini comme un étage. Lorsqu'un joueur se déplace entre des lieux, les étapes suivantes s'exécutent dans l'ordre. 1. Allocation de l'étage : crée et charge le nouvel étage. 2. Déplacement : déplace le joueur vers le point demandé sur l'étage entièrement chargé. 3. Suppression de l'étage : supprime le lieu où se trouvait le joueur auparavant, s'il est vide (= plus aucun joueur ne s'y trouve).
param1 : GUID de l'étage  
param2 : nom de l'étage  
param3 : instance de FloorGenerator


#### OnFloorAllocatedClientside

```csharp
public static event Action<string, string, FloorGenerator> OnFloorAllocatedClientside
```

Appelé lors de l'allocation d'un étage. (côté client) Chaque lieu de Sephiria est défini comme un étage. Lorsqu'un joueur se déplace entre des lieux, les étapes suivantes s'exécutent dans l'ordre. 1. Allocation de l'étage : crée et charge le nouvel étage. 2. Déplacement : déplace le joueur vers le point demandé sur l'étage entièrement chargé. 3. Suppression de l'étage : supprime le lieu où se trouvait le joueur auparavant, s'il est vide (= plus aucun joueur ne s'y trouve).
param1 : GUID de l'étage  
param2 : nom de l'étage  
param3 : instance de FloorGenerator


#### OnAssignRoomSpawnPhases

```csharp
public static event Action<RoomSpawnAssignContext> OnAssignRoomSpawnPhases
```

Appelé une fois la composition aléatoire des ennemis d'une étape décidée. (côté serveur) Déclenché juste après que le générateur d'étage a choisi les phases de monstres d'une salle de combat (ou d'une zone de combat), avant que le spawner ne les reçoive. Remplacez ou modifiez ctx.Phases pour changer ce qui y apparaît. Le jeu n'impose aucun équilibrage ici ; consultez RoomSpawnAssignContext pour les règles (RNG déterministe via ctx.Seed, ne jamais toucher ctx.Sources, les entités doivent être enregistrées dans UnitDatabase).


#### OnPlayerAvatarSpawnedServerside

```csharp
public static event Action<PlayerAvatar> OnPlayerAvatarSpawnedServerside
```

Appelé à l'apparition d'un joueur. (côté serveur) Vous pouvez enregistrer des rappels liés au joueur dans ce hook. Levé à la fin de PlayerAvatar.OnStartServer, après l'application des stats de base. La tenue, l'étage et le contenu de l'inventaire peuvent encore être renseignés plus tard dans la même frame ou dans les frames suivantes. Utilisez-le pour vous abonner aux événements d'instance propres à l'avatar (OnDamagedServerside, OnAttackUnit, OnKillUnit, OnDie, OnAddedBuff, OnEnteredFloorServerside, ...). Les abonnements sur l'avatar disparaissent avec lui, aucun désabonnement n'est donc nécessaire pour eux.


#### OnPlayerAvatarSpawnedClientside

```csharp
public static event Action<PlayerAvatar> OnPlayerAvatarSpawnedClientside
```

Appelé à l'apparition de l'avatar d'un joueur. (côté client) Levé une fois par avatar à la fin de son OnStartClient ; se déclenche aussi pour les joueurs distants. Vérifiez avatar.isLocalPlayer / avatar.isOwned pour distinguer le joueur local. Les SyncVars sont déjà appliquées, mais l'étage et le contenu de l'inventaire peuvent arriver plus tard.


#### OnUnitSpawnedServerside

```csharp
public static event Action<UnitAvatar> OnUnitSpawnedServerside
```

Se déclenche lorsqu'une unité (joueur, ennemi, invocation ou compagnon) apparaît sur le serveur. Serveur (hôte) uniquement.


#### OnUnitDespawnedServerside

```csharp
public static event Action<UnitAvatar> OnUnitDespawnedServerside
```

Se déclenche juste avant qu'une unité soit retirée du serveur. Serveur (hôte) uniquement.


#### OnPlayerEvade

```csharp
public static event Action<PlayerAvatar, DamageInstance> OnPlayerEvade
```

Se déclenche lorsqu'un joueur esquive une attaque. (côté serveur)


#### OnGetDebuff

```csharp
public static event Func<CharacterDebuff, UnitAvatar, CharacterDebuff> OnGetDebuff
```

Permet de remplacer le prefab de debuff juste avant qu'un debuff soit appliqué à une unité. Le gestionnaire reçoit (prefab actuel, lanceur) et renvoie un prefab de remplacement ; renvoyer null le laisse inchangé.


#### GridInventoryStartPermission

```csharp
public static event Action<GridInventory, PlayerAvatar> GridInventoryStartPermission
```

Se déclenche lorsque le serveur prend la permission d'écriture sur l'inventaire et commence à retirer tous les effets d'objets.


#### GridInventoryEndPermission

```csharp
public static event Action<GridInventory, PlayerAvatar> GridInventoryEndPermission
```

Se déclenche juste après que le serveur a rendu la permission d'écriture et réappliqué les effets d'objets.

### Méthodes

#### RegisterNetworkPrefab

```csharp
public static bool RegisterNetworkPrefab(GameObject prefab)
```

Enregistre un objet (prefab) pouvant apparaître via le réseau. L'objet doit avoir un composant NetworkIdentity.

Le serveur (hôte) et les clients doivent chacun détenir une liste des objets pouvant utiliser le réseau, et cette liste doit être identique pour tout le monde.

Exemple :

```csharp
HorayModAPI.OnAllDatabasesReady += () => {
    HorayModAPI.RegisterNetworkPrefab(myNewCustomPrefab);
};
```

Renvoie : true si l'enregistrement a réussi


#### RegisterNetworkPrefab

```csharp
public static bool RegisterNetworkPrefab(GameObject prefab, string stableKey)
```

Enregistre un prefab créé à l'exécution (comme un clone d'un modèle intégré) en tant qu'objet pouvant apparaître via le réseau, en utilisant un assetId fixe calculé à partir de `stableKey`. Utilisez le nom de l'unité du mod (`<modId>:<id>`) comme clé.

Renvoie : true si l'enregistrement a réussi


#### RegisterNetworkPrefabs

```csharp
public static int RegisterNetworkPrefabs(IEnumerable<GameObject> prefabs)
```

Enregistre plusieurs prefabs en tant qu'objets pouvant apparaître via le réseau en une seule fois.

Renvoie : le nombre de prefabs enregistrés avec succès


#### UnregisterNetworkPrefab

```csharp
public static bool UnregisterNetworkPrefab(GameObject prefab)
```

Désenregistre un prefab réseau enregistré.

Renvoie : true s'il a été retiré


#### RegisterSpecialStatCategory

```csharp
public static bool RegisterSpecialStatCategory(SpecialStatCategoryEntity category)
```

Enregistre une catégorie de stats spéciales dans l'interface des stats.

Renvoie : true si enregistré


#### RegisterSpecialStat

```csharp
public static bool RegisterSpecialStat(StatusEntity status, Func<SpecialStatContext, SpecialStatValue> reader = null)
```

Enregistre une StatusEntity comme entrée d'une catégorie de stats spéciales dans l'interface des stats. Si vous passez `reader`, la valeur est lue via cette fonction au lieu des paramètres de la ressource.

Renvoie : true si enregistré


#### GetModOptions

```csharp
public static ModOptions GetModOptions(string modId)
```

Récupère les options propres à l'appareil liées à modId. Les mods du Workshop peuvent utiliser `Context.Options` à la place.


#### TryShowItemInteraction

```csharp
public static bool TryShowItemInteraction(ItemInteractionOptions options, out ItemInteractionSession session)
```

Ouvre un panneau d'interaction avec les objets utilisé conjointement avec l'inventaire. Renvoie false si l'interface est occupée.


#### TryGetUIWidgets

```csharp
public static bool TryGetUIWidgets(out UIWidgetFactory widgets)
```

Récupère une fabrique de widgets d'interface au style du jeu. Attachez-les sous un Canvas d'interface existant.


#### TryShowDialogue

```csharp
public static bool TryShowDialogue(Transform anchor, DialoguePage page, out DialogueSession session)
```

Ouvre une boîte de dialogue dans le monde. Renvoie false si l'interface est occupée ; affichez la page suivante avec `session.Show`.


#### TryShowDialogue

```csharp
public static bool TryShowDialogue(Transform anchor, DialoguePage page, out DialogueSession session, Vector3? offset)
```

Ouvre une boîte de dialogue avec un décalage dans l'espace du monde. Si null, le décalage par défaut du prefab est utilisé.


#### TryShowDialogue

```csharp
public static bool TryShowDialogue(Transform anchor, DialoguePage page, out DialogueSession session, Vector3? offset = null, Vector2? cameraOffset = null, bool focusCamera = true)
```

Ouvre une boîte de dialogue avec des décalages distincts pour le dialogue et la caméra. La mise au point de la caméra ne s'applique que localement.


## HorayModLocalizationContext

```csharp
public class HorayModLocalizationContext
```

Un utilitaire pour enregistrer les textes de localisation d'un mod.

### Méthodes

#### AddText

```csharp
public void AddText(string language, string key, string value)
```

Enregistre un texte pour une langue spécifique.


#### AddText

```csharp
public void AddText(string key, Dictionary<string, string> texts)
```

Enregistre un texte pour plusieurs langues à la fois
texts : { "ko-KR" : "불의 검", "en-US" : "Fire Sword", ... }
