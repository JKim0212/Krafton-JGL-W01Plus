using Unity.VisualScripting;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] Transform[] maps;
    [SerializeField] GameObject obstacle;
    public void GenerateObstacles(int numObstacles)
    {
        foreach (Transform m in maps)
        {
            GenerateObstacleLoop(m, numObstacles);
        }
    }

    void GenerateObstacleLoop(Transform m, int numObstacles)
    {
        for (int i = 0; i < numObstacles; i++)
        {
            Vector3 spawnPos = m.position + new Vector3(Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0);
            GameObject spawned = Instantiate(obstacle, spawnPos, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360f))), m);
            if(Vector3.Distance(spawnPos, GameManager.instance.player.transform.position) <= 20){
                Destroy(spawned);
            }

        }
    }

    public void RemoveAllObstacle()
    {
        foreach (Transform m in maps)
        {
            for (int i = 0; i < m.childCount; i++)
            {
                m.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
