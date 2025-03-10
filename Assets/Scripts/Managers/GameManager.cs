using UnityEngine;
using System;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("Player")]
    public GameObject player, boss;
    public Transform leftWeapon, rightWeapon;

    [Header("Game Stat")]
    public float health;
    public float curHealth;
    private int money;
    public int Money { get { return money; } set { money = value; } }
    public float playerMoveSpeed;
    public float attackSpeed;
    public float attackDamage;
    public float stationDistance, bossSpawnDistance;

    [Header("Managers")]
    public UIManager ui;
    [SerializeField] StationController station;
    public WeaponManager weap;
    public PoolManager pool;
    [SerializeField] SpawnManager sp;
    [SerializeField] MapManager map_m;
    [SerializeField] TutoManager tuto;
    public TutoManager Tuto => tuto;

    [Header("Cut Scene")]
    [SerializeField] GameObject blackBar;
    public bool isCutScene;


    [Header("Progress")]
    int stageNum = 0;
    public int StageNum => stageNum;
    public bool isPlaying = false;
    [SerializeField] float stationAppearTime;
    private float time;
    private bool locationFound = false;
    [SerializeField] LevelData[] levels;

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
        time = 0f;
    }

    void Update()
    {
        if (isPlaying)
        {
            time += Time.deltaTime;
            ui.UpdateTimer(time);
            if (time >= stationAppearTime && !locationFound)
            {
                if (stageNum == 0)
                {
                    tuto.ShowTuto(2);
                }
                player.GetComponent<PlayerController>().pointStation(locationFound = true);
            }

            if (Input.GetMouseButton(0))
            {
                weap.Left.GetComponent<IWeapon>().IsShooting = true;
                weap.Shoot(0);
            }
            else
            {
                weap.Left.GetComponent<IWeapon>().IsShooting = false;
            }
            if (Input.GetMouseButton(1))
            {
                weap.Right.GetComponent<IWeapon>().IsShooting = true;
                weap.Shoot(1);
            }
            else
            {
                weap.Right.GetComponent<IWeapon>().IsShooting = false;
            }

            sp.Spawn();

        }

    }
    public void StartGame()
    {
        sp.SpawnInterval = levels[stageNum].SpawnInterval;
        sp.SpawnProb = levels[stageNum].SpawnRate;
        map_m.GenerateObstacles(levels[stageNum].NumObstacles);
        locationFound = false;
        isPlaying = true;
        station.Spawn();
        weap.UpdateStats();
        player.GetComponent<PlayerController>().UpdateStats();
        tuto.ShowTuto(0);
    }
    public void EndStage()
    {
        if (stageNum < levels.Length)
        {
            player.GetComponent<PlayerController>().pointStation(locationFound = false);
            isPlaying = false;
            blackBar.SetActive(true);
            StartCoroutine(EndStageCo());
        }
    }

    IEnumerator EndStageCo()
    {
        for (int i = 0; i < pool.transform.childCount; i++)
        {
            pool.transform.GetChild(i).gameObject.SetActive(false);
        }
        map_m.RemoveAllObstacle();
        yield return new WaitForSeconds(1.5f);
        ui.Upgrade();
        weap.PrepareSlots();
        player.transform.position = station.transform.position;

    }

    public void StartNextStage()
    {
        stageNum += 1;
        if (stageNum < levels.Length)
        {
            sp.SpawnInterval = levels[stageNum].SpawnInterval;
            sp.SpawnProb = levels[stageNum].SpawnRate;
            map_m.GenerateObstacles(levels[stageNum].NumObstacles);
            player.GetComponent<PlayerController>().playerRb.linearVelocity = Vector3.right * 10;
            player.GetComponent<PlayerController>().UpdateStats();
            weap.UpdateStats();
            StartCoroutine(CutScene());
        }



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
        yield return new WaitForSeconds(3f);
        station.Spawn();
        blackBar.SetActive(false);
        isPlaying = true;
        player.GetComponent<PlayerController>().playerRb.linearVelocity = Vector3.zero;
        time = 0f;
        locationFound = false;
        if (stageNum == levels.Length - 1)
        {
            StartFinalStage();
        }
    }

    void StartFinalStage()
    {
        float angle = UnityEngine.Random.Range(0f, 360f) * Mathf.Deg2Rad;
        Vector3 delta = new Vector3(Mathf.Cos(angle) * bossSpawnDistance, Mathf.Sin(angle) * bossSpawnDistance, 0f);
        boss.transform.position = player.transform.position + delta;
        boss.SetActive(true);
    }
    public void UpdateWeapon(int weaponCode, int slotNum)
    {
        weap.UpdateSlots(weaponCode, slotNum);
    }

    public void EndGame(bool isWin)
    {
        isPlaying = false;
        StartCoroutine(EndCo(isWin));
    }

    IEnumerator EndCo(bool isWin)
    {
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < pool.transform.childCount; i++)
        {
            pool.transform.GetChild(i).gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(0.5f);
        ui.EndGame(isWin);
    }

    //Combat system

    public void DamagePlayer(float damageToPlayer)
    {
        curHealth -= damageToPlayer;
        ui.UpdateHealth();
        if (curHealth <= 0)
        {
            Destroy(player);
            EndGame(false);
        }
    }



}
