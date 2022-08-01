using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_2018_4_OR_NEWER
using UnityEngine.Networking;
#endif
using UnityEngine.UI;

public class WebView : MonoBehaviour
{
   SampleWebView sampleWebView;

    public void run()
    {
        sampleWebView = new SampleWebView();

        StartCoroutine(sampleWebView.Execute());
    }

    public void Start()
    {
        run();
    }
}
