# FAQ


## Comment consulter les journaux ?

Les journaux du jeu sont écrits dans le fichier ci-dessous. Il est réécrit à chaque lancement du jeu, et la session précédente est conservée dans `Player-prev.log`.

```
%USERPROFILE%\AppData\LocalLow\TEAMHORAY\Sephiria\Player.log
```

Les lignes liées aux mods commencent par une balise entre crochets ; recherchez donc cette balise.

| Balise | Contenu |
|---|---|
| `[WorkshopModManager]` | Consultation de la liste des abonnements, téléchargements, analyse du dossier local, résumé du plan de chargement |
| `[WorkshopModRuntime]` | Activation des DLL et résultats de la fusion des localisations |
| `[WorkshopUploader]` | Soumission à l'examen et publication dans le Workshop |
| `[ModGuardClient]` | Communication avec le serveur d'examen |
| `[WorkshopLobbyMods]` | Comparaison des ensembles de mods dans les salons multijoueur |
| `모드 내 언어 파일 발견` (« fichier de langue trouvé dans le mod ») | Nombre de clés lues dans chaque fichier de localisation |


## Qu'est-ce que « Mod local (dossier AddOns) » dans le panneau Steam Workshop ?

Vous verrez peut-être une entrée « Mod local (dossier AddOns) » dans le panneau. Il s'agit d'une ancienne fonctionnalité de modding ; ne l'utilisez pas pour de nouveaux mods.
Les anciens mods ne peuvent pas être synchronisés avec les autres joueurs ; nous recommandons donc de les supprimer.


## Je veux déplacer le dossier WorkshopLocal ailleurs que dans le dossier d'installation de Sephiria.

Si vous souhaitez conserver vos mods sur un autre disque ou dans un dossier de travail, vous pouvez passer le chemin en argument de lancement. Dans votre bibliothèque Steam, allez dans Sephiria → Propriétés → Général → Options de lancement et saisissez :

```
-workshop_local=D:\mods\my-first-mod
```

Séparez les chemins multiples par des points-virgules, et entourez l'ensemble de guillemets si un chemin contient des espaces.

```
"-workshop_local=D:\My Mods\first;D:\My Mods\second"
```

Les dossiers passés en argument sont traités exactement comme des sous-dossiers de `WorkshopLocal/`.


## J'ai perdu uploadticket.json, mod-guard-manifest.json et mod-guard.sig.json.

Si seuls les fichiers de certificat (`mod-guard-manifest.json`, `mod-guard.sig.json`) manquent et que `uploadticket.json` est toujours présent, sélectionnez le mod dans le panneau et appuyez sur le bouton **Obtenir la preuve d'approbation** pour les télécharger à nouveau depuis le serveur d'examen. En revanche, si vous avez aussi perdu `uploadticket.json` (par exemple après un formatage), le jeu traite le dossier comme un nouveau mod : le publier tel quel créerait un nouvel élément du Workshop au lieu de mettre à jour l'existant. Pour continuer à mettre à jour l'élément existant, récupérez-le dans cet ordre :

1. Abonnez-vous à votre propre mod sur le Workshop.
2. Copiez l'intégralité du dossier téléchargé par Steam (`<bibliothèque Steam>/steamapps/workshop/content/2436940/<ID de l'élément du Workshop>/`) dans `WorkshopLocal/`. Le nom du dossier n'a pas d'importance.
3. Dans le dossier copié, créez `uploadticket.json` avec le contenu ci-dessous. Pour le nombre, utilisez les chiffres qui suivent `id=` dans l'URL de la page du Workshop.
```json
{
  "modId": "yourname.first-mod",
  "publishedFileId": 123456789
}
```
4. Au lancement du jeu, le mod apparaît comme « Non soumis » dans le panneau. Passez par demande de vérification → approbation → obtention de la preuve d'approbation → nouvelle publication dans le Workshop, et **l'élément existant est mis à jour**. (Remarque : si vous avez dupliqué un dossier de mod existant pour créer un mod différent, vous **devez supprimer** `uploadticket.json` et les fichiers de certificat. Si vous les laissez, le nouveau mod écrasera l'élément du Workshop du mod d'origine.)
