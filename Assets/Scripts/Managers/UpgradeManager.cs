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
    [SerializeField] Dictionary<string, int> upgradePrice = new Dictionary<string, int>();
    [SerializeField] int priceChange;
    private GameManager gm;

    void Start()
    {
        gm = GameManager.instance;
        for (int i = 0; i < initialCosts.Length; i++)
        {
            upgradePrice[upgradeNames[i]] = initialCosts[i];
        }
    }
    public void UpgradeStats(string code)
    {
        if(gm.Money >= upgradePrice[code]){
            gm.Money -= upgradePrice[code];
            if(!code.Equals("Repair")){
                upgradePrice[code] += priceChange;
            }
            switch(code){
                case "Health":
                gm.health += upgradeAmounts[0];
                gm.curHealth += upgradeAmounts[0];
                break;
                case "Attack Speed":
                gm.attackSpeed *= upgradeAmounts[1];
                break;
                case "Attack Damage":
                gm.attackDamage *= upgradeAmounts[2];
                break;
                case "Move Speed":
                gm.playerMoveSpeed *= upgradeAmounts[3];
                break;
                case "Repair":
                gm.curHealth += upgradeAmounts[4];
                break;
            }
        }
    }
}
