using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SpeechBubbleCanvas : MonoBehaviour
{

    public GameObject bubble;
    public TextMeshProUGUI talkText;

    private float hideBubbleIn;

    void Update()
    {
        transform.forward = Camera.main.transform.forward;

        if (bubble.activeSelf)
        {
            hideBubbleIn -= Time.deltaTime;
            if (hideBubbleIn <= 0)
            {
                bubble.transform.DOScale(0, 0.2f).SetEase(Ease.InOutExpo).OnComplete(() =>
                {
                    bubble.SetActive(false);
                });
            }
        }
    }

    public void SetText(string text)
    {        
        talkText.text = text;
        bubble.transform.localScale = Vector3.zero;
        bubble.SetActive(true);
        bubble.transform.DOScale(1, 0.2f).SetEase(Ease.InOutExpo).OnComplete(() => 
        {
            hideBubbleIn = 10f;
        });

    }

}
