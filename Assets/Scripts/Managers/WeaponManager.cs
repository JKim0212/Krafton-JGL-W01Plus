using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] GameObject left, right, weapon;
    private GameManager gm;

    void Start()
    {
        gm = GameManager.instance;
    }
    public void SwitchWeapon(int pos)
    {
        switch (pos)
        {
            case 0:
                left.transform.SetParent(null);
                left.SetActive(false);
                left = weapon;
                break;
            case 1:
                right.transform.SetParent(null);
                right.SetActive(false);
                right = weapon;
                break;
            default:
                Debug.Log("Wrong Weapon Slot Code");
                break;
        }

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

    public void Shoot(int slot){
        if(slot == 0){
            left.GetComponent<IWeapon>().Shoot();
        } else if(slot == 1){
            right.GetComponent<IWeapon>().Shoot();
        }
    }
    public void UpdateStats()
    {
        left.GetComponent<IWeapon>().UpdateStats(gm.attackDamage);
        right.GetComponent<IWeapon>().UpdateStats(gm.attackDamage);
    }
}
