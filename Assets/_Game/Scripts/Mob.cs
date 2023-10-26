using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class Mob : MonoBehaviour
{

    private bool playIsDead;
    private GameObject player;
    private PlayerMoverment playerMoverment;
    public Rigidbody2D rb;
    public LayerMask layerMark;
    [SerializeField] private float distanceX;
    private float speed = 12.5f;
    private float distance;
    private bool isFacingRight = true;
    private bool isDead = false;
    private float length;

    public float getDistance()
    {
        return distance;
    }
    public float getDistanceX()
    {
        return distanceX;
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        length = GetComponent<Collider2D>().bounds.size.x;
        playerMoverment = FindObjectOfType<PlayerMoverment>();
    }

    public void setDead(bool dead)
    {
        isDead = dead;
    }
    public bool getDead()
    {
        return isDead;
    }

    private void FixedUpdate()
    {

        distance = Vector2.Distance(transform.position, player.transform.position);


        if (distance <= distanceX)
        {
            if (playerMoverment.getDead()) return;
            //moverment
            Vector2 target = new Vector2(player.transform.position.x, rb.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
            if (transform.position.x < player.transform.position.x && !isFacingRight)
            {
                Flip();
            }
            else if (transform.position.x > player.transform.position.x && isFacingRight)
            {
                Flip();

            }


        }

    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.rotation = Quaternion.Euler(new Vector3(0, isFacingRight ? 0 : 180, 0));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bool x = true;
            playerMoverment.setDead(x);
        }
    }

}
