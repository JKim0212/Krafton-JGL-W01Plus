using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float health;
    public int cash;
    public float stationDistance;

    public static GameManager instance;
    [SerializeField] StationController station;
    void Awake()
    {
        if(instance == null){
            instance = this;
        } else{
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            station.Spawn();
        }
    }
}
