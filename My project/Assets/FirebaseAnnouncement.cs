using Firebase;
using Firebase.Firestore;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirebaseAnnouncement : MonoBehaviour
{
    public GameObject content, textGO;
    FirebaseFirestore db;
    GameObject go;
    public Font myNewFont;
    // Start is called before the first frame update
    void Start()
    {
        db = FirebaseFirestore.DefaultInstance;
        GetAnnouncement();
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    public void GetAnnouncement()
    {
        DocumentReference docRef = db.Collection("Duyuru").Document("Duyuru");
        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            DocumentSnapshot snapshot = task.Result;
            if (snapshot.Exists)
            {
                Debug.Log(string.Format("Document data for {0} document:", snapshot.Id));
                Dictionary<string, object> city = snapshot.ToDictionary();
                foreach (KeyValuePair<string, object> pair in city)
                {
                    CreateText(pair.Value.ToString());
                    Debug.Log(string.Format("{0}: {1}", pair.Key, pair.Value));
                }
            }
            else
            {
                Debug.Log(string.Format("Document {0} does not exist!", snapshot.Id));
            }
        });

    }


    public void CreateText(string text)
    {
        go = Instantiate(textGO, content.transform.position, Quaternion.identity);
        go.transform.SetParent(gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform);
        go.GetComponent<RectTransform>().sizeDelta = new Vector2(890.4222f, 240.8582f);

        
        go.GetComponent<Text>().text = text;
        //if (content.gameObject.transform.childCount <= 6)
        //{
        //    content.GetComponent<RectTransform>().offsetMin = new Vector2(content.GetComponent<RectTransform>().offsetMin.x, -132.1884f);
        //    content.GetComponent<RectTransform>().offsetMax = new Vector2(content.GetComponent<RectTransform>().offsetMax.x, 3.051742e-05f);

        //}
        //else if (content.gameObject.transform.childCount > 7 && content.gameObject.transform.childCount < 10)
        //{
        //    content.GetComponent<RectTransform>().offsetMin = new Vector2(content.GetComponent<RectTransform>().offsetMin.x, -1499.31f);
        //    content.GetComponent<RectTransform>().offsetMax = new Vector2(content.GetComponent<RectTransform>().offsetMax.x, -6.103516e-05f);

        //}
        //else if (content.gameObject.transform.childCount >= 10 && content.gameObject.transform.childCount < 13)
        //{
        //    content.GetComponent<RectTransform>().offsetMin = new Vector2(content.GetComponent<RectTransform>().offsetMin.x, -2305.196f);
        //    content.GetComponent<RectTransform>().offsetMax = new Vector2(content.GetComponent<RectTransform>().offsetMax.x, -1499.31f);

        //}
        //else if (content.gameObject.transform.childCount >= 13 && content.gameObject.transform.childCount < 16)
        //{
        //    content.GetComponent<RectTransform>().offsetMin = new Vector2(content.GetComponent<RectTransform>().offsetMin.x, -2995.957f);
        //    content.GetComponent<RectTransform>().offsetMax = new Vector2(content.GetComponent<RectTransform>().offsetMax.x, 0.0003662109f);
        //}
        //else if (content.gameObject.transform.childCount >= 16 && content.gameObject.transform.childCount < 19)
        //{
        //    content.GetComponent<RectTransform>().offsetMin = new Vector2(content.GetComponent<RectTransform>().offsetMin.x, -3977.563f);
        //    content.GetComponent<RectTransform>().offsetMax = new Vector2(content.GetComponent<RectTransform>().offsetMax.x, 0.0007324219f);
        //}
        //else
        //{
        //    content.GetComponent<RectTransform>().offsetMin = new Vector2(content.GetComponent<RectTransform>().offsetMin.x, -7018.315f);
        //    content.GetComponent<RectTransform>().offsetMax = new Vector2(content.GetComponent<RectTransform>().offsetMax.x, 0.0004882813f);
        //}



    }
}
