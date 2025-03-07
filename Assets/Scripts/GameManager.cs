using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public RandomEnemySpawner spawnManager;
    [SerializeField] private CameraController cameraController;
    public BossSpawner bossSpawnManager;

    
    [Header("Game Mechanic")]
    public GameObject player;
    public float maxHealth;
    public float curHealth;
    public bool isPlaying;
    public int missileAmount;
    
    public GameObject managers;

    [HideInInspector] public int totalScore = 0;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        curHealth = maxHealth;
        missileAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamagePlayer(float damage)
    {
        if(damage > 0 && !player.GetComponent<PlayerController>().isShielded){
            cameraController.ShakeCamera();
        }
        curHealth -= damage;
        
        if (curHealth <= 0)
        {
            isPlaying = false;
            player.SetActive(false);
            UIManager.instance.EndGame();
            
        }
        else if(curHealth > maxHealth)
        {
            Debug.Log("Health Overflow detected, resetting health to max.");
            curHealth = maxHealth;
        }

        UIManager.instance.UpdateHealth();
    }

    private IEnumerator IncreaseScore()
    {
        while (isPlaying)
        {
            totalScore++;
            yield return new WaitForSeconds(1.0f);
            UIManager.instance.UpdateScore();
        }
    }

    public void IncreaseScore(int score)
    {
        totalScore += score;
        UIManager.instance.UpdateScore();
    }
}
