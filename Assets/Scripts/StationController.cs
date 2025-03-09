using UnityEngine;

public class StationController : MonoBehaviour
{
    public void Spawn(){
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        Vector3 delta = new Vector3(Mathf.Cos(angle)*GameManager.instance.stationDistance, Mathf.Sin(angle)*GameManager.instance.stationDistance,0f);
        transform.position = transform.position + delta;
    }
}
