using UnityEngine;

public class BasicShot: Weapon
{
    protected override void Shoot(){
        Vector3 targetPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(targetPos.ToString());
    }
}
