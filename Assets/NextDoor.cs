using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NextDoor : MonoBehaviour
{

    private bool isKey = false;
    private Animator anim;
    private GameManagerScript gameManager;
    private float timer = 0f;
    private bool waiting = false;
    public void setKey(bool isKey)
    {
        this.isKey = isKey;
    }
    public bool getKey()
    {
        return isKey;
    }
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
                gameManager.nextScreen();
                waiting = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isKey)
        {
            anim.CrossFade(Open, 0, 0);
            waiting = true;
        }
    }
    private static readonly int Open = Animator.StringToHash("DoorOpen");

}
