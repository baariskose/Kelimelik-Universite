using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dots : MonoBehaviour
{
    LineRenderer lr;
    GameObject lineCarryObject;
    // Start is called before the first frame update
    void Start()
    {
        //lineCarryObject = GameObject.Find("LineRenderer");
        //lr = lineCarryObject.GetComponent<LineRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        //lineCarryObject.gameObject.transform.position = gameObject.transform.position;
        //lr.SetPosition(0,Camera.main.WorldToScreenPoint(gameObject.transform.position));
    }
    private void OnMouseDrag()
    {
        //var mousePos = Camera.main.WorldToScreenPoint(Input.mousePosition);
        //mousePos.z = 0;
        //lr.SetPosition(1, mousePos);
    }

}
