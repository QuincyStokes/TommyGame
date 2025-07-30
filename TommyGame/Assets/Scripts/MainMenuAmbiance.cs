using System.Collections;
using UnityEngine;

public class MainMenuAmbiance : MonoBehaviour
{
    public float initialDelay;
    public Player player;
    public Vector3 targetFireScale;
    public Vector3 targetFirePosition;
    public float fireAnimationTime;
    public Transform fire;

    private bool isCoroutineRunning;

    private void Start()
    {
        StartCoroutine(DoSillyMainMenuThings());

    }

    private void Update()
    {
        if (!isCoroutineRunning)
        {
            DoSillyMainMenu();
        }
    }

    private void DoSillyMainMenu()
    {
        StartCoroutine(DoSillyMainMenuThings());
    }

    private IEnumerator DoSillyMainMenuThings()
    {
        isCoroutineRunning = true;
        //Run Right
        fire.localPosition = new Vector3(-6, 4, 0);
        yield return new WaitForSeconds(initialDelay);
        yield return StartCoroutine(player.MoveTo(new Vector3(17, -4, 0)));

        fire.localPosition = new Vector3(6, 4, 0);
        //Run Left
        yield return new WaitForSeconds(initialDelay * 2);
        yield return StartCoroutine(player.MoveTo(new Vector3(-17, -4, 0)));
        
        isCoroutineRunning = false;
    }

    
}
