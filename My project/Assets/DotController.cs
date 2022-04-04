using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotController : MonoBehaviour
{
    public List<GameObject> dots;
    public List<GameObject> dotsHolder;
   
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Main Camera").GetComponent<GameManager>();
    
        //gameManager.dots = new List<GameObject>();
        //gameManager.dots.AddRange(dots);
        dots = gameManager.chosenDots;
        //dotsHolder = gameManager.chosenDots;
        //DotsMakeInvisible();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void DotsMakeInvisible()
    {
        foreach (var dot in dots)
        {
            dot.SetActive(false);
        }
    }
    public void DotsReset()
    {
        foreach (var dot in gameManager.dots)
        {
            dot.transform.position = new Vector2(0,0);
            dot.SetActive(false);
        }
        gameObject.GetComponent<CreateLetter>().totalAngle = 0;
        //gameManager.chosenDots = new List<GameObject>();
        dotsHolder = new List<GameObject>();
    }
    public void WriteDotText(string letter)
    {
        GameObject chosen = ChooseDot();
        chosen.SetActive(true);
        Debug.Log("Write");
        chosen.GetComponent<TEXDraw3D>().text = letter;
    }
    public void WriteDotText2(string letter)
    {
        GameObject chosen = ChooseDot();
        chosen.SetActive(true);
        Debug.Log("Write");
      //  chosen.GetComponent<Tez>().text = letter;
    }
    public GameObject ChooseDot()
    {
        Debug.Log("Choose");
        Debug.Log("dotsholder count2   " + dotsHolder.Count );
        GameObject chosen = dotsHolder[Random.Range(0, dotsHolder.Count)];
        chosen.SetActive(true);
        dotsHolder.Remove(chosen);
        return chosen;
    }


}
