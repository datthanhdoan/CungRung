using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KillDogScript : MonoBehaviour
{
    // Start is called before the first frame update
    private DogAttack dogAttack;
    void Start()
    {
        dogAttack = FindObjectOfType<DogAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Mod"))
        {
            dogAttack.setDead(true);
        }
    }
}
