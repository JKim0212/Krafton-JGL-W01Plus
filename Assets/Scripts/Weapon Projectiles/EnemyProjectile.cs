using UnityEngine;

public class EnemyProjectile : Projectile
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player")){
            GameManager.instance.DamagePlayer(damage);
            Destroy(gameObject);
        }
    }
}
