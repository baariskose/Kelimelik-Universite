using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SquareController : MonoBehaviour
{
    public List<GameObject> squares;
    public GameObject SquareContent1, SquareContent2;
    public GameObject square;
    public int letterCount =0;
    public float totalLetterCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        squares = new List<GameObject>();
        //squares.AddRange(GameObject.FindGameObjectsWithTag("square"));
        squares.Sort(CompareListByName);
        
      
    }
    public void MakeSquareInvisible()
    {
        Debug.Log("total" + totalLetterCount);
        foreach (var square in squares)
        {
            if(totalLetterCount > letterCount)
            {
                square.transform.GetChild(0).gameObject.SetActive(false);
            }
            else
            {
                square.SetActive(false);
            }
           
        }
        squares.RemoveRange(0, squares.Count);
    }
    private static int CompareListByName(GameObject i1, GameObject i2)
    {
        return i1.name.CompareTo(i2.name);
    }
    public void TextSquareCreate(string letter)
    {
        if (letterCount < 7)
        {
            GameObject squareClone = Instantiate(square, gameObject.transform.position, Quaternion.identity);
            squareClone.transform.SetParent(SquareContent1.transform);
            squareClone.transform.GetChild(0).gameObject.SetActive(false);
            squareClone.gameObject.transform.GetChild(0).gameObject.GetComponent<TEXDraw>().text = "\\caviar " + letter;
            squares.Add(squareClone);
        }
        else
        {
            GameObject squareClone = Instantiate(square, gameObject.transform.position, Quaternion.identity);
            squareClone.transform.SetParent(SquareContent2.transform);
            squareClone.transform.GetChild(0).gameObject.SetActive(false);
            squareClone.gameObject.transform.GetChild(0).gameObject.GetComponent<TEXDraw>().text = "\\caviar " + letter;
            squares.Add(squareClone);
        }

        //squares[letterCount].SetActive(true);
        //squares[letterCount].transform.GetChild(0).gameObject.SetActive(false);
        //squares[letterCount].gameObject.transform.GetChild(0).gameObject.GetComponent<TEXDraw3D>().text = "\\caviar "+letter;
        letterCount++;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
