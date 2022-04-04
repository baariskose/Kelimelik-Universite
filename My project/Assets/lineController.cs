using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineController : MonoBehaviour
{
    // Start is called before the first frame update
    private LineRenderer lr;
    private Transform[] points;
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //for (int i = 0; i < points.Length; i++)
        //{
        //    lr.SetPosition(i, points[i].position);
        //}
    }
    public void SetUpLine(Transform[] points)
    {
        lr.positionCount = points.Length;
        this.points = points;
       
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if("harf"== collision.gameObject.tag)
        {
            lr.SetPosition(2, collision.gameObject.transform.position);
        }
    }
    private void OnMouseDown()
    {
        //lr.SetPosition(0,gameObject.transform.position);
        
    }
    private void OnMouseDrag()
    {
       
        
    }



}
