using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeechBubble : MonoBehaviour
{

    public SpriteRenderer backgroundSprite;
    public TextMeshPro textMesh;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            SetText(textMesh.text + "As a remedy to life in society");
        }
    }

    public void SetText(string text)
    {
        Debug.Log("H1: " + textMesh.bounds.size.y + " , " + textMesh.rectTransform.rect.height);
        textMesh.SetText(text);
        StartCoroutine(AdjustSize());
    }

    IEnumerator AdjustSize()
    {
        yield return new WaitForEndOfFrame();
        Debug.Log("H2: " + textMesh.bounds.size.y + " , " + textMesh.rectTransform.rect.height);        
        backgroundSprite.size = new Vector2(backgroundSprite.size.x, textMesh.bounds.size.y * 1.4f);
        backgroundSprite.transform.localPosition = new Vector3(backgroundSprite.transform.localPosition.x, backgroundSprite.size.y / 6f, backgroundSprite.transform.localPosition.z);
    }

}
