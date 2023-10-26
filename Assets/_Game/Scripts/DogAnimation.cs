using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DogAnimation : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject player;
    [SerializeField] private Animator anim;
    private float distanceX = 10f, distance;
    private PlayerMoverment playerMoverment;
    private DogMoverment dogmov;
    private int currentState;

    private void Start()
    {
        dogmov = FindObjectOfType<DogMoverment>();

        playerMoverment = player.GetComponent<PlayerMoverment>();

    }
    private int GetState()
    {

        if (playerMoverment.getDead()) return Idle;
        return distance < distanceX ? Run : Idle;

    }
    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        var state = GetState();
        if (state != currentState)
        {
            anim.CrossFade(state, 0, 0);
            currentState = state;

        }
    }
    #region Animation Keys
    private static readonly int Dead = Animator.StringToHash("dog_die");
    private static readonly int Run = Animator.StringToHash("dog_run");
    private static readonly int Idle = Animator.StringToHash("dog_idle");
    #endregion
}
