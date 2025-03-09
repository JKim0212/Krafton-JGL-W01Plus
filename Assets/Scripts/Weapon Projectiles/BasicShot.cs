using UnityEngine;

public class BasicShot : Projectile
{
    //Damage and disappear on hit
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy")){
            collision.gameObject.GetComponent<EnemyController>().DamageToEnemy(damage);
            Destroy(gameObject);
        } else if(collision.CompareTag("Obstacle")){
            Destroy(gameObject);
        } else if(collision.gameObject.CompareTag("Boss")){
            collision.gameObject.GetComponent<BossController>().DamageToBoss(damage);
            Destroy(gameObject);
        }
    }
}
