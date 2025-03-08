using System.Collections;
using UnityEngine;

public class MissileProjectile : Projectile
{
    [SerializeField] float explosionRadius;
    [SerializeField] GameObject explosionEffect;
    GameObject sprite;
    void Start()
    {
        explosionEffect.transform.localScale = Vector3.one * explosionRadius;
        sprite = transform.Find("Sprite").gameObject;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy") || collision.CompareTag("Obstacle")){
            Explode();
            }
    }

    void Explode(){
        sprite.SetActive(false);
        explosionEffect.SetActive(true);
        RaycastHit2D[] Targets =Physics2D.CircleCastAll(transform.position, explosionRadius, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Game Objects"));
        
        foreach(RaycastHit2D hit in Targets){
            Debug.Log(hit.collider.gameObject.name);
            if(hit.collider.gameObject.CompareTag("Enemy")){
                Destroy(hit.collider.gameObject);
            }
        }
        StartCoroutine(ExplosionCo());
    }

    IEnumerator ExplosionCo(){
        yield return new WaitForSeconds(0.75f);
        Destroy(gameObject);
    }
}
