using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Extensions;
using Firebase.Storage;
using System;
using UnityEngine.Networking;
using UnityEngine.UI;

public class FirebaseStorageManager : MonoBehaviour
{
    FirebaseStorage storage;
    public RawImage rawImage;
    StorageReference storageReference;
    // Start is called before the first frame update
    void Start()
    {
        rawImage = gameObject.GetComponent<RawImage>();
        storage = FirebaseStorage.DefaultInstance;
       



    }
    IEnumerator LoadImage(string MediaUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl); //Create a request
        yield return request.SendWebRequest(); //Wait for the request to complete
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            rawImage.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            // setting the loaded image to our object
        }
    }
    public void GetPhoto(string name)
    {
        storageReference = storage.GetReferenceFromUrl("gs://bulmacahane-f4bb7.appspot.com").Child(name+".PNG");
        storageReference.GetDownloadUrlAsync().ContinueWithOnMainThread(task =>
        {
            if (!task.IsFaulted && !task.IsCanceled)
            {
                StartCoroutine(LoadImage(Convert.ToString(task.Result))); //Fetch file from the link
                gameObject.GetComponent<RawImage>().enabled = true;
            }
            else
            {
                Debug.Log(task.Exception);
               
            }
        });
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
