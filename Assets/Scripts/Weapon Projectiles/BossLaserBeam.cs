using System.Collections;
using UnityEngine;

public class BossLaserBeam : MonoBehaviour
{
    bool inDamageCooldown = false;
    float damageCooldown = 0.1f;
    float laserLifeTime = 3f;
    float damage = 10f;

    public void Init(float damage, float damageCooldown, float laserLifeTime)
    {
        this.damage = damage;
        this.damageCooldown = damageCooldown;
        this.laserLifeTime = laserLifeTime;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!inDamageCooldown)
            {
                inDamageCooldown = true;
                StartCoroutine(Damage());
            }
        }
    }

    IEnumerator Damage()
    {
        GameManager.instance.DamagePlayer(damage);
        yield return new WaitForSeconds(damageCooldown);
        inDamageCooldown = false;
    }
}
