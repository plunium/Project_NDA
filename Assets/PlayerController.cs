using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool isDead = false;

    // Cette fonction est appelée lorsque le joueur entre en collision avec un autre collider
    void OnTrigeerEnter3D(Collision collision)
    {
        // Vérifier si le collider avec lequel le joueur entre en collision est un obstacle
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Die(); // Appeler la fonction Die
        }
    }

    // Fonction pour gérer la mort du joueur
    void Die()
    {
        if (!isDead)
        {
            // Mettez ici le code pour gérer la mort du joueur,
            // comme désactiver les contrôles, afficher un écran de fin de jeu, etc.
            Debug.Log("Le joueur est mort !");
            isDead = true;
        }
    }
}