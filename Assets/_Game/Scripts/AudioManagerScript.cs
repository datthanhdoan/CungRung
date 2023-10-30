using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    private float audioTime;
    private AudioClip currentStatePlayer, currentStateDog, currentStateRock;
    private PlayerMoverment playerMoverment;
    private DogMoverment dogmov;
    [Header("SFX Effect")]
    [SerializeField] private AudioSource SFXEffect;
    [SerializeField] private AudioClip deadth;
    [SerializeField] private AudioClip jump;
    [SerializeField] private AudioClip climb;
    [SerializeField] private AudioClip run;
    [SerializeField] private AudioClip dogWol;
    [SerializeField] private AudioClip rock;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        playerMoverment = FindObjectOfType<PlayerMoverment>();
        dogmov = FindObjectOfType<DogMoverment>();

    }
    private AudioClip GetStatePlayer()
    {
        if (playerMoverment.getDead()) return deadth;
        if (playerMoverment.getClimb()) return climb;
        if (!playerMoverment.getGround()) return jump;
        //if (isGrounded && isShift && Mathf.Abs(moveHorizontal) > 0.1f) return SlowDown;
        //if (isGrounded && isShift) return Down;
        //if (isGrounded) return Mathf.Abs(moveHorizontal) > 0.1f ? Run : Idle;
        else return null;

    }
    private AudioClip GetStateDog()
    {
        if (dogmov.getIsRunning()) return dogWol;
        else return null;

    }

    private void Update()
    {

        // Player
        var statePlayer = GetStatePlayer();
        if (statePlayer != currentStatePlayer)
        {
            SFXEffect.PlayOneShot(statePlayer);
            currentStatePlayer = statePlayer;
        }
        // Dog
        var stateDog = GetStateDog();
        if (stateDog != currentStateDog)
        {
            SFXEffect.PlayOneShot(stateDog);
            currentStateDog = stateDog;
        }


    }



}
