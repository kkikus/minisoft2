using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropFunction : MonoBehaviour, IDropHandler
{
    Transform parent;
    GameObject itemBeingDragged;
    Sprite btnPurple;

    public void OnDrop(PointerEventData eventData)
    {
        parent = transform.parent;
        int numberCircles = parent.childCount;
        itemBeingDragged = DragHandeler.itemBeingDragged;
        //vytvorenie kopie prikazu/karticky
        if (!itemBeingDragged.transform.parent.CompareTag("Slots"))
        {
            if (!FreeAllCircles()) {
                FullCardMessage();
                return;
            }
            GameObject newGameObject = Instantiate(itemBeingDragged, itemBeingDragged.transform.parent.position, Quaternion.identity);
            newGameObject.transform.SetParent(itemBeingDragged.transform.parent, false);
            newGameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        // ked prikaz/karticka ma rodica s tagom "Slots" a chce ho umiestnit na ine miesto, ktore tiez maju tagy "Slots", tak sa nevymaze, ale umiestni sa tam
        else if (DragHandeler.getRemoved && itemBeingDragged.transform.parent.CompareTag("Slots"))
        {
            DragHandeler.getRemoved = false;
        }

        //ak je prazdny kruh, tak mozeme pridat karticku/prikaz, bez toho aby sme dalsie vyplnene prikazy posuvali do prava
        if (transform.childCount == 0)
        {
            itemBeingDragged.transform.SetParent(transform);
        }
        else
        {
            int idCircleSelected = getIdCircle(numberCircles);
            int freeIdCircle = FindFreeCircle(idCircleSelected, numberCircles);
            if (freeIdCircle > -1)
            {
                for (int i = freeIdCircle; i > idCircleSelected; i--)
                {
                    parent.GetChild(i - 1).transform.GetChild(0).transform.SetParent(parent.GetChild(i).transform);
                }
                itemBeingDragged.transform.SetParent(transform);
            }
        }
    }

    public bool FreeAllCircles()
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            if (parent.GetChild(i).childCount == 0)
                return true;
        }
        return false;
    }

    private int getIdCircle(int n)
    {
        for (int i = 0; i < n; i++)
        {
            if (parent.GetChild(i) == transform)
                return i;
        }
        return -1;
    }
    //bud najde volne miesto alebo poziciu itemBeingDragged - bude to ta pozicia, kt. je blizsia k id
    private int FindFreeCircle(int id,int n)
    {
        for (int i = id; i < n; i++)
        {
            if (parent.GetChild(i).childCount == 0)
                return i;
            else if (parent.GetChild(i) == itemBeingDragged.transform.parent)
                return i;
        }
        return -1;
    }

    private void FullCardMessage()
    {
        GameObject newMessage = new GameObject();
        newMessage.name = "FullCardPanel";
        newMessage.AddComponent<Image>();
        newMessage.GetComponent<Image>().sprite = Resources.Load<Sprite>("button-purple");
        newMessage.transform.position = new Vector3(0, 0, 0);
        newMessage.AddComponent<Animator>();
        newMessage.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("InfoEmptyCirclesPanel") as RuntimeAnimatorController;

        RectTransform messageTransform = newMessage.GetComponent<RectTransform>();
        messageTransform.sizeDelta = new Vector2(680, 300);

        GameObject newText = new GameObject();
        newText.name = "FullCardPanel";
        newText.AddComponent<Text>();
        newText.GetComponent<Text>().text = "Zoznam je plný, ak chceš pridať novú kartičku, musíš pre ňu uvoľniť miesto v zozname.";
        newText.GetComponent<Text>().font = Resources.Load<Font>("LithosPro-Regular");
        newText.GetComponent<Text>().fontSize = 40;
        newText.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
    
        newText.transform.position = new Vector3(-2, 2, 0);

        RectTransform textTransform = newText.GetComponent<RectTransform>();
        textTransform.sizeDelta = new Vector2(560, 290);
        newText.transform.SetParent(newMessage.transform, false);

        newMessage.transform.SetParent(GameObject.Find("PanelGame").transform, false);

        StartCoroutine(Countdown(newMessage));
    }

    private IEnumerator Countdown(GameObject go)
    {
        yield return new WaitForSeconds(5);
        Destroy(go);
    }
}
