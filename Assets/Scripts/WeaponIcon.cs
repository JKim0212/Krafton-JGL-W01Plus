using UnityEngine;
using UnityEngine.EventSystems;
public class WeaponIcon : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject draggedIcon;
    public int weaponCode;
    Vector3 startPos;
    private Transform onDragParent;
    [HideInInspector] public Transform startParent;
    private bool changed;
    public bool Changed{
        get{return changed;}
        set{changed = value;}
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!changed)
        {
            draggedIcon = gameObject;
            startPos = transform.position;
            startParent = transform.parent;

            GetComponent<CanvasGroup>().blocksRaycasts = false;
            transform.SetParent(onDragParent);
        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!changed)
        {
            transform.position = Input.mousePosition;
        }

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!changed)
        {
            draggedIcon = null;
            GetComponent<CanvasGroup>().blocksRaycasts = true;

            if (transform.parent == onDragParent)
            {
                transform.position = startPos;
                transform.SetParent(startParent);
            }
            else
            {
                changed = true;
            }
        }

    }

    public void setParentTransform(){
        onDragParent = transform.parent.GetComponent<RectTransform>();
    }
}
