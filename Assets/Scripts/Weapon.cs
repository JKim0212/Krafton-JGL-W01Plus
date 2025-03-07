using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum WeaponSlot
    {
        left,
        right
    }
    [SerializeField] float damage, attackSpeed, projectileSpeed;
    protected Camera mainCam;
    private GameManager gm;
    WeaponSlot weaponSlot;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
