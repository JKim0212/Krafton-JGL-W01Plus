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


    protected virtual IEnumerator ShootCo()
    {
        yield return new WaitForSeconds(attackSpeed);
        inCoolDown = false;
    }
}
