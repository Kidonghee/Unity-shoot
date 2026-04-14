using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemies;

    [SerializeField]
    private GameObject boss;

    private float[] spawnPosX = { -4f, -2f, 0f, 2f, 4f };
    private float spawnInterval = 1.5f;

    void Start()
    {
        StartCoroutine(EnemyRoutine());
    }

    public void StopEnemyRoutine()
    {
        StopAllCoroutines();
    }

    IEnumerator EnemyRoutine()
    {
        yield return new WaitForSeconds(3f);

        float moveSpeed = 5f;
        int spawnCount = 0;
        int enemyIndex = 0;

        while (true)
        {
            foreach (float posX in spawnPosX)
            {
                SpawnEnemy(posX, enemyIndex, moveSpeed);
            }

            spawnCount++;

            if (spawnCount % 10 == 0)
            {
                enemyIndex++;
                moveSpeed += 2f;
            }

            if (enemyIndex >= enemies.Length)
            {
                SpawnBoss();
                enemyIndex = 0;
                moveSpeed = 5f;
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemy(float posX, int index, float moveSpeed)
    {
        Vector3 spawnPos = new Vector3(posX, transform.position.y, transform.position.z);

        if (Random.Range(0, 5) == 0)
        {
            index++;
        }

        if (index >= enemies.Length)
        {
            index = enemies.Length - 1;
        }

        GameObject enemyObject = Instantiate(enemies[index], spawnPos, Quaternion.identity);

        Enemy enemy = enemyObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.SetMoveSpeed(moveSpeed);
        }
    }

    void SpawnBoss()
    {
        Debug.Log("🔥 보스 등장!");
        Instantiate(boss, transform.position, Quaternion.identity);
    }
}