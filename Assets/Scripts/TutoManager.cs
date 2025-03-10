using System.Collections;
using UnityEngine;

public class TutoManager : MonoBehaviour
{
    [SerializeField] GameObject movementTuto;
    [SerializeField] GameObject shopTuto;
    [SerializeField] GameObject stageTuto;
    public GameObject ShopTuto => shopTuto;

    public void ShowTuto(int code)
    {
        switch (code)
        {
            case 0:
                StartCoroutine(MoveTuto());
                break;
            case 1:
                ShopTutoOpen(shopTuto.activeSelf);
                break;
            case 2:
                StartCoroutine(StageTuto());
                break;
        }
    }

    IEnumerator MoveTuto()
    {
        movementTuto.SetActive(true);
        yield return new WaitForSeconds(10f);
        movementTuto.SetActive(false);
    }

    IEnumerator StageTuto()
    {
        stageTuto.SetActive(true);
        yield return new WaitForSeconds(5f);
        stageTuto.SetActive(false);
    }
    void ShopTutoOpen(bool activate)
    {
        shopTuto.SetActive(!activate);
    }
}
