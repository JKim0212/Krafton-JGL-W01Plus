using UnityEngine;

public interface IWeapon
{
    public bool IsShooting{
        get;
        set;
    }
    public int WeaponCode{get;}
    void UpdateStats(float damModifier);
    void Shoot();
}
