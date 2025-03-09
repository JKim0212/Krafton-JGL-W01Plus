using System.Collections;
using System.Linq;
using UnityEngine;

public class LaserWeapon : Weapon, IWeapon
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Shoot()
    {
        if (!inCoolDown)
        {
            inCoolDown = true;
            Vector3 targetPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            targetPos.z = 0f;
            Vector2 shootDir = targetPos - shotPlace.position;

            //Damage all enemy that the laser hits.
            //Blocked when hitting obstacle
            RaycastHit2D[] hits = Physics2D.RaycastAll(shotPlace.position, shootDir, Mathf.Infinity, LayerMask.GetMask("Game Objects"));
            foreach (RaycastHit2D hit in hits.OrderBy(x => x.distance))
            {
                if (hit.collider.gameObject.CompareTag("Obstacle"))
                {
                    targetPos = hit.point;
                    break;
                }
                if (hit.collider.gameObject.CompareTag("Enemy"))
                {
                    hit.collider.GetComponent<EnemyController>().DamageToEnemy(damage);
                } else if (hit.collider.gameObject.CompareTag("Boss")){
                    hit.collider.GetComponent<BossController>().DamageToBoss(damage);
                }

            }
            GameObject laser = Instantiate(projectile, shotPlace.position, Quaternion.identity);
            laser.GetComponent<LineRenderer>().SetPositions(new Vector3[] { shotPlace.position, targetPos });
            StartCoroutine(DestroyLaser(laser));
            StartCoroutine(ShootCo());
        }
    }

    IEnumerator DestroyLaser(GameObject laser)
    {
        yield return new WaitForSeconds(projectileSpeed);
        Destroy(laser);
    }

    public void UpdateStats(float damModifier)
    {
        damage *= damModifier;
    }
}
