
Profiling exécuté sur une partie composé de :
  * Une navigation dans le menu pour changer la map
  * Une partie 1 joueur
  * Un enregistrement du replay à la fin
  * Lancement du replay

Comme on peut le voir dans `cpu-gpu-overview`, le pourcentage de CPU utilisé reste globalement stable, bien que localement anarchique (ce qui est normal).

On observe néanmoins deux pics qui correspondent à la sauvegarde et au chargement du replay.
Cela peut être confirmé en regardant le pourcentage de CPU utilisé par fonction (voir `cpu-details-save` et `cpu-details-load`).
