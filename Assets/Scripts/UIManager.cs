using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public enum Panel
    {
        Upgrade,
        Weapon
    }
    private GameManager gm;

    [Header("Pannels")]
    [SerializeField] GameObject stationPanel, upgradePanel, weaponPanel;
    private Panel activePanel;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        activePanel = Panel.Upgrade;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Upgrade()
    {
        stationPanel.SetActive(true);
    }

    public void ChangePanel(int panel)
    {
        switch (panel)
        {
            case 0:
                if (activePanel == Panel.Weapon)
                {
                    weaponPanel.SetActive(false);
                    upgradePanel.SetActive(true);
                    activePanel = Panel.Upgrade;
                }
                break;
            case 1:
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
}
