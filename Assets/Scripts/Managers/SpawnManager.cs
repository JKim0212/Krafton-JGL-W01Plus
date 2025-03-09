using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    float spawnInterval = 0.5f;
    public float SpawnInterval { private get; set; }
    bool spawnCooldown = false;
    [SerializeField] GameObject[] enemiesToSpawn;
    float[] spawnProb = { 33, 66 };
    public float[] SpawnProb { private get; set; }

    public void Spawn()
    {
        // if (!spawnCooldown)
        // {
        //     float rand = Random.Range(0, 100f);
        //     if (rand <= spawnProb[0])
        //     {
        //         Instantiate(enemiesToSpawn[0], GetRandomPosition(), Quaternion.identity, GameManager.instance.pool.gameObject.transform);
        //     }
        //     else if (rand >= spawnProb[0] && rand <= spawnProb[1])
        //     {
        //         Instantiate(enemiesToSpawn[1], GetRandomPosition(), Quaternion.identity, GameManager.instance.pool.gameObject.transform);
        //     }
        //     else
        //     {
        //         Instantiate(enemiesToSpawn[2], GetRandomPosition(), Quaternion.identity, GameManager.instance.pool.gameObject.transform);
        //     }
        //     spawnCooldown = true;
        //     StartCoroutine(SpawnCo());
        // }

    }

    IEnumerator SpawnCo()
    {
        yield return new WaitForSeconds(spawnInterval);
        spawnCooldown = false;
    }

    Vector3 GetRandomPosition()
    {
        Vector3 randomPos = Vector3.zero;
        float min = -0.1f;
        float max = 1.1f;
        float zPos = 10;

        int flag = Random.Range(0, 4);
        switch (flag)
        {
            case 0:
                randomPos = new Vector3(max, Random.Range(min, max), zPos);
                break;
            case 1:
                randomPos = new Vector3(min, Random.Range(min, max), zPos);
                break;
            case 2:
                randomPos = new Vector3(Random.Range(min, max), max, zPos);
                break;
            case 3:
                randomPos = new Vector3(Random.Range(min, max), min, zPos);
                break;
        }
        randomPos = Camera.main.ViewportToWorldPoint(randomPos);
        return randomPos;
    }
}
