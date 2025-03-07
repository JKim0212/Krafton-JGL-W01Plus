using UnityEngine;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.UnityLinker;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] GameObject player;
    [Header("Game Stat")]
    public float health;
    [HideInInspector] public float curHealth;
    public int money;
    public float playerMoveSpeed;
    public float attackSpeed;
    public float attackDamage;
    public float stationDistance;
    
    [Header("Managers")]
    [SerializeField] UIManager ui;
    [SerializeField] StationController station;
    [SerializeField] GameObject blackBar;
    public bool isPlaying;

    [Header("Progress")]
    [SerializeField] int stageNum;
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
        blackBar.SetActive(true);
        StartCoroutine(EndStageCo());
    }

    IEnumerator EndStageCo(){

        yield return new WaitForSeconds(1.5f);
        ui.Upgrade();
        player.SetActive(false);
        player.transform.position = station.transform.position;
    }

    public void StartNextStage(){
       player.SetActive(true);
       player.GetComponent<PlayerController>().playerRb.linearVelocity = Vector3.right * 10;
       StartCoroutine(CutScene());
    }

    IEnumerator CutScene(){
        yield return new WaitForSeconds(3f);
        blackBar.GetComponent<BlackBarController>().hideBar();
    }


}
