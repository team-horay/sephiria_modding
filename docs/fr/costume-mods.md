# Mods de tenues

Ajoutons une tenue en n'utilisant que des fichiers PNG et JSON, sans code. Ce type de mod peut ajouter les deux choses suivantes :

- **Nouvelles tenues** : créez une tenue entièrement nouvelle. Vous pouvez définir son apparence, ses stats, ses objets de départ, etc.
- **Apparences pour des tenues existantes** : ajoutez une apparence (skin) à une tenue existante comme Lapin rose.

Cette page s'appuie sur l'exemple [`examples/costume-sample`](../../examples/costume-sample). Téléchargez-le pour vous faire une idée de la structure d'un mod.

## Structure du mod

```
costume-sample/
├── metadata.json
├── thumbnail.png
├── Costumes/                  ← dossier des tenues
│   └── WhiteCat/              ← un dossier = une tenue
│       ├── costume.json
│       ├── icon.png
│       ├── idle.png
│       ├── move.png
│       ├── idle_back.png
│       ├── move_back.png
│       ├── attack.png
│       └── ...                ← PNG des autres mouvements
└── Localization/              ← textes du nom et de la description de la tenue
    ├── ko-KR.json
    ├── en-US.json
    └── ...
```


## metadata.json

Lorsque vous créez votre propre mod à partir de l'exemple, changez `modId` et `author` dans `metadata.json`. (Consultez [`metadata.json`](metadata.md) pour plus de détails.)

```json
{
  "info": {
    "title": { "ko-KR": "하얀 고양이 코스튬", "en-US": "White Cat Costume" },
    "description": { "ko-KR": "하얀 고양이 코스튬을 추가합니다.", "en-US": "Adds the White Cat costume." }
  },
  "tags": ["Costume"],
  "mod": {
    "schemaVersion": 1,
    "modId": "yourname.costume-whitecat",
    "version": "1.0.0",
    "author": "Your Name",
    "multiplayer": { "installation": "everyone" },
    "modules": [
      { "type": "costume", "path": "Costumes" },
      { "type": "localization", "path": "Localization" }
    ]
  }
}
```


## Animations de sprites

- Une tenue nécessite pas mal d'animations si vous les incluez toutes.
- Si c'est votre première tenue, essayez d'en créer une simple avec seulement `idle, move, airborne, fall`.

| Mouvement | Description | Obligatoire |
|---|---|---|
| `idle` | Immobile | Obligatoire |
| `move` | Déplacement | Obligatoire |
| `airborne` | En l'air | Facultatif |
| `fall` | Effondrement | Facultatif |
| `attack` | Attaque | Facultatif |
| `greatsword_lower` | Coup tournoyant | Facultatif |
| `greatsword_sweep` | Rotation | Facultatif |
| `hitfeedback` | Coup reçu | Facultatif |

### Animations de dos

- Pour une tenue plus naturelle, vous pouvez dessiner des sprites de dos. C'est facultatif.
- Si vous décidez d'ajouter des sprites de dos, vous devez inclure les mouvements `idle_back, move_back`.
- Les animations de dos sont définies comme suit.

| Mouvement | Description |
|---|---|
| `idle_back` | Immobile (dos) |
| `move_back` | Déplacement (dos) |
| `attack_back` | Attaque (dos) |
| `greatsword_upper` | Coup tournoyant (dos) |

## Préparer les graphismes

- Pour chaque mouvement, créez une feuille de sprites PNG avec les images de ce mouvement disposées horizontalement.
- La hauteur de l'image est égale à la hauteur d'une image, et la largeur vaut largeur d'image × nombre d'images. Ne laissez pas d'espace entre les images.
- Nous recommandons de donner la même taille à chaque image individuelle.
- Dans l'exemple du Chat blanc, où chaque sprite fait 20×16, les tailles d'image sont les suivantes.

| Fichier | Taille | Images |
|---|---|---|
| `idle.png` | 120×16 | 6 |
| `move.png` | 160×16 | 8 |
| `attack.png` | 60×16 | 3 |
| `fall.png` | 20×16 | 1 |

*Image d'exemple*


### Rédiger les informations de la nouvelle tenue

Définissez votre propre tenue en rédigeant un fichier `costume.json`.

Exemple `Costumes/WhiteCat/costume.json` :

