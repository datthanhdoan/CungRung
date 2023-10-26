using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private PlayerMoverment playerMoverment;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        playerMoverment = FindObjectOfType<PlayerMoverment>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerMoverment.setDead(true);
        }
    }
}
