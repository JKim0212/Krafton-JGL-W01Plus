using UnityEngine;

public class StationController : MonoBehaviour
{
    private GameManager gm;
    
    void Start()
    {
        gm = GameManager.instance;
    }
    public void Spawn(){
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        Vector3 delta = new Vector3(Mathf.Cos(angle)*gm.stationDistance, Mathf.Sin(angle)*gm.stationDistance,0f);
        transform.position = transform.position + delta;
    }
}