```json
{
  "schemaVersion": 1,
  "id": "whitecat",
  "name": "Costume_WhiteCat_Name",
  "flavorText": "Costume_WhiteCat_FlavorText",
  "icon": "icon.png",

  "frameSize": [20, 16],
  "pivot": [10, 2],
  "pixelsPerUnit": 16,

  "animations": {
    "idle":             { "file": "idle.png", "fps": 12 },
    "idle_back":        { "file": "idle_back.png", "fps": 12 },
    "move":             { "file": "move.png", "fps": 4, "footstepFrames": [3, 7] },
    "move_back":        { "file": "move_back.png", "fps": 4, "footstepFrames": [3, 7] },
    "attack":           { "file": "attack.png", "fps": 20, "loop": false },
    "attack_back":      { "file": "attack_back.png", "fps": 20, "loop": false },
    "greatsword_lower": { "file": "greatsword_lower.png", "fps": 20, "loop": false },
    "greatsword_upper": { "file": "greatsword_upper.png", "fps": 20, "loop": false },
    "greatsword_sweep": { "file": "greatsword_sweep.png", "fps": 30, "loop": false },
    "airborne":         { "file": "airborne.png", "fps": 12 },
    "fall":             { "file": "fall.png", "fps": 1 },
    "hitfeedback":      { "file": "hitfeedback.png", "fps": 20, "loop": false }
  },

  "stats": ["DEFENSE/-10", "EVASION/1200"],
  "startingItems": [],
  "onlyDefaultWeapon": false
}
```

