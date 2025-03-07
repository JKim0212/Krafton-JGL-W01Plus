using UnityEngine;

public class BlackBarController : MonoBehaviour
{
    [SerializeField] Animator blackBarAnim;

    private GameManager gm;
    private GameObject player;

    void Start()
    {
        gm = GameManager.instance;   
    }
    // void Update()
    // {
    //     transform.position = player.transform.position;
    // }
    public void hideBar(){
        blackBarAnim.SetTrigger("Hide");
    }
}
