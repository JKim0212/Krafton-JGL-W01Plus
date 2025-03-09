using UnityEngine;

public class BasicEnemy : EnemyController
{

    protected override void Move()
    {
        rb.linearVelocity = (player.transform.position-transform.position).normalized * moveSpeed;
        Vector2 lookdir = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
        float angle = Mathf.Atan2(lookdir.y, lookdir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        }
}
