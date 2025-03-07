using System.Collections;
using UnityEngine;

public class BasicShot : Weapon
{
    protected override void Shoot()
    {
        if (!inCoolDown)
        {
            inCoolDown = true;
            Vector3 targetPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = targetPos - shotPlace.position;
            direction.z = 0f;
            GameObject shot = Instantiate(projectile, shotPlace.position, Quaternion.identity);
            shot.GetComponent<Rigidbody2D>().linearVelocity = direction.normalized * projectileSpeed + direction.normalized * gm.player.GetComponent<Rigidbody2D>().linearVelocity.magnitude;
            StartCoroutine(ShootCo());
        }

    }

    IEnumerator ShootCo()
    {
        yield return new WaitForSeconds(attackSpeed);
        inCoolDown = false;
    }
}
