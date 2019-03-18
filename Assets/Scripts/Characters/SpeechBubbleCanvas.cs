using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using DoodleStudio95;

public class SpeechBubbleCanvas : MonoBehaviour
{

    public GameObject bubble;
    public DoodleAnimator doodleAnimator;
    public DoodleAnimationFile anim1;
    public DoodleAnimationFile anim2;
    public DoodleAnimationFile anim3;
    public Color texCol1;
    public Color texCol2;
    public Color texCol3;
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

    public void SetText(string text, int style)
    {
        switch (style)
        {
            case 0:
                doodleAnimator.ChangeAnimation(anim1);
                talkText.color = texCol1;
                break;
            case 1:
                doodleAnimator.ChangeAnimation(anim2);
                talkText.color = texCol2;
                break;
            case 2:
                doodleAnimator.ChangeAnimation(anim3);
                talkText.color = texCol3;
                break;

        }
        talkText.text = text;
        bubble.transform.localScale = Vector3.zero;
        bubble.SetActive(true);
        bubble.transform.DOScale(1, 0.2f).SetEase(Ease.InOutExpo).OnComplete(() => 
        {
            hideBubbleIn = 10f;
        });

    }

}
