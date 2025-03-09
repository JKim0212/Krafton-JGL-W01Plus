using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed;
    [HideInInspector] public Rigidbody2D playerRb;
    [SerializeField] GameObject shooter;
    [SerializeField] GameObject stationPointer;
    [SerializeField] Camera mainCam;
    private GameManager gm;
    private bool isBouncing;
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
            playerRb.linearVelocity = Vector2.zero;
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            isBouncing = true;
            playerRb.AddForce(-playerRb.linearVelocity * 1.5f, ForceMode2D.Impulse);
            StartCoroutine(Bounce());
        } else if(collision.gameObject.CompareTag("Boss")){
            isBouncing = true;
            playerRb.AddForce(-playerRb.linearVelocity * 3.5f, ForceMode2D.Impulse);
            StartCoroutine(Bounce());
        }
    }

    public void pointStation(bool locationFound)
    {
        stationPointer.SetActive(locationFound);
    }

    public void UpdateStats()
    {
        moveSpeed = gm.playerMoveSpeed;
    }
    void FixedUpdate()
    {
        if (gm.isPlaying)
        {
            if (!isBouncing)
            {
                Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
                playerRb.linearVelocity = moveDir.normalized * moveSpeed;
            }


            Vector3 mousePos = Input.mousePosition;
            Vector3 worldPos = mainCam.WorldToScreenPoint(transform.localPosition);
            Vector2 offset = new Vector2(mousePos.x - worldPos.x, mousePos.y - worldPos.y);
            float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
            shooter.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            shooter.transform.rotation = Quaternion.Euler(0, 0, -0);
        }
        if (!gm.isPlaying && !gm.isCutScene)
        {
            playerRb.linearVelocity *= Mathf.Lerp(1f, 0f, Time.fixedDeltaTime);
        }
    }


    IEnumerator Bounce()
    {
        yield return new WaitForSeconds(0.2f);
        isBouncing = false;
    }
}
