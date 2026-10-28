# metadata.json

Le fichier `metadata.json`, situé à la racine du dossier de votre mod, est le fichier central qui décrit à la fois **ce qu'est** votre mod et **comment** il s'exécute.


## Structure générale

```json
{
  "info": {
    "title": "New Artifact Pack",
    "description": "Adds 12 artifacts.",
    "preview": "thumbnail.png"
  },
  "mod": {
    "schemaVersion": 1,
    "modId": "yourname.artifact-pack",
    "version": "0.9.1",
    "author": "Your Name",
    "multiplayer": { "installation": "everyone" },
    "modules": [
      { "type": "dll", "path": "bin/ArtifactPack.dll", "entryClass": "ArtifactPack.Entry" },
      { "type": "localization", "path": "Localization" }
    ],
    "libraries": [
      "bin/Helper.dll"
    ],
    "dependencies": [
      { "modId": "someone.shared-runtime", "version": "2.5.3", "workshopId": "123456789" }
    ]
  },
  "tags": ["Item"]
}
```

Le niveau supérieur définit trois éléments.

| Clé | Rôle | Obligatoire |
|---|---|---|
| `info` | Titre, description et aperçu affichés sur le Steam Workshop et dans le panneau en jeu | Obligatoire |
| `tags` | Tags du Workshop | Facultatif |
| `mod` | Comment le mod s'exécute | Obligatoire |

## info

### title

- Le nom du mod. S'il est vide, le `modId` est utilisé à la place.
- Vous pouvez utiliser une table de langues pour définir le nom du mod dans chaque langue.
	- En plus de votre propre langue, nous vous recommandons d'ajouter l'anglais.
- Codes de langue pris en charge : `ko-KR`, `en-US`, `ja-JP`, `zh-CN`, `zh-TW`, `de-DE`, `fr-FR`, `es-ES`, `it-IT`, `pl-PL`, `pt-BR`, `ru-RU`, `sv-SE`, `th-TH`, `tr-TR`

```json
"info": {
  "title": { "en-US": "New Artifact Pack", "ko-KR": "새 아티팩트 팩" }
}
```

### description

- La description du mod.
- Comme pour `title`, vous pouvez définir la description par langue.

```json
"info": {
  "description": { "en-US": "Adds 12 artifacts.", "ko-KR": "아티팩트 12종을 추가합니다." }
}
```

### preview

- Une image d'aperçu est obligatoire pour publier dans le Workshop. (PNG ou JPG, moins de 1 Mo)
- Indiquez le chemin relatif vers l'image d'aperçu.
- Si ce champ est omis, le jeu cherche `thumbnail.png`, `preview.png`, `thumbnail.jpg` et `preview.jpg` dans le dossier racine, dans cet ordre.


## mod

### schemaVersion

- La version actuelle est 1.
- Notez que ce numéro pourra augmenter à l'avenir en cas de changement majeur dans le fonctionnement des mods.

### modId

- Une chaîne unique qui identifie votre mod. Utilisez des lettres latines minuscules, des chiffres, des points, des tirets et des tirets bas. 64 caractères maximum.
- **Choisissez un nom unique.** Si votre `modId` entre en conflit avec celui d'un autre mod, il ne fonctionnera pas correctement. Le préfixer avec votre nom de créateur réduit fortement les risques de collision.
(par ex. `horay.artifact-pack`)
- **Ne changez plus votre `modId` une fois publié.** D'autres personnes peuvent créer des mods enfants basés sur le vôtre via `dependencies`, et ils identifient le mod parent par son `modId`.


### version

- Une chaîne qui définit la version de votre mod.
- C'est elle qui est comparée lorsqu'un autre mod exige une version précise via `dependencies.version`.
- **Incrémentez la version à chaque mise à jour de votre mod.** Lorsqu'un joueur rejoint un salon multijoueur, le jeu vérifie que l'hôte et les participants ont les mêmes versions de mods. Si vous modifiez le contenu en gardant la même version, des joueurs avec un contenu de mod différent peuvent se retrouver dans le même salon, ce qui peut faire échouer la synchronisation du contenu ou bloquer la progression de la partie.
- Nous recommandons le même format 1.0.0 que celui utilisé par le jeu.

