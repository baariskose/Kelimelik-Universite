using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSettings : MonoBehaviour
{
    RectTransform rectTransform;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
       
    }

    // Update is called once per frame
    void Update()
    {
        int child = gameObject.transform.childCount;

        if (child < 5)
        {
           // rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, 299.6827f); 
        }
        else if (child >= 5)
        {
           // rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, -299.6827f);
        }
        
    }
    
}
