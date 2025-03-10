using UnityEngine;
using System.Collections;
public class BossMissileProjectile : Projectile
{

    [SerializeField] float explosionRadius;
    [SerializeField] GameObject explosionEffect;
    [SerializeField] GameObject sprite;
    [SerializeField] float projectileSpeed = 5f;
    [SerializeField] float lifeTime = 3f;
    GameObject player;
    Rigidbody2D rb;
    bool exploded = false;
    void Start()
    {
        player = GameManager.instance.player;
        sprite = transform.Find("Sprite").gameObject;
        rb = GetComponent<Rigidbody2D>();
        ParticleSystem explosion = explosionEffect.GetComponent<ParticleSystem>();
        ParticleSystem.ShapeModule shape = explosion.shape;
        shape.radius = explosionRadius;
        StartCoroutine(DestructionTimer());
    }
    void FixedUpdate()
    {
        if (GameManager.instance.isPlaying && !exploded)
        {
            Vector2 lookdir = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
            float angle = Mathf.Atan2(lookdir.y, lookdir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            rb.linearVelocity = (player.transform.position - transform.position).normalized * projectileSpeed;
        }

    }
    IEnumerator DestructionTimer()
    {
        yield return new WaitForSeconds(lifeTime);
        Explode();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Obstacle"))
        {
            Explode();
        }
    }

    void Explode()
    {
        exploded = true;
        sprite.SetActive(false);
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        explosionEffect.SetActive(true);
        RaycastHit2D Target = Physics2D.CircleCast(transform.position, explosionRadius, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Player"));
        if (Target) GameManager.instance.DamagePlayer(damage);
        StartCoroutine(ExplosionCo());
    }

    IEnumerator ExplosionCo()
    {
        yield return new WaitForSeconds(0.55f);
        Destroy(gameObject);
    }


}
