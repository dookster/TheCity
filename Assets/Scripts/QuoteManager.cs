using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class QuoteManager : MonoBehaviour
{
    public SpeechBubbleCanvas bubble;

    string path = "Assets/Resources/quotes.txt";

    [TextArea(10, 50)]
    public string quotesRaw;

    public List<string> quotes;

    void Start()
    {        
        quotes = new List<string>(quotesRaw.Split('\n'));
    }

    void Update()
    {
        
    }

    public void ShowQuote(SpriteChar talker)
    {
        //bubble.transform.SetParent(talker.bubbleHinge, false);
        //bubble.transform.localPosition = Vector3.zero;
        bubble.transform.position = talker.bubbleHinge.position;
        bubble.SetText(quotes[Random.Range(0, quotes.Count)]);
        talker.currentState = SpriteChar.State.Standing;
        talker.navMeshAgent.isStopped = true;
    }

    public void SaveQuote(string text)
    {
        if(Application.platform != RuntimePlatform.WebGLPlayer)
        {
            StreamWriter writer = new StreamWriter(path, true);
            writer.WriteLine("\n#\n" + text);
            writer.Close();
        }
        UploadQuote(text);
    }

    public void UploadQuote(string text)
    {
        StartCoroutine(UploadMethod(text));
    }

    IEnumerator UploadMethod(string text)
    {
        yield return new WaitForEndOfFrame();
        System.DateTime theTime = System.DateTime.Now;
        string datetime = theTime.ToString("yyyyMMdd-HHmmss");

        var bytes = System.Text.Encoding.UTF8.GetBytes(text);
        WWWForm form = new WWWForm();
        form.AddField("action", "level upload");
        form.AddField("file", "file");
        form.AddBinaryData("file", bytes, "Q" + datetime + ".txt", "text/plain");

        UnityWebRequest www = UnityWebRequest.Post("https://aergia.dk/upload/upload.php", form);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }

        //WWW w = new WWW("https://aergia.dk/upload/upload.php", form);
        //yield return w;
        //print("after yield w");
        //if (w.error != null)
        //{
        //    Debug.Log("error");
        //    Debug.Log(w.error);
        //}
    }

}
