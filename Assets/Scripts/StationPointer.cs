using UnityEngine;

public class StationPointer : MonoBehaviour
{
    [SerializeField] GameObject station;
    public GameObject Station{
        get{return station;}
        set{station = value;}
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        float angle = Mathf.Atan2(station.transform.position.y - transform.position.y, station.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
