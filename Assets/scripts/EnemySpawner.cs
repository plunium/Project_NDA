using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public Transform path;
    public float spawnInterval = 2f;
    public float spawnDistance = 20f;
    public float spawnVariation = 2f;
    public float lateralSpawnVariation = 2f;
    public int enemiesToSpawn = 3;
    public float maxDistanceToPlayer = 20f; // Ajustez cette valeur en fonction de la distance souhaitée.
    private float nextSpawnTime = 0f;

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            // Spawn seulement lorsque le joueur entre dans la zone.
            if (PlayerEnteredSpawnZone())
            {
                SpawnEnemies();
                nextSpawnTime = Time.time + spawnInterval;
            }
        }

        // Vérifiez la distance entre le joueur et les ennemis et détruisez-les si nécessaire.
        DestroyFarEnemies();
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            // Ajoutez une variation aléatoire à la distance de spawn.
            float randomSpawnDistance = Random.Range(spawnDistance - spawnVariation, spawnDistance + spawnVariation);

            // Ajoutez une variation latérale aléatoire à la position de spawn.
            float randomLateralOffset = Random.Range(-lateralSpawnVariation, lateralSpawnVariation);
            Vector3 spawnPosition = path.position + path.forward * randomSpawnDistance + path.right * randomLateralOffset;

            // Choisissez votre prefab d'ennemi à partir du tableau ou de votre référence unique.
            GameObject enemyPrefab = enemyPrefabs.Length > 0 ? enemyPrefabs[Random.Range(0, enemyPrefabs.Length)] : null;

            if (enemyPrefab != null)
            {
                GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                enemy.transform.LookAt(path.position + path.forward);

                // Ajoutez un script EnemyController (à créer) à l'ennemi pour gérer sa destruction.
                EnemyController enemyController = enemy.GetComponent<EnemyController>();
                if (enemyController != null)
                {
                    enemyController.SetSpawner(this);
                }
            }
        }
    }

    bool PlayerEnteredSpawnZone()
    {
        // Assurez-vous que le collider déclencheur est attaché à l'objet EnemySpawner et est configuré comme déclencheur.
        Collider collider = GetComponent<Collider>();
        if (collider != null && collider.isTrigger)
        {
            // Vérifiez si le joueur est à l'intérieur du collider déclencheur.
            return collider.bounds.Contains(GameObject.FindGameObjectWithTag("Player").transform.position);
        }

        return false;
    }

    void DestroyFarEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            // Vérifiez si l'ennemi existe toujours (il pourrait être détruit par ailleurs).
            if (enemy != null)
            {
                // Calculez la distance entre le joueur et l'ennemi.
                float distanceToPlayer = Vector3.Distance(enemy.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);

                // Détruisez l'ennemi si la distance dépasse le seuil maximal.
                if (distanceToPlayer > maxDistanceToPlayer)
                {
                    RemoveEnemy(enemy);
                }
            }
        }
    }

    public void RemoveEnemy(GameObject enemy)
    {
        // Implémentez ici la logique pour retirer l'ennemi de votre liste ou effectuer d'autres actions nécessaires.
        // Par exemple, vous pourriez le désactiver, le mettre en file d'attente pour réutilisation, etc.
        Destroy(enemy);
    }
}

