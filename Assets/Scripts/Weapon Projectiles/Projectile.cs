using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected float damage;
    protected Vector3 targetPos;
    public void Init(float damage, Vector3 target){
        this.damage = damage;
        targetPos = target;
        transform.parent = GameManager.instance.pool.transform;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
