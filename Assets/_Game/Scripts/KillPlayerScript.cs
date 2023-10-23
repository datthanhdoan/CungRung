using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class KillPlayerScript : MonoBehaviour
{
    public LayerMask layerMark;
    private float numberOfRays = 5;
    private float length;
    private float spacing;
    private void Start()
    {
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        spacing = length / numberOfRays;
    }

    private void FixedUpdate()
    {
        Vector2 rayOrigin = new Vector2(transform.position.x - (numberOfRays - 1) * spacing / 2, transform.position.y);

        for (int i = 0; i < numberOfRays; i++)
        {

            Vector2 rayDirection = Vector2.up;
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayDirection, 0.1f, layerMark);

            Debug.DrawRay(rayOrigin, rayDirection * 0.3f, Color.red);

            if (hit)
            {
                //playerAnimator.setDead(true);
            }

            rayOrigin.x += spacing;
        }
    }
}
