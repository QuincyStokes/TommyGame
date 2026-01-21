using System.Collections;
using UnityEngine;

public class FloatingWallet : MonoBehaviour
{
    [Header("References")]
    public SpriteRenderer sr;
    [Header("Vertical Bobbing")]
    [SerializeField] private float verticalOffset = 0.5f;
    [SerializeField] private float cycleSpeed = 1f; // cycles per second

    [Header("Horizontal Movement")]
    [SerializeField] private float horizontalMoveSpeed = 0f;

    [Header("Scenario Settings")]
    [SerializeField] private float delayTime;

    private Vector3 startPosition;
    private float time;
    private bool canFloat;

    private void Awake()
    {
        startPosition = transform.position;
        StartCoroutine(DelayFloat());
        sr.enabled = false;
    }

    private IEnumerator DelayFloat()
    {
        yield return new WaitForSeconds(delayTime);
        canFloat = true;
        sr.enabled = true;
    }

    private void Update()
    {
        if(canFloat)
        {
            time += Time.deltaTime;

            float verticalBob = Mathf.Sin(time * Mathf.PI * 2f * cycleSpeed) * verticalOffset;

            Vector3 position = startPosition;
            position.y += verticalBob;
            position.x += horizontalMoveSpeed * time;

            transform.position = position;

        }
    }
}
