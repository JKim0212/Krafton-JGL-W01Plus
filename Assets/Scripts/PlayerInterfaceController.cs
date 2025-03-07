using UnityEngine;

public class PlayerInterfaceController : MonoBehaviour
{
    [SerializeField] GameObject itemNotifier;
    [SerializeField] GameObject bossNotifier;

    [SerializeField] GameObject itemObject;
    [SerializeField] GameObject bossObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SetItemObject(GameObject itemObject)
    {
        this.itemObject = itemObject;
        itemNotifier.SetActive(true);
    }

    public void SetItemNotifier(bool status)
    {
        itemNotifier.SetActive(status);
    }

    public void SetBossObject(GameObject bossObject)
    {
        this.bossObject = bossObject;
        bossNotifier.SetActive(true);
    }

    public void SetBossNotifier(bool status)
    {
        bossNotifier.SetActive(status);
    }

    // Update is called once per frame
    void Update()
    {
        // Set Direction of Item Notifier
        if(itemObject != null){
            itemNotifier.transform.LookAt(itemObject.transform.position, Vector3.right);
        }
        // Set Direction of Boss Notifier
        if(bossObject != null){
            bossNotifier.transform.LookAt(bossObject.transform.position, Vector3.right);
        }
    }
}
