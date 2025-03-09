using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField]Transform[] maps;
    [SerializeField] GameObject obstacle;
    public void GenerateObstacles(int numObstacles){
        foreach(Transform m in maps){
            GenerateObstacleLoop(m,numObstacles/4);
        }
    }

    void GenerateObstacleLoop(Transform m,int numObstacles){
        for(int i = 0; i< numObstacles;i++){
            Vector3 spawnPos = m.position + new Vector3(Random.Range(-20f, 20f), Random.Range(-20f, 20f),0);
            if(spawnPos.y <= GameManager.instance.player.transform.position.y+2f || spawnPos.y <= GameManager.instance.player.transform.position.y-2f){
                spawnPos.y *= 2;
            }
            if(spawnPos.x <= GameManager.instance.player.transform.position.x+2f || spawnPos.y <= GameManager.instance.player.transform.position.x-2f){
                spawnPos.x *= 2;
            }
            Instantiate(obstacle, spawnPos, Quaternion.Euler(new Vector3(0,0,Random.Range(0, 360f))), m);
        }
    }

    public void RemoveAllObstacle(){
        foreach(Transform m  in maps){
            for(int i = 0; i < m.childCount; i++){
                m.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
