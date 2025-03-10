using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossController : MonoBehaviour
{
    protected GameObject player;
    [SerializeField] GameObject hitEffect;
    protected GameManager gm;
    protected Rigidbody2D rb;

    [Header("Boss Stats")]
    [SerializeField] float health = 10f;
    [SerializeField] float moveSpeed = 20f;
    [SerializeField] float collisionDamage = 1f;
    [SerializeField] float attackRange = 10f;
    private bool inRange = false;
    [Header("Boss Combat")]
    [SerializeField] Transform ShootPoint;
    [SerializeField] GameObject basicProjectile, missileProjectile, gravityField;
    [SerializeField] float basicAttackSpeed, basicProjectileSpeed, basicAttackDamage;
    bool inCoolDown = false;
    [SerializeField] float skillCoolDown;
    bool inSkillCoolDown = false;
    [SerializeField] float missileDamage, missileCount, missileCoolDown;
    [SerializeField] float laserDamage, laserLifeTime, laserCooltime, laserChargeTime;
    [SerializeField] GameObject laserProjectile, laserChargeFx;

    bool isShootingLaser = false;
    [SerializeField] float gravityTime;

    [SerializeField] GameObject death, sprites;
    void Start()
    {
        gm = GameManager.instance;
        player = gm.player;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (gm.isPlaying && !isShootingLaser)
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
                Shoot();
                Skill();
            }

        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }


    void Shoot()
    {
        if (!inCoolDown)
        {
            inCoolDown = true;
            GameObject proj = Instantiate(basicProjectile, ShootPoint.position, transform.rotation);
            proj.GetComponent<EnemyProjectile>().Init(basicAttackDamage, Vector2.zero);
            proj.GetComponent<Rigidbody2D>().linearVelocity = proj.transform.right * basicProjectileSpeed;
            StartCoroutine(ShootCo());
        }
    }

    IEnumerator ShootCo()
    {
        yield return new WaitForSeconds(basicAttackSpeed);
        inCoolDown = false;
    }
    void Skill()
    {
        if (!inSkillCoolDown)
        {
            inSkillCoolDown = true;
            int skillNum = Random.Range(0, 3);

            switch (skillNum)
            {
                case 0:
                    Missile();
                    break;
                case 1:
                    GravityPull();
                    break;
                case 2:
                    LaserBeam();
                    break;
            }
            StartCoroutine(SkillCo());
        }

    }
    IEnumerator SkillCo()
    {
        yield return new WaitForSeconds(skillCoolDown);
        inSkillCoolDown = false;
    }
    void Missile()
    {
        StartCoroutine(MissileCo());
    }
    IEnumerator MissileCo()
    {
        for (int i = 0; i < missileCount; i++)
        {
            Instantiate(missileProjectile, ShootPoint.position, transform.rotation);
            yield return new WaitForSeconds(missileCoolDown);
        }
    }
    void GravityPull()
    {
        gravityField.SetActive(true);
        StartCoroutine(GravityCo());
    }

    IEnumerator GravityCo()
    {
        yield return new WaitForSeconds(gravityTime);
        gravityField.SetActive(false);
    }

    void LaserBeam()
    {
        Debug.Log("Shooting Laser");
        laserChargeFx.SetActive(true);
        StartCoroutine(ShootLaser());

    }

    IEnumerator ShootLaser()
    {
        yield return new WaitForSeconds(laserChargeTime);
        isShootingLaser = true;
        Vector3 targetPos = player.transform.position;
        GameObject beam = Instantiate(laserProjectile, transform.position, transform.rotation);
        beam.GetComponent<LineRenderer>().SetPositions(new Vector3[] { ShootPoint.position, targetPos });
        beam.GetComponent<BossLaserBeam>().Init(laserDamage, laserCooltime, laserLifeTime);
        beam.GetComponent<EdgeCollider2D>().SetPoints(new List<Vector2>(new Vector2[] { transform.InverseTransformPoint(ShootPoint.position), transform.InverseTransformPoint(targetPos) }));
        yield return new WaitForSeconds(laserLifeTime);
        Destroy(beam);
        laserChargeFx.SetActive(false);
        isShootingLaser = false;
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
        Instantiate(hitEffect, transform.position, Quaternion.identity);
        health -= damageAmount;
        if (health <= 0f)
        {
            StartCoroutine(DeathExplode());
            gm.EndGame(true);
        }
    }

    IEnumerator DeathExplode(){
        death.SetActive(true);
        sprites.SetActive(false);
        yield return new WaitForSeconds(0.8f);
        Destroy(gameObject);
    }
}
