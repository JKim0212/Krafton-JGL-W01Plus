using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum WeaponSlot
    {
        left,
        right
    }
    [SerializeField] protected float damage, attackSpeed, projectileSpeed;
    protected Camera mainCam;
    private GameManager gm;
    [HideInInspector] public WeaponSlot weaponSlot;
    [SerializeField] protected GameObject projectile;
    [SerializeField] protected Transform shotPlace;
    
    protected bool inCoolDown;
    void Start()
    {
        gm = GameManager.instance;
        mainCam = Camera.main;
    }

    void Update()
    {
        if (weaponSlot == WeaponSlot.left)
        {
            if (Input.GetMouseButton(0))
            {
                Shoot();
            }
        }
        if (weaponSlot == WeaponSlot.right)
        {
            if (Input.GetMouseButton(1))
            {
                Shoot();
            }
        }

    }
    protected virtual void Shoot(){

    }
}
