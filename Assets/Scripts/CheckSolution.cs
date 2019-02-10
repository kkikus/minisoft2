using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckSolution : MonoBehaviour {

    string[] result;
    string[] result2;
    int[] priority; //ani prioriut a ani data dictionary uy netreba
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
    List<int> resolution; //toto uz netreba
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
                if ((prefabCard.tag != result[index] && result2 == null) || (prefabCard.tag != result[index] && result2 != null && prefabCard.tag != result2[index]))
                {
                    isCorrect = false;
                }

                if (prefabCard.tag == "arrow" || prefabCard.tag == "leftArrow")
                {
                    time -= Time.deltaTime;
                    if (time < 0)
                    {             
                        if (krok < frameTractor)
                        {
                            if(prefabCard.tag == "arrow")
                                Tractor.transform.position = new Vector3(Tractor.transform.position.x + (getLengthX()), Tractor.transform.position.y, Tractor.transform.position.z);
                            else
                                Tractor.transform.position = new Vector3(Tractor.transform.position.x - (getLengthX()), Tractor.transform.position.y, Tractor.transform.position.z);
                            krok++;
                        }
                        else
                        {
                            index++;
                            if (prefabCard.tag == "arrow")
                                tractorPos++;
                            else
                                tractorPos--;
                            krok = 0;
                        }
                        time = 0.5f;
                    }

                }
                else
                {
                    if (tractorPos > 0 && tractorPos < GameObject.Find("VegetablePanel").transform.childCount)
                    {
                        Transform vegetable = GameObject.Find("VegetablePanel").transform.GetChild(tractorPos).transform;
                        time -= Time.deltaTime;
                        if (time < 0)
                        {
                            if (vegetable.childCount > 0 && vegetable.GetChild(0).tag == prefabCard.tag)
                            {
                                for (int i = 0; i < vegetable.childCount; i++)
                                {
                                    Destroy(vegetable.GetChild(i).gameObject);
                                }
                            }
                            index++;
                            time = 0.5f;
                        }
                    }
                    else { index++; }   
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
            result = game.getResult();
            result2 = game.getResult2();
            priority = game.getPriority();
            circlesPanel = findCirclesPanel();
            Tractor = getTractor();
            numberCircles = circlesPanel.childCount;
            index = 0;
            isCorrect = true;
            time = 0.5f;
            krok = 0;
            frameTractor = 2;
            tractorPos = 0;
            startAnimate = true;
        }
    }

    void checkAndShowMessage()
    {
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
        deleteVegetablePanel();
        game.startGame();
    }

    public void nextTask()
    {
        CorrectSolution.SetActive(false);
        deleteCardPanel();
        deleteTractor();
        deleteCirclesPanel();
        deleteMoundPanel();
        deleteVegetablePanel();
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
        deleteVegetablePanel();
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

    static void deleteVegetablePanel()
    {
        Destroy(GameObject.Find("VegetablePanel"));
    }

    static Transform findCirclesPanel()
    {
        return GameObject.Find("CirclesPanel").transform;
    }

}
