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

    private List<GameObject> enemyPool;
    public int poolSize = 15;

    private void Start()
    {
        enemyPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.SetActive(false); 
            enemyPool.Add(enemy);
        }
    }

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

        GameObject enemy = GetPooledEnemy();
        if (enemy == null)
        {
            Debug.LogWarning("No available enemies in the pool. Consider increasing the pool size.");
            return;
        }

        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];
        enemy.transform.position = spawnPoint.position; 
        enemy.transform.rotation = Quaternion.identity;

        currentColorIndex = (currentColorIndex + 1) % colors.Length;
        Material selectedColor = colors[Random.Range(0, colors.Length)];

        Renderer enemyRenderer = enemy.GetComponent<Renderer>();
        if (enemyRenderer != null)
        {
            enemyRenderer.material = selectedColor;
        }
        else
        {
            Debug.LogError("Missing Renderer on enemyPrefab.");
        }

        Rigidbody enemyRb = enemy.GetComponent<Rigidbody>();
        if (enemyRb != null)
        {
            enemyRb.velocity = spawnPoint.right * enemySpeed * -1f;
        }

        enemy.SetActive(true);

        StartCoroutine(DeactivateAfterDelay(enemy, 10f));
    }

    GameObject GetPooledEnemy()
    {
        foreach (GameObject enemy in enemyPool)
        {
            if (!enemy.activeInHierarchy)
            {
                return enemy;
            }
        }
        return null; 
    }

    private System.Collections.IEnumerator DeactivateAfterDelay(GameObject enemy, float delay)
    {
        yield return new WaitForSeconds(delay);
        enemy.SetActive(false);

        Rigidbody enemyRb = enemy.GetComponent<Rigidbody>();
        if (enemyRb != null)
        {
            enemyRb.velocity = Vector3.zero;
        }
    }
}