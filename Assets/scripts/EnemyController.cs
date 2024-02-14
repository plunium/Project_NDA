using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemySpawner spawner;

    public void SetSpawner(EnemySpawner _spawner)
    {
        spawner = _spawner;
    }

    void OnTriggerEnter(Collider other)
    {
        // Détruire l'ennemi lorsque le joueur passe devant.
        if (other.CompareTag("PlayerArmature"))
        {
            DestroyEnemy();
        }
    }

    void OnBecameInvisible()
    {
        // Détruire l'ennemi lorsqu'il n'est plus visible par la caméra.
        DestroyEnemy();
    }

    void DestroyEnemy()
    {
        // Informer le spawner que l'ennemi doit être retiré de sa liste.
        if (spawner != null)
        {
            spawner.RemoveEnemy(gameObject);
        }
    }
}

