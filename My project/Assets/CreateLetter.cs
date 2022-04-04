using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreateLetter : MonoBehaviour
{
    public GameObject location, stick, letter;
    GameManager gameManager;
    float angle;
    float startAngle;
    public float totalAngle;
    public bool isClick = false;
    public float letterCount;
    DotController dotController;
    FirebaseManager firebaseManager;
    public TextMeshPro textMeshPro;
    private void Awake()
    {
       
    }
    // Start is called before the first frame update
    void Start()
    {
        dotController = GameObject.Find("Scripts").GetComponent<DotController>();
       
        gameManager = GameObject.Find("Main Camera").GetComponent<GameManager>();
        firebaseManager = GameObject.Find("Main Camera").GetComponent<FirebaseManager>();
        CreateLetterBegining();
        startAngle = 0;

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void LocateDots()
    {

        if (letterCount != 0)
        {
            angle = 360 / letterCount;

            if (totalAngle < 360)
            {
                gameManager.chosenDots = new List<GameObject>();
                for (int i = 0; i < letterCount; i++)
                {
                    gameManager.dots[i].transform.position = location.transform.position;
                    gameManager.dots[i].gameObject.SetActive(true);
                    gameManager.chosenDots.Add(gameManager.dots[i]);
                    stick.transform.Rotate(new Vector3(0, 0, stick.gameObject.transform.rotation.z + angle));
                    totalAngle += angle;
                }
                stick.gameObject.SetActive(false);
                dotController.dotsHolder = gameManager.chosenDots;
                foreach (var letter in firebaseManager.currentQuestionLetters)
                {
                    dotController.WriteDotText(letter);
                }
               


            }
            
        }
    }
    public void WaitforLetter()
    {
        
    }
    public void CreateLetterBegining()
    {
       
        for (int i = 0; i < 18; i++)
        {
            GameObject go = Instantiate(letter,new Vector2(0,0), Quaternion.identity);
            gameManager.dots.Add(go);
            go.SetActive(false);
        }
    }
}
