using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class clickControl : MonoBehaviour
{
    public static string girilenDeger;
    static bool ilkTiklama;
    bool aktif = true;
    public static bool kontrol;

    TEXDraw3D textMesh;
    Text wordTxt;

    GameManager gameManager;

    

   

    private void Start()
    {
      

        //dwordTxt = GameObject.Find("wordTxt").GetComponent<Text>();
        gameManager = GameObject.Find("Main Camera").GetComponent<GameManager>();



        transform.GetChild(0).gameObject.SetActive(false);

    }
    void Update()
    {

        if (!ilkTiklama)
        {
            textMesh = this.GetComponent<TEXDraw3D>();
            textMesh.color = Color.black;
            aktif = true;
        }

    }

    void tiklandi()
    {

        textMesh = this.GetComponent<TEXDraw3D>();
        textMesh.color = Color.yellow;

        girilenDeger += this.GetComponent<TEXDraw3D>().text;
        //wordTxt.text = girilenDeger;
        aktif = false;

        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(242, 211, 136);
    }

    private void OnMouseDown()
    {
        if (!ilkTiklama)
        {
            tiklandi();

            ilkTiklama = true;
            gameManager.SetFirstPos(gameObject.transform);
            gameManager.tik = true;
        }
    }

    private void OnMouseEnter()
    {
        if (ilkTiklama && aktif)
        {
            tiklandi();
            gameManager.SetLinePos(gameObject);
        }
    }

    private void OnMouseUp()
    {
        ilkTiklama = false;
        kontrol = true;
        gameManager.tik = false;
        gameManager.RemoveDots();
        
        gameManager.CorrectAnswer(girilenDeger);
       // gameObject.transform.GetChild(0).gameObject.SetActive(false);
        //wordTxt.text = "";
        Debug.Log("son:" + girilenDeger);
        
    }

    
}