| Champ | Obligatoire | Description |
|---|---|---|
| `schemaVersion` | Oui | Identifiant du format. Saisissez l'entier `1`. |
| `id` | Oui | Utilisez des lettres latines minuscules, des chiffres, des tirets bas et des tirets, de 1 à 31 caractères. Pour éviter les collisions, il est enregistré en jeu sous la forme `<modId>:<id>`. |
| `name` | Oui | La **clé de localisation** du nom de la tenue. Voir [Préparer les fichiers de localisation](#préparer-les-fichiers-de-localisation) ci-dessous. |
| `flavorText` | Non | La **clé de localisation** de la description de la tenue. Voir [Préparer les fichiers de localisation](#préparer-les-fichiers-de-localisation) ci-dessous. |
| `icon` | Oui | Chemin vers le PNG de l'icône d'aperçu de la tenue. |
| `face` | Non | Chemin vers le PNG du visage pour l'ATH multijoueur. |
| `pixelsPerUnit` | Non | Modifiez cette valeur pour utiliser des graphismes plus détaillés. Plus la valeur est élevée, plus le rendu est fin. Le PPU par défaut est 16. |
| `frameSize` | Non | La `[largeur, hauteur]` de chaque sprite. Par défaut : `[16, 16]`. |
| `pivot` | Non | Le point de pivot de chaque sprite. Le coin inférieur gauche est `[0, 0]`. Par défaut : `[largeur/2, 0]`. |
| `animations` | Oui | Graphismes de chaque mouvement. Voir [animations](#animations). |
| `stats` | Non | Liste des stats à définir. Consultez la [Liste des stats](stats.md). |
| `startingItems` | Non | Tableau des ID des objets de départ. Consultez la [Liste des objets](item-table.md). |
| `body` | Non | Définit la hitbox. Voir [body](#body). |
| `skins` | Non | Apparences supplémentaires pour la même tenue. Voir [skins](#skins). |

### animations

```json
  "animations": {
    "idle":      { "file": "idle.png", "fps": 12 },
    "move":      { "file": "move.png", "fps": 4, "footstepFrames": [3, 7] },
    "attack":    { "file": "attack.png", "fps": 20, "loop": false },
    "fall":      { "file": "fall.png", "fps": 1 },
    "hitfeedback": { "file": "hitfeedback.png", "fps": 20, "loop": false, "frameSize": [24, 16], "pivot": [12, 2] }
  }
```

- Définissez les animations préparées ci-dessus avec quatre propriétés : `{ "file", "fps", "loop", "footstepFrames" }`.
- Vous pouvez aussi définir `frameSize` et `pivot` séparément pour chaque mouvement.
- Si vous n'avez dessiné qu'un seul mouvement à une taille différente, comme sur la dernière ligne, définissez `frameSize` et `pivot` sur ce mouvement.

#### Propriétés

| Champ | Obligatoire | Description |
|---|---|---|
| `file` | Oui | Le nom du fichier à lire. |
| `fps` | Non | Vitesse de lecture de l'animation. (Par défaut : 12) |
| `loop` | Non | Si l'animation boucle. (Par défaut : true ; false pour les animations d'attaque) |
| `footstepFrames` | Non | Numéros d'images à partir de zéro. Un bruit de pas est joué lorsque ces images sont affichées. |


### body

```json
"body": { "hitboxRadius": 4, "hitboxOffset": [0, 4], "movementRadius": 0.25, "bodyScale": 1 }
```

- Définit les zones de collision de déplacement et de coup du personnage.
- Les tenues intégrées utilisent ces valeurs par défaut.
- Sauf raison particulière, nous recommandons d'omettre ce champ.

#### Propriétés

| Champ | Obligatoire | Description |
|---|---|---|
| `hitboxRadius` | Non | Rayon du cercle de hitbox, en pixels, de 0,5 à 64. Par défaut `4`. |
| `hitboxOffset` | Non | Centre du cercle de hitbox, `[x, y]`, en pixels par rapport au pivot ; x positif vers la droite et y positif vers le haut, ±64. Par défaut `[0, 4]`. |
| `movementRadius` | Non | Rayon du cercle de collision de déplacement qui entre en collision avec les murs et les obstacles, en unités (16 pixels = 1), de 0,05 à 2. Par défaut `0.25`. |
| `bodyScale` | Non | Échelle de la position de l'épaule où l'arme est tenue, de 0,1 à 4. Par défaut `1`. Augmentez-la lorsque l'arme s'enfonce dans le corps d'une grande tenue. |



### skins

```json
"skins": [
  { "id": "winter", "name": "Skin_WhiteCat_Winter_Name", "folder": "skins/Winter", "icon": "icon.png",
    "animations": { "idle": "idle.png", "move": "move.png" } }
]
```

- Ajoute des apparences supplémentaires à la même tenue.
- Les animations suivent le même format que ci-dessus.
- Si vous omettez `frameSize` ou `pivot`, les valeurs propres à la tenue sont utilisées.




## Préparer les fichiers de localisation

`Localization/ko-KR.json` :

```json
{
  "Costume_WhiteCat_Name": "하얀 고양이",
  "Costume_WhiteCat_FlavorText": "줄무늬가 인상적입니다."
}
```

`Localization/en-US.json` :

```json
{
  "Costume_WhiteCat_Name": "White Cat",
  "Costume_WhiteCat_FlavorText": "The stripes are rather impressive."
}
```

- Dans `name` et `flavorText`, écrivez une **clé de localisation** plutôt qu'un texte, et placez le texte dans le dossier `Localization/` du même mod, un fichier par langue.
- Utilisez des lettres latines, des chiffres, des tirets bas et des tirets pour les clés, sans espaces. Inclure le nom de votre mod aide à éviter les collisions avec d'autres mods.
- Si une clé **n'est présente dans aucun fichier de langue**, la tenue ne sera pas chargée. Vous devez fournir au moins une langue.
- Nous recommandons d'inclure aussi l'anglais (`en-US`) afin que davantage de personnes puissent profiter de votre tenue.
- N'oubliez pas de déclarer également un module `localization` dans `modules` de `metadata.json`.



## Vérifier

1. Placez le dossier de votre mod dans `WorkshopLocal/` et lancez le jeu.
2. Démarrez une partie, ouvrez la sélection de tenue dans le village (**Changer de tenue**) et vérifiez que votre nouvelle tenue a été ajoutée.
3. Si vous modifiez le contenu de la tenue, vous devez redémarrer le jeu.



## Bonus : ajouter seulement un skin à une tenue existante

Vous pouvez créer une apparence pour une tenue existante en rédigeant un fichier `skin.json` au lieu de `costume.json`.

```
Costumes/
└── PinkRabbit_Pajama/
    ├── skin.json
    ├── icon.png
    ├── idle.png
    └── move.png
```

```json
{
  "schemaVersion": 1,
  "costume": "PinkRabbit",
  "id": "pajama",
  "name": "Skin_PinkRabbit_Pajama_Name",
  "icon": "icon.png",
  "animations": { "idle": "idle.png", "move": "move.png" }
}
```

- `costume` est l'id de la tenue cible. Utilisez l'id d'une tenue intégrée (comme `PinkRabbit`) ou, pour la tenue d'un autre mod, son id d'exécution incluant le nom du mod. (Pour l'exemple du Chat blanc, `yourname.costume-whitecat:whitecat`.)
- `stats`, `startingItems`, `body`, `skins` et `flavorText` sont repris tels quels de la tenue existante ; vous ne pouvez donc pas les définir.
- Le remplacement de l'apparence par défaut d'une tenue intégrée n'est pas pris en charge.
