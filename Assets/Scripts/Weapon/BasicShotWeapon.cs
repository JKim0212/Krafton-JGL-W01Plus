using System.Collections;
using UnityEngine;

public class BasicShotWeapon : Weapon, IWeapon
{
    public void Shoot()
    {
        if (!inCoolDown)
        {
            inCoolDown = true;
            Vector3 targetPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = targetPos - shotPlace.position;
            direction.z = 0f;
            GameObject shot = Instantiate(projectile, shotPlace.position, Quaternion.identity);
            shot.GetComponent<BasicShot>().Init(damage, targetPos);
            shot.GetComponent<Rigidbody2D>().linearVelocity = direction.normalized * projectileSpeed + direction.normalized * gm.player.GetComponent<Rigidbody2D>().linearVelocity.magnitude;
            StartCoroutine(ShootCo());
        }

    }

    public void UpdateStats(float damModifier)
    {
        damage *= damModifier;
    }
}
