using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class FloodRooftopBrain : MonoBehaviour
{
    public Helicopter helicopter;
    public Vector3 helicopterFirstPosition;
    public HelicopterTapController htc;
    public Player player;

    [Header("Player Positions")]
    public Vector2 underRopePosition;

    private void Start()
    {
        htc.OnTapsCompleted += HandleTapsCompleted;
        htc.OnTapsFailed += HandleTapsLost;
        player = GameObject.Find("Player").GetComponent<Player>();
        StartCoroutine(BeginningScenario());
        htc.isTapTime = false;
    }

    private IEnumerator BeginningScenario()
    {
        yield return StartCoroutine(helicopter.Fly(helicopterFirstPosition, 3));
        
        //Drop the rope
        helicopter.DropRope();
        htc.isTapTime = true;
        //now can listen for when our taps are done
    }

    private void HandleTapsCompleted()
    {
        //The player clibms the rope
        //player
        //They dissapear into the helicopter
        //The helicopter flies away
        //Success
        StartCoroutine(DoTapsCompletedSequence());
    }

    private IEnumerator DoTapsCompletedSequence()
    {
        yield return StartCoroutine(player.MoveTo(underRopePosition));
        yield return StartCoroutine(player.ClimbTo(helicopter.transform.position));
        
    }

    private void HandleTapsLost()
    {
        //Helicopter has to leave because the rain is too tough
        //Player gets left
        //Game over screen
    }
}
