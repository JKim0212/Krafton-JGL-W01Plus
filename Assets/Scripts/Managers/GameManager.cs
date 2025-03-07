using UnityEngine;
using System;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("Player")]
    public GameObject player;
    public Transform leftWeapon, rightWeapon;

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

    [Header("Cut Scene")]
    [SerializeField] GameObject blackBar;
    public bool isCutScene;


    [Header("Progress")]
    [SerializeField] int stageNum;
    public bool isPlaying;
    [SerializeField] float stationAppearTime;
    private float time;
    private bool locationFound = false;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        curHealth = health;
    }

    void Update()
    {
        if (isPlaying)
        {
            time += Time.deltaTime;
            if (time >= stationAppearTime && !locationFound)
            {
                player.GetComponent<PlayerController>().pointStation(locationFound = true);
            }
        }

    }
    public void EndStage()
    {
        player.GetComponent<PlayerController>().pointStation(locationFound = false);
        isPlaying = false;
        blackBar.SetActive(true);
        StartCoroutine(EndStageCo());
    }

    IEnumerator EndStageCo()
    {

        yield return new WaitForSeconds(1.5f);
        ui.Upgrade();
        player.SetActive(false);
        player.transform.position = station.transform.position;
    }

    public void StartNextStage()
    {   
        player.SetActive(true);
        player.GetComponent<PlayerController>().playerRb.linearVelocity = Vector3.right * 10;
        player.GetComponent<PlayerController>().UpdateStats();
        StartCoroutine(CutScene());
    }

    IEnumerator CutScene()
    {
        isCutScene = true;
        yield return new WaitForSeconds(3f);
        blackBar.GetComponent<BlackBarController>().hideBar();
        StartCoroutine(EndCutScene());
    }

    IEnumerator EndCutScene()
    {
        isCutScene = false;
        yield return new WaitForSeconds(2f);
        station.Spawn();
        blackBar.SetActive(false);
        isPlaying = true;
        player.GetComponent<PlayerController>().playerRb.linearVelocity = Vector3.zero;
        time = 0f;
        locationFound = false;
    }


}
