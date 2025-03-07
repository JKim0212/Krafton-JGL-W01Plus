using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public int itemCode;
    /* 
    Item Code: 
    0 for Heal
    1 for Missile
    2 for Skill Usage
    */

    private float rotSpeed = 8f;
    private GameManager gm;
    private ItemSpawnTimeManager itemSpawnTimeManager;
    private PlayerController playerController;
    private PlayerInterfaceController playerInterfaceController;

    [SerializeField] float healMount = 10f;
    [SerializeField] int missileAmount = 5;

    private void Start()
    {
        gm = GameManager.instance;
        itemSpawnTimeManager = ItemSpawnTimeManager.instance;
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        playerInterfaceController = GameObject.Find("Player").GetComponent<PlayerInterfaceController>();

        playerInterfaceController.SetItemObject(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, rotSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Item has been picked up.");
            ItemEffect();
            itemSpawnTimeManager.SetIsSpawned(false); // Reset status of isSpawned
            Destroy(gameObject);

            // Disable item notifier due to item has been picked up
            playerInterfaceController.SetItemNotifier(false);
        }
    }

    public void ItemEffect()
    {
        switch (itemCode)
        {
            case 0:
                gm.DamagePlayer(-1 * healMount);
                Debug.Log("Health has been increased.");
                break;
            case 1:
                gm.missileAmount += this.missileAmount;
                Debug.Log("Missile amount has been increased into " + missileAmount);
                UIManager.instance.UpdateMissile();
                break;
            case 2:
                Debug.Log("Skill item activated.");
                UseSkill();            
                break;
            default:
                Debug.LogError("Invalid item code detected, no effect applied.");
                break;
        }
    }

    public void UseSkill()
    {
        // Skill usage
        int skillMode = Random.Range(0, 3);
       
        // Use skill based on skillMode
        switch (skillMode)
        {
            case 0:
                Debug.Log("Gravity Shot has been used.");
                playerController.GravityShot();
                break;
            case 1:
                // Shield
                Debug.Log("Shield has been used.");
                playerController.Shield();
                break;
            case 2:
                // Charge
                Debug.Log("Charge has been used.");
                playerController.Charge();
                break;
            default:
                Debug.LogError("Invalid skill mode detected, no effect applied.");
                break;
        }
    }
}
