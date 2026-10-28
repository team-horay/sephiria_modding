# Mods de localisation

[Votre premier mod](getting-started.md) a couvert les bases de la création d'un mod ; cette page présente les règles communes aux mods de localisation.
[`examples/translation-sample`](../../examples/translation-sample) contient un exemple complet que vous pouvez copier directement dans `WorkshopLocal/` pour l'essayer.



## Fonctionnement

- Le jeu possède une table **clé → chaîne** par langue. Lorsqu'un mod est chargé, les valeurs du mod écrasent la table d'origine.
- Votre fichier n'a besoin de contenir que les clés que vous voulez modifier.
- Le nom du fichier est la langue. `ko-KR.json` écrase la table coréenne, et `en-US.json` écrase la table anglaise.
- Si plusieurs mods de localisation définissent la même clé, c'est le mod chargé en dernier qui l'emporte.
- Un mod qui ne modifie que du texte n'affecte pas les autres participants ; nous recommandons donc de définir `multiplayer.installation` sur `local_only` dans `metadata.json`.
- Les clés comme les valeurs sont des chaînes. Les objets imbriqués et les tableaux ne sont pas autorisés.
- Enregistrez les fichiers en **UTF-8**.

```
my-translation-mod/
├── metadata.json              ← ajoutez { "type": "localization", "path": "Localization" } dans modules
├── thumbnail.png
└── Localization/
    └── ko-KR.json
```



## Ce qu'il ne faut pas traduire

Les textes du jeu contiennent des expressions interprétées à l'exécution.
Ne traduisez pas les éléments ci-dessous ; conservez-les en anglais exactement tels quels (sensibles à la casse).

| Type    | Exemple                                                                     | Description                                 |
| ----- | --------------------------------------------------------------------------- | ------------------------------------------- |
| Mots-clés   | `{VALUE}`, `{EFFECT0}`, `{LV}`, `{COUNT}`, `{ITEMNAME}`, `{COUNTER}/{GOAL}` | Remplacés par des nombres ou des noms à l'exécution. Ne changez pas un seul caractère entre les accolades.   |
| Balises de jeu | `<tag=FinalDamage>`, `<tag=WeaponAction_Fury>`, `<tag=TEXT:Qliphoth>`       | Remplacées par des infobulles de mots-clés et des icônes. Les noms à l'intérieur des balises sont fixes, casse comprise. |
| Balises de mise en forme | `<color=red>…</color>`, `<sprite="Keyword" name=Dice>`, `<indent=10>`       | Mise en forme du texte. Les balises ouvrantes et fermantes doivent aller par paires.        |
| Sauts de ligne   | `\n`                                                                        | Un saut de ligne à l'intérieur d'une chaîne JSON.                        |




## Ajouter une nouvelle langue

- Vous pouvez aussi ajouter une langue que le jeu ne prend pas en charge. Si le nom du fichier est un nouveau code de langue, le jeu l'enregistre comme nouvelle langue et l'affiche dans la liste des paramètres de langue.
- Les fichiers de langue d'origine se trouvent dans `<dossier d'installation du jeu>/Sephiria_Data/StreamingAssets/Localization/`, un par langue ; servez-vous-en comme référence pour traduire.
- Les langues déjà prises en charge par le jeu sont `ko-KR`, `en-US`, `ja-JP`, `zh-CN`, `zh-TW`, `de-DE`, `fr-FR`, `es-ES`, `it-IT`, `pl-PL`, `pt-BR`, `ru-RU`, `sv-SE`, `th-TH` et `tr-TR`.
- **Traduisez toutes les clés.** Les clés absentes de votre fichier de mod ne s'affichent pas vides : elles retombent sur l'**anglais** (ou le coréen si l'anglais manque aussi). Le plus simple pour commencer est de copier le fichier `en-US.json` d'origine.
- **Renseignez les clés de paramètres commençant par `#`.** `#LanguageName` est le nom affiché dans la liste des paramètres de langue, et `#FontName` est la police à utiliser. Adaptez les clés de ponctuation comme `#Colon`, `#Bullet` et `#ParenthesisOpen` aux conventions de la langue concernée.
- Pour `#FontName`, vous pouvez choisir parmi les polices intégrées au jeu. Le jeu inclut des polices pour les écritures suivantes :

| Écriture | `#FontName` |
|---|---|
| Latin, cyrillique, hangeul | `GalCyrillic` |
| Japonais | `JF-Dot-MPlus10` |
| Chinois simplifié | `Fusion_Pixel_10_Simple` |
| Chinois traditionnel | `BoutiqueBitmap9x9` |
| Thaï | `NotoSansThai` |



## Dépannage

| Symptôme | Que vérifier |
|---|---|
| Le mod est activé mais le texte ne change pas | Si le nom de la clé correspond exactement à l'original, caractère par caractère. Si le nom du fichier correspond à la langue actuelle du jeu (`ko-KR.json` ne s'applique qu'en coréen). |
| Seules certaines clés ne changent pas | Si un autre mod de localisation écrase la même clé plus tard. Essayez de changer l'ordre dans le panneau. |
| `{VALUE}` s'affiche tel quel dans le texte | Le mot-clé est orthographié différemment de l'original. Copiez-le directement depuis le texte d'origine. |
| `<tag=…>` s'affiche tel quel dans le texte | L'orthographe ou la casse du nom de la balise est différente. |
| Le texte coréen (ou d'une autre langue non anglaise) apparaît illisible | Le fichier n'est pas en UTF-8. Enregistrez-le à nouveau en UTF-8. |
| Des caractères s'affichent sous forme de carrés | Ces caractères ne sont pas inclus dans les polices intégrées du jeu. |
| Un fichier entier est ignoré | Ce fichier contient une erreur de syntaxe JSON. `Player.log` enregistre l'erreur avec le nom du fichier. |

Le fichier journal se trouve dans `%USERPROFILE%\AppData\LocalLow\TEAMHORAY\Sephiria\Player.log`. Recherchez « 모드 내 언어 파일 발견 » (fichier de langue trouvé dans le mod) pour voir quels fichiers ont été lus et combien de clés chacun contenait.
