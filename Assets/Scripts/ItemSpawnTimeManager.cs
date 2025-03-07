using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemSpawnTimeManager : MonoBehaviour
{
    public static ItemSpawnTimeManager instance;

    [Header("Generic")]
    private float timeSinceLastSpawn = 0f;
    [SerializeField] float spawnInterval = 30f;
    [SerializeField] bool isAbleToSpawn = false; // If true, enemy has able to spawn item when they are dead
    [SerializeField] bool isSpawned = false; // If true, item is not spawned anymore

    [Header("Generic")]
    public List<GameObject> itemList;

    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isAbleToSpawn && !isSpawned){ // If enemy is dead and item should not be spawned before
            timeSinceLastSpawn += Time.deltaTime;
            if (timeSinceLastSpawn >= spawnInterval)
            {
                isAbleToSpawn = true;
                timeSinceLastSpawn = 0f;
                Debug.Log("Item is able to be spawned, destroy enemy and get Items.");
            }
        }
    }

    public bool IsAbleToSpawnItem(){
        return isAbleToSpawn;
    }

    public void SetAbleToSpawnItem(bool value){
        isAbleToSpawn = value;
    }

    public void SetIsSpawned(bool value){
        isSpawned = value;
    }

    public GameObject SpawnItem(){
        isSpawned = true;
        return itemList[Random.Range(0, itemList.Count)];
    }
}
