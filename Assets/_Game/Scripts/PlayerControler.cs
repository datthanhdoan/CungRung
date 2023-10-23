using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

public class PlayerControler : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveHorizontal;
    private float moveVertical;
    private float Speed = 5.5f;
    private float JumpHeight = 10f;
    private bool inGround = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");
        if (Mathf.Abs(moveHorizontal) > 0.1f)
        {
            // transform.Translate(new Vector2(moveHorizontal * Speed * Time.deltaTime, rb.velocity.y));
            rb.velocity = new Vector2(moveHorizontal * Speed * Time.deltaTime , rb.velocity.y);
        }
        if (moveVertical > 0.1f && inGround)
        {
            // rb.AddForce(new Vector2(0f, moveVertical * JumpHeight), ForceMode2D.Impulse);
            rb.velocity = (new Vector2(0f,moveVertical*JumpHeight));
        }

        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform"))
        {
            inGround = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform"))
        {
            inGround = false;
        }
    }
}
