using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed;
    [HideInInspector] public Rigidbody2D playerRb;
    [SerializeField] GameObject shooter;
    [SerializeField] GameObject stationPointer;
    [SerializeField] Camera mainCam;
    // Update is called once per frame
    private GameManager gm;

    void Start()
    {
        gm = GameManager.instance;
        moveSpeed = gm.playerMoveSpeed;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Station") && gm.isPlaying)
        {
            gm.EndStage();
        }
    }

    public void pointStation(bool locationFound){
        stationPointer.SetActive(locationFound);       
    }

    public void UpdateStats(){
        moveSpeed = gm.playerMoveSpeed;
    }
    void FixedUpdate()
    {
        if (gm.isPlaying)
        {
            Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
            transform.Translate(moveDir.normalized * moveSpeed * Time.deltaTime);

            Vector3 mousePos = Input.mousePosition;
            Vector3 worldPos = mainCam.WorldToScreenPoint(transform.localPosition);
            Vector2 offset = new Vector2(mousePos.x - worldPos.x, mousePos.y - worldPos.y);
            float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
            shooter.transform.rotation = Quaternion.Euler(0, 0, angle);
        } else {
            shooter.transform.rotation = Quaternion.Euler(0,0,-0);
        }
        if(!gm.isPlaying && !gm.isCutScene){
            playerRb.linearVelocity = playerRb.linearVelocity * Mathf.Lerp(1f, 0f, Time.fixedDeltaTime);
        }
    }
}
