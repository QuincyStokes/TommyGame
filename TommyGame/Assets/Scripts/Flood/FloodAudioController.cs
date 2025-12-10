using UnityEngine;
using System.Collections;
public class FloodAudioController : MonoBehaviour
{
    [SerializeField] private float firstSFXDelay = 2f;
    [SerializeField] private AudioClip floodStartSFX;

    private void Start()
    {
        if (floodStartSFX != null)
        {
            StartCoroutine(DelayFirstSFX());
        }
    }

    private IEnumerator DelayFirstSFX()
    {
        yield return new WaitForSeconds(firstSFXDelay);
        StartCoroutine(AudioManager.Instance.StopMusic());
        AudioManager.Instance.PlaySFX(floodStartSFX, 1f);
        AudioSource.PlayClipAtPoint(floodStartSFX, Camera.main.transform.position);
    }
}
