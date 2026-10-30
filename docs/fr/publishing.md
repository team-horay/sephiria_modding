# Publier dans le Workshop

Cette page explique comment publier votre mod dans le Steam Workshop, comment fonctionne le système d'examen, et comment les mods sont synchronisés entre les joueurs en multijoueur.


## Avant de publier

- Pour publier un mod dans le Workshop, une image d'aperçu (PNG ou JPG, moins de 1 Mo) est obligatoire. Définissez-la avec `info.preview`, ou placez un fichier `thumbnail.png` dans le dossier de votre mod. Consultez la [Référence de metadata.json](metadata.md#preview) pour plus de détails.
- `info.title` peut contenir jusqu'à 128 caractères et `info.description` jusqu'à 7999 caractères. Si vous les avez rédigés en plusieurs langues, c'est la version `en-US` qui est utilisée sur la page du Workshop.
- Indiquez les catégories du Workshop dans `tags`. Consultez la [Référence de metadata.json](metadata.md) pour les catégories disponibles.
- Tous les fichiers du dossier sont soumis à l'examen ; assurez-vous donc que le dossier de votre mod ne contient pas de copies des DLL du jeu, de fichiers `.pdb` ni de fichiers intermédiaires de compilation.

## Étapes de publication

1. Lancez Sephiria.
2. Ouvrez le panneau **Steam Workshop** sur l'écran titre et trouvez le mod que vous avez créé.
3. Sélectionnez votre mod et appuyez sur **Publier dans le Workshop**.
4. Pour les mods sans code, la publication dans le Workshop est la dernière étape.
5. Pour les mods contenant du code (DLL), une fois la publication dans le Workshop terminée, le mod est automatiquement soumis au **système d'examen** avec l'ID de son élément du Workshop.
6. Rendez-vous sur la page du Workshop publiée, effectuez un dernier test (par exemple en vous y abonnant), puis passez la visibilité en « Public ».

