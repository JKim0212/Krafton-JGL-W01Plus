using UnityEngine;

public class StationController : MonoBehaviour
{
    private GameManager gm;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gm = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn(){
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        Vector3 delta = new Vector3(Mathf.Cos(angle)*gm.stationDistance, Mathf.Sin(angle)*gm.stationDistance,0f);
        transform.position = transform.position + delta;
    }
}
