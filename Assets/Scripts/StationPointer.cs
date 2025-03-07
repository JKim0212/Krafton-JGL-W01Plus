using Unity.VisualScripting;
using UnityEngine;

public class StationPointer : MonoBehaviour
{
    [SerializeField] Transform station;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        float angle = Mathf.Atan2(station.position.y - transform.position.y, station.position.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
