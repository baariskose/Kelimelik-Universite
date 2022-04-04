using Firebase.Firestore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Firestore;
using Firebase.Extensions;
using System.Threading.Tasks;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Firebase.Storage;

public class FirebaseManager : MonoBehaviour
{
    FirebaseFirestore db;
    ButtonController buttonController;
    SquareController squareController;
    FirebaseStorageManager storageManager;
    GameManager gameManager;
    public GameObject TextA;
    public TextMeshProUGUI questionText;
    public TEXDraw questionText2;
    public RawImage rawImage;
    //Dictionary<string, object> city;
    public string currentLesson;
    public string currentSubject;
    public string currentQuestionText;
    public string currentQuestionAnswer;
    public string[] currentQuestionAnswerLetters;
    public string[] currentQuestionLetters;
    DotController dotController;
    CreateLetter createLetter;
    // Start is called before the first frame update
    int currentQuestionNumber = 1;
    public static List<string> lessonsName;
    public TEXDraw soru, cevap , metin;

    private void Awake()
    {
        lessonsName = new List<string>();
        db = FirebaseFirestore.DefaultInstance;
        GetMyCollections();
    }
    void Start()
    {
        //GetData2();
        GameObject go2 = GameObject.Find("RawImage");
        storageManager = go2.GetComponent<FirebaseStorageManager>();
        
        questionText = TextA.GetComponent<TextMeshProUGUI>();
        GameObject go = GameObject.Find("Scripts");
        dotController = go.GetComponent<DotController>();
        buttonController = go.GetComponent<ButtonController>();
        squareController = go.GetComponent<SquareController>();
        gameManager = GameObject.Find("Main Camera").GetComponent<GameManager>();
        createLetter = go.GetComponent<CreateLetter>();

        
        if (PlayerPrefs.GetInt("isNew") == 0)
        {

            Debug.Log("asda:" + PlayerPrefs.GetInt("isNew"));
            PlayerPrefs.SetInt("isNew", 1);
            RunOnlyOnceSetPlayerPrefs();
        }
        Debug.Log("asdapartnd:" + PlayerPrefs.GetInt("isNew"));
        //PlayerPrefs.SetInt("isNew", 1);

        Invoke("DersleriYazdir", 2);
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void GetSubjectName()
    {
        currentSubject = EventSystem.current.currentSelectedGameObject.name;
    }
    public void GetData()
    {
        currentQuestionNumber = PlayerPrefs.GetInt(currentSubject);
        Debug.Log("curr subject" + currentSubject);
        Debug.Log("curr question: Soru" + currentQuestionNumber);
        DocumentReference docRef = db.Collection(currentSubject).Document("Soru" + currentQuestionNumber.ToString());
        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            DocumentSnapshot snapshot = task.Result;
            if (snapshot.Exists)
            {
                Debug.Log(string.Format("Document data for {0} document:", snapshot.Id));
                Dictionary<string, object> city = snapshot.ToDictionary();
                foreach (KeyValuePair<string, object> pair in city)
                {
                    if (pair.Key == "SoruMetin")
                    {
                    
                        if(pair.Value.ToString() == "foto")
                        {
                            rawImage.enabled = true;
                            storageManager.GetPhoto(currentSubject + "Soru" + currentQuestionNumber);
                        }
                        else
                        {
                            rawImage.enabled = false;
                            currentQuestionText = "\\caviar "+ pair.Value.ToString();
                            questionText2.text = currentQuestionText;
                        }
                      
                    }
                    else if (pair.Key == "Soru")
                    {
                        currentQuestionLetters = pair.Value.ToString().Split(',');
                        createLetter.letterCount = currentQuestionLetters.Length;
                        createLetter.LocateDots();
                    }
                    else if (pair.Key == "Cevap")
                    {
                        currentQuestionAnswer = pair.Value.ToString();
                        currentQuestionAnswerLetters = currentQuestionAnswer.Split(' ');
                        squareController.totalLetterCount = currentQuestionAnswerLetters.Length;

                        currentQuestionAnswer = "";
                        squareController.SquareContent1.SetActive(true);
                        squareController.SquareContent2.SetActive(true);
                        foreach (var letter in currentQuestionAnswerLetters)
                        {
                            currentQuestionAnswer += letter;
                            squareController.TextSquareCreate(letter);
                        }
                        GameManager.trueAnswer = currentQuestionAnswer;
                        squareController.letterCount = 0;
                    }
                    Debug.Log(string.Format("{0}: {1}", pair.Key, pair.Value));
                }
            }
            else
            {
                Debug.Log(string.Format("Document {0} does not exist!", snapshot.Id));
                gameManager.endQuestionPanel.SetActive(true);
            }
        });

    }
    
    public void FillSubjectButtons()
    {

        DocumentReference docRef = db.Collection("Dersler").Document(currentLesson);
        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            DocumentSnapshot snapshot = task.Result;
            if (snapshot.Exists)
            {
                Debug.Log(string.Format("Document data for {0} document:", snapshot.Id));
                Dictionary<string, object> city = snapshot.ToDictionary();
                foreach (KeyValuePair<string, object> pair in city)
                {
                    Debug.Log(string.Format("{0}: {1}", pair.Key, pair.Value));
                    buttonController.CreateButton(pair.Value.ToString(), 2);
                }
            }
            else
            {
                Debug.Log(string.Format("Document {0} does not exist!", snapshot.Id));
            }
        });
    }
    public void NextQuestion()
    {
        currentQuestionNumber = PlayerPrefs.GetInt(currentSubject);
        //Debug.Log("currentNumber"+ currentQuestionNumber);
        //DocumentReference docRef = db.Collection(currentSubject).Document("Soru" + currentQuestionNumber);
        //docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        //{

        //    DocumentSnapshot snapshot = task.Result;

        //    if (snapshot.Exists)
        //    {
        //        Debug.Log("geldi if içi");
        //        Debug.Log(string.Format("Document data for {0} document:", snapshot.Id));
        //        Dictionary<string, object> city = snapshot.ToDictionary();
        //        foreach (KeyValuePair<string, object> pair in city)
        //        {
        //            Debug.Log(string.Format("{0}: {1}", pair.Key, pair.Value));
        //        }
        //    }
        //    else
        //    {
        //        Debug.Log(string.Format("Document {0} does not exist!", snapshot.Id));
        //    }
        //});

        PlayerPrefs.SetInt(currentSubject, ++currentQuestionNumber);
        GetData();
      
    }
    public void GetMyCollections()
    {
        Query allCitiesQuery = db.Collection("Dersler");

        allCitiesQuery.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            QuerySnapshot allCitiesQuerySnapshot = task.Result;


            foreach (DocumentSnapshot documentSnapshot in allCitiesQuerySnapshot.Documents)
            {

                // Debug.Log(string.Format("Document data for {0} document:", documentSnapshot.Id));
                lessonsName.Add(documentSnapshot.Id);
                buttonController.CreateButton(documentSnapshot.Id, 1);
                Dictionary<string, object> city = documentSnapshot.ToDictionary();
                foreach (KeyValuePair<string, object> pair in city)
                {
                    //Debug.Log(string.Format("{0}: {1}", pair.Key, pair.Value));
                    if (PlayerPrefs.GetInt(pair.Value.ToString()) == 0)
                    {
                        PlayerPrefs.SetInt(pair.Value.ToString(), 1);
                        Debug.Log("girddiiii------");
                    }
                }

                // Newline to separate entries
                // Debug.Log("");
            }
        });
    }
    public void RunOnlyOnceSetPlayerPrefs()
    {
        Query allCitiesQuery = db.Collection("Dersler");
        allCitiesQuery.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            QuerySnapshot allCitiesQuerySnapshot = task.Result;

            foreach (DocumentSnapshot documentSnapshot in allCitiesQuerySnapshot.Documents)
            {
                Debug.Log(string.Format("Document data for {0} document:", documentSnapshot.Id));
                Dictionary<string, object> city = documentSnapshot.ToDictionary();
                foreach (KeyValuePair<string, object> pair in city)
                {
                    Debug.Log(string.Format("{0}: {1}", pair.Key, pair.Value));
                    PlayerPrefs.SetInt(pair.Value.ToString(), 1);
                }

                // Newline to separate entries
                Debug.Log("");
            }
        });
    }
    public void GetDataByName(string collectionName, DocumentSnapshot documentSnapshot)
    {
        Dictionary<string, object> city = documentSnapshot.ToDictionary();
        foreach (KeyValuePair<string, object> pair in city)
        {
            Debug.Log(string.Format("{0}: {1}", pair.Key, pair.Value));
        }
    }
    public void DersleriYazdir()
    {
        foreach (var item in lessonsName)
        {
            //  Debug.Log("bura" + item);
        }

    }
}
