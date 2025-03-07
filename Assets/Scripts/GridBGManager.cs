using UnityEngine;

public class GridBGManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Vector3 initPos;
    [SerializeField] Vector3 curPos;
    [SerializeField] Vector3 distWithPlayer;
    private float gridSize = 16f;
    // 0 for X, 1 for Y

    void Start()
    {
        player = GameObject.Find("Player");
        initPos = transform.position;
        curPos = initPos;
    }

    // Update is called once per frame
    void Update()
    {
        // Calulate distance between player and current position
        distWithPlayer = player.transform.position - curPos;

        // If player is out of the grid, move the grid to the player's position
        if(distWithPlayer.x > gridSize || distWithPlayer.x < -gridSize)
        {
            curPos = 
                new Vector3(
                    player.transform.position.x,
                    curPos.y,
                    curPos.z
                );
            transform.position = curPos;
        }
        else if(distWithPlayer.y > gridSize || distWithPlayer.y < -gridSize)
        {
            curPos = 
                new Vector3(
                    curPos.x,
                    player.transform.position.y,
                    curPos.z
                );
            transform.position = curPos;
        }
    }
}
