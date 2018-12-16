using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour {

    int numberTask;
    Sprite backgroundCirclesPanel;
    Sprite imageCircle;
    Sprite imageMound;
    Sprite imageTractor;
    Sprite imageCarrot;
    Sprite[] cards;
    GameObject[] cardsPrefab;
    public GameObject InfoEmptyCirclesPanel;
    public GameObject FullCardMessage;
    public GameObject gameobj;
    public GameObject finalGameMessage;
    public GameObject infoMessage;
    int[,] dataCarrot;
    int number;
    int indexAnimatorPanel;
    CheckSolution checkSolution;


    void Start ()
    {
        numberTask = 1;
        backgroundCirclesPanel = Resources.Load<Sprite>("backgroundForCircle");
        imageCircle = Resources.Load<Sprite>("circleForCard");
        imageMound = Resources.Load<Sprite>("mound");
        imageTractor = Resources.Load<Sprite>("tractor");
        imageCarrot = Resources.Load<Sprite>("carot");
        cards = new Sprite[]{ Resources.Load<Sprite>("cardCarrot"), Resources.Load<Sprite>("cardArrow") };
        cardsPrefab = new GameObject[] { (GameObject)Instantiate(Resources.Load("Prefabs/CardCarrotPrefab") as GameObject), (GameObject)Instantiate(Resources.Load("Prefabs/CardArrowPrefab") as GameObject) };
        dataCarrot = null;
        checkSolution = gameobj.GetComponent<CheckSolution>();
        startGame();
    }
	

    public void startGame()
    {
       // dataCarrot - { pozicia:pocet mrkiev }
        if (numberTask == 1)
        {
            number = 3;
            dataCarrot = new int[,] { {Random.Range(1,3), 1 } };
        }
        else if (numberTask == 2)
        {
            number = 3;
            dataCarrot = new int[,] { { 1, 1 }, {2, 1 } };
        }
        else if (numberTask == 3)
        {
            number = 4;
            dataCarrot = new int[,] { { Random.Range(1,4), Random.Range(2,4)}};
        }
        else if (numberTask == 4)
        {
            number = 4;
            dataCarrot = new int[,] { { 2, Random.Range(1, 3) }, { 3, Random.Range(1, 3) } };
        }
        else if (numberTask == 5)
        {
            number = 5;
            dataCarrot = new int[,] { { 2, Random.Range(1, 3) }, { 3, Random.Range(1, 3) }, { 4, Random.Range(1, 3) } };
        }
        else if(numberTask == 6)
        {
            number = 5;
            dataCarrot = new int[,] { { 1, Random.Range(1, 3) }, { 2, 3 }, { 4, Random.Range(1, 3) } };
        }
        else if (numberTask == 7)
        {
            number = 6;
            dataCarrot = new int[,] { { Random.Range(1, 3), Random.Range(1, 4) }, { Random.Range(3, 5), Random.Range(1, 3) }, { 5, Random.Range(2, 4) } };
        }
        else if (numberTask == 8)
        {
            number = 7;
            dataCarrot = new int[,] { { Random.Range(1, 3), Random.Range(1, 4) }, { Random.Range(3, 5), Random.Range(2, 4) }, { Random.Range(5, 7), Random.Range(1, 4) } };
        }
        createMounds(number, dataCarrot);
        createBackgroundCirclesPanel(number, dataCarrot);
        createCarrotPanel(dataCarrot);
        createCardPanel();
    }

    public int[,] getDataCarrot()
    {
        return dataCarrot;
    }

    int getNumberCarrot(int[,] dataCarrot)
    {
        int number = 0;
        for (int i = 0; i < dataCarrot.GetLength(0); i++)
        {
            number += dataCarrot[i, 1];
        }
        return number;
    }

    GameObject getPanelGame()
    {
        return GameObject.Find("PanelGame");
    }

    void createBackgroundCirclesPanel(int numberCard, int[,] dataCarrot)
    {
        GameObject parentOfCirclesPanel = getPanelGame();
        int numberCarrots = getNumberCarrot(dataCarrot);

        GameObject newBackgroundCirclesPanel = new GameObject();
        newBackgroundCirclesPanel.name = "CirclesPanel";
        newBackgroundCirclesPanel.AddComponent<Image>();
        newBackgroundCirclesPanel.GetComponent<Image>().sprite = backgroundCirclesPanel;
        newBackgroundCirclesPanel.transform.position = new Vector3(0, 195, 0);

        RectTransform circlesPanelRectTransform = newBackgroundCirclesPanel.GetComponent<RectTransform>();
        int width = (numberCard + numberCarrots) * 110 + 50;
        circlesPanelRectTransform.sizeDelta = new Vector2(width , 168);

        newBackgroundCirclesPanel.AddComponent<GridLayoutGroup>();
        GridLayoutGroup componentCirclesPanel = newBackgroundCirclesPanel.GetComponent<GridLayoutGroup>();
        componentCirclesPanel.cellSize = new Vector2(100, 100);
        componentCirclesPanel.spacing = new Vector2(10, 0);
        componentCirclesPanel.childAlignment = TextAnchor.MiddleCenter;
        componentCirclesPanel.constraint = GridLayoutGroup.Constraint.FixedRowCount;
        componentCirclesPanel.constraintCount = 1;

        newBackgroundCirclesPanel.transform.SetParent(parentOfCirclesPanel.transform, false);

        createCirclesPanel(numberCard + numberCarrots, newBackgroundCirclesPanel.transform);
    }

    private void createCirclesPanel(int numberCard, Transform parent)
    {
        for (int i = 0; i < numberCard; i++)
        {
            GameObject newCirclePanel = new GameObject();
            newCirclePanel.name = "Circle" + i;
            newCirclePanel.transform.tag = "Slots";
            newCirclePanel.AddComponent<GridLayoutGroup>();
            newCirclePanel.GetComponent<GridLayoutGroup>().cellSize = new Vector2(100, 100);
            newCirclePanel.GetComponent<GridLayoutGroup>().childAlignment = TextAnchor.MiddleCenter;
            newCirclePanel.AddComponent<DropFunction>();
            newCirclePanel.AddComponent<Image>();
            newCirclePanel.GetComponent<Image>().sprite = imageCircle;

            newCirclePanel.transform.SetParent(parent, false);
        }
    }

    private void createMounds(int numberMound, int[,] dataCarrot)
    {
        GameObject parentOfPanel = getPanelGame();

        GameObject MoundPanel = new GameObject();
        MoundPanel.name = "MoundPanel";
        MoundPanel.AddComponent<Image>();
        MoundPanel.GetComponent<Image>().color = new Color32(255, 255, 225, 0);
        MoundPanel.transform.position = new Vector3(526, -213, 0);

        RectTransform moundPanelRectTransform = MoundPanel.GetComponent<RectTransform>();
        int width = numberMound * 200;
        moundPanelRectTransform.sizeDelta = new Vector2(width, 268);
        moundPanelRectTransform.pivot = new Vector2(1, 1);

        MoundPanel.AddComponent<GridLayoutGroup>();
        GridLayoutGroup componentMoundPanel = MoundPanel.GetComponent<GridLayoutGroup>();
        componentMoundPanel.cellSize = new Vector2(180, 130);
        componentMoundPanel.spacing = new Vector2(20, 0);
        componentMoundPanel.childAlignment = TextAnchor.MiddleRight;
        componentMoundPanel.constraint = GridLayoutGroup.Constraint.FixedRowCount;
        componentMoundPanel.constraintCount = 1;

        MoundPanel.transform.SetParent(parentOfPanel.transform, false);

        createMound(numberMound, MoundPanel.transform);
        createTractor(width);

    }

    void createTractor(int width)
    { 
        GameObject Tractor = new GameObject();
        Tractor.name = "Tractor";
        Tractor.AddComponent<Image>();
        Tractor.GetComponent<Image>().sprite = imageTractor;
        RectTransform TractorRectTransform = Tractor.GetComponent<RectTransform>();
        TractorRectTransform.sizeDelta = new Vector2(266, 262);
        Tractor.transform.position = new Vector3(526 - width + 100, -177, 0);
        Tractor.transform.SetParent(getPanelGame().transform, false);
    }

    private void createMound(int numberMound, Transform parent)
    {
        for (int i = 0; i < numberMound; i++)
        {
            GameObject newMound = new GameObject();
            newMound.name = "Mound" + i;
            newMound.AddComponent<Image>();
            newMound.GetComponent<Image>().sprite = imageMound;
            newMound.transform.SetParent(parent, false);
        }
    }

    private void createCarrotPanel(int[,] dataCarrot)
    {
        GameObject parentOfPanel = getPanelGame();

        GameObject CarrotPanel = new GameObject();
        CarrotPanel.name = "CarrotPanel";
        CarrotPanel.AddComponent<Image>();
        CarrotPanel.GetComponent<Image>().color = new Color32(255, 255, 225, 0);
        CarrotPanel.transform.position = new Vector3(-216, -232, 0);

        RectTransform CarrotPanelRectTransform = CarrotPanel.GetComponent<RectTransform>();
        CarrotPanelRectTransform.sizeDelta = new Vector2(1486, 220);

        CarrotPanel.AddComponent<GridLayoutGroup>();
        GridLayoutGroup componentCarrotPanel = CarrotPanel.GetComponent<GridLayoutGroup>();
        componentCarrotPanel.cellSize = new Vector2(170, 150);
        componentCarrotPanel.spacing = new Vector2(20, 0);
        componentCarrotPanel.childAlignment = TextAnchor.MiddleRight;
        componentCarrotPanel.constraint = GridLayoutGroup.Constraint.FixedRowCount;
        componentCarrotPanel.constraintCount = 1;

        CarrotPanel.transform.SetParent(parentOfPanel.transform, false);

        createCarrot(number, dataCarrot, CarrotPanel.transform);
    }

    private void createCarrot(int number, int[,] dataCarrot, Transform parent)
    {
        for (int i = 0; i < number; i++)
        {
            GameObject childCarrotPanel = new GameObject();
            childCarrotPanel.name = "childCarrotPanel"+i;
            childCarrotPanel.AddComponent<Image>();
            childCarrotPanel.GetComponent<Image>().color = new Color32(255, 255, 225, 0);
            childCarrotPanel.transform.SetParent(parent,false);

            for (int j = 0; j < dataCarrot.GetLength(0); j++)
            {
                if (dataCarrot[j, 0] == i)
                {
                    for (int k = 0; k < dataCarrot[j,1]; k++)
                    {
                        GameObject Carrot = new GameObject();
                        Carrot.name = "Carrot" + k;
                        Carrot.AddComponent<Image>();
                        Carrot.GetComponent<Image>().sprite = imageCarrot;
                        Carrot.transform.position = new Vector3(Carrot.transform.position.x + k*20, Carrot.transform.position.y, 0);
                        Carrot.transform.SetParent(childCarrotPanel.transform, false);
                    }
                }
            }
        }

    } 
    private void createCardPanel()
    {
        GameObject parentOfCardPanel = getPanelGame();

        GameObject newCardsPanel = new GameObject();
        newCardsPanel.name = "CardPanel";
        newCardsPanel.AddComponent<Image>();
        newCardsPanel.GetComponent<Image>().color = new Color32(255, 255, 225, 0);
        newCardsPanel.transform.position = new Vector3(-321, 418.5f, 0);

        RectTransform cardPanelRectTransform = newCardsPanel.GetComponent<RectTransform>();
        cardPanelRectTransform.sizeDelta = new Vector2(373, 182);

        newCardsPanel.AddComponent<GridLayoutGroup>();
        GridLayoutGroup componentCardPanel = newCardsPanel.GetComponent<GridLayoutGroup>();
        componentCardPanel.cellSize = new Vector2(100, 100);
        componentCardPanel.spacing = new Vector2(10, 0);
        componentCardPanel.childAlignment = TextAnchor.MiddleCenter;
        componentCardPanel.constraint = GridLayoutGroup.Constraint.FixedRowCount;
        componentCardPanel.constraintCount = 1;

        newCardsPanel.transform.SetParent(parentOfCardPanel.transform, false);

        //vytvorenie prikazov
        string[] nameCardsPanel = { "CardCarrotPanel", "CardArrowPanel" };
        string[] tag = { "carrot", "arrow" };

        for (int i = 0; i < 2; i++)
        {
            GameObject newCard = new GameObject();
            newCard.name = nameCardsPanel[i];
            newCard.AddComponent<GridLayoutGroup>();
            newCard.GetComponent<GridLayoutGroup>().cellSize = new Vector2(100, 100);
            newCard.GetComponent<GridLayoutGroup>().childAlignment = TextAnchor.MiddleCenter;
            newCard.AddComponent<Image>();
            newCard.GetComponent<Image>().sprite = cards[i];

            cardsPrefab[i].transform.tag = tag[i];
            cardsPrefab[i].transform.SetParent(newCard.transform, false);

            newCard.transform.SetParent(newCardsPanel.transform, false);
        }

    }

    public void check()
    {
        if (!checkAllCircles())
        {
            InfoEmptyCirclesPanel.SetActive(true);
            StartCoroutine(Countdown(InfoEmptyCirclesPanel));
        }
        else
        {
            checkSolution.check();
        } 
    }

    public void checkIsNextTask(int n)
    {
        if (n == 0)
        {
            createTractor(number * 200);
            createCarrotPanel(dataCarrot);
        }
        else
        {
            if (numberTask > 8)
            {
                finalGameMessage.SetActive(true);
            }
            else
            {
                numberTask++;
                startGame();
            }
        }
    }

    public void gameAgain()
    {
        finalGameMessage.SetActive(false);
        numberTask = 1;
        startGame();
    }

    public void getMenu()
    {
        finalGameMessage.SetActive(false);
        numberTask = 1;
    }

    bool checkAllCircles()
    {
        Transform parent = GameObject.Find("CirclesPanel").transform;
        for (int i = 0; i < parent.childCount; i++)
        {
            if (parent.GetChild(i).childCount == 0)
                return false;
        }
        return true;
    }

    private IEnumerator Countdown(GameObject go)
    {
        yield return new WaitForSeconds(2);
        go.SetActive(false);
    }

    public void deleteCardInCirclesPanel()
    {
        Transform circlesPanel = GameObject.Find("CirclesPanel").transform;
        for (int i = 0; i < circlesPanel.childCount; i++)
        {
            if (circlesPanel.GetChild(i).transform.childCount > 0)
                Destroy(circlesPanel.GetChild(i).transform.GetChild(0).gameObject);
        }
    }

    public GameObject getFullCardMessages()
    {
        return FullCardMessage;
    }

    public void setActiveInfoPanel()
    {
        if (infoMessage.active)
        {
            infoMessage.SetActive(false);
        }
        else
        {
            infoMessage.SetActive(true);
        }
    }
 

  







}
