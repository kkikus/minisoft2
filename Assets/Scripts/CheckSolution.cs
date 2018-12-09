using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckSolution : MonoBehaviour {

    int[,] dataCarrot;
    int indexCheckedCard;
    public GameObject gameobj;
    public GameObject UnncorrectSolution;
    public GameObject CorrectSolution;
    Game game;
    bool startAnimate;
    int numberCircles;
    Transform circlesPanel;
    int index;
    bool isCorrect;
    GameObject Tractor;
    float time;
    int frameTractor;
    int krok;
    List<int> resolution;
    int tractorPos;


    // Use this for initialization
    void Start ()
    {
        startAnimate = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (startAnimate)
        {
            if (index < numberCircles)
            {
                circlesPanel.GetChild(index).transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color32(115, 180, 121, 225);
                Transform prefabCard = circlesPanel.GetChild(index).transform.GetChild(0);

                if ((prefabCard.tag == "arrow" && resolution[index] == 1) || (prefabCard.tag == "carrot" && resolution[index] == 0))
                    isCorrect = false;

                if (prefabCard.tag == "arrow")
                {
                    time -= Time.deltaTime;
                    if (time < 0)
                        {             
                        if (krok < frameTractor)
                        {
                            Tractor.transform.position = new Vector3(Tractor.transform.position.x + (getLengthX()), Tractor.transform.position.y,Tractor.transform.position.z);
                            krok++;
                        }
                        else
                        {
                            index++;
                            tractorPos++;
                            krok = 0;
                        }
                        time = 0.5f;
                    }

                }
                else if(prefabCard.tag == "carrot")
                {
                    time -= Time.deltaTime;
                    if (time < 0)
                    {
                        if (GameObject.Find("CarrotPanel").transform.GetChild(tractorPos).transform.childCount > 0)
                        {
                            Destroy(GameObject.Find("CarrotPanel").transform.GetChild(tractorPos).transform.GetChild(0).gameObject);
                        }
                        index++;
                        time = 0.5f;
                    }
                }
            }
            else
            {
                startAnimate = false;
                checkAndShowMessage();
            }
        }
      
    }

    public void check()
    {
        if (!startAnimate)
        {
            game = gameobj.GetComponent<Game>();
            dataCarrot = game.getDataCarrot();
            circlesPanel = findCirclesPanel();
            Tractor = getTractor();
            numberCircles = circlesPanel.childCount;
            index = 0;
            isCorrect = true;
            time = 0.5f;
            krok = 0;
            frameTractor = 2;
            tractorPos = 0;
            resolution = getResolution();
            startAnimate = true;
        }
    }

    List<int> getResolution()
    {
        bool isThereCarrot = false;
        List<int> arr = new List<int>();
        for (int i = 0; i < numberCircles; i++)
        {
            for (int j = 0; j < dataCarrot.GetLength(0); j++)
            {
                if (dataCarrot[j, 0] == i)
                {
                    for (int k = 0; k < dataCarrot[j, 1]; k++)
                    {
                        arr.Add(1);
                    }
                    arr.Add(0);
                    isThereCarrot = true;
                }
            }
            if (!isThereCarrot) {
                arr.Add(0);
            }
            isThereCarrot = false;
            if (arr.Count == numberCircles)
                return arr;
        }
        return arr;
    }

    void checkAndShowMessage()
    {
        resolution.Clear();
        if (!isCorrect)
        {
            UnncorrectSolution.SetActive(true);
            StartCoroutine(Countdown(UnncorrectSolution));
        }
        else
        {
            CorrectSolution.SetActive(true);
            setDefaultColor();
        }
    }

    public void againSameTask()
    {
        CorrectSolution.SetActive(false);
        deleteCardPanel();
        deleteTractor();
        deleteCirclesPanel();
        deleteMoundPanel();
        deleteCarrotPanel();
        game.startGame();
    }

    public void nextTask()
    {
        CorrectSolution.SetActive(false);
        deleteCardPanel();
        deleteTractor();
        deleteCirclesPanel();
        deleteMoundPanel();
        deleteCarrotPanel();
        game.checkIsNextTask(1);
    }

    float getLengthX()
    {
        float lengthX = (GameObject.Find("MoundPanel").transform.GetChild(1).transform.position.x / 2) - (GameObject.Find("MoundPanel").transform.GetChild(0).transform.position.x / 2);
        return lengthX;
    }

    GameObject getTractor()
    {
        return GameObject.Find("Tractor");
    }

    private IEnumerator Countdown(GameObject go)
    {
        yield return new WaitForSeconds(2);
        go.SetActive(false);
        setDefaultColor();
        deleteTractor();
        deleteCarrotPanel();
        game.checkIsNextTask(0);
    }

    void setDefaultColor()
    {
        for(int i = 0; i < numberCircles; i++)
            circlesPanel.GetChild(i).transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 225);
    }

    static void deleteCardPanel()
    {
        Destroy(GameObject.Find("CardPanel"));
    }

    static void deleteTractor()
    {
        Destroy(GameObject.Find("Tractor"));
    }

    static void deleteCirclesPanel()
    {
        Destroy(GameObject.Find("CirclesPanel"));
    }

    static void deleteMoundPanel()
    {
        Destroy(GameObject.Find("MoundPanel"));
    }

    static void deleteCarrotPanel()
    {
        Destroy(GameObject.Find("CarrotPanel"));
    }

    static Transform findCirclesPanel()
    {
        return GameObject.Find("CirclesPanel").transform;
    }

}
