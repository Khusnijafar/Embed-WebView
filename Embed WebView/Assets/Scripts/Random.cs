using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


// terus kalau udah bisa, buat 1 class dengan method run isinya invoke webview dari isi ajax td

public class Random : MonoBehaviour
{
    // add ajax to method run
    static void run()
    {
        // create ajax
        string url = "https://dog.ceo/api/breeds/image/random";
        WWW www = new WWW(url);
        // wait for ajax
        while (!www.isDone)
        {
            // do nothing
        }
        // print ajax
        print(www.text);   
    }

    // Start is called before the first frame update
    void Start()
    {
        // call method run
        run();
    }

    // Update is called once per frame
}
