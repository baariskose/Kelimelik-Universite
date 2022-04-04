using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    SquareController squareController;

    [HideInInspector] public string kelime;
    public GameObject lessonPanel, subjectPanel,panelBg,panelBgSub,correctAnswerPanel,menuPanel;
    public GameObject  endQuestionPanel;
    public Button mute;
    public List<Sprite> mutesImg;
    public List<GameObject> dots; // dot pool
    public List<GameObject> chosenDots;
    public ScrollRect myScrollRect;
    public ScrollRect myScrollRectSub;
    public bool tik = false;
    private LineRenderer line;
    private Vector3 mousePos;
    public Material material;
    private LineRenderer activeLine;
    private List<LineRenderer> ll;
    private Transform firstPosGO;
    DotController dotController;
    FirebaseManager firebaseManager;
    InterstitialAds gecisReklam;
    public static string trueAnswer = "";
    public static int adCounter=0;
    public AudioSource audioSource;
    public AudioClip click, correctAnswer;
    bool isMusicOn;
    int isMusicOnOff=0;
    // Start is called before the first frame update
    void Start()
    {

        
        menuPanel.SetActive(true);
        isMusicOnOff = PlayerPrefs.GetInt("music");
        Debug.Log(isMusicOnOff);
        if (isMusicOnOff % 2 == 0)
        {
            gameObject.GetComponent<AudioSource>().volume = 1;
            mute.GetComponent<Image>().sprite = mutesImg[0];
        }
        else
        {
            gameObject.GetComponent<AudioSource>().volume = 0;
            mute.GetComponent<Image>().sprite = mutesImg[1];
        }
        audioSource = gameObject.GetComponent<AudioSource>();
        myScrollRect.verticalNormalizedPosition = 0.5518112f;
        ll = new List<LineRenderer>();
        GameObject go = GameObject.Find("Scripts");
        
        dotController = go.GetComponent<DotController>();
        squareController = go.GetComponent<SquareController>();
        firebaseManager= GameObject.Find("Main Camera").GetComponent<FirebaseManager>();
        gecisReklam = go.GetComponent<InterstitialAds>();
    }

    // Update is called once per frame
    void Update()
    {
        
        //Debug.Log("1 "+myScrollRect.verticalNormalizedPosition);
        //Debug.Log("2 "+myScrollRectSub.verticalNormalizedPosition);
        if (Input.GetMouseButtonDown(0))
        {

            if (!tik) return;
            if (line == null)
            {
                createLine();

            }
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            activeLine.SetPosition(0, firstPosGO.position);
            activeLine.SetPosition(1, firstPosGO.position);
        }
        else if (Input.GetMouseButtonUp(0) && line)
        {
            Destroy(activeLine.gameObject);
            RemoveDots();
            activeLine = null;
        }
        else if (Input.GetMouseButton(0) && activeLine)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            activeLine.SetPosition(1, mousePos);
        }

        if(menuPanel.gameObject.activeInHierarchy || subjectPanel.gameObject.activeInHierarchy || lessonPanel.activeInHierarchy)
        {
            squareController.SquareContent1.SetActive(false);
            squareController.SquareContent2.SetActive(false);
        }
        else
        {
            squareController.SquareContent1.SetActive(true);
            squareController.SquareContent2.SetActive(true);
        }
    }
    public void RemoveDots()
    {
        int k = 0;
        foreach (var dot in dots)
        {
            dot.transform.GetChild(0).gameObject.SetActive(false);
            k++;
        }

        for (int i = ll.Count - 1; i >= 0; i--)
        {
            Destroy(ll[0].gameObject);
            ll.RemoveAt(0);

        }

    }
    public void SetFirstPos(Transform g)
    {
        firstPosGO = g;
    }
    public void SetLinePos(GameObject g)
    {
        createLine();
        activeLine.SetPosition(0, g.transform.position);
        activeLine.SetPosition(1, g.transform.position);
    }
    private void createLine()
    {
      
        line = new GameObject("Line").AddComponent<LineRenderer>();
        line.material = material;
        line.startWidth = 0.5f;
        line.endWidth = 0.5f;
        line.alignment = LineAlignment.TransformZ;

        line.numCapVertices = 30;

        activeLine = line;
        ll.Add(activeLine);
    }

    public void OnLessonPanel()
    {
        lessonPanel.SetActive(true);
        myScrollRect.verticalNormalizedPosition = 0.5518112f;
    }
    public void OffLessonPanel()
    {
        lessonPanel.SetActive(false);
        myScrollRect.verticalNormalizedPosition = 0.5518112f;
     
    }
    public void OnSubjectPanel()
    {
        subjectPanel.SetActive(true);
        panelBgSub.SetActive(true);
        panelBg.SetActive(false);
        myScrollRectSub.verticalNormalizedPosition = 0.4864304f;
    }
    public void OffSubjectPanel()
    {
        subjectPanel.SetActive(false);
        panelBgSub.SetActive(false);
        myScrollRectSub.verticalNormalizedPosition = 0.4864304f;
    }
    public void GoLessonPanel()
    {
        subjectPanel.SetActive(false);
        lessonPanel.SetActive(true);
        myScrollRect.verticalNormalizedPosition = 0.5518112f;
        panelBgSub.SetActive(false);
        panelBg.SetActive(true);
        menuPanel.SetActive(false);
    
    }
    public void GoSubjectPanel()
    {
        //if (correctAnswerPanel.activeInHierarchy == true)
        //{
        //    NextQuestion();
        //}
        subjectPanel.SetActive(true);
        panelBgSub.SetActive(true);
        dotController.DotsReset();
        correctAnswerPanel.SetActive(false);
        endQuestionPanel.SetActive(false);
        myScrollRectSub.verticalNormalizedPosition = 0.4864304f;
        Debug.Log("gelllloo");

        GameObject.Find("RawImage").GetComponent<RawImage>().enabled = false;
     

    }
    public void GoMenu()
    {
        OffLessonPanel();
        menuPanel.SetActive(true);

    }
    public void GoQuestionPanel()
    {
        subjectPanel.SetActive(false);
        lessonPanel.SetActive(false);
        panelBg.SetActive(false);
        audioSource.PlayOneShot(click);
    }
    public void CorrectAnswer(string answer)
    {
        Debug.Log("true" + trueAnswer);
        Debug.Log("answer" + answer);
        if(answer == trueAnswer)
        {
            //correctAnswerPanel.SetActive(true);
            Debug.Log("Doðru");
            
            foreach (var square in squareController.squares)
            {
                square.transform.GetChild(0).gameObject.SetActive(true);
            }
            correctAnswerPanel.SetActive(true);
            clickControl.girilenDeger = "";
            audioSource.PlayOneShot(correctAnswer);
        }
        else
        {
            Debug.Log("Yanlýþ");
            clickControl.girilenDeger = "";
        }
    }
    public void NextQuestion()
    {
        firebaseManager.NextQuestion();
        correctAnswerPanel.SetActive(false);
        dotController.DotsReset();
        squareController.totalLetterCount = 0;
        squareController.MakeSquareInvisible();
        adCounter = PlayerPrefs.GetInt("adCounter");
        PlayerPrefs.SetInt("adCounter", ++adCounter);
        if(adCounter >= 4)
        {
            Debug.Log("reklam");
            gecisReklam.ShowAd();
        }
        audioSource.PlayOneShot(click);
       
        Debug.Log("gelllloo");
        GameObject.Find("RawImage").GetComponent<RawImage>().enabled = false;
    }
    public void MusicOnOff()
    {
        isMusicOnOff++;
        PlayerPrefs.SetInt("music",isMusicOnOff);
        if (isMusicOnOff %2 == 0)
        {
            gameObject.GetComponent<AudioSource>().volume = 1;
            mute.GetComponent<Image>().sprite = mutesImg[0];
        }
        else
        {
            gameObject.GetComponent<AudioSource>().volume = 0;
            mute.GetComponent<Image>().sprite = mutesImg[1];
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