### author

Faites savoir à tout le monde qui vous êtes !

### multiplayer / installation

Définit si les joueurs d'une partie multijoueur doivent partager ce mod. Valeurs autorisées :

| Valeur | Description |
|---|---|
| `local_only` | N'affecte que votre propre écran et votre interface (les mods de localisation et les mods d'interface simples entrent dans cette catégorie) |
| `everyone` | Tout le monde dans le salon doit avoir activé le même mod (tout mod qui ajoute ou modifie du contenu entre dans cette catégorie) |
| `host_only` | Seul l'hôte doit l'avoir activé ; fonctionne sans changement côté participants (les mods multijoueur qui n'affectent que les calculs côté serveur entrent dans cette catégorie) |

Si vous déclarez `installation` comme `local_only` ou `host_only` alors que votre mod touche à du contenu dont les participants ont aussi besoin (enregistrement de nouveaux objets ou unités, objets réseau, code qui s'exécute aussi sur les clients des participants), les joueurs d'un même salon risquent de jouer à des parties différentes, ou la synchronisation du contenu peut échouer et bloquer la progression normale de la partie. Vérifiez bien ce que fait réellement votre mod.


### modules

- Les modules définissent ce que fait votre mod, ce qui en fait **la section la plus importante**.
- Les modules actuels sont `localization, dll, costume, data`.
- Comme dans l'exemple ci-dessous, vous pouvez combiner plusieurs modules dans un seul paquet.
- Si un seul module est inconnu, l'ensemble du paquet est marqué « non pris en charge » et le mod est ignoré au chargement.

```json
"modules": [
  { "type": "localization", "path": "Localization" },
  { "type": "dll", "path": "bin/ArtifactPack.dll", "entryClass": "ArtifactPack.Entry" },
  { "type": "costume", "path": "Costumes" },
  { "type": "data", "path": "Packs", "consumer": "someone.shared-runtime" }
]
```

#### type : module localization

```json
"modules": [
  { "type": "localization", "path": "Localization" }
]
```

- Le module `localization` est destiné aux mods qui incluent des fichiers de langue.
- Définissez `path` sur le dossier qui contient les fichiers de langue.
- Placez dans le dossier `path` des **fichiers JSON nommés d'après les codes de langue**, comme `ko-KR.json` et `en-US.json`. Consultez les [Mods de localisation](localization-mods.md) pour les codes de langue et le format de fichier utilisés par le jeu.

#### type : module dll

```json
"modules": [
  { "type": "dll", "path": "bin/ArtifactPack.dll", "entryClass": "ArtifactPack.Entry" }
]
```

- Le module `dll` vous permet de vous brancher sur le jeu avec du code C#.
- Définissez `path` sur le chemin du fichier DLL.
- Définissez `entryClass` sur le nom complet de la classe de point d'entrée dans la DLL, espace de noms compris. Le jeu ne cherche pas de point d'entrée dans la DLL, vous **devez donc le spécifier**. (Pour les classes imbriquées, utilisez `Ns.Outer+Inner`.)
- N'utilisez pas le même `entryClass` dans deux modules différents.
- Fonctionne uniquement sur les builds Mono.
- Consultez les [Mods DLL](dll-mods.md) pour plus de détails.

#### type : module costume

```json
"modules": [
  { "type": "costume", "path": "Costumes" }
]
```

- Vous permet de créer vos propres tenues sans écrire la moindre ligne de C#.
- Définissez `path` sur le dossier qui contient vos fichiers de tenue.
- En multijoueur, tout le monde doit l'avoir installé.
- Consultez les [Mods de tenues](costume-mods.md) pour plus de détails.


#### type : module data

```json
"modules": [
  { "type": "data", "path": "Packs", "consumer": "someone.shared-runtime" }
]
```

- Un module de données générique pour les mods. Il n'est pas utilisé dans les cas courants ; il sert principalement aux dépendances entre mods.
- `consumer` indique quel mod utilisera ce module de données, mais vous l'omettez en général. S'il est omis, tous les mods listés dans `dependencies` peuvent lire ces données.
- Si vous avez plusieurs mods parents et que les données ne sont destinées qu'à l'un d'eux, indiquez le `modId` de ce mod dans `consumer`. Cette valeur doit aussi figurer dans `dependencies`.

### libraries

```json
"libraries": [
  "bin/Helper.dll",
  { "path": "bin/Newtonsoft.Json.dll" }
]
```

- La liste des assemblys auxiliaires référencés par votre module `dll`.
- Le jeu résout les références en utilisant **uniquement les fichiers déclarés ici**. Il ne suffit pas de placer une DLL dans le dossier pour qu'elle soit trouvée.
- Chaque entrée peut être une chaîne ou un objet `{ "path": "..." }`.
- Chaque entrée doit se terminer par `.dll`, et le mod ne fonctionnera pas correctement si elle fait doublon avec une DLL de `modules`.
- Les bibliothèques ne sont pas traitées comme des points d'entrée. Même si l'une d'elles contient une classe `WorkshopModBase`, elle ne sera pas appelée.

### dependencies

```json
"dependencies": [
  { "modId": "someone.shared-runtime", "version": "2.5.3", "workshopId": "123456789" }
]
```

- Si un mod parent est un mod de « création de contenu », un mod enfant peut ajouter du nouveau contenu construit par-dessus.
- Dans ce cas, vous pouvez déclarer une dépendance vers un autre mod avec `dependencies`.
- Lorsqu'un mod parent est spécifié, votre mod ne peut pas être chargé avant ce mod parent.
- Si un mod parent est manquant, désactivé ou échoue au chargement, le chargement de tous ses mods enfants est annulé.
- Une dépendance peut comporter les trois champs suivants.

| Champ | Obligatoire | Règle |
|---|---|---|
| `modId` | Oui | Le `modId` du mod sur lequel vous vous appuyez |
| `version` | Non | **À utiliser uniquement si vous avez besoin d'une chaîne de version strictement identique.** Si la version diffère, le chargement est ignoré ; il est donc généralement plus sûr de l'omettre. |
| `workshopId` | Non | Permet aux utilisateurs de trouver le mod requis et de s'y abonner. |


## tags

- Les catégories utilisées lors de la publication dans le Workshop.
- Un mod peut avoir plusieurs tags. (Pour un mod tout-en-un qui ajoute des objets et des tenues, utilisez `"tags": ["Item", "Costume"]`.)

| Catégorie | Contenu |
|---|---|
| `Item` | Objets (artefacts, tablettes, potions, etc.), combos |
| `Costume` | Tenues |
| `Weapon` | Armes |
| `NPC` | Voyageurs, marchands, etc. |
| `Enemy` | Nouveaux ennemis, boss, etc. |
| `Gameplay` | Modifications du gameplay |
| `QoL` | Améliorations de l'interface, du confort et de l'accessibilité |
| `Library` | Runtimes, frameworks, packs de données, etc. dont dépendent d'autres mods |
| `Localization` | Traductions |

## Règles relatives aux chemins
 
- Les chemins dans `modules[].path`, `libraries` et `info.preview` suivent tous les mêmes règles.
- Seuls les chemins relatifs au dossier du mod sont autorisés. (Les chemins absolus, les lettres de lecteur et `..` sont refusés.)
- Utilisez `/` comme séparateur.
- Les chemins passant par des liens symboliques ou des jonctions sont refusés.
- **La casse doit correspondre exactement au nom réel.** Sous Windows, écrire `localization` ouvre quand même le dossier, mais le jeu le refuse car différent de `Localization`. (Cette règle garantit le même comportement sur Steam Deck (Linux).)
