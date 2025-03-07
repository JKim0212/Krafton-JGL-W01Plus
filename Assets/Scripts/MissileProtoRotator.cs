using UnityEngine;

public class MissileProtoRotator : MonoBehaviour
{
    private GameObject player;
    private PlayerController playerController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null && (playerController.spawnDistance > Vector2.Distance(transform.position, player.transform.position)))
        {
            // 타겟의 위치와 현재 오브젝트의 위치를 이용하여 방향 벡터 계산
            Vector2 direction = - player.transform.position + transform.position;

            // 방향 벡터의 각도를 계산
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // z축 회전 값 설정
            transform.rotation = Quaternion.Euler(0, 0, angle - 90); // 2D 환경에서는 Y축 회전을 사용하므로 -90도 조정
        }
    }
}
