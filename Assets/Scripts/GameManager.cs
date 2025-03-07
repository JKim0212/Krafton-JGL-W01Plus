using UnityEngine;
using System;
using System.Collections;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Game Stat")]
    public float health;
    private float curHealth;
    public int cash;
    public float playerMoveSpeed;
    public float stationDistance;
    
    [Header("Managers")]
    private UIManager ui;
    [SerializeField] StationController station;
    public bool isPlaying;
    void Awake()
    {
        if(instance == null){
            instance = this;
        } else{
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ui = UIManager.instance;
        curHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            station.Spawn();
        }
    }

    public void EndStage(){
        isPlaying = false;
        StartCoroutine(EndStageCo());
    }

    IEnumerator EndStageCo(){
        yield return new WaitForSeconds(1.5f);
        ui.Upgrade();
    }
}
