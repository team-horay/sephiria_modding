# Mods DLL

Les modules DLL vous permettent d'exécuter du code écrit en C# dans le jeu. Abonnez-vous à `HorayModAPI` pour vous brancher sur les joueurs, les objets, l'interface, et plus encore.
Ce document couvre tout, de la création d'un projet jusqu'à l'exécution de votre première DLL dans le jeu. Pour la liste complète des événements et des fonctions d'enregistrement, consultez la [Référence de l'API](api/README.md).


## Prérequis

- [.NET SDK](https://dotnet.microsoft.com/download) 8 ou ultérieur
- Un éditeur C#


## Créer un projet

Reportez-vous à l'exemple [`examples/dll-sample`](../../examples/dll-sample). Ce document s'en sert comme référence.

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
    <!-- Dossier d'installation du jeu. Adaptez-le à votre configuration, ou passez-le avec dotnet build -p:GameDir=... -->
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
    <!-- Ajoutez de la même manière Mirror si vous manipulez des objets réseau comme PlayerAvatar, UnityEngine.UI si vous créez de l'interface, et 0Harmony si vous appliquez des patchs -->
    <Reference Include="Mirror">
      <HintPath>$(GameDir)\Sephiria_Data\Managed\Mirror.dll</HintPath>
      <Private>false</Private>
    </Reference>
  </ItemGroup>
</Project>
```

1. **Ciblez `netstandard2.1`** et définissez la version du langage sur C# 9.
2. **Référencez les DLL du jeu, mais ne les copiez pas.** Faites pointer les références vers les fichiers de `<dossier d'installation du jeu>/Sephiria_Data/Managed/`, et mettez-les toutes en `Private=false`. Si des DLL du jeu sont copiées dans le dossier de votre mod, le même assembly est chargé deux fois, ce qui provoque des erreurs obscures et ajoute inutilement des fichiers à l'examen.
3. `Assembly-CSharp.dll` contient la majeure partie du code qui fait tourner le jeu.


## Structure du code


```csharp
using UnityEngine;

namespace PhysicalDamagePlus
{
    public class Entry : WorkshopModBase
    {
        public const int Bonus = 5;

        // Appelé une fois par processus. Abonnez-vous aux événements ici, et une seule fois.
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

        // Appelé uniquement sur l'hôte (serveur). Modifiez l'état du jeu ici.
        private static void HandlePlayerSpawnedServer(PlayerAvatar player)
        {
            if (player == null)
                return;
            player.AddCustomStat(ECustomStat.PhysicalDamage, Bonus);
        }

        // Appelé sur chaque participant. La valeur ajoutée par le serveur arrive via la synchronisation.
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

Votre classe hérite des éléments suivants de la classe de base.

| Membre | Description |
|---|---|
| `metadata` | `modName`, `modVersion`, `modAuthor`. Renseignés avec les valeurs de `metadata.json`. |
| `Context` | `PackageRoot` (chemin absolu du dossier du mod), `FileId` (ID de l'élément du Steam Workshop ; pour les paquets locaux, une valeur distincte qui n'entre jamais en collision avec un ID Steam), `Definition` (le manifeste validé), `ResolvePath(relativePath)`. Utilisez-le pour trouver des fichiers dans le dossier de votre mod. Le chemin d'installation diffère pour chaque utilisateur, ne le codez donc jamais en dur. |
| `GetDataPackages()` | La liste des paquets de modules `data` qui désignent ce mod comme leur `consumer`. Appelez-la une fois l'activation terminée (dans un gestionnaire `OnLoad*Database` ou après `OnAllDatabasesReady`). |

`Context` est valide dès l'appel de `OnModLoaded`. Dans le constructeur, il est encore null.


## Cycle de vie

Les mods DLL sont activés **lorsque vous démarrez une partie et entrez dans le monde**. Aucun code ne s'exécute encore sur l'écran titre. L'ordre de démarrage est le suivant.

1. La scène du monde se charge et `GameDataLoader` démarre.
2. Les modules DLL du plan de chargement sont chargés dans l'ordre, chaque classe de point d'entrée est instanciée, et `OnModLoaded` est appelé.
3. Au fur et à mesure de l'initialisation des bases de données, des événements comme `OnLoadItemDatabase` et `OnLoadUnitDatabase` se déclenchent les uns après les autres.
4. Lorsque tout est terminé, `OnAllDatabasesReady` est appelé.

## S'abonner

La classe `HorayModAPI` fournit des événements prédéfinis qui vous permettent de vous brancher sur de nombreuses situations.

- Abonnez-vous aux événements **une seule fois**, dans `OnModLoaded`. Les événements de base de données se déclenchent à nouveau à chaque chargement ; faites donc ce qui « doit se produire à chaque fois » (enregistrement d'objets, etc.) dans les gestionnaires.
- Gérez soigneusement les abonnements et désabonnements. Désabonnez-vous (-=) des événements auxquels vous vous êtes abonné dans `OnModUnloaded`.

La liste complète des événements se trouve dans la [Référence de l'API](api/README.md). Le tableau ci-dessous indique l'événement recommandé pour chaque chose que vous pourriez vouloir faire.

| Ce que vous voulez faire | Événement |
|---|---|
| Ajouter des objets ou des artefacts | `OnLoadItemDatabase` |
| Ajouter des armes, des miracles, des passifs, des tenues ou des props | `OnLoadWeaponDatabase`, `OnLoadMiracleDatabase`, `OnLoadPassiveDatabase`, `OnLoadCostumeDatabase`, `OnLoadPropDatabase` |
| Ajouter des unités ennemies ou des PNJ | `OnLoadUnitDatabase` + `RegisterNetworkPrefab(prefab, stableKey)` |
| Ajouter des voyageurs (PNJ du village) | `OnLoadSocialIDDatabase` |
| Ajouter l'affichage d'une stat | `OnLoadSpecialStatDatabase` + `RegisterSpecialStat` |
| Enregistrer du texte localisé depuis le code | `OnLocalizationReady` (`AddText`) |
| Ajouter un bouton au menu titre | `OnTitleMenuReady` |
| Début de partie ou de session | `OnStartGameServerside/Clientside`, `OnStartSessionServerside/Clientside` |
| Lors de la génération d'un étage | `OnFloorAllocatedServerside/Clientside`, `OnAssignRoomSpawnPhases` |
| Apparition d'un joueur ou d'une unité | `OnPlayerAvatarSpawned*`, `OnUnitSpawnedServerside`, `OnUnitDespawnedServerside` |
| Se brancher sur les vérifications de combat | `OnPlayerEvade`, `OnGetDebuff` |
| Permissions de manipulation de l'inventaire | `GridInventoryStartPermission`, `GridInventoryEndPermission` |


## Éviter les conflits

Les entités ajoutées par votre mod sont nommées au format `<modId>:<id>`, par ex. `yourname.artifact-pack:flame-ring`. Cette règle évite les conflits avec d'autres mods et permet de distinguer le contenu des mods dans l'historique des parties et les statistiques.


## Côté serveur, côté client

- Sephiria est conçu autour du multijoueur, de nombreux événements existent donc en versions distinctes côté serveur et côté client.
- L'hôte déclenche les deux ; les autres participants ne déclenchent que la version Clientside.
- En règle générale, ne faites ce qui modifie l'état du jeu (apparitions, butin, dégâts) que dans la version Serverside, et ce qui ne modifie que l'affichage dans la version Clientside.


## Patchs Harmony

Lorsque vous devez modifier quelque chose que les événements ne permettent pas d'atteindre, vous pouvez utiliser Harmony.
Depuis le patch 1.1.0, **Harmony 2.4.3 est intégré** au jeu ; il suffit donc d'y ajouter une référence.

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

- **N'embarquez pas `0Harmony.dll` avec votre mod.** Une copie embarquée est ignorée, et même si vous incluez une version différente, votre mod ne s'exécutera pas sur cette version.
- Utiliser votre `modId` tel quel comme ID Harmony évite les collisions avec les autres.
- Les patchs se cassent bien plus facilement que l'API officielle lors des mises à jour du jeu. Faites ce que vous pouvez avec les événements, et gardez les patchs en dernier recours.
- Les chargeurs externes comme BepInEx et MelonLoader ne sont pas pris en charge. Un mod qui se comporte comme un chargeur externe (c'est-à-dire qui trouve et exécute des DLL de plugins situées hors du Workshop) reçoit un avertissement et n'est activé qu'après acceptation par l'utilisateur. Voir [Mods qui exécutent du code extérieur au Workshop](#mods-qui-exécutent-du-code-extérieur-au-workshop) pour plus de détails.


## DLL auxiliaires

Si votre mod utilise d'autres bibliothèques (par ex. des paquets tiers autres que Newtonsoft.Json), placez leurs DLL dans le dossier de votre mod et déclarez-les dans `libraries` de `metadata.json`. Les DLL non déclarées ne sont pas utilisées pour la résolution des références.

```json
"libraries": [ "bin/MyHelper.dll" ]
```

- N'incluez pas de DLL déjà livrées avec le jeu (Unity, Mirror, Newtonsoft.Json, 0Harmony, etc.). La copie du jeu se charge en premier, donc celle incluse dans votre mod est soit ignorée, soit en conflit.
- Les DLL natives (C/C++) ne peuvent pas être utilisées. Seuls les assemblys managés sont chargés.
- Si un assembly du même nom a déjà été chargé par un autre mod, pour une DLL de `modules` le jeu n'en choisit pas un des deux : il **suspend l'exécution**. Pour une DLL de `libraries`, le jeu utilise celle qui est déjà chargée et ignore votre copie, ce qui peut provoquer des dysfonctionnements si les versions diffèrent. Il est plus sûr de séparer les bibliothèques partagées dans un mod distinct et de le lier via `dependencies`.

### Utiliser la bibliothèque d'un autre mod

Si plusieurs mods utilisent le même code, placez ce code dans un mod parent et liez les mods enfants à celui-ci via `dependencies`.

1. Le mod parent déclare le code partagé dans `modules` ou `libraries` et ajoute `Library` à ses `tags`.
2. Le mod enfant indique le `modId` du mod parent dans `dependencies` et se compile contre la DLL du mod parent avec `Private=false`. **Ne copiez pas la DLL du mod parent dans le mod enfant.**
3. Le jeu charge d'abord le mod parent ; les références du mod enfant sont donc résolues vers l'assembly du mod parent déjà chargé.

```json
"dependencies": [
  { "modId": "someone.shared-runtime", "workshopId": "123456789" }
]
```

- Le mod enfant doit aussi avoir son propre module `dll` et son `entryClass`. Il n'est pas pris en charge que le mod parent trouve et charge lui-même les DLL du mod enfant. Faites plutôt appeler par le mod enfant la fonction d'enregistrement du mod parent depuis son propre `OnModLoaded`.
- Si le mod parent modifie ses types ou membres publics lors d'une mise à jour, les mods enfants peuvent cesser de fonctionner. Évitez de supprimer ou de modifier les API publiques existantes d'une bibliothèque partagée.

### Ne créez pas de DLL à l'exécution !

Tout le code qu'un mod exécute doit se trouver dans un élément du Workshop examiné. Si les fichiers DLL d'un mod changent pendant l'exécution du mod, le chargement de ce mod est restreint. Incluez toutes les DLL telles quelles dans l'élément du Workshop, sans aucune compression ni streaming. Autrement dit, les cas suivants peuvent enfreindre le système d'examen. Placez toutes les DLL nécessaires dans le mod au moment de le publier.

- Le mod télécharge une DLL depuis l'extérieur pendant son exécution et charge l'assembly
- Le mod crée et exécute sa propre DLL pendant son exécution (y compris réécrire une autre DLL avec Mono.Cecil ou un outil similaire, puis l'exécuter)
- Le mod extrait une DLL empaquetée en lui avec son propre format de compression et l'utilise

### Mods qui exécutent du code extérieur au Workshop

Un mod qui trouve et exécute des DLL que l'utilisateur place **en dehors de son dossier de mod**, par exemple dans le dossier d'installation du jeu ou dans `AppData` (par exemple un mod de compatibilité avec les plugins d'un autre chargeur), n'est pas rejeté, mais le système d'examen lui ajoute un **avertissement**.

- Lorsque l'utilisateur active le mod, un avertissement s'affiche, et le mod n'est activé que s'il l'accepte. La question est reposée à chaque mise à jour du mod.
- Les DLL externes exécutées de cette façon ne sont pas examinées, et les problèmes qu'elles causent ne sont pas pris en charge.
- Un mod qui télécharge lui-même ces DLL pendant son exécution relève des cas interdits ci-dessus.

Pour utiliser le code d'un autre mod, ne chargez pas de DLL directement depuis le dossier de ce mod ; liez-le via `dependencies` comme décrit dans [Utiliser la bibliothèque d'un autre mod](#utiliser-la-bibliothèque-dun-autre-mod). Les DLL que le jeu n'a pas chargées lui-même échappent à l'ordre de chargement, à la vérification des conflits de noms et à celle des dépendances ; elles ne sont donc pas prises en charge.

Si vous devez réécrire une DLL (par exemple convertir l'IL pour une autre API Harmony), faites-le avant la publication et incluez la DLL convertie dans votre mod.


## Compiler et tester

```bash
dotnet build -c Release
```

- Une fois la compilation réussie et la DLL présente dans `WorkshopLocal/<mod>/bin/`, lancez le jeu, vérifiez que votre mod apparaît sous « Mes mods » dans le panneau Steam Workshop de l'écran titre, puis démarrez une partie.
- Si vous conservez le `.pdb` à côté de la sortie de compilation, les traces de pile dans le journal incluent les numéros de ligne. Retirez-le lors de la publication.


## Mises à jour du jeu et compatibilité

Lorsque le jeu est mis à jour, `Assembly-CSharp.dll` change. Les mods qui n'utilisent que les événements officiels continuent généralement de fonctionner, mais les mods qui touchent directement aux classes internes ou appliquent des patchs Harmony peuvent se casser.

- Après une mise à jour, recompilez avec le nouveau dossier `Managed/` et testez.
- Si vous indiquez la **version du jeu prise en charge** dans la description de votre page Workshop, les utilisateurs pourront trouver rapidement la cause d'un problème. La version du jeu peut aussi être enregistrée automatiquement dans les métadonnées de l'élément du Workshop lors de la publication.
- Si vous modifiez votre code, il doit à nouveau être soumis au système d'examen. Consultez [Publier dans le Workshop](publishing.md#examen-du-code).


## Considérations pour le multijoueur

- La plupart des mods avec code auront `multiplayer.installation` défini sur `everyone`.
- Si votre mod ne modifie l'état du jeu que dans des événements `Serverside`, et que les valeurs modifiées sont un état de jeu déjà synchronisé (par ex. `customStats`), de sorte que les participants n'ont besoin ni de code ni d'enregistrement, vous pouvez le déclarer `host_only`. Les participants ne téléchargent alors pas le mod. Un mod qui enregistre de nouveaux objets, utilise `RegisterNetworkPrefab` ou modifie quoi que ce soit dans des événements `Clientside` ne peut pas être `host_only`.
- Les objets créés via le réseau (unités, projectiles, etc.) doivent être enregistrés avec `RegisterNetworkPrefab` pour être créés aussi du côté des participants. Si vous passez une `stableKey`, le même ID est conservé même lorsque la version de votre mod change.
- L'hôte et les participants doivent même avoir le même **ordre de chargement** des mods. Évitez le code dont le résultat dépend de cet ordre.


## Synchronisation réseau (SyncVar, RPC, Command)

Le multijoueur de Sephiria repose sur la bibliothèque réseau open source « Mirror ». Mirror permet d'utiliser facilement les fonctions de communication en déclarant les attributs suivants.

- `[SyncVar]` : attribut placé sur une variable membre d'une classe. Il synchronise la valeur de la variable entre le serveur et les clients.
- `[ClientRpc]`, `[TargetRpc]` : attributs placés sur une fonction. Ils demandent aux clients d'exécuter la fonction depuis le serveur.
- `[Command]` : attribut placé sur une fonction. Il demande au serveur d'exécuter la fonction depuis un client.

Ces attributs ne fonctionnent pas correctement dans un environnement de mods. Ils ne marchent que si le Weaver de Mirror insère, lors de la compilation, le code de transmission correspondant à chaque attribut ; or une DLL générée en dehors du jeu ne passe pas par le Weaver, si bien que même si la compilation réussit, ces attributs ne font strictement rien. Ce type de bug paraît normal sur l'écran de l'hôte et ne pose problème que sur l'écran des participants : il est donc difficile à détecter en testant seul. (Même en forçant le passage par le Weaver, des bugs tels que des collisions de hachage de fonction peuvent subsister.)


### `SyncVar<T>`, `ModRpc`

Pour résoudre ce problème, Sephiria fournit à la place du code de communication destiné aux mods : `SyncVar<T>` et `ModRpc`.

| Ce que vous voulez faire | À utiliser |
|---|---|
| Garder une valeur synchronisée en permanence (y compris pour un participant qui arrive en retard) | `SyncVar<T>` |
| Appel du serveur vers tous les clients | `ModRpc.Send` |
| Appel du serveur vers un client précis | `ModRpc.SendTo` |
| Appel d'un client vers le serveur | `ModRpc.SendCommand` |
| Modifier une valeur que le jeu synchronise déjà | Propriété `Network<FieldName>` |

- Cette fonctionnalité est disponible à partir de la version 1.1.2 du jeu.
- Un mod qui utilise cette fonctionnalité doit déclarer `multiplayer.installation` à `everyone`.


### Préparer le composant

`SyncVar<T>` et `ModRpc` ne peuvent être utilisés que dans un composant qui hérite de `NetworkBehaviour`. Ce composant doit être attaché au même objet, **dans le même ordre**, chez l'hôte et chez les participants. Si vous l'attachez avec `AddComponent` à un objet déjà instancié, il n'est pas pris en compte par la synchronisation : il doit donc déjà être attaché avant l'enregistrement de l'objet comme prefab réseau.

```csharp
private static GameObject prefab;

// Abonnez-vous à HorayModAPI.OnAllDatabasesReady depuis OnModLoaded.
private static void RegisterPrefab()
{
    if (prefab) return;

    // Placez-le sous un parent inactif. L'Awake du prefab ne s'exécute pas, et les copies créées avec Instantiate démarrent actives.
    var holder = new GameObject("MyMod_Prefabs");
    holder.SetActive(false);
    Object.DontDestroyOnLoad(holder);

    prefab = new GameObject("FlameOrb");
    prefab.transform.SetParent(holder.transform);
    prefab.AddComponent<NetworkIdentity>();
    prefab.AddComponent<FlameOrb>();
    HorayModAPI.RegisterNetworkPrefab(prefab, "yourname.my-mod:flame-orb");
}

// Faites apparaître l'objet depuis le serveur. Si vous passez le deuxième argument, ce joueur en devient le propriétaire.
private static void SpawnFor(PlayerAvatar player)
{
    var orb = Object.Instantiate(prefab);
    NetworkServer.Spawn(orb, player.connectionToClient);
}
```

### `SyncVar<T>` : synchroniser une valeur

```csharp
public class FlameOrb : NetworkBehaviour
{
    public readonly SyncVar<int> stack = new SyncVar<int>(0);

    public FlameOrb()
    {
        // Comme il n'y a pas de Weaver, enregistrez-la vous-même dans le constructeur. L'ordre des champs doit toujours être le même.
        InitSyncObject(stack);
    }

    private void Awake()
    {
        stack.Callback += (oldValue, newValue) => UpdateVisual(newValue);
    }

    private void UpdateVisual(int value) { }
}
```

- Modifiez la valeur **sur le serveur**, par exemple avec `stack.Value = 3`. Une valeur modifiée par un participant n'est pas transmise au serveur.
- Un participant qui arrive en retard reçoit lui aussi la valeur actuelle à son entrée.
- `Callback` est appelé chez l'hôte lorsqu'une valeur est affectée, et chez les participants lorsqu'une valeur est reçue. Il est aussi appelé si la valeur reçue à l'entrée diffère de la valeur par défaut. Une exception levée dans le callback est consignée dans le journal et la synchronisation continue.
- Vous pouvez utiliser des types de base comme `int`, `float`, `string` et `Vector3`. Le code de sérialisation n'est pas généré automatiquement pour un struct créé par un mod ; envoyez donc ses champs séparément.
- Pour désigner un autre objet, n'utilisez pas `SyncVar<GameObject>` : envoyez son `netId` dans un `SyncVar<uint>`. Il est possible que, côté réception, cet objet ne soit pas encore créé.
- Le nombre de SyncVar enregistrables sur un composant est de 64, héritage compris.

### `ModRpc` : appeler une fonction

Enregistrez une fois la fonction au chargement du mod. Le nom utilisé à l'appel est `modId:functionName`.

```csharp
protected override void OnModLoaded()
{
    ModRpc.RegisterRpc<FlameOrb>("yourname.my-mod", "explode", (orb, r) => orb.PlayExplosion(r.ReadVector3()));
    ModRpc.RegisterCommand<FlameOrb>("yourname.my-mod", "ignite", (orb, r, sender) => orb.ServerIgnite(r.ReadInt(), sender));
}
```

```csharp
// Serveur → tous les clients qui voient cet objet
ModRpc.Send(this, "yourname.my-mod:explode", w => w.WriteVector3(position));

// Serveur → un client. Si vous passez null comme connexion, l'envoi va au propriétaire de l'objet.
ModRpc.SendTo(connectionToClient, this, "yourname.my-mod:explode", w => w.WriteVector3(position));

// Client → serveur
ModRpc.SendCommand(this, "yourname.my-mod:ignite", w => w.WriteInt(slot));
```

- Lisez dans l'ordre où vous avez écrit. Si l'ordre ou les types de l'écriture et de la lecture diffèrent, les valeurs sont faussées ou une exception est levée.
- Par défaut, seul **le propriétaire de l'objet** peut appeler `RegisterCommand`. Pour le recevoir sur un objet sans propriétaire, passez `requiresAuthority: false`. Dans ce cas, n'importe quel participant peut l'appeler. Prenez garde à la triche, etc.
- Si vous envoyez dans une direction différente de celle de l'enregistrement, ou sous un nom non enregistré, seul un message d'erreur est consigné et rien n'est transmis.
- Si la fonction est absente côté réception ou si le type du composant diffère, seul un avertissement est consigné et l'appel est ignoré. Une exception levée dans le gestionnaire est également consignée, et le jeu continue.
- Le canal par défaut est le transport fiable. Pour des effets envoyés fréquemment, vous pouvez passer l'argument `channelId: Channels.Unreliable`.

### Points à respecter absolument

- Un client peut envoyer n'importe quelle valeur via un Command. Validez dans le gestionnaire serveur le numéro d'emplacement, la quantité, la cible, etc.
- Un participant qui arrive en retard ne reçoit pas les RPC antérieurs à son entrée. Envoyez l'état à conserver avec `SyncVar<T>`, et réservez les RPC aux événements passagers comme les sons et les effets.
- **Un RPC peut arriver avant le SyncVar.** Si vous lisez `stack.Value` dans le gestionnaire d'un RPC, vous pouvez obtenir l'ancienne valeur. Si vous avez besoin de la nouvelle valeur, envoyez-la aussi comme argument du RPC, ou traitez-la dans `Callback`.

### Modifier les valeurs du code de base de Sephiria

Si vous affectez directement un champ `[SyncVar]` d'une classe du jeu, la valeur n'est pas transmise aux participants. Utilisez la propriété préfixée par `Network`. La casse est celle du nom du champ, telle quelle.

```csharp
unit.maxHp = 500;         // Seule la valeur de l'hôte change.
unit.NetworkmaxHp = 500;  // Elle est aussi transmise aux participants.
```

Avant l'apparition de l'objet (juste après le prefab ou `Instantiate`), même une affectation directe est transmise avec la valeur lors de l'apparition.

### Test

L'hôte seul ne fait pas apparaître les problèmes ci-dessus. Avant de publier, vérifiez avec deux PC l'écran de l'hôte et celui du participant, et testez aussi le cas d'une entrée tardive après avoir modifié une valeur. Quand le paquet local (`WorkshopLocal`) est activé, personne d'autre ne peut rejoindre la partie ; pour tester le multijoueur, abonnez-vous donc à un élément que vous avez mis en ligne sur le Workshop en Unlisted. Un élément mis en ligne pour la première fois est automatiquement Unlisted.


## Dépannage

| Symptôme | Que vérifier |
|---|---|
| Le résumé d'état indique que la classe de point d'entrée est introuvable | L'espace de noms ou le nom de classe dans `entryClass` ne correspond pas à votre code. Les classes imbriquées s'écrivent `Outer+Inner`. |
| Le résumé d'état affiche une erreur de type de la classe de point d'entrée | La classe de point d'entrée n'hérite pas de `WorkshopModBase` (ou `HorayModBase`), n'est pas publique, est abstraite, ou n'a pas de constructeur public sans paramètre. |
| Le résumé d'état indique que le mod a été suspendu à cause d'un conflit de nom d'assembly | Un autre mod (AddOns compris) utilise le même `AssemblyName`. Renommez le vôtre. |
| Le résumé d'état affiche un échec de chargement de bibliothèque | Une DLL listée dans `libraries` est manquante ou n'est pas un assembly managé. |
| `FileNotFoundException` dans le journal | Une DLL référencée n'est pas dans le dossier de votre mod ou n'est pas déclarée dans `libraries`. Ou bien une DLL du jeu a été copiée avec `Private=true` et les deux entrent en conflit. |
| Exception juste après l'entrée dans le monde | Une exception dans un gestionnaire d'événement de base de données. Cherchez l'espace de noms de votre mod dans la trace de pile du journal. |
| Les effets s'accumulent après un retour à l'écran titre et une nouvelle entrée | Vous vous réabonnez à l'intérieur d'un gestionnaire. Abonnez-vous une seule fois, dans `OnModLoaded`. |


## Exemple

[`examples/dll-sample`](../../examples/dll-sample) est un mod minimal qui augmente les Dégâts physiques de 5 à l'apparition du joueur. Son dossier `package/` inclut une DLL précompilée ; il suffit donc de copier ce dossier dans `WorkshopLocal/` pour qu'il fonctionne immédiatement sans compilation. Pour en faire votre propre mod, vous n'avez qu'à changer `AssemblyName`, `modId`, `entryClass` et le chemin de sortie. Démarrez une partie et ouvrez la fenêtre des stats pour le voir fonctionner sous la forme Dégâts physiques +5.
