using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource bgMusicSource;
    public AudioClip firstBgMusic;
    public float initialDelay;
    private void Start()
    {
        bgMusicSource.clip = firstBgMusic;
        StartCoroutine(PlayBgMusic());
    }

    private IEnumerator PlayBgMusic()
    {
        yield return new WaitForSeconds(initialDelay);
        bgMusicSource.Play();
    }
}
