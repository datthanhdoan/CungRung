using UnityEngine;

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
    public bool getDead()
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
        moveSpeed = 500f;
        jumpForce = 25f;
        width = GetComponent<CapsuleCollider2D>().size.x;
        height = GetComponent<CapsuleCollider2D>().size.y;
        spacingRays = width / (numberOfRays - 1);
    }

    void Update()
    {

        // state animation
        var state = GetState();
        if (state != currentState)
        {
            if (currentState == Dead) return;
            anim.CrossFade(state, 0.03f, 0);
            currentState = state;

        }
        if (isDead) return;
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        checkOnGround();
        checkAHead();
        if (isGrounded && Input.GetKeyDown("space"))
        {
            rb.velocity = (new Vector2(rb.velocity.x, 1 * jumpForce));
            isGrounded = false;
        }
        if (moveHorizontal > 0f) isFacingRight = true;
        if (moveHorizontal < 0f) isFacingRight = false;

        // Move up when climbing
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
    }

    private void FixedUpdate()
    {
        // for raycast direction

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
        Vector2 rayOrigin = new Vector2(transform.position.x, transform.position.y);
        float rayLength = height / 2 + 0.6f;

        RaycastHit2D[] hits = Physics2D.RaycastAll(rayOrigin, Vector2.down, rayLength, groundLayer);
        foreach (RaycastHit2D hit in hits)
        {
            Debug.DrawRay(hit.point, Vector2.down * 0.1f, Color.blue);
        }

        isGrounded = hits.Length > 0;
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
        for (int i = 0; i < numberOfRays - 2; i++)
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

        //  RaycastHit2D hitcap = Physics2D.CapsuleCast(transform.position, new Vector2(0.6f, 2), new CapsuleDirection2D(Vector2.one), 0, Vector2.right);

        isClimb = anyRaycastHit;
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Mob"))
    //    {
    //        isDead = true;
    //        Debug.Log("Player DEad");
    //    }
    //}


}