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
- Les chargeurs externes comme BepInEx et MelonLoader ne sont pas pris en charge.


## DLL auxiliaires

Si votre mod utilise d'autres bibliothèques (par ex. des paquets tiers autres que Newtonsoft.Json), placez leurs DLL dans le dossier de votre mod et déclarez-les dans `libraries` de `metadata.json`. Les DLL non déclarées ne sont pas utilisées pour la résolution des références.

```json
"libraries": [ "bin/MyHelper.dll" ]
```

- N'incluez pas de DLL déjà livrées avec le jeu (Unity, Mirror, Newtonsoft.Json, 0Harmony, etc.). La copie du jeu se charge en premier, donc celle incluse dans votre mod est soit ignorée, soit en conflit.
- Les DLL natives (C/C++) ne peuvent pas être utilisées. Seuls les assemblys managés sont chargés.
- Si un assembly du même nom a déjà été chargé par un autre mod, le jeu n'en choisit pas un des deux : il **suspend l'exécution** de votre mod. Il est plus sûr de séparer les bibliothèques partagées dans un mod distinct et de le lier via `dependencies`.


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
- Si vous modifiez votre code, il doit à nouveau passer l'examen avant de pouvoir être publié. Consultez [Publier dans le Workshop](publishing.md).


## Considérations pour le multijoueur

- La plupart des mods avec code auront `multiplayer.installation` défini sur `everyone`.
- Si votre mod ne modifie l'état du jeu que dans des événements `Serverside`, et que les valeurs modifiées sont un état de jeu déjà synchronisé (par ex. `customStats`), de sorte que les participants n'ont besoin ni de code ni d'enregistrement, vous pouvez le déclarer `host_only`. Les participants ne téléchargent alors pas le mod. Un mod qui enregistre de nouveaux objets, utilise `RegisterNetworkPrefab` ou modifie quoi que ce soit dans des événements `Clientside` ne peut pas être `host_only`.
- Les objets créés via le réseau (unités, projectiles, etc.) doivent être enregistrés avec `RegisterNetworkPrefab` pour être créés aussi du côté des participants. Si vous passez une `stableKey`, le même ID est conservé même lorsque la version de votre mod change.
- L'hôte et les participants doivent même avoir le même **ordre de chargement** des mods. Évitez le code dont le résultat dépend de cet ordre.


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
