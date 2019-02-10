using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour {

    int numberTask; // level
    Sprite backgroundCirclesPanel;
    Sprite imageCircle;
    Sprite imageMound;
    Sprite imageTractor;
    Sprite imageCarrot;
    Sprite imageCucumber;
    Sprite imageTomato;
    Sprite[] cards;
    GameObject[] cardsPrefab;
    public GameObject InfoEmptyCirclesPanel;
    public GameObject FullCardMessage;
    public GameObject gameobj;
    public GameObject finalGameMessage;
    public Text textMessage;
    public Text textLevel;
    public Font font;
    Dictionary<int, int[,]> data;
    int number;
    int indexAnimatorPanel;
    CheckSolution checkSolution;
    string textTask;
    int[] priority;
    string[] result;
    string[] result2;

    void Start ()
    {
        if (PlayerPrefs.HasKey("numberTask"))
        {
            numberTask = PlayerPrefs.GetInt("numberTask");
        }
        else
            numberTask = 1;
        backgroundCirclesPanel = Resources.Load<Sprite>("backgroundForCircle");
        imageCircle = Resources.Load<Sprite>("circleForCard");
        imageMound = Resources.Load<Sprite>("mound");
        imageTractor = Resources.Load<Sprite>("tractor");
        imageCarrot = Resources.Load<Sprite>("carot");
        imageCucumber = Resources.Load<Sprite>("cucumber");
        imageTomato = Resources.Load<Sprite>("tomato");
        cards = new Sprite[]{
            Resources.Load<Sprite>("cardLeftArrow"),
            Resources.Load<Sprite>("cardArrow"),
            Resources.Load<Sprite>("cardCarrot"),
            Resources.Load<Sprite>("cardTomato"),
            Resources.Load<Sprite>("cardCucumber")
        };
        cardsPrefab = new GameObject[] {
            (GameObject)Instantiate(Resources.Load("Prefabs/CardLeftArrowPrefab") as GameObject),
            (GameObject)Instantiate(Resources.Load("Prefabs/CardArrowPrefab") as GameObject),
            (GameObject)Instantiate(Resources.Load("Prefabs/CardCarrotPrefab") as GameObject),
            (GameObject)Instantiate(Resources.Load("Prefabs/CardTomatoPrefab") as GameObject),
            (GameObject)Instantiate(Resources.Load("Prefabs/CardCucumberPrefab") as GameObject)
        };
        data = null;
        priority = null;
        result = null;
        result2 = null;
        checkSolution = gameobj.GetComponent<CheckSolution>();
        startGame();
    }
	

    public void startGame()
    {
        // 0 - carrot, 1 - tomato, 2 - cucumber
        // data = { 1 : { {position, number}, {position, number}, {position, number}, .. }, 2: {{}, ..}}
        data = new Dictionary<int, int[,]>();
        if (numberTask == 1)
        {
            number = 3; // number of mounds
            data.Add(0, new int[,] { { 1, 1 } });
            textTask = "Pozbieraj mrkvy a dostaň sa do domčeka.";
            result = new string[] {"arrow", "carrot", "arrow", "arrow"};
            priority = new int[] { 0 };
        }
        else if (numberTask == 2)
        {
            number = 3;
            data.Add(0, new int[,] { { 1, 1 }, { 2, 1 } });
            textTask = "Pozbieraj mrkvy a dostaň sa do domčeka.";
            result = new string[] { "arrow", "carrot", "arrow", "carrot", "arrow" };
            priority = new int[] { 0 };
        }
        else if (numberTask == 3)
        {
            number = 3;
            data.Add(0, new int[,] { { 1, 2 }});
            data.Add(1, new int[,] { { 2, 1 } });
            textTask = "Pozbieraj mrkvy, paradajky a dostaň sa do domčeka.";
            result = new string[] { "arrow", "carrot", "arrow", "tomato", "arrow" };
            priority = new int[] { 0, 1 };
        }
        else if (numberTask == 4)
        {
            number = 4;
            data.Add(0, new int[,] { { 1, 1 } });
            data.Add(1, new int[,] { { 2, 2 } });
            textTask = "Najprv pozbieraj paradajky, potom mrkvy a dostaň sa do domčeka.";
            result = new string[] { "arrow", "arrow", "tomato", "leftArrow" ,"carrot", "arrow", "arrow", "arrow" };
            priority = new int[] {1, 0};
        }
        else if (numberTask == 5)
        {
            number = 4;
            data.Add(1, new int[,] { { 1, 1 }, {3, 1} });
            data.Add(0, new int[,] { { 2, 1 } });
            textTask = "Najprv pozbieraj mrkvy, potom paradajky a dostaň sa do domčeka.";
            result = new string[] { "arrow", "arrow", "carrot", "leftArrow", "tomato", "arrow", "arrow", "tomato", "arrow" };
            priority = new int[] { 0, 1 };
        }
        else if(numberTask == 6)
        {
            number = 4;
            data.Add(0, new int[,] { { 1, 1 } });
            data.Add(1, new int[,] { { 2, 1 } });
            data.Add(2, new int[,] { { 3, 2 } });
            textTask = "Najprv pozbieraj mrkvy, potom paradajky a nakoniec uhorky a dostaň sa do domčeka.";
            result = new string[] { "arrow", "carrot", "arrow", "tomato", "arrow","cucumber", "arrow"};
            priority = new int[] { 0, 1, 2 };
        }
        else if (numberTask == 7)
        {
            number = 5;
            data.Add(0, new int[,] { { 3, 2 } });
            data.Add(1, new int[,] { { 1, 3 } });
            data.Add(2, new int[,] { { 2, 1 } });
            textTask = "Najprv pozbieraj uhorky, potom paradajky a nakoniec mrkvy a dostaň sa do domčeka.";
            result = new string[] { "arrow", "arrow", "cucumber", "leftArrow", "tomato", "arrow", "arrow", "carrot","arrow", "arrow" };
            priority = new int[] { 2, 1, 0 };
        }
        else if (numberTask == 8)
        {
            number = 5;
            data.Add(0, new int[,] { { 2, 3 } });
            data.Add(1, new int[,] { { 4, 2 } });
            data.Add(2, new int[,] { { 1, 1 }, { 3, 1 } });
            textTask = "Najprv pozbieraj mrkvy, potom paradajky a nakoniec uhorky a dostaň sa do domčeka.";
            result = new string[] { "arrow", "arrow", "carrot", "arrow", "arrow", "tomato", "leftArrow", "cucumber", "leftArrow",
                "leftArrow", "cucumber", "arrow", "arrow", "arrow", "arrow" };
            result2 = new string[] { "arrow", "arrow", "carrot", "arrow", "arrow", "tomato", "leftArrow", "leftArrow",
                "leftArrow", "cucumber", "arrow", "arrow", "cucumber", "arrow", "arrow" };
            priority = new int[] { 0, 1, 2 };
        }
        textLevel.text = "Úroveň " + numberTask;
        textMessage.text = textTask;
        createMounds(number, data);
        createBackgroundCirclesPanel(data);
        createVegetablePanel(data);
        createCardPanel();
    }

    public string[] getResult()
    {
        return result;
    }

    public string[] getResult2()
    {
        return result2;
    }

    GameObject getPanelGame()
    {
        return GameObject.Find("PanelGame");
    }

    public int[] getPriority()
    {
        return priority;
    }

    void createBackgroundCirclesPanel(Dictionary<int, int[,]> data)
    {
        GameObject parentOfCirclesPanel = getPanelGame();
        int numberVegetablesMound = result.Length;

        GameObject newBackgroundCirclesPanel = new GameObject();
        newBackgroundCirclesPanel.name = "CirclesPanel";
        newBackgroundCirclesPanel.AddComponent<Image>();
        newBackgroundCirclesPanel.GetComponent<Image>().sprite = backgroundCirclesPanel;
        newBackgroundCirclesPanel.transform.position = new Vector3(0, 195, 0);

        RectTransform circlesPanelRectTransform = newBackgroundCirclesPanel.GetComponent<RectTransform>();
        int width = (numberVegetablesMound) * 110 + 50;
        circlesPanelRectTransform.sizeDelta = new Vector2(width , 168);

        newBackgroundCirclesPanel.AddComponent<GridLayoutGroup>();
        GridLayoutGroup componentCirclesPanel = newBackgroundCirclesPanel.GetComponent<GridLayoutGroup>();
        componentCirclesPanel.cellSize = new Vector2(100, 100);
        componentCirclesPanel.spacing = new Vector2(10, 0);
        componentCirclesPanel.childAlignment = TextAnchor.MiddleCenter;
        componentCirclesPanel.constraint = GridLayoutGroup.Constraint.FixedRowCount;
        componentCirclesPanel.constraintCount = 1;

        newBackgroundCirclesPanel.transform.SetParent(parentOfCirclesPanel.transform, false);

        createCirclesPanel(numberVegetablesMound, newBackgroundCirclesPanel.transform);
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

    private void createMounds(int numberMound, Dictionary<int, int[,]> dataCarrot)
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

    private void createVegetablePanel(Dictionary<int, int[,]> data)
    {
        GameObject parentOfPanel = getPanelGame();

        GameObject VegetablePanel = new GameObject();
        VegetablePanel.name = "VegetablePanel";
        VegetablePanel.AddComponent<Image>();
        VegetablePanel.GetComponent<Image>().color = new Color32(255, 255, 225, 0);
        VegetablePanel.transform.position = new Vector3(-216, -270, 0);

        RectTransform VegetablePanelRectTransform = VegetablePanel.GetComponent<RectTransform>();
        VegetablePanelRectTransform.sizeDelta = new Vector2(1486, 220);

        VegetablePanel.AddComponent<GridLayoutGroup>();
        GridLayoutGroup componentVegetablePanel = VegetablePanel.GetComponent<GridLayoutGroup>();
        componentVegetablePanel.cellSize = new Vector2(170, 150);
        componentVegetablePanel.spacing = new Vector2(20, 0);
        componentVegetablePanel.childAlignment = TextAnchor.MiddleRight;
        componentVegetablePanel.constraint = GridLayoutGroup.Constraint.FixedRowCount;
        componentVegetablePanel.constraintCount = 1;

        VegetablePanel.transform.SetParent(parentOfPanel.transform, false);

        createVegetable(number, data, VegetablePanel.transform);
    }

    private void createVegetable(int number, Dictionary<int, int[,]> data, Transform parent)
    {
        for (int i = 0; i < number; i++)
        {
            GameObject childVegetablePanel = new GameObject();
            childVegetablePanel.name = "childVegetablePanel"+i;
            childVegetablePanel.AddComponent<Image>();
            childVegetablePanel.GetComponent<Image>().color = new Color32(255, 255, 225, 0);
            childVegetablePanel.transform.SetParent(parent,false);
            foreach (var item in data)
            {
                int j = 0;
                while (j < item.Value.GetLength(0))
                {
                    if (item.Value[j, 0] == i)
                    {
                        for (int k = 0; k < item.Value[j, 1]; k++)
                        {
                            GameObject Vegetable = new GameObject();
                            if (item.Key == 0)
                            {
                                Vegetable.name = "Carrot" + k;
                                Vegetable.transform.tag = "carrot";
                                Vegetable.AddComponent<Image>();
                                Vegetable.GetComponent<Image>().sprite = imageCarrot;
                            }
                            else if (item.Key == 1)
                            {
                                Vegetable.name = "Tomato" + k;
                                Vegetable.transform.tag = "tomato";
                                Vegetable.AddComponent<Image>();
                                Vegetable.GetComponent<Image>().sprite = imageTomato;
                            }
                            else
                            {
                                Vegetable.name = "Cucumber" + k;
                                Vegetable.transform.tag = "cucumber";
                                Vegetable.AddComponent<Image>();
                                Vegetable.GetComponent<Image>().sprite = imageCucumber;
                            }
                            Vegetable.transform.position = new Vector3(Vegetable.transform.position.x + k * 20, Vegetable.transform.position.y, 0);
                            Vegetable.transform.SetParent(childVegetablePanel.transform, false);
                        }
                    }
                    j++;
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
        newCardsPanel.transform.position = new Vector3(0, 418.5f, 0);

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
        string[] nameCardsPanel = { "CardLeftArrowPanel", "CardArrowPanel", "CardCarrotPanel", "CardTomatoPanel", "CardCucumberPanel" };
        string[] tag = { "leftArrow", "arrow", "carrot", "tomato", "cucumber" };
        int count = numberTask < 3 ? 3 : numberTask < 6 ? 4 : 5;
        for (int i = 0; i < count; i++)
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
            createVegetablePanel(data);
        }
        else
        {
            if (numberTask >= 8)
            {
                finalGameMessage.SetActive(true);
            }
            else
            {
                numberTask++;
                PlayerPrefs.SetInt("numberTask", numberTask);
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
}
