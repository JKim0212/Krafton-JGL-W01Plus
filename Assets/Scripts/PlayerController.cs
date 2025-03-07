using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    [SerializeField] Rigidbody2D playerRb;
    [SerializeField] GameObject shooter;
    [SerializeField] Camera mainCam;
    // Update is called once per frame
    void Update()
    {
        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        transform.Translate(moveDir.normalized * moveSpeed * Time.deltaTime);

        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPos = mainCam.WorldToScreenPoint(transform.localPosition);
        Vector2 offset = new Vector2(mousePos.x - worldPos.x, mousePos.y - worldPos.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        shooter.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
