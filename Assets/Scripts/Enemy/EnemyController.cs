using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class EnemyController : MonoBehaviour
{
    protected GameObject player;
    protected GameManager gm;
    
    [Header("Enemy Stats")]
    [SerializeField] protected float health = 10f;
    [SerializeField] protected float moveSpeed =5f;
    void Start()
    {
        gm = GameManager.instance;
        player = gm.player;
    }

    void Update()
    {
        Move();
    }

    protected virtual void Move(){

    }
}
