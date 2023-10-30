using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PreviousDoor : MonoBehaviour
{

    private Animator anim;
    private GameManagerScript gameManager;
    private float timer = 0f;
    private bool waiting = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManagerScript>();
    }

    void Update()
    {
        if (waiting)
        {
            timer += Time.deltaTime;
            if (timer >= 1.0f)
            {
                gameManager.previousScreen();
                waiting = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            anim.CrossFade(Open, 0, 0);
            waiting = true;
        }
    }
    private static readonly int Open = Animator.StringToHash("DoorOpen");

}
