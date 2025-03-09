using System.Collections;
using UnityEngine;

public class MissileWeapon : Weapon, IWeapon
{
    [SerializeField] SpriteRenderer indicator;
    
    public void Shoot()
    {
        if (!inCoolDown)
        {
            indicator.color = Color.red;
            inCoolDown = true;
            Vector3 targetPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            targetPos.z = 0;
            Vector3 direction = targetPos - shotPlace.position;
            direction.z = 0f;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            GameObject shot = Instantiate(projectile, shotPlace.position, Quaternion.Euler(0, 0, angle));

            shot.GetComponent<MissileProjectile>().Init(damage, targetPos);
            shot.GetComponent<Rigidbody2D>().linearVelocity = direction.normalized * projectileSpeed + direction.normalized * gm.player.GetComponent<Rigidbody2D>().linearVelocity.magnitude;
            StartCoroutine(ShootCo());
        }
    }

    protected override IEnumerator ShootCo(){
        yield return new WaitForSeconds(attackSpeed);
        indicator.color = Color.green;
        inCoolDown = false;
    }

    public void UpdateStats(float damModifier)
    {
        damage *= damModifier;
    }
}
