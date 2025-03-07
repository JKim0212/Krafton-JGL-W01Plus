using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [Header("Generic")]
    [SerializeField] List<GameObject> bossList;
    private GameObject player;
    private GameObject bossObject;

    internal PlayerController pc;
    private GameManager gm;
    public bool isBossSpawned = false;

    [Header("Timing Control")]
    private float timeSinceLastSpawn;
    [SerializeField] float spawnInterval = 10f;


    [Header("Dist Control")]
    [SerializeField] float distFromPlayerToEnemy = 30f;

    [Header("Difficulty Control")]
    [SerializeField] float bossLifeInit = 10;
    private float bossLifeCurrent;
    [SerializeField] int bossSpawnedCount = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pc = player.GetComponent<PlayerController>();
        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();

        bossLifeCurrent = bossLifeInit;
        bossSpawnedCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isBossSpawned){
            timeSinceLastSpawn += Time.deltaTime;
            if (timeSinceLastSpawn >= spawnInterval)
            {
                Debug.Log("Boss spawned");
                SpawnBoss();
                isBossSpawned = true;
                timeSinceLastSpawn = 0f;
            }
        }
    }

    void SpawnBoss()
    {
        bossSpawnedCount++;
        bossObject = Instantiate(bossList[Random.Range(0, bossList.Count)], SetSpawnLocation(), Quaternion.identity);
        bossObject.GetComponent<BossEnemy>().SetLife(bossLifeCurrent);

        bossLifeCurrent += 2 * bossSpawnedCount;
        Debug.Log("Boss life: " + bossLifeCurrent);
    }

    private Vector3 SetSpawnLocation()
    {
        // Set new area of spawnning bullet box
        Vector3 playerPosition = player.transform.position;
        Vector2 randomDirection = Random.insideUnitCircle.normalized * distFromPlayerToEnemy;
        return playerPosition + new Vector3(randomDirection.x, randomDirection.y, 0);
    }

    internal void enableBossSpawn(){
        isBossSpawned = false;
    }
}
