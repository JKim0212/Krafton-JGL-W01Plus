using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float laserDamage;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("@@DE ---> 레이저 충돌!");
            
            if (GameManager.instance != null)
            {
                GameManager.instance.DamagePlayer(laserDamage);
            }
        }
    }
}
