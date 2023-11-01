using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BowShooting : MonoBehaviour
{

    public GameObject bullet;
    public GameObject player;
    public Transform bulletPos;
    [SerializeField] private float _Distance = 30;
    private float timer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance < _Distance)
        {
            timer += Time.deltaTime;
            if (timer > 1)
            {
                timer = 0;
                Shoot();
            }
        }

    }
    void Shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }
}
