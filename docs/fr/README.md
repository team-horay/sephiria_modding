[한국어](../../README.md) | [English](../../README.en.md) | [日本語](../ja/README.md) | [简体中文](../zh-CN/README.md) | [繁體中文](../zh-TW/README.md) | [Deutsch](../de/README.md) | **Français** | [Español](../es/README.md) | [Italiano](../it/README.md) | [Polski](../pl/README.md) | [Português (Brasil)](../pt-BR/README.md) | [Русский](../ru/README.md) | [Svenska](../sv/README.md) | [ไทย](../th/README.md) | [Türkçe](../tr/README.md)

# Guide de modding de Sephiria

Merci de rejoindre la communauté de modding de Sephiria.

Ce dépôt est le dépôt officiel des guides, exemples et modèles de modding pour [Sephiria](https://store.steampowered.com/app/2436940). Les mods peuvent être partagés via le [Steam Workshop](https://steamcommunity.com/app/2436940/workshop/).



## Règles

Afin que le jeu reste sûr pour tout le monde, veuillez respecter les règles ci-dessous. Toute infraction peut entraîner des restrictions sur la publication de mods ou sur l'accès au jeu.

1. **Aucune activité malveillante** : les logiciels malveillants, portes dérobées, enregistreurs de frappe, logiciels de cryptominage et toute autre fonctionnalité compromettant l'appareil, le compte ou les données d'un utilisateur sont interdits.
2. **Utilisation non commerciale** : tous les mods doivent être créés et distribués à des fins non commerciales.
3. **Aucune atteinte aux droits ni contenu nuisible** : les contenus qui enfreignent les droits d'auteur, la vie privée ou le droit à l'image d'autrui, ou qui contiennent des contenus sexuels ou font l'apologie de la haine, du harcèlement ou de la violence réelle, sont interdits.
4. **Aucune perturbation du jeu** : ne provoquez pas intentionnellement de dysfonctionnement du jeu et n'entravez pas le jeu normal des autres joueurs.
5. **Aucune collecte de données** : ne collectez pas d'informations sur les utilisateurs et ne les transmettez pas à l'extérieur.
6. **Aucun contournement des vérifications d'accès** : ne contournez pas illicitement les vérifications d'achat, de propriété ou d'accès du jeu ou de la plateforme de distribution.
7. **Examen des mods** : les mods contenant du code (DLL) font l'objet d'un examen, comprenant une analyse et une décompilation, afin de vérifier le respect des règles, et peuvent être refusés même après leur publication si un problème est détecté. Ne falsifiez pas les résultats de l'examen ni les marques d'approbation. Les fichiers doivent être fournis dans un état permettant leur examen ; l'obfuscation est interdite.
8. **Utilisation des ressources du jeu** : la décompilation et l'analyse du code, des graphismes, du son et des autres ressources du jeu sont autorisées dans la mesure nécessaire à la création de mods. Toutefois, ne redistribuez pas les ressources extraites du jeu elles-mêmes.
9. **Aucune usurpation ni altération** : ne vous faites pas passer pour une autre personne, et ne modifiez ni ne redistribuez les mods créés par d'autres sans leur consentement.



## Avant de commencer

- Les mods contenant du code (DLL) sont automatiquement **soumis au système d'examen** lorsque vous les publiez dans le Workshop. L'examen vérifie principalement que le code ne contient pas de comportement malveillant, et les mods non soumis ou refusés ne s'exécutent pas dans les parties des autres joueurs. Les mods sans code, comme les traductions et les tenues, peuvent être publiés immédiatement sans examen. Notez que les mods que le système d'examen ne peut pas lire seront refusés (fichiers obfusqués, DLL compilées en C/C++, etc.).
- En multijoueur, tous les participants du salon doivent avoir activé le même ensemble de mods. (Les mods d'interface personnels, comme les traductions, en sont exemptés.)
- Après avoir activé des mods, vous devez redémarrer le jeu pour changer leur ordre ou les désactiver à nouveau.
- Les mods auxquels vous êtes abonné sont désactivés par défaut. Vous pouvez les activer vous-même depuis la liste.



## Documentation

| Document | Contenu |
|---|---|
| [Votre premier mod](getting-started.md) | Mise en place d'un dossier de développement, test d'un mod en local, lecture des journaux |
| [Référence de metadata.json](metadata.md) | Signification de chaque champ, règles de dépendances et de versions, déclaration des modules |
| [Mods de localisation](localization-mods.md) | Structure des fichiers de langue, recherche des clés, règles de préservation des balises et des espaces réservés |
| [Mods de tenues](costume-mods.md) | Créer votre propre tenue |
| [Mods DLL](dll-mods.md) | Configuration du projet, point d'entrée, événements `HorayModAPI`, débogage |
| [Publier dans le Workshop](publishing.md) | Publication dans le Workshop, examen des mods avec code, règles du multijoueur |
| [Référence de l'API](api/README.md) | Types et membres publics tels que `HorayModAPI`, `WorkshopModBase`, les options de mod et l'interface. Générée automatiquement à partir du code source du jeu |
| [Annexe et FAQ](faq.md) |  |


## Licence

Le code de ce dépôt (exemples et modèles) est distribué sous [licence MIT](../../LICENSE).
La documentation peut être librement citée, traduite et redistribuée ; merci d'en indiquer la source.
Le jeu Sephiria lui-même et ses ressources sont des œuvres de TEAM HORAY et ne sont pas couverts par cette licence.
