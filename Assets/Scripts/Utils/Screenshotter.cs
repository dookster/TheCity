using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshotter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ScreenCapture.CaptureScreenshot("Screenshot" + System.DateTime.Now.ToString("yyyyMMddhhmmss") + ".jpg");
        }
    }
}
