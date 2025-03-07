using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomEnemySpawner : MonoBehaviour
{
    [Header("Generic")]
    public List<GameObject> enemyList;
    private GameObject player;
    internal PlayerController pc;
    private GameManager gm;

    [Header("Timing Control")]
    private float timeSinceLastSpawn;
    [SerializeField] float spawnInterval = 2.5f;
    [SerializeField] float spawnIntervalCoolTime = 2.0f;
    [SerializeField] float spawnIntervalMin = 0.8f;
    [SerializeField] float spawnIntervalModScale = -0.025f;

    [Header("Dist Control")]
    [SerializeField] float distFromPlayerToEnemy = 18f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pc = player.GetComponent<PlayerController>();
        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    internal void SetIntervalMin(float spawnIntervalMinInput)
    {
        spawnIntervalMin = spawnIntervalMinInput;
    }

    // Update is called once per frame
    void Update()
    {
        if(true){
            timeSinceLastSpawn += Time.deltaTime;
            if (timeSinceLastSpawn >= spawnInterval)
            {
                SpawnEnemy();
                timeSinceLastSpawn = 0f;
                if(spawnIntervalMin != 0){
                    //Debug.Log("Next enemy will be spawned after: " + spawnInterval + " seconds");
                    spawnInterval = Mathf.Max(spawnIntervalMin, spawnInterval + spawnIntervalModScale);
                }
            }
        }
    }

    public void SpawnEnemy()
    {
        Instantiate(enemyList[Random.Range(0, enemyList.Count)], SetSpawnLocation(), Quaternion.identity);
    }

    private Vector3 SetSpawnLocation()
    {
        // Set new area of spawnning bullet box
        Vector3 playerPosition = player.transform.position;
        Vector2 randomDirection = Random.insideUnitCircle.normalized * distFromPlayerToEnemy;
        return playerPosition + new Vector3(randomDirection.x, randomDirection.y, 0);
    }

    internal void RequestCooltime()
    {
        if(spawnInterval < spawnIntervalCoolTime){
            spawnInterval = spawnIntervalCoolTime;
            Debug.Log("Enemy spawning Time gets slower due to the item");
        }
    }
}
