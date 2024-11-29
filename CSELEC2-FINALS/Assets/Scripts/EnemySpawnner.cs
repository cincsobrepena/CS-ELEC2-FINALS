using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public float spawnInterval = 2f;
    private float timer = 0f;
    public float enemySpeed = 20f;

    public Material[] colors;

    private int currentColorIndex = 0;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points assigned.");
            return;
        }

        int randomIndex = Random.Range(0, spawnPoints.Length);
        Debug.Log("Spawning at index: " + randomIndex);

        // Get the color for the enemy
        currentColorIndex = (currentColorIndex + 1) % colors.Length;
        Material selectedColor = colors[Random.Range(0, colors.Length)];

        GameObject newEnemy = Instantiate(enemyPrefab, spawnPoints[randomIndex].position, Quaternion.identity);
        Rigidbody newEnemyRb = newEnemy.GetComponent<Rigidbody>();
        Renderer enemyRenderer = newEnemy.GetComponent<Renderer>();

        newEnemyRb.velocity = spawnPoints[randomIndex].right * enemySpeed * -1f;
        if (enemyRenderer != null)
        {
            // Assign the color to the enemy
            enemyRenderer.material = selectedColor;

            // Pass the color to the enemy script (optional for future logic)
            /*newEnemy.GetComponent<Enemy>().*/
        }
        else
        {
            Debug.LogError("Missing Renderer on enemyPrefab.");
        }
        Destroy(newEnemy, 5f);
    }
}