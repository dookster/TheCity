using DoodleStudio95;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChar : MonoBehaviour
{
    enum Side { Front = 0, Back = 1, Left = 2, Right = 3 }
    public Transform player;

    public SpriteRenderer front;
    public SpriteRenderer back;
    public SpriteRenderer right;
    public SpriteRenderer left;

    public DoodleAnimator frontAnim;
    public DoodleAnimator backAnim;
    public DoodleAnimator rightAnim;
    public DoodleAnimator leftAnim;

    private float theta;
    private Vector3 a;
    private float whichWay;
    private Side sideToShow;

    void Start()
    {
        
    }

    void Update()
    {
        sideToShow = GetAngleIndex();
        ShowSprite(sideToShow);
    }

    private Side GetAngleIndex()
    {
        a = player.position - transform.position;

        a.Normalize();
        var b = transform.forward;

        theta = Mathf.Acos(Vector3.Dot(a, b)) * Mathf.Rad2Deg;

        Vector3 normalizedDirection = player.transform.position - transform.position;
        whichWay = Vector3.Cross(transform.forward, normalizedDirection).y;

        //if (a.x * a.z < 0)
        //    theta = 360.0f - theta;

        if (theta < 45)
        {
            return Side.Front;
        }
        else if (theta < 135)
        {
            if (whichWay < 0)
            {
                return Side.Left;
            }
            else
            {
                return Side.Right;
            }
        }
        else
        {
            return Side.Back;
        }
    }

    public void SetAnimationSpeed(float speed)
    {        
        frontAnim.speed = speed;
        backAnim.speed = speed;
        rightAnim.speed = speed;
        leftAnim.speed = speed;
    }

    private void ShowSprite(Side side)
    {
        front.enabled = false;
        back.enabled = false;
        left.enabled = false;
        right.enabled = false;
        switch (side)
        {
            case Side.Front:
                front.enabled = true;
                break;
            case Side.Back:
                back.enabled = true;
                break;
            case Side.Left:
                left.enabled = true;
                break;
            case Side.Right:
                right.enabled = true;
                break;
            default:
                break;
        }
    }

    private Rect guiPos = new Rect(0, 0, 720, 30);
    void OnGUI()
    {
        //GUI.Label(guiPos, "Angle from the Player is: " + theta + " and side=" + sideToShow);
    }
}
