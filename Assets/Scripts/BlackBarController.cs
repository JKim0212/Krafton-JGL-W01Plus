using UnityEngine;

public class BlackBarController : MonoBehaviour
{
    [SerializeField] Animator blackBarAnim;

    public void hideBar(){
        blackBarAnim.SetTrigger("Hide");
    }
}
