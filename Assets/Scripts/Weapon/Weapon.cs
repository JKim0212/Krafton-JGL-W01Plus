using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected float damage, attackSpeed, projectileSpeed;
    protected Camera mainCam;
    protected GameManager gm;
    [SerializeField] protected GameObject projectile;
    [SerializeField] protected Transform shotPlace;
    [SerializeField] int weaponCode;
    public int WeaponCode{
        get{return weaponCode;}
    }
    protected bool inCoolDown;
    void Start()
    {
        gm = GameManager.instance;
        mainCam = Camera.main;
        damage *= gm.attackDamage;
    }

    // void Update()
    // {
    //     if (gm.isPlaying)
    //     {
    //         if (weaponSlot == WeaponSlot.left)
    //         {
    //             if (Input.GetMouseButton(0))
    //             {
    //                 Shoot();
    //             }
    //         }
    //         if (weaponSlot == WeaponSlot.right)
    //         {
    //             if (Input.GetMouseButton(1))
    //             {
    //                 Shoot();
    //             }
    //         }
    //     }
    // }


    protected virtual IEnumerator ShootCo()
    {
        yield return new WaitForSeconds(attackSpeed);
        inCoolDown = false;
    }
}
