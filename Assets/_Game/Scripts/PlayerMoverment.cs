using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerMoverment : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    public LayerMask groundLayer;
    public LayerMask climbLayer;
    // Moverment
    private float moveSpeed;
    private float jumpForce;
    private float moveHorizontal;
    private bool isDead = false;
    private bool isFacingRight = true;
    // Climd , ladder
    private bool isClimb = false;
    private float width, height;
    private float numberOfRays = 5;
    private float spacingRays;
    private bool isGrounded = true;
    // animation
    [SerializeField] private Animator anim;
    private int currentState;
    public bool IsDead()
    {
        return isDead;

    }
    public void setDead(bool dead)
    {
        isDead = dead;

    }
    private int GetState()
    {
        if (isDead) return Dead;
        if (isClimb) return Climb;
        if (!isGrounded) return Jump;
        if (isGrounded) return Mathf.Abs(moveHorizontal) > 0.1f ? Run : Idle;
        else return Idle;

    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = 600f;
        jumpForce = 25f;

        width = GetComponent<CapsuleCollider2D>().size.x;
        height = GetComponent<CapsuleCollider2D>().size.y;
        spacingRays = width / (numberOfRays - 1);
    }

    void Update()
    {
        if (isDead) return;
        // state animation
        var state = GetState();
        if (state != currentState)
        {
            anim.CrossFade(state, 0, 0);
            currentState = state;

        }

        moveHorizontal = Input.GetAxisRaw("Horizontal");
        checkOnGround();
        checkAHead();
        if (isGrounded && Input.GetKeyDown("space"))
        {
            rb.velocity = (new Vector2(rb.velocity.x, 1 * jumpForce));
            isGrounded = false;
        }
    }

    private void FixedUpdate()
    {
        // for raycast direction
        if (moveHorizontal > 0f) isFacingRight = true;
        if (moveHorizontal < 0f) isFacingRight = false;
        Debug.Log(isFacingRight);
        if (isClimb)
        {
            rb.velocity = (new Vector2(0f, 5f));
        }
        //moverment
        if (Mathf.Abs(moveHorizontal) > 0.1f)
        {
            rb.velocity = (new Vector2(moveHorizontal * Time.fixedDeltaTime * moveSpeed, rb.velocity.y));
            transform.rotation = Quaternion.Euler(new Vector3(0, moveHorizontal > 0 ? 0 : 180, 0));
            //cach nay kha la lag
            //transform.Translate(new Vector2(moveHorizontal, moveVertical) * moveSpeed * Time.deltaTime);
        }
        //if (isClimb)
        //{
        //    rb.velocity = new Vector2(rb.velocity.x, 3f);
        //}

    }
    #region Animation Keys
    private static readonly int Run = Animator.StringToHash("Player_Run");
    private static readonly int Idle = Animator.StringToHash("Player_Idle");
    private static readonly int Jump = Animator.StringToHash("Player_JumpIn");
    private static readonly int Dead = Animator.StringToHash("Player_Dead");
    private static readonly int Climb = Animator.StringToHash("Player_Climb");

    #endregion
    private void checkOnGround()
    {
        Vector2 rayOrigin = new Vector2(transform.position.x - width / 2, transform.position.y);

        bool anyRaycastHit = false;

        for (int i = 0; i < numberOfRays; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, height / 2 + 0.1f, groundLayer);
            Debug.DrawRay(rayOrigin, Vector2.down * (height / 2 + 0.1f), Color.blue);
            rayOrigin.x += spacingRays;

            if (hit.collider != null)
            {
                anyRaycastHit = true;
                break;
            }
        }

        isGrounded = anyRaycastHit;
    }


    private void checkAHead()
    {
        //bool checkDone = false;
        Vector2 rayOrigin = isFacingRight
            ? new Vector2(transform.position.x + width / 2, transform.position.y - height / 2)
            : new Vector2(transform.position.x - width / 2, transform.position.y - height / 2);

        Vector2 direction = isFacingRight ? Vector2.right : Vector2.left;

        bool anyRaycastHit = false;
        int index = 0;
        // them 5 raycast cho chieu cao 
        for (int i = 0; i < numberOfRays + 5; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, direction, spacingRays, climbLayer);
            Debug.DrawRay(rayOrigin, direction * spacingRays, Color.blue);
            rayOrigin.y += height / (numberOfRays + 5 - 1);
            if (hit.collider != null)
            {
                anyRaycastHit = true;
                break;
            }
            index++;

        }

        isClimb = anyRaycastHit;
    }



}