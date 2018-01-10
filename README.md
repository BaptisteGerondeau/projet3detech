# projet3detech
TelTeam - Projet 3D Entretainment Technologies

Ce projet est une adaptation du concept du jeu SpaceTeam pour Android, dont tous les droits et crédits vont à l'équipe de développement de ce jeu : voir le site http://spaceteam.ca/

Malheureusement, suite à des contraintes horaires et de manque de ressources graphiques (et de talents artistiques), ce projet n'a pas pu aboutir sur un jeu complet. Il manque les animations et la majorité du côté visuel : la lecture entière du code du KinectManager, ainsi que la réécriture du BaseInputModule (sans trop de ressources disponibles sur Internet existant pour la Kinect v1) m'a particulièrement occupé.
Le debuggage, puis la création d'une architecture propre pour la gestion de la génération d'ordres, ainsi que la verification de l'execution des consignes ne m'ont pas permis d'avoir le temps de finir (d'autant plus que je ne dispose pas de Kinect v1).

En l'état, le projet est un framework Unity supportant la Kinect v1, en s'appuyant sur le fantastique Kinect Manager, issu de cet Asset Pack : https://www.assetstore.unity3d.com/en/#!/content/7747
 
Pour implémenter les interactions avec les éléments de la GUI, il a fallu réécrire un module "Base Input" pour remplacer la souris. En effet le Kinect Manager permet d'interagir avec tous les éléments du jeu à part la couche GUI qui est traitée différemment par Unity : les lancers de rayons sont propres à cette couche et isolés des éléments du jeu.

Pour utiliser ce framework, on doit mettre en place le Kinect Manager comme pour utiliser l'asset pack tout seul. Ensuite on ajoute le script KinectInputModule en remplacant le BaseInputModule. On ajuste l'offset en Y (pour les 2 joueurs) pour avoir la main droite du joueur en position mi-hauteur à la moitié de l'écran (la moitié de la resolution verticale est une bonne valeur de départ).
On ajoute ensuite des Quads colorés (ou autre GameObject) pour faire office de curseur.
Ainsi on peut interagir avec les boutons du jeu.

Il est également possible d'ajouter les mouvements de type SwipeUp, SwipeDown, Jump, Crouch pour avoir d'autres interactions que du Point & Click avec la main (à noter que le click s'effectue via un cooldown de sélection : on reste un temps dans le carré du bouton, ce temps étant reglable dans le panneau de configuration du script). Ces interactions sont à ajouter aux endroits commenter, en parallèle du code concernant le curseur dans le KinectInputModule.
A l'origine il avait été planifié que les Swipe soient exploités pour changer les valeurs des sliders dans la GUI, Jump & Crouch étant prévu pour répondre à des évenements aléatoires (une météorite approche : Sautez !).

Je recommande à quiconque voulant créer des jeux utilisant de manière intensive des éléments non-3D et Kinect de passer via les bibliothèques natives du Kinect SDK (application WPF). Bien que la documentation sur Unity est beaucoup plus abondante, et l'interface de l'editeur fantastique les bibliothèques Microsoft sont bien plus stables et précises.

Malheureusement Kinect est une technologie tombée en désuétude, trop vite pour que des concepts de jeux vraiment originaux (ou fonctionnels...) puissent être mis sur le marché...
Mais les ressources sont toujours sur Internet, et la rareté des applications permet de facilement s'illustrer avec un concept original.

Je souhaite bonne chance et bon courage à tous ceux qui s'aventureront à programmer avec Kinect.

En remerciant M. Hazem Wannous et l'équipe enseignante de Telecom Lille, ainsi que les développeurs de KinectManager, Unity et le KinectSDK.

La licence de ce projet est soumise au conditions d'utilisation des assets packs contenus dans celui-ci ainsi que du Kinect SDK.
