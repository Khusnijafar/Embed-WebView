using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


// terus kalau udah bisa, buat 1 class dengan method run isinya invoke webview dari isi ajax td
// tampilin image yg dari https://dog.ceo/api/breeds/image/random ke tampilan di webviewnya

public class Random : MonoBehaviour
{
    public string jsonURL;
    public Renderer thisRenderer;

    void Start()
    {
       StartCoroutine(getData());

       thisRenderer.material.color = Color.red;
    }

    public IEnumerator getData()
    {

        Debug.Log("Processing...");
        Debug.Log("URL: " + jsonURL);
        WWW _www = new WWW(jsonURL);
        yield return _www;

        JSonDataClass jsnData = JsonUtility.FromJson<JSonDataClass>(_www.text);
        Debug.Log("www" + jsnData.message);
        WWW image = new WWW(jsnData.message);
        yield return image;

        thisRenderer.material.color = Color.white;
        thisRenderer.material.mainTexture = image.texture;
        if(_www.error == null) 
        {
           processJsonData(_www.text);              
        }
        else 
        {
            Debug.Log("Error: ");
        }
    }

    private void processJsonData(string _url)
    {
        JSonDataClass jsnData = JsonUtility.FromJson<JSonDataClass>(_url);
        Debug.Log(jsnData.message);
    }  
}



  // static void run()
    // {
    //     string url = "https://dog.ceo/api/breeds/image/random";
    //     WWW www = new WWW(url);
    //     while (!www.isDone)
    //     {
    //         // do nothing
    //     }
    //     print(www.text);   
    // }

    // // Display webview from method run
    


    // // Start is called before the first frame update
    // void Start()
    // {
    //     // call method run
    //     run();
    // }

    // // Update is called once per frame