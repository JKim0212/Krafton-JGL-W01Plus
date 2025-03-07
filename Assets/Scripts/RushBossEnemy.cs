using System.Collections;
using UnityEngine;

public class RushBossEnemy : BossEnemy
{
    [Header("Characteristics")]
    [SerializeField] private float rushDistance = 5.0f;
    [SerializeField] private float rushSpeed = 10.0f;
    [SerializeField] private float rushDelay = 3.0f;

    private bool _isRushing;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(RushCoroutine());
    }

  

    private IEnumerator RushCoroutine()
    {
        while (true)
        {
            if (gm.isPlaying)
            {
                yield return new WaitForSeconds(rushDelay);

                if (player != null)
                {
                    _isRushing = true;
                    var startPos = transform.position;
                    var targetPos = startPos + ((player.transform.position - transform.position)).normalized * rushDistance;

                    // Rush
                    var elapsedTime = 0f;
                    while (elapsedTime < rushDistance / rushSpeed)
                    {
                        transform.position = Vector2.Lerp(startPos, targetPos, elapsedTime / (rushDistance / rushSpeed));
                        elapsedTime += Time.deltaTime;
                        yield return null;
                    }

                    transform.position = targetPos;
                    _isRushing = false;
                }
            } else {
                break;
            }

        }
    }
}
