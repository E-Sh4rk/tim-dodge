
Détails des instantanés (voir `memory-overview`) :
  * 1 : Juste après le lancement du jeu
  * 2 : Juste apres changement de map (difficultée facile au lieu de moyen)
  * 3 : Début de partie 1 joueur
  * 4 : Fin de la partie
  * 5 : Retour menu
  * 6 : Début replay de la dernière partie
  * 7 : Fin replay de la dernière partie
  * 8 : Retour menu

Plusieurs constatations :
  * 1 -> 2 : On constate que le changement de la map supprime bien l'ancienne map de la mémoire (la map facile étant plus légère que la map moyenne)
  * 2 -> 3 : Les éléments de jeu de base sont alloués et donc plus de mémoire est utilisée.
  * 3 -> 4 : La mémoire utilisée croit légeremment tout au long du jeu car l'historique de la partie grossit (utilisé pour les replays et le retour dans le temps). Cela reste raisonnable : moins de 5 mo pour une partie normale.
  * 4 -> 5 : Les éléments de jeu sont bien libérés.
  * 5 -> 6 -> 7 : Cette fois, il s'agit du lancement d'un replay et donc tous les objets de la partie sont alloués dès le début. Ainsi, aucune augmentation n'a lieu durant tout le replay.
  * 7 -> 8 : Les éléments de jeu sont bien libérés.
  * 1 -> 5 -> 8 : On constate une augmentation de la mémoire utilisé lors des différents retours menus. Voir `memory5-details` et `memory8-details` pour une comparaison plus détaillée des instantanés 5 et 8.
  Cela est dû à plusieurs choses : premièrement, les différents menus et tous leurs éléments se chargent au fur et à mesure (quand besoin est), et restent ensuite en mémoire jusqu'à la fin (ce sont les mêmes instances qui sont réutilisés par la suite quand besoin est). Deuxièmement, lors d'un retour menu, la précédente partie n'est pas totalement supprimée : elle le sera lorsqu'une nouvelle partie sera demandée et que le garbage collector aura fait son travail.
