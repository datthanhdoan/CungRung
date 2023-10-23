using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class DogAttack : MonoBehaviour
{

    private bool playIsDead;
    public GameObject player;
    public Rigidbody2D rb;
    public LayerMask layerMark;
    public Animator animator;
    private float speed = 8.5f;
    private float distance;
    private bool isFacingRight = true;
    private bool isDead = false;
    private float length, timer;


    void Start()
    {
        timer = 0;
        rb = GetComponent<Rigidbody2D>();
        length = GetComponent<Collider2D>().bounds.size.x;
    }

    public void setDead(bool dead)
    {
        isDead = dead;
    }
    public bool getDead()
    {
        return isDead;
    }
    void Update()
    {
        // cacl distance between dog and player

    }
    private void FixedUpdate()
    {
        if (isDead)
        {
            animator.SetBool("isDead", true);
            return;
        }
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 moveDirection = new Vector2(player.transform.position.x - transform.position.x, 0f);
        Debug.Log(distance);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, 1.1f, layerMark);
        if (hit)
        {
            //playerAnimator.setDead(true);
        }
        //if (playerAnimator != null && playerAnimator.getDead() )
        //{
        //    animator.SetFloat("speed", 0);
        //    rb.velocity = Vector2.zero;
        //}
        //else

        if (distance < 7)
        {
            // change animator
            animator.SetFloat("speed", Mathf.Abs(speed));

            //moverment



            transform.Translate(moveDirection.normalized * speed * Time.deltaTime);

            if (transform.position.x < player.transform.position.x && isFacingRight == false)
            {
                Flip();
                transform.position = new Vector2(transform.position.x + length, transform.position.y);
            }
            else if (transform.position.x > player.transform.position.x && isFacingRight == true)
            {
                Flip();
                transform.position = new Vector2(transform.position.x - length, transform.position.y);
            }


        }
        else
        {
            animator.SetFloat("speed", 0);
        }
    }


    void Flip()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, isFacingRight ? 0 : 180, 0));
        isFacingRight = !isFacingRight;
    }
}
