using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour
{

    public Transform player;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player, Vector3.up);
        Vector3 locEul = transform.localEulerAngles;
        locEul.x = 0;
        transform.localEulerAngles = locEul;
    }
}