Si la publication dans le Workshop s'est terminée mais que seule la soumission à l'examen a échoué (par exemple à cause d'une erreur réseau), appuyez à nouveau sur **Publier dans le Workshop** pour le même mod. Si le contenu n'a pas changé, le mod n'est pas republié dans le Workshop ; seule la soumission est renvoyée. La soumission doit être effectuée avec le compte Steam qui a créé l'élément du Workshop.

## Examen du code

- Le système d'examen décompile les DLL et les vérifie pour détecter les communications réseau, les accès aux fichiers, l'obfuscation et les comportements malveillants. L'examen combine analyse automatisée et vérifications par un opérateur, et nous savons qu'aucun système d'examen ne peut détecter tous les risques. Nous visons néanmoins à réduire la diffusion de mods malveillants grâce à ce processus d'examen. Merci de votre coopération.
- Tout mod contenant du code (DLL) **doit également être soumis au système d'examen des mods.** Pour cela, publiez toujours votre mod avec le bouton **Publier dans le Workshop** du jeu.
- Les mods publiés dans le Workshop mais non soumis au système d'examen ne peuvent généralement pas être exécutés par les joueurs.
- Les joueurs qui téléchargent un mod consultent son statut d'examen à l'aide de l'ID de l'élément du Workshop du mod et des DLL installées.
  - **Soumis / Approuvé** : le mod s'exécute. Il s'exécute même avant la fin de l'examen.
  - **Non soumis / Refusé** : le mod ne s'exécute pas, et la raison est affichée dans le panneau des mods.
- Si un problème est détecté lors de l'examen, un mod peut être refusé même après sa publication. Pour un mod refusé, vous pouvez consulter la raison avec **Voir le motif du rejet** dans le panneau. Les DLL refusées ne peuvent pas être republiées telles quelles ; corrigez le problème et publiez à nouveau.
- Même sans module `dll`, un mod est soumis à l'examen s'il possède des `libraries` ou s'il contient une DLL ou un EXE dont l'extension a été changée.
- Les mods soumis à l'examen du code doivent peser 20 Mo ou moins une fois le dossier compressé.

### Raisons d'échec de l'examen

|Message|Cause|
|---|---|
|Aucune soumission n’existe dans le système d’examen.|Le mod a été publié dans le Workshop mais n'a pas été soumis au système d'examen. Soumettez-le à nouveau avec le bouton **Publier dans le Workshop**.|
|Le système d’examen a refusé l’approbation de ce mod.|L'équipe d'exploitation a refusé le mod pour une raison donnée. Consultez le motif du rejet.|
|Impossible d’obtenir des informations du serveur d’examen.|La connexion internet de l'abonné est peut-être coupée, ou le serveur d'examen rencontre peut-être un problème.|
|Ce mod provient de l’ancien système d’examen.|Le serveur d'examen a été mis à niveau et les données de vérification existantes du mod ont été réinitialisées. Soumettez-le à nouveau.|
|Une DLL non autorisée a été détectée dans le mod.|Si l'ensemble des fichiers DLL du mod change pendant son exécution, le chargement de ce mod est restreint. Autrement dit, les cas suivants peuvent enfreindre le système d'examen : le mod télécharge une DLL depuis l'extérieur pendant son exécution et charge l'assembly ; le mod crée et exécute sa propre DLL pendant son exécution ; le mod extrait une DLL empaquetée en lui avec son propre format de compression et l'utilise.|

## Visibilité et passage en public

- Lors de la première publication, la visibilité est définie sur `Non répertorié`.
- Avant de passer en public, abonnez-vous vous-même, vérifiez que la copie installée fonctionne correctement, puis passez en « Public ».
- Vous pouvez modifier la description de la page, les captures d'écran, les descriptions dans d'autres langues, etc. depuis la page de l'élément du Steam Workshop.

## Mettre à jour votre mod

Vous pouvez mettre à jour votre mod de la même manière que vous l'avez publié.

- Une mise à jour qui modifie des DLL est automatiquement soumise à nouveau après la publication et repasse par l'examen. Si vous n'avez modifié aucune DLL, le statut d'examen reste le même.
- Ne changez pas votre `modId`. Le mod serait traité comme un mod différent, ce qui peut casser les dépendances, entre autres.
- **Incrémentez toujours `version` dans `metadata.json` à chaque mise à jour.** Les versions des mods sont comparées lorsqu'un joueur rejoint un salon multijoueur ; si le contenu a changé mais pas la version, des joueurs avec un contenu de mod différent peuvent se retrouver dans la même partie et rencontrer des problèmes.

## Ticket de publication

Après la publication, `uploadticket.json` est ajouté au dossier de votre mod. Il contient l'ID de l'élément du Workshop et les informations de soumission à l'examen, alors **ne le supprimez pas.** Si vous gérez le dossier de votre mod avec git, le plus sûr est de valider ce fichier aussi. Si vous changez d'ordinateur ou formatez, vous aurez besoin de ce fichier pour continuer à mettre à jour le même élément du Workshop. Sans lui, le jeu traite le dossier comme un nouveau mod jamais publié.

`mod-guard-manifest.json` et `mod-guard.sig.json`, utilisés par le système d'examen avant la version 1.1.1, ne sont plus utilisés. Même s'ils sont encore présents, ils ne sont pas publiés dans le Workshop, et vous pouvez les supprimer.

Consultez la [FAQ](faq.md) pour savoir comment récupérer ce fichier si vous le perdez.


## Règles du multijoueur

### Configuration du salon multijoueur

Lorsque l'hôte crée un salon multijoueur, **la liste des mods activés par l'hôte** est enregistrée auprès du salon.

- Mods dont `installation` vaut `host_only` : les participants ne sont pas tenus de les installer ; ils sont seulement affichés dans la liste.
- Mods dont `multiplayer.installation` vaut `everyone` (ou n'est pas déclaré) : les participants doivent aussi les installer.
- Mods de tenues et mods de données : les participants doivent aussi les installer.

### Rejoindre un salon

Si la configuration de mods actuelle d'un joueur diffère de celle du salon, il passe par ces étapes : téléchargement → activation → redémarrage du jeu, puis nouvelle tentative pour rejoindre.

### Mods de test installés dans WorkshopLocal

Le multijoueur n'est pas disponible dans une partie où des mods locaux de `WorkshopLocal/` sont chargés. Pour tester le multijoueur, publiez votre mod dans le Workshop, videz le dossier local et utilisez la copie à laquelle vous êtes abonné. Vous pouvez vous abonner via le lien même lorsque l'élément est `Non répertorié`.
