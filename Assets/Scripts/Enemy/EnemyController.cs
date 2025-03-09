using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    protected GameObject player;
    protected GameManager gm;
    protected Rigidbody2D rb;
    private bool isBouncing = false;
    [Header("Enemy Stats")]
    [SerializeField] protected float health = 10f;
    [SerializeField] protected float moveSpeed = 5f;
    [SerializeField] protected float collisionDamage = 1f;

    [SerializeField] float attackRange;
    protected bool inRange;


    void Start()
    {
        gm = GameManager.instance;
        player = gm.player;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if(gm.isPlaying && !isBouncing){
            inRange = Vector2.Distance(player.transform.position,transform.position) <= attackRange;
            Move();
        }
        
    }

    protected virtual void Move()
    {

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player")){
            isBouncing = true;
            gm.DamagePlayer(collisionDamage);
            rb.AddForce(-rb.linearVelocity*1.5F, ForceMode2D.Impulse);
            StartCoroutine(Bounce());
        }
    }

    IEnumerator Bounce(){
        yield return new WaitForSeconds(0.5f);
        isBouncing = false;
    }

    public void DamageToEnemy(float damageAmount){
        health -= damageAmount;
        if(health <= 0f){
            Destroy(gameObject);
        }
    }
}
