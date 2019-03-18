using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class QuoteManager : MonoBehaviour
{
    public SpeechBubbleCanvas bubble;

    string path
    {
        get
        {
            return Application.persistentDataPath + "/quotes.txt";
        }
    }

    [TextArea(10, 50)]
    public string quotesRaw;

    [TextArea(10, 50)]
    public string quotesRawFromOnline;

    public List<string> quotesOld;
    public List<string> quotesFromOnline;
    public List<string> quotesFromLocal;

    private bool wasLocalLast;

    void Start()
    {        
        quotesOld = new List<string>(quotesRaw.Split('\n'));
        quotesFromOnline = new List<string>(quotesRawFromOnline.Split('\n'));
        LoadQuotesFromFile();
    }

    void Update()
    {

        if (Input.GetKeyUp(KeyCode.F11))
        {
            File.Delete(path);
            LoadQuotesFromFile();
        }
    }

    public void ShowQuote(SpriteChar talker)
    {
        //bubble.transform.SetParent(talker.bubbleHinge, false);
        //bubble.transform.localPosition = Vector3.zero;
        bubble.transform.position = talker.bubbleHinge.position;
        SetQuoteText();
        talker.currentState = SpriteChar.State.Standing;
        talker.navMeshAgent.isStopped = true;
    }

    private void SetQuoteText()
    {
        // one third chance to get local text, rest is even

        int totaltCount = quotesFromOnline.Count + quotesOld.Count;
        float totalChance = 0.66f / totaltCount;
        float r = Random.value;
        if (r < 0.2f && quotesFromLocal.Count > 0 && !wasLocalLast)
        {
            bubble.SetText(quotesFromLocal[Random.Range(0, quotesFromLocal.Count)], 2);
            wasLocalLast = true;
        }
        else if (r < 0.2f + (totalChance * quotesFromOnline.Count))
        {
            bubble.SetText(quotesFromOnline[Random.Range(0, quotesFromOnline.Count)],1);
            wasLocalLast = false;
        }
        else
        {
            bubble.SetText(quotesOld[Random.Range(0, quotesOld.Count)], 0);
            wasLocalLast = false;
        }
    }

    private void LoadQuotesFromFile()
    {
        //AssetDatabase.ImportAsset(path);
        if (!File.Exists(path))
        {
            StreamWriter sw = System.IO.File.CreateText(path);
            sw.Close();
        }

        StreamReader sr = new StreamReader(path);
        string fileString = sr.ReadToEnd();
        sr.Close();

        List<string> str = new List<string>(fileString.Replace("\n", "").Split('#'));
        str.RemoveAll((s) => string.IsNullOrWhiteSpace(s));
        quotesFromLocal = str;
    }

    public void SaveQuote(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return;
        }

        if(Application.platform != RuntimePlatform.WebGLPlayer)
        {
            StreamWriter writer = new StreamWriter(path, true);
            writer.WriteLine("\n#\n" + text);
            writer.Close();
            quotesFromLocal.Add(text);
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
