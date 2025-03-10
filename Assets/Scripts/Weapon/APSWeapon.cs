using UnityEngine;
using System.Collections;
using System.Linq;

public class APSWeapon : Weapon, IWeapon
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Shoot()
    {
        if (!inCoolDown)
        {
            {
                RaycastHit2D hit = Physics2D.CircleCast(gm.player.transform.position, 2f, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Projectile"));
                if (hit)
                {
                    inCoolDown = true;
                    Vector3 targetPos = hit.collider.transform.position;
                    GameObject laser = Instantiate(projectile, shotPlace.position, Quaternion.identity);
                    laser.GetComponent<LineRenderer>().SetPositions(new Vector3[] { shotPlace.position, targetPos });
                    StartCoroutine(DestroyLaser(laser));
                    Destroy(hit.collider.gameObject);
                    StartCoroutine(ShootCo());
                }

            }
        }
    }
    IEnumerator DestroyLaser(GameObject laser)
    {
        yield return new WaitForSeconds(projectileSpeed);
        Destroy(laser);
    }
    public void UpdateStats(float damModifier, float speedModifier)
    {
        currentDamage = damage*damModifier;
        currentAttackSpeed = attackSpeed * speedModifier;
    }
}
