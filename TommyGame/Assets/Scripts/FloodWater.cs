using System.Collections;
using UnityEngine;

public class FloodWater : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sr; 
    [SerializeField] private float transitionTime;
    [SerializeField] private Vector2 targetPosition;
    [SerializeField] private float startDelay;

    private void Start()
    {
        StartCoroutine(DoFlood());
    }

    private IEnumerator DoFlood()
    {
        yield return new WaitForSeconds(startDelay);
        float elapsed = 0f;
        Vector2 startPos = transform.position;
        while (elapsed < transitionTime)
        {
            float t = elapsed / transitionTime;
            _sr.color = new Color(1, 1, 1, Mathf.Clamp(t, 0, .7f));
            transform.position = Vector2.Lerp(startPos, targetPosition, t);

            elapsed += Time.deltaTime;
            yield return null;
        }
    }

}
