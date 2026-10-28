# Votre premier mod

Ce guide explique la structure de base d'un mod Sephiria et vous accompagne dans la création de votre premier mod, jusqu'à sa vérification en jeu.

## Ce dont vous avez besoin

- Un bon éditeur de texte (par exemple [Visual Studio Code](https://code.visualstudio.com/))

## Trouver le dossier d'installation du jeu

Ouvrez le dossier d'installation de Sephiria.

- Depuis votre bibliothèque Steam
  - `Bibliothèque Steam → clic droit sur Sephiria → Gérer → Parcourir les fichiers locaux`
- Windows (chemin d'installation par défaut)
  - `C:\Program Files (x86)\Steam\steamapps\common\Sephiria`
- macOS (chemin d'installation par défaut)
  - `~/Library/Application Support/Steam/steamapps/common/Sephiria` (le dossier qui contient `Sephiria.app`)

## Créer le dossier WorkshopLocal et le dossier de votre mod

1. Créez un dossier `WorkshopLocal/` dans le dossier d'installation du jeu.
2. À l'intérieur, créez un dossier pour votre propre mod.
   - Sephiria traite chaque dossier comme un mod.
   - Les mods créés ici peuvent être testés en jeu et publiés dans le Steam Workshop.

Dans ce guide, vous allez créer `my-first-mod`, un mod qui remplace le texte du bouton « Démarrer le jeu » de l'écran titre par « Hello World! ». La structure finale ressemble à ceci :

```
<dossier d'installation du jeu>/
├── Sephiria.exe
└── WorkshopLocal/
    ├── my-first-mod/          ← un mod
    │   ├── metadata.json      ← le fichier central qui définit le mod
    │   ├── thumbnail.png      ← vignette d'aperçu du mod (obligatoire pour publier dans le Workshop)
    │   └── Localization/      ← dossier lu par le module de localisation
    │       └── en-US.json     ← le texte à remplacer
    └── another-mod/           ← un autre mod
        └── ...
```

## Le fichier metadata.json

- Créez `metadata.json` dans le dossier de votre mod et collez-y le contenu ci-dessous.
- `metadata.json` : le fichier clé qui définit votre mod.
- `yourname` : remplacez-le par votre propre pseudonyme.
- Enregistrez le fichier en **UTF-8**. Dans le Bloc-notes, choisissez UTF-8 comme encodage dans « Enregistrer sous ».
- Consultez la [Référence de metadata.json](metadata.md) pour la signification de chaque champ.

```json
{
  "info": {
    "title": "My First Mod",
    "description": "Hello World!"
  },
  "mod": {
    "schemaVersion": 1,
    "modId": "yourname.my-first-mod",
    "version": "0.0.1",
    "author": "Your Name",
    "multiplayer": { "installation": "local_only" },
    "modules": [
      { "type": "localization", "path": "Localization" }
    ]
  }
}
```

## Créer le fichier de langue

- Créez un dossier `Localization/` dans le dossier de votre mod.
- Dans `Localization/`, créez un fichier `en-US.json` avec le contenu ci-dessous.
- Vous pouvez aussi renommer le fichier avec votre propre code de langue pour vos tests.

```json
{
  "Title_GameStart": "Hello World!"
}
```

## Vérifier

À ce stade, vous pouvez vérifier si le mod fonctionne.

1. Lancez Sephiria.
2. Avec la langue du jeu réglée sur l'anglais, regardez l'écran titre : le bouton « Start Game » affiche maintenant « Hello World! ». Les mods placés dans `WorkshopLocal/` sont activés par défaut, vous n'avez donc pas besoin de les activer.
3. Appuyez sur le bouton **Steam Workshop** de l'écran titre pour ouvrir le panneau du Workshop. « My First Mod » doit apparaître dans la section « Mes mods ».

### Mon mod n'est pas appliqué !

- Vérifiez que la syntaxe JSON est correcte.
- Le `path` dans `metadata.json` doit correspondre exactement au nom réel du dossier, casse comprise.
- Vérifiez que `metadata.json` est encodé en UTF-8.
- Vérifiez que la langue du jeu correspond à la langue indiquée dans le nom du fichier.

## Étapes suivantes

1. Découvrez comment les mods sont définis dans la [Référence de metadata.json](metadata.md).
2. Les [Mods de localisation](localization-mods.md) et les [Mods de tenues](costume-mods.md) sont relativement faciles à créer : commencez par ceux-là.
3. Les [Mods DLL](dll-mods.md) sont recommandés si vous avez des bases en programmation.
4. Publiez votre mod dans le Workshop grâce à [Publier dans le Workshop](publishing.md).
