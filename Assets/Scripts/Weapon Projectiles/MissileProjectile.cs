using System.Collections;
using UnityEngine;

public class MissileProjectile : Projectile
{
    [SerializeField] float explosionRadius;
    [SerializeField] GameObject explosionEffect;
    GameObject sprite;
    void Start()
    {
        sprite = transform.Find("Sprite").gameObject;
        ParticleSystem explosion = explosionEffect.GetComponent<ParticleSystem>();
        ParticleSystem.ShapeModule shape = explosion.shape;
        shape.radius = explosionRadius;
    }
    void Update()
    {
        if (Vector3.Distance(transform.position, targetPos) <= 0.1f)
        {
            Explode();
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("Boss"))
        {
            Explode();
        }
    }
    //Explode and damage all enemy in area
    void Explode()
    {
        sprite.SetActive(false);
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        explosionEffect.SetActive(true);
        RaycastHit2D[] Targets = Physics2D.CircleCastAll(transform.position, explosionRadius, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Game Objects"));

        foreach (RaycastHit2D hit in Targets)
        {
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<EnemyController>().DamageToEnemy(damage);
            }
            else if (hit.collider.gameObject.CompareTag("Boss"))
            {
                hit.collider.GetComponent<BossController>().DamageToBoss(damage);
            }
        }
        StartCoroutine(ExplosionCo());
    }

    IEnumerator ExplosionCo()
    {
        yield return new WaitForSeconds(0.55f);
        Destroy(gameObject);
    }
}
