using System.Collections;
using System.Collections.Generic;
using System.IO;
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
        

        if (Input.GetKeyUp(KeyCode.I) && !bookParent.activeSelf)
        {
            infoParent.SetActive(!infoParent.activeSelf);
        }
        if (!bookParent.activeSelf)
        {
            if (Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.E))
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
        else 
        {
            if (Input.GetKeyUp(KeyCode.Return))
            {
                // hide book and save
                bookParent.SetActive(false);
                playerDrifter.enabled = true;
                handsParent.SetActive(true);
                mouseLook.enabled = true;
                quoteManager.SaveQuote(inputText.text);
            }
            
        }


        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (bookParent.activeSelf)
            {
                bookParent.SetActive(false);
                playerDrifter.enabled = true;
                handsParent.SetActive(true);
                mouseLook.enabled = true;
            }
            else if (infoParent.activeSelf)
            {
                infoParent.SetActive(false);
            }
            else if (Application.platform != RuntimePlatform.WebGLPlayer)
            {
                Application.Quit();
            }
        }
    }


}
