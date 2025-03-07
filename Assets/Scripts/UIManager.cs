using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Rendering;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    private GameManager gm;
    [SerializeField] TextMeshProUGUI healthText, missileText, scoreText, timerText;
    [SerializeField] TextMeshProUGUI moveLvlText, attackLvlText, skillLvlText;
    [SerializeField] Slider healthSlider;
    [SerializeField] TextMeshProUGUI scoreTextGameOver;

    [SerializeField] GameObject startPanel, upgradePanel, endPanel, gameInfo;
    [SerializeField] int moveLvl, attackLvl, skillLvl;
    private float time;
    private int min;
    private int sec;

    [Header("Score Evaluation")]
    [SerializeField] float scoreInterval = 1f;
    private float scoreTimer = 0f;
    [SerializeField] int scoreCutAmateur = 250;
    [SerializeField] int scoreCutIntermediate = 500;
    [SerializeField] int scoreCutPro = 1000;
    [SerializeField] TextMeshProUGUI gameoverEvalText;
    [SerializeField] string quoteRookie;
    [SerializeField] string quoteAmateur;
    [SerializeField] string quoteIntermediate;
    [SerializeField] string quotePro;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        gm = GameManager.instance;
        timerText.text = "00:00";
        time = 0;
        min = 0;
        sec = 0;
        scoreTimer = 0f;
    }

    void Update()
    {
        
        if (gm.isPlaying)
        {
            scoreTimer += Time.deltaTime;
            if(scoreTimer >= scoreInterval){
                gm.totalScore += 1;
                scoreTimer = 0f;
                UpdateScore();
            }
            time += Time.deltaTime;
            UpdateTimer();
        }

    }

    // Update is called once per frame
    public void UpdateScore()
    {
        scoreText.text = gm.totalScore.ToString();
    }

    public void UpdateHealth()
    {
        healthSlider.value = gm.curHealth;
        healthText.text = gm.curHealth.ToString();
    }

    public void UpdateMissile()
    {
        missileText.text = "" + gm.missileAmount;
    }

    void UpdateTimer()
    {
        min = (int)(time / 60f);
        sec = (int)Mathf.Ceil(time % 60f) - 1;
        timerText.text = min + ":" + sec.ToString("D2");
    }

    public void StartGame()
    {
        startPanel.SetActive(false);
        gm.isPlaying = true;
        gm.player.SetActive(true);
        gm.managers.SetActive(true);
        gameInfo.SetActive(true);
    }

    public void Upgrade()
    {
        if (gm.isPlaying)
        {
            gm.isPlaying = false;
            upgradePanel.SetActive(true);
        }

    }

    public void UpgradeChoice(int upgradeCode)
    {
        switch (upgradeCode)
        {
            case 0:
                //upgrade move speed;
                gm.player.GetComponent<PlayerController>().moveSpeed *= 1.5f;
                moveLvl += 1;
                moveLvlText.text = "Lv. " + moveLvl;
                break;
            case 1:
                //upgrade attack speed;
                gm.player.GetComponent<PlayerController>().fireRate *= 0.9f;
                attackLvl += 1;
                attackLvlText.text = "Lv. " + attackLvl;
                break;
            case 2:
                //upgrade skill power;
                gm.player.GetComponent<PlayerController>().skillPower *= 1.5f;
                skillLvlText.text = "Lv. " + skillLvl;
                break;
            default:
                Debug.Log("Wrong Upgrade Code");
                break;
        }
        gm.isPlaying = true;
        upgradePanel.SetActive(false);
    }

    public void EndGame()
    {
        gameInfo.SetActive(false);
        gm.isPlaying = false;
        EvaulateScore();
        endPanel.SetActive(true);
        scoreTextGameOver.text = "" + gm.totalScore;
    }



    private void EvaulateScore()
    {
        if (gm.totalScore < scoreCutAmateur)
        {
            gameoverEvalText.text = quoteRookie;
        }
        else if (gm.totalScore < scoreCutIntermediate)
        {
            gameoverEvalText.text = quoteAmateur;
        }
        else if (gm.totalScore < scoreCutPro)
        {
            gameoverEvalText.text = quoteIntermediate;
        }
        else
        {
            gameoverEvalText.text = quotePro;
        }
    }

    public void Restart()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
