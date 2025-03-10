using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.InputSystem;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] int[] initialCosts;
    [SerializeField] float[] upgradeAmounts;
    [SerializeField] string[] upgradeNames;
    Dictionary<string, int> upgradePrice = new Dictionary<string, int>();
    Dictionary<string, int> upgradelevel = new Dictionary<string, int>();
    [SerializeField] int priceChange;
    private GameManager gm;

    void Start()
    {
        gm = GameManager.instance;
        for (int i = 0; i < initialCosts.Length; i++)
        {
            upgradePrice[upgradeNames[i]] = initialCosts[i];
            upgradelevel[upgradeNames[i]] = 0;
        }
        for(int i = 0; i < initialCosts.Length - 1; i++){

        }
    }
    public void UpgradeStats(string code)
    {
        if (gm.Money >= upgradePrice[code] && upgradelevel[code] <= 5)
        {


            switch (code)
            {
                case "Health":
                    gm.Money -= upgradePrice[code];
                    gm.health += upgradeAmounts[0];
                    gm.curHealth += upgradeAmounts[0];
                    upgradePrice[code] += priceChange;
                    upgradelevel[code] += 1;
                    gm.ui.UpdateUpgrade(code, upgradePrice[code], upgradelevel[code]);
                    break;
                case "Attack Speed":
                    gm.Money -= upgradePrice[code];
                    gm.attackSpeed *= upgradeAmounts[1];
                    upgradePrice[code] += priceChange;
                    upgradelevel[code] += 1;
                    gm.ui.UpdateUpgrade(code, upgradePrice[code], upgradelevel[code]);
                    break;
                case "Attack Damage":
                    gm.Money -= upgradePrice[code];
                    gm.attackDamage *= upgradeAmounts[2];
                    upgradePrice[code] += priceChange;
                    upgradelevel[code] += 1;
                    gm.ui.UpdateUpgrade(code, upgradePrice[code], upgradelevel[code]);
                    break;
                case "Move Speed":
                    gm.Money -= upgradePrice[code];
                    gm.playerMoveSpeed *= upgradeAmounts[3];
                    upgradePrice[code] += priceChange;
                    upgradelevel[code] += 1;
                    gm.ui.UpdateUpgrade(code, upgradePrice[code], upgradelevel[code]);
                    break;
                case "Repair":
                    if (!(gm.curHealth == gm.health))
                    {
                        gm.Money -= upgradePrice[code];
                        gm.curHealth += upgradeAmounts[4];
                        if (gm.curHealth >= gm.health) gm.curHealth = gm.health;
                    }

                    break;
            }
            gm.ui.UpdateHealth();
            gm.ui.UpdateMoney();
        }
    }
}
