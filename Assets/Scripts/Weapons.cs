using UnityEngine;

[CreateAssetMenu(fileName = "Weapons", menuName = "Scriptable Objects/Weapons")]
public class Weapons : ScriptableObject
{
    float initialDamage;
    float initialAttackSpeed;
    float projectileSpeed;

    GameObject projectile;
    Transform shotPlace;


}
