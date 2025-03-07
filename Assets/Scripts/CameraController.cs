using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    //private Transform playerTransform;

    public bool inDamange;
    private Vector3 damangeFXPosition;


    void Start()
    {
        // Assign player transform
        //playerTransform = player.GetComponent<Transform>();

        transform.position = new Vector3(player.position.x, player.position.y, -10);
    }


    void Update()
    {
        if(inDamange){
            damangeFXPosition = new Vector3(MakeDamageFXMagnitude(), MakeDamageFXMagnitude(), 0);
        }
        else{
            damangeFXPosition = Vector3.zero;
        }

        transform.position = new Vector3(player.position.x, player.position.y, -10) + damangeFXPosition;
    }

    float MakeDamageFXMagnitude(){
        return Random.Range(-0.2f, 0.2f);
    }

    // Is only called when player got damage in positive value(damaging)
    public void ShakeCamera(){
        inDamange = true;
        Invoke(nameof(StopShake), 0.1f);
    }

    private void StopShake()
    {
        inDamange = false;
    }
}
