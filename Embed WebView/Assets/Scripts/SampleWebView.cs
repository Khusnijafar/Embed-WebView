using System.Collections;
using UnityEngine;
#if UNITY_2018_4_OR_NEWER
using UnityEngine.Networking;
#endif
using UnityEngine.UI;

public class SampleWebView
{
    public string Url;
    public Text status;
    WebViewObject webViewObject;

    public IEnumerator Execute()
    {
        webViewObject = (new GameObject("WebViewObject")).AddComponent<WebViewObject>();
        webViewObject.Init(
            ld: (msg) =>
            {
                Debug.Log(string.Format("CallOnLoaded[{0}]", msg));
                webViewObject.EvaluateJS(
                    @"
                  if (window && window.webkit && window.webkit.messageHandlers && window.webkit.messageHandlers.unityControl) {
                    window.Unity = {
                      call: function(msg) {
                        window.webkit.messageHandlers.unityControl.postMessage(msg);
                      }
                    }
                  } 
                "
                );
            }
        );
#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
        webViewObject.bitmapRefreshCycle = 1;
#endif
        webViewObject.SetMargins(5, 100, 5, Screen.height / 12);
        webViewObject.SetTextZoom(100);
        webViewObject.SetVisibility(true);

        var server = "http://localhost:8000/";
        var files = new string[] { "randomImage.html", };
        var top = System.IO.Path.Combine(Application.persistentDataPath, "html");
        if (System.IO.Directory.Exists(top))
        {
            System.IO.Directory.Delete(top, true);
        }
        foreach (var file in files)
        {
            var unityWebRequest = UnityWebRequest.Get(server + file);
            Debug.Log(unityWebRequest);
            yield return unityWebRequest.SendWebRequest();
            var data = unityWebRequest.downloadHandler.data;
            System.IO.Directory.CreateDirectory(
                System.IO.Path.Combine(top, System.IO.Path.GetDirectoryName(file))
            );
            byte[] fileBytes = System.IO.File.ReadAllBytes(file);
            System.IO.File.WriteAllBytes(System.IO.Path.Combine(top, file), fileBytes);
        }
        webViewObject.LoadURL(
            "file://" + System.IO.Path.Combine(top, "randomImage.html").Replace(" ", "%20")
        );
    }
}
