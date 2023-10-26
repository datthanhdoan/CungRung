using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KillMob : MonoBehaviour
{
    // Start is called before the first frame update
    private Mob mob;
    void Start()
    {
        mob = FindObjectOfType<Mob>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Mod"))
        {
            mob.setDead(true);
        }
    }
}
