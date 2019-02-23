using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshWander : MonoBehaviour
{
    public SpriteChar spriteChar;
    public NavMeshAgent navMeshAgent;

    void Start()
    {
        
    }

    void Update()
    {
        float normSpeed = navMeshAgent.velocity.magnitude / navMeshAgent.speed;
        if(normSpeed < 0.01f)
        {
            spriteChar.SetAnimationSpeed(0);
        }
        else if (normSpeed < 0.25f)
        {
            spriteChar.SetAnimationSpeed(0.25f);
        }
        else if (normSpeed < 0.5f)
        {
            spriteChar.SetAnimationSpeed(0.5f);
        }
        else if (normSpeed < 0.75f)
        {
            spriteChar.SetAnimationSpeed(0.75f);
        }
        else
        {
            spriteChar.SetAnimationSpeed(1f);
        }
    }
}
