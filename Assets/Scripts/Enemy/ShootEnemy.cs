using System.Collections;
using UnityEngine;

public class ShootEnemy : EnemyController
{
    [SerializeField] GameObject projectile;
    [SerializeField] float damage = 1f;
    [SerializeField] float attackSpeed = 0.5f;
    [SerializeField] float projectileSpeed = 5f;
    bool inCoolDown = false;
    protected override void Move()
    {
       Vector2 lookdir = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
            float angle = Mathf.Atan2(lookdir.y, lookdir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        if (!inRange)
        {
            
            rb.linearVelocity = lookdir.normalized * moveSpeed;
        } else {
            rb.linearVelocity = Vector2.zero;
            if(!inCoolDown){
                inCoolDown = true;
                Shoot();
            }
        }

    }

    void Shoot(){

        GameObject proj = Instantiate(projectile, transform.position, transform.rotation);
        proj.GetComponent<EnemyProjectile>().Init(damage, Vector2.zero);
        proj.GetComponent<Rigidbody2D>().linearVelocity = proj.transform.right * projectileSpeed;
        StartCoroutine(ShootCo());
    }

    IEnumerator ShootCo(){
        yield return new WaitForSeconds(attackSpeed);
        inCoolDown = false;
    }
}
