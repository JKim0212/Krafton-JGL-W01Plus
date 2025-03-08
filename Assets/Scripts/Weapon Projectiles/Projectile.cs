using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected float damage;
    public void Init(float damage){
        this.damage = damage;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
