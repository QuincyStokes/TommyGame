using System.Collections;
using UnityEngine;

public class DelayActivation : MonoBehaviour
{
    private SpriteRenderer _sr;
    public float delayTime;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        if (_sr != null)
        {
            _sr.enabled = false;
            StartCoroutine(DelayStart());
        }
    }

    private IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(delayTime);
        _sr.enabled = true;
    }
}
