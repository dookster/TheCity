using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoUI : MonoBehaviour
{

    public GameObject infoParent;
    public GameObject bookParent;
    public TMP_InputField inputText;

    public GameObject handsParent;
    public FirstPersonDrifter playerDrifter;
    public MouseLook mouseLook;

    public QuoteManager quoteManager;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            infoParent.SetActive(!infoParent.activeSelf);
        }

        if (Input.GetKeyUp(KeyCode.Return))
        {
            if (bookParent.activeSelf)
            {
                // hide book
                bookParent.SetActive(false);
                playerDrifter.enabled = true;
                handsParent.SetActive(true);
                mouseLook.enabled = true;
                quoteManager.SaveQuote(inputText.text);
            }
            else
            {
                // show book
                bookParent.SetActive(true);
                inputText.Select();
                inputText.ActivateInputField();
                inputText.text = "";
                playerDrifter.enabled = false;
                handsParent.SetActive(false);
                mouseLook.enabled = false;
            }
        }
    }


}
