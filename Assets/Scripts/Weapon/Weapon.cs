using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected float damage, attackSpeed, projectileSpeed, currentDamage, currentAttackSpeed;
    protected Camera mainCam;
    protected GameManager gm;
    [SerializeField] protected GameObject projectile;
    [SerializeField] protected Transform shotPlace;
    [SerializeField] int weaponCode;

    private bool isShooting = false;
    public bool IsShooting{
        get{return isShooting;}
        set{isShooting = value;}
    }
    public int WeaponCode{
        get{return weaponCode;}
    }
    protected bool inCoolDown;
    void Start()
    {
        gm = GameManager.instance;
        mainCam = Camera.main;
        currentDamage = damage * gm.attackDamage;
        currentAttackSpeed = attackSpeed * gm.attackSpeed;
    }


    protected virtual IEnumerator ShootCo()
    {
        yield return new WaitForSeconds(currentAttackSpeed);
        inCoolDown = false;
    }
}
