using UnityEngine;

public interface IWeapon
{
    public int WeaponCode{get;}
    void UpdateStats(float damModifier);
    void Shoot();
}
