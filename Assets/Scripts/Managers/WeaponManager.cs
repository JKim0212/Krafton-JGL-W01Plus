using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] GameObject left, right;
    private GameManager gm;
    [SerializeField] GameObject[] Weapons;
    public GameObject[] weaponIcons;
    public List<int> inactiveWeapons;
    [SerializeField] RectTransform[] slots;
    public RectTransform iconSlot;
    private int[] equippedCodes;
    public bool[] changed = { false, false };
    void Start()
    {
        gm = GameManager.instance;
        Debug.Log(left.name);
        equippedCodes = new int[] { left.GetComponent<IWeapon>().WeaponCode, right.GetComponent<IWeapon>().WeaponCode };
    }
    public void Shoot(int slot)
    {
        if (slot == 0)
        {
            left.GetComponent<IWeapon>().Shoot();
        }
        else if (slot == 1)
        {
            right.GetComponent<IWeapon>().Shoot();
        }
    }
    public void PrepareSlots()
    {
        for (int i = 0; i < changed.Length; i++)
        {
            changed[i] = false;
        }

        weaponIcons[equippedCodes[0]].transform.SetParent(slots[0]);
        weaponIcons[equippedCodes[0]].transform.localPosition = Vector3.zero;
        weaponIcons[equippedCodes[0]].transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90f));
        weaponIcons[equippedCodes[0]].SetActive(true);
        weaponIcons[equippedCodes[0]].GetComponent<WeaponIcon>().Changed = true;
        weaponIcons[equippedCodes[0]].GetComponent<WeaponIcon>().setParentTransform();

        weaponIcons[equippedCodes[1]].transform.SetParent(slots[1]);
        weaponIcons[equippedCodes[1]].transform.localPosition = Vector3.zero;
        weaponIcons[equippedCodes[1]].transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90f));
        weaponIcons[equippedCodes[1]].SetActive(true);
        weaponIcons[equippedCodes[1]].GetComponent<WeaponIcon>().Changed = true;
        weaponIcons[equippedCodes[1]].GetComponent<WeaponIcon>().setParentTransform();

        for (int i = 2; i < slots.Count() - 1; i++)
        {
            Debug.Log("inactive Weapons " + inactiveWeapons.Count.ToString());
            if (inactiveWeapons.Count == 0)
            {
                break;
            }
            int inactiveCode = UnityEngine.Random.Range(0, inactiveWeapons.Count);
            int choiceCode = inactiveWeapons[inactiveCode];
            weaponIcons[choiceCode].transform.SetParent(slots[i]);
            weaponIcons[choiceCode].transform.localPosition = Vector3.zero;
            weaponIcons[choiceCode].transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            weaponIcons[choiceCode].SetActive(true);
            weaponIcons[choiceCode].GetComponent<WeaponIcon>().Changed = false;
            weaponIcons[choiceCode].GetComponent<WeaponIcon>().setParentTransform();
            inactiveWeapons.RemoveAt(inactiveCode);
            Debug.Log("inactive Weapons " + inactiveWeapons.Count.ToString());
        }
    }
    public void UpdateStats()
    {
        left.GetComponent<IWeapon>().UpdateStats(gm.attackDamage);
        right.GetComponent<IWeapon>().UpdateStats(gm.attackDamage);
    }

    public void UpdateSlots(int weaponCode, int slotNum)
    {
        changed[slotNum] = true;
        switch (slotNum)
        {
            case 0:
                if (!(equippedCodes[0] == weaponCode))
                {
                    left.transform.SetParent(null);
                    left.SetActive(false);
                    left = Weapons[weaponCode];
                }
                break;
            case 1:
                if (!(equippedCodes[1] == weaponCode))
                {
                    right.transform.SetParent(null);
                    right.SetActive(false);
                    right = Weapons[weaponCode];
                }
                break;
            default:
                Debug.Log("Wrong Weapon Slot Code");
                break;
        }
        equippedCodes[slotNum] = weaponCode;
        UpdateWeapons();

    }

    void UpdateWeapons()
    {
        if (left != null)
        {
            left.SetActive(true);
            left.transform.position = gm.leftWeapon.position;
            left.transform.SetParent(gm.leftWeapon);
        }
        if (right != null)
        {
            right.SetActive(true);
            right.transform.position = gm.rightWeapon.position;
            right.transform.SetParent(gm.rightWeapon);
        }

    }
}
