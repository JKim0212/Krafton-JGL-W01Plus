using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    protected GameObject player;
    protected GameManager gm;
    protected Rigidbody2D rb;
    [SerializeField] GameObject earth;
    [Header("Boss Stats")]
    [SerializeField] float health = 10f;
    [SerializeField] float moveSpeed = 20f;
    [SerializeField] float collisionDamage = 1f;
    [SerializeField] float attackRange;
    private bool inRange = false;

    void Start()
    {
        gm = GameManager.instance;
        player = gm.player;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {

        Vector2 lookdir = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
        float angle = Mathf.Atan2(lookdir.y, lookdir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        inRange = Vector2.Distance(player.transform.position, transform.position) <= attackRange;
        if (!inRange)
        {

            rb.linearVelocity = lookdir.normalized * moveSpeed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;

        }

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            gm.DamagePlayer(collisionDamage);
        }
    }


    public void DamageToBoss(float damageAmount)
    {
        health -= damageAmount;
        if (health <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
