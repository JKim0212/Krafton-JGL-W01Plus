using System.Collections;
using UnityEngine;

public class FlameThrowerWeapon : Weapon, IWeapon
{
    public void Shoot()
    {
        if (!inCoolDown)
        {
            //Damage all enemies within range and between certain angles
            RaycastHit2D[] hits = Physics2D.CircleCastAll(shotPlace.position, projectileSpeed, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Game Objects"));
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.gameObject.CompareTag("Enemy") || hit.collider.gameObject.CompareTag("Boss"))
                {
                    Vector2 offset = new Vector2(hit.point.x - shotPlace.transform.position.x, hit.point.y - shotPlace.transform.position.y);
                    float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
                    if (angle >= -45f && angle <= 45f)
                    {
                        if (hit.collider.gameObject.CompareTag("Enemy"))
                        {
                            hit.collider.GetComponent<EnemyController>().DamageToEnemy(damage);
                        }
                        else if (hit.collider.gameObject.CompareTag("Boss"))
                        {
                            hit.collider.GetComponent<BossController>().DamageToBoss(damage);
                        }

                    }
                }
            }
        }
        StartCoroutine(ShootCo());
    }
    void Update()
    {
        projectile.SetActive(IsShooting);
    }
    public void UpdateStats(float damModifier, float speedModifier)
    {
        currentDamage = damage*damModifier;
        currentAttackSpeed = attackSpeed * speedModifier;
    }
}