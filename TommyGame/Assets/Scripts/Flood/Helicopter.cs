using UnityEngine;
using System.Collections;

public class Helicopter : MonoBehaviour
{
    public float bobSpeed = 1.5f;   
    public float bobHeight = 0.5f; 
    public GameObject ropeObject;

    // We use this to track where the helicopter "is" without the bobbing
    private Vector3 basePosition;

    private void Start()
    {
        // Initialize basePosition to where the helicopter starts
        basePosition = transform.position;
        ropeObject.SetActive(false);
    }

    private void Update()
    {
        // 1. Calculate the bobbing offset using a Sine wave
        // We use (bobHeight / 2) so the total movement range equals bobHeight
        float yOffset = Mathf.Sin(Time.time * bobSpeed) * (bobHeight / 2f);

        // 2. Set the actual transform to the base position PLUS the offset
        transform.localPosition = basePosition + new Vector3(0, yOffset, 0);
    }

    public void FlyTo(Vector3 pos, float time)
    {
        StopAllCoroutines(); // Prevent multiple movement commands from overlapping
        StartCoroutine(Fly(pos, time));
    }

    public IEnumerator Fly(Vector3 targetPos, float time)
    {
        Vector3 startPos = basePosition; // Start from our current logical base
        float elapsed = 0f;
        
        while (elapsed < time)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / time;
            
            // Move the BASE position, not the transform directly
            basePosition = Vector3.Lerp(startPos, targetPos, t);
            
            yield return null;
        }

        basePosition = targetPos; // Ensure we land exactly at the target
    }

    public void DropRope()
    {
        ropeObject.SetActive(true);
    }
}