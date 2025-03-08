using UnityEngine;

public class BasicShot : Projectile
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy")){
            Destroy(collision.gameObject);
            Destroy(gameObject);
        } else if(collision.CompareTag("Obstacle")){
            Destroy(gameObject);
        }
    }
}
