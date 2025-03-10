using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public enum Panel
    {
        Upgrade,
        Weapon
    }
    private GameManager gm;

    [Header("Pannels")]
    [SerializeField] GameObject stationPanel;
    [SerializeField] GameObject upgradePanel, weaponPanel, startPanel, playPanel;

    [Header("Text")]
    [SerializeField] TextMeshProUGUI healthText, moneyText, timerText, upgradeMoneyText, endGameText;
    [SerializeField] TextMeshProUGUI maxHealthButton, attackSpeedButton, moveSpeedButton, attackDamageButton, fixButton;
    [SerializeField] Slider healthSlider, upgradeHealthSlider;
    private Panel activePanel;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        activePanel = Panel.Upgrade;
        gm = GameManager.instance;
        healthSlider.maxValue = gm.health;
        upgradeHealthSlider.maxValue = gm.health;
    }


    public void Upgrade()
    {
        playPanel.SetActive(false);
        stationPanel.SetActive(true);
    }

    public void ChangePanel(int panel)
    {
        switch (panel)
        {
            case 0:
                if (gm.StageNum == 0)
                {
                    gm.Tuto.ShowTuto(1);
                }
                if (activePanel == Panel.Weapon)
                {
                    weaponPanel.SetActive(false);
                    upgradePanel.SetActive(true);
                    activePanel = Panel.Upgrade;
                }
                break;
            case 1:
                if (gm.StageNum == 0)
                {
                    gm.Tuto.ShowTuto(1);
                }
                if (activePanel == Panel.Upgrade)
                {
                    upgradePanel.SetActive(false);
                    weaponPanel.SetActive(true);
                    activePanel = Panel.Weapon;
                }
                break;
            default:
                Debug.Log("Incorrect Button selected");
                break;
        }
    }

    public void NextStage()
    {
        upgradePanel.SetActive(true);
        activePanel = Panel.Upgrade;
        weaponPanel.SetActive(false);
        stationPanel.SetActive(false);
        playPanel.SetActive(true);
        gm.StartNextStage();
        gm.Tuto.ShopTuto.SetActive(false);
    }

    public void StartGame()
    {
        startPanel.SetActive(false);
        playPanel.SetActive(true);
        UpdateHealth();
        UpdateMoney();
        gm.StartGame();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void EndGame(bool isWin)
    {
        if (isWin)
        {
            endGameText.text = "Victory!";
        }
        else
        {
            endGameText.text = "You Lost!";
        }
        endGameText.gameObject.SetActive(true);
    }

    public void UpdateHealth()
    {
        healthSlider.value = gm.curHealth;
        upgradeHealthSlider.value = gm.curHealth;
        healthSlider.maxValue = gm.health;
        upgradeHealthSlider.maxValue = gm.health;
        healthText.text = "Health : " + gm.curHealth;
    }

    public void UpdateMoney()
    {
        moneyText.text = "Money : " + gm.Money;
        upgradeMoneyText.text = "Money : " + gm.Money;
    }

    public void UpdateTimer(float time)
    {
        int min = (int)(time / 60f);
        int sec = (int)Mathf.Ceil(time % 60f) - 1;
        timerText.text = min.ToString("D2") + ":" + sec.ToString("D2");
    }

    public void UpdateUpgrade(string upgradeCode, int price, int level)
    {
        switch (upgradeCode)
        {
            case "Health":
                maxHealthButton.text = $"최대 체력 증가\nLv. {level}\n가격: {price}";
                break;
            case "Attack Damage":
                attackDamageButton.text = $"데미지 증가\nLv. {level}\n가격: {price}";
                break;
            case "Attack Speed":
                attackSpeedButton.text = $"공격 속도 증가\nLv. {level}\n가격: {price}";
                break;
            case "Move Speed":
                moveSpeedButton.text = $"이동 속도 증가\nLv. {level}\n가격: {price}";
                break;
            case "Repair":
                fixButton.text = $"수리\n가격: {price}";
                break;

        }
    }

    public void Restart(){
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
