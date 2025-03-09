using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Scriptable Objects/LevelData")]
public class LevelData : ScriptableObject
{
    [SerializeField] string stageName;
    public string StageName => stageName;
    [SerializeField] float[] spawnRate;
    public float[] SpawnRate => spawnRate;
    [SerializeField] float spawnInterval;
    public float SpawnInterval => spawnInterval;
    [SerializeField] int numObstacles;
    public int NumObstacles => numObstacles;

}
