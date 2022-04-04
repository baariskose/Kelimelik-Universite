using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public GameObject buttonPrefab;
    public GameObject panelToAttachButtonsTo;
    public GameObject subjectPanelToAttachButtonsTo;

    GameManager gameManager;
    CreateLetter createLetter;
    SquareController squareController;
    ButtonController buttonController;
    GameObject button;
    FirebaseManager firebaseManager;
    List<GameObject> buttons;
    List<GameObject> subjectButtons;
    void Start()//Creates a button and sets it up
    {
       
        GameObject go = GameObject.Find("Scripts");
        GameObject go2 = GameObject.Find("Main Camera");
        
        createLetter = go.GetComponent<CreateLetter>();
        buttons = new List<GameObject>();
        subjectButtons = new List<GameObject>();
        firebaseManager = go2.GetComponent<FirebaseManager>();
        gameManager = go2.GetComponent<GameManager>();
        squareController = go.GetComponent<SquareController>();
        buttonController = go.GetComponent<ButtonController>();
    }
    private void Update()
    {

    }
    public void CreateButton(string buttonName, int missionId)
    {

        button = (GameObject)Instantiate(buttonPrefab);
        button.transform.GetChild(0).name = buttonName;
        button.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = buttonName;
        if (missionId == 1)
        {
            button.transform.SetParent(panelToAttachButtonsTo.transform);//Setting button parent
            button.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(OnClick);//Setting what button does when clicked
                                                                                             //Next line assumes button has child with text as first gameobject like button created from GameObject->UI->Button
                                                                                             //button.transform.GetChild(0).GetComponent<Text>().text = "This is button text";//Changing text
            buttons.Add(button);
        }
        else
        {
            button.transform.SetParent(subjectPanelToAttachButtonsTo.transform);//Setting button parent
            button.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(OnClickSubject);//Setting what button does when clicked
            subjectButtons.Add(button);                                                                                   //Next line assumes button has child with text as first gameobject like button created from GameObject->UI->Button
                                                                                                                          //button.transform.GetChild(0).GetComponent<Text>().text = "This is button text";//Changing text
        }

    }
    void OnClick()
    {
        foreach (var subject in subjectButtons)
        {
            Destroy(subject);
        }
        //Debug.Log("clicked!" + EventSystem.current.currentSelectedGameObject.name);
        firebaseManager.currentLesson = EventSystem.current.currentSelectedGameObject.name;
        firebaseManager.FillSubjectButtons();
        gameManager.OffLessonPanel();
        gameManager.OnSubjectPanel();
        gameManager.audioSource.PlayOneShot(gameManager.click);


    }
    void OnClickSubject()
    {
     
        //Debug.Log("clicked!" + EventSystem.current.currentSelectedGameObject.name);
        firebaseManager.currentSubject = EventSystem.current.currentSelectedGameObject.name;
        firebaseManager.GetData();
        squareController.totalLetterCount = 0;
        squareController.MakeSquareInvisible();
        gameManager.OffSubjectPanel();
        gameManager.audioSource.PlayOneShot(gameManager.click);


    }
}
