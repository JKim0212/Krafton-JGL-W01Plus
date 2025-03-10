using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
public class WeaponSlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image image;
    [SerializeField] int slotNum;

    GameObject Icon()
    {
        if (transform.childCount > 0)
        {
            return transform.GetChild(0).gameObject;
        }
        else
        {
            return null;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (Icon() == null)
        {
            WeaponIcon.draggedIcon.transform.SetParent(transform);
            WeaponIcon.draggedIcon.transform.position = transform.position;
        }
        else
        {
            if (WeaponIcon.draggedIcon != null && !GameManager.instance.weap.changed[slotNum])
            {
                GameObject oldWeapon = Icon();
                oldWeapon.transform.SetParent(GameManager.instance.weap.iconSlot);
                GameManager.instance.weap.inactiveWeapons.Add(oldWeapon.GetComponent<WeaponIcon>().weaponCode);
                oldWeapon.SetActive(false);
                WeaponIcon.draggedIcon.transform.SetParent(transform);
                WeaponIcon.draggedIcon.transform.position = transform.position;
                WeaponIcon.draggedIcon.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90f));
                GameManager.instance.UpdateWeapon(WeaponIcon.draggedIcon.GetComponent<WeaponIcon>().weaponCode, slotNum);
            }
        }
    }



    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!GameManager.instance.weap.changed[slotNum])
        {
            Color tempCol = image.color;
            tempCol.a = 0.2f;
            image.color = tempCol;
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Color tempCol = image.color;
        tempCol.a = 0f;
        image.color = tempCol;
    }

}
