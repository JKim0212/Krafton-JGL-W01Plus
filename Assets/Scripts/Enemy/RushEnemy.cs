using System.Collections;
using NUnit.Framework;
using UnityEngine;

public class RushEnemy : EnemyController
{
    bool isRushing = false;
    [SerializeField] float rushCoolDown;
    protected override void Move()
    {

        if (!isRushing)
        {
            isRushing = true;
            Vector2 lookdir = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
            float angle = Mathf.Atan2(lookdir.y, lookdir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            rb.linearVelocity = lookdir.normalized * moveSpeed;
            StartCoroutine(RushCo());
        }
    }

    IEnumerator RushCo()
    {
        yield return new WaitForSeconds(0.8f);
        rb.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(rushCoolDown);
        isRushing = false;
    }
}
