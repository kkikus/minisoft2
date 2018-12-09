using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandeler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public static GameObject itemBeingDragged;
    Vector3 startPosition;
    Transform startParent;
    static Boolean removed = false;

    public void OnBeginDrag(PointerEventData eventData)
    {
        itemBeingDragged = gameObject;
        startPosition = transform.position;
        startParent = transform.parent;
        GetComponent<CanvasGroup>().blocksRaycasts = false;

        //odstranenie policka
        if (itemBeingDragged.transform.parent.CompareTag("Slots"))
        {
            removed = true;
        }
    }

    public static Boolean getRemoved
    {
        get
        {
            return removed;
        }
        set
        {
            removed = value;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (removed)
        {
            Destroy(itemBeingDragged);
            removed = false;
        }
        else if (transform.parent == startParent)
        {
            transform.position = startPosition;
        }
        itemBeingDragged = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;

    }
}
