using DoodleStudio95;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpriteChar : MonoBehaviour
{
    [System.Serializable]
    public struct OverLay
    {
        public DoodleAnimationFile front;
        public DoodleAnimationFile back;
        public DoodleAnimationFile right;
        public DoodleAnimationFile left;
    }

    public enum State { Standing = 0, Walking = 1 }
    public enum Side { Front = 0, Back = 1, Left = 2, Right = 3 }

    public State currentState = State.Standing;

    public Transform player;

    public Transform bubbleHinge;

    public List<Color> colors;
    public List<Color> overlayColors;

    public SpriteRenderer front;
    public SpriteRenderer back;
    public SpriteRenderer right;
    public SpriteRenderer left;

    public SpriteRenderer frontOverRender;
    public SpriteRenderer backOverRender;
    public SpriteRenderer rightOverRender;
    public SpriteRenderer leftOverRender;

    public DoodleAnimator frontOverAnim;
    public DoodleAnimator backOverAnim;
    public DoodleAnimator rightOverAnim;
    public DoodleAnimator leftOverAnim;

    public DoodleAnimator frontAnim;
    public DoodleAnimator backAnim;
    public DoodleAnimator rightAnim;
    public DoodleAnimator leftAnim;

    public OverLay[] overlays;

    public NavMeshAgent navMeshAgent;

    private bool hasOverlay = false;

    public float theta;
    public Vector3 angle;
    public float whichWay;
    public Side sideToShow;
    private QuoteManager quoteManager;

    private Vector3 wanderTarget;

    void Start()
    {
        player = Camera.main.transform;
        quoteManager = GameObject.Find("QuoteManager").GetComponent<QuoteManager>();

        // overlay
        SetOverlay();

        if (currentState == State.Walking)
        {
            MoveToRandomPoint();
        }
    }

    private void SetOverlay()
    {
        Color c = colors[Random.Range(0, colors.Count)];
        front.color = c;
        back.color = c;
        right.color = c;
        left.color = c;

        int overlayIndex = Random.Range(-1, overlays.Length);

        if (overlayIndex >= 0)
        {
            hasOverlay = true;

            frontOverAnim.ChangeAnimation(overlays[overlayIndex].front);
            backOverAnim.ChangeAnimation(overlays[overlayIndex].back);
            rightOverAnim.ChangeAnimation(overlays[overlayIndex].right);
            leftOverAnim.ChangeAnimation(overlays[overlayIndex].left);

            frontOverRender.enabled = true;
            backOverRender.enabled = true;
            rightOverRender.enabled = true;
            leftOverRender.enabled = true;

            c = overlayColors[Random.Range(0, overlayColors.Count)];
            frontOverRender.color = c;
            backOverRender.color = c;
            rightOverRender.color = c;
            leftOverRender.color = c;
        }
        else
        {
            frontOverRender.enabled = false;
            backOverRender.enabled = false;
            rightOverRender.enabled = false;
            leftOverRender.enabled = false;
        }
    }

    void Update()
    {
        sideToShow = GetAngleIndex();
        ShowSprite(sideToShow);

        if (currentState == State.Standing)
        {
            if (Vector3.Distance(transform.position, player.position) < 10)
            {
                Vector3 targetForward = (player.position) - transform.position;
                transform.forward = Vector3.Lerp(transform.forward, targetForward.normalized, Time.deltaTime);

                Vector3 locEul = transform.localEulerAngles;
                locEul.x = 0;
                transform.localEulerAngles = locEul;
            }
        }
        else if (currentState == State.Walking)
        {
            if (Vector3.Distance(transform.position, wanderTarget) < 2 || navMeshAgent.velocity.magnitude < 0.1f)
            {
                MoveToRandomPoint();
            }
        }
    }

    private void MoveToRandomPoint()
    {
        wanderTarget = RandomNavmeshLocation(50);
        navMeshAgent.SetDestination(wanderTarget);
    }

    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

    private Side GetAngleIndex()
    {
        Vector3 pPos = player.position;
        pPos.y = transform.position.y;
        angle = pPos - transform.position;

        angle.Normalize();
        var b = transform.forward;

        theta = Mathf.Acos(Vector3.Dot(angle, b)) * Mathf.Rad2Deg;

        Vector3 normalizedDirection = pPos - transform.position;
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

        frontOverAnim.speed = speed;
        backOverAnim.speed = speed;
        rightOverAnim.speed = speed;
        leftOverAnim.speed = speed;
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

        if (hasOverlay)
        {
            frontOverRender.enabled = false;
            backOverRender.enabled = false;
            leftOverRender.enabled = false;
            rightOverRender.enabled = false;
            switch (side)
            {
                case Side.Front:
                    frontOverRender.enabled = true;
                    break;
                case Side.Back:
                    backOverRender.enabled = true;
                    break;
                case Side.Left:
                    leftOverRender.enabled = true;
                    break;
                case Side.Right:
                    rightOverRender.enabled = true;
                    break;
                default:
                    break;
            }
        }
    }

    private Rect guiPos = new Rect(0, 0, 720, 30);
    void OnGUI()
    {
        //GUI.Label(guiPos, "Angle from the Player is: " + theta + " and side=" + sideToShow);
    }
}
