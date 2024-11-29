using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    private Renderer enemyRenderer;

    private void Awake()
    {
        enemyRenderer = GetComponent<Renderer>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Assuming your player is tagged "Player"
        {
            // Stop the game when the enemy hits the player
            
            Debug.Log("Game Over: Enemy hit the player!");
            RestartGame();
            // Optionally, display a Game Over UI or trigger some effects here.
        }

        if (other.CompareTag("Bullet"))
        {
            
            // Get the Renderer of the bullet to access its color
            Renderer bulletRenderer = other.GetComponent<Renderer>();

            if (bulletRenderer != null && enemyRenderer != null)
            {
                other.gameObject.SetActive(false);

                if (bulletRenderer.material.color == enemyRenderer.material.color)
                {
                    gameObject.SetActive(false);
                    Debug.Log("Enemy destroyed: Colors matched!");
                }
                else
                {
                    Debug.Log("Colors did not match: Enemy not destroyed.");
                }
            }
        }
    }
    public void SetColor(Material color)
    {
        if (enemyRenderer != null)
        {
            enemyRenderer.material = color;
        }
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
