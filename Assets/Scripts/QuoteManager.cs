using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuoteManager : MonoBehaviour
{
    public SpeechBubbleCanvas bubble;

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

}
