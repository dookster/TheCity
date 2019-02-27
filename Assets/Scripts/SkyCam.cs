using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyCam : MonoBehaviour
{

    public Camera mainCam;

    void Start()
    {
        
    }

    void LateUpdate()
    {
        transform.rotation = mainCam.transform.rotation;
    }
}
