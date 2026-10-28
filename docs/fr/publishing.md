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
3. Sélectionnez votre mod.
4. Les mods contenant du code (DLL) passent par l'examen du code.
5. Les mods sans code peuvent être publiés immédiatement dans le Workshop.
6. Rendez-vous sur la page du Workshop publiée, effectuez un dernier test (par exemple en vous y abonnant), puis passez la visibilité en « Public ».

## Examen du code

- Le système d'examen vérifie chaque fichier du dossier de votre mod. Les DLL sont décompilées et vérifiées pour détecter les communications réseau, les accès aux fichiers, l'obfuscation et les comportements malveillants ; les fichiers de données comme les images, les sons et les JSON sont vérifiés pour s'assurer que leur format est valide.
- Si aucun problème n'est détecté, le mod est approuvé automatiquement ; si quelque chose paraît suspect, un opérateur le vérifie manuellement. Les mods clairement malveillants sont refusés automatiquement.
- L'examen combine analyse automatisée et vérifications par un opérateur, et nous savons qu'aucun système d'examen ne peut détecter tous les risques. Nous visons néanmoins à réduire la diffusion de mods malveillants grâce à ce processus préalable d'examen et d'approbation. Merci de votre coopération.
- Le fait qu'un mod contienne du code est déterminé par le **contenu** des fichiers, pas par leur nom. Même sans module `dll`, un mod est soumis à l'examen s'il possède des `libraries` ou s'il contient une DLL ou un EXE dont l'extension a été changée.
- Les mods soumis à l'examen du code doivent peser 20 Mo ou moins une fois le dossier compressé.

## Preuve d'approbation (mods avec code uniquement)

- Les mods contenant du code reçoivent une preuve d'approbation.
- Elle certifie que les fichiers du mod sont identiques à la version approuvée.
- Les mods DLL doivent passer par cette certification avant que les joueurs qui téléchargent le mod puissent exécuter son code.
- Si un seul fichier change après la certification, la preuve devient invalide et le mod doit être examiné à nouveau.

## Visibilité et passage en public

- Lors de la première publication, la visibilité est définie sur `Non répertorié`.
- Avant de passer en public, abonnez-vous vous-même, vérifiez que la copie installée fonctionne correctement, puis passez en « Public ».
- Vous pouvez modifier la description de la page, les captures d'écran, les descriptions dans d'autres langues, etc. depuis la page de l'élément du Steam Workshop.

## Mettre à jour votre mod

Vous pouvez mettre à jour votre mod de la même manière que vous l'avez publié.

- Les mods DLL ont besoin d'une nouvelle preuve d'approbation.
- Ne changez pas votre `modId`. Le mod serait traité comme un mod différent, ce qui peut casser les dépendances, entre autres.
- **Incrémentez toujours `version` dans `metadata.json` à chaque mise à jour.** Les versions des mods sont comparées lorsqu'un joueur rejoint un salon multijoueur ; si le contenu a changé mais pas la version, des joueurs avec un contenu de mod différent peuvent se retrouver dans la même partie et rencontrer des problèmes.

## Ticket de publication

Après la demande d'examen et la publication, les fichiers ci-dessous sont ajoutés au dossier de votre mod. Ils contiennent les informations d'approbation de l'examen, alors **ne les supprimez pas.** Si vous gérez le dossier de votre mod avec git, le plus sûr est de valider ces trois fichiers aussi. Si vous changez d'ordinateur ou formatez, vous aurez besoin de ces fichiers pour continuer à mettre à jour le même élément du Workshop.

| Fichier | Contenu |
|---|---|
| `uploadticket.json` | Le ticket d'examen et l'ID de l'élément du Workshop. Sans lui, le jeu traite le dossier comme un nouveau mod jamais publié. |
| `mod-guard-manifest.json`, `mod-guard.sig.json` | La preuve d'approbation de l'examen : la liste des fichiers approuvés et une signature. Les modifier invalide l'approbation. |

Consultez la [FAQ](faq.md) pour savoir comment récupérer ces fichiers si vous les perdez.

## Révocation de l'approbation

Si une infraction aux règles est constatée après la publication, un opérateur peut révoquer l'approbation. Un mod révoqué n'est pas chargé même s'il est installé, et il apparaît dans le panneau comme « Approbation de l'examen révoquée ».


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
