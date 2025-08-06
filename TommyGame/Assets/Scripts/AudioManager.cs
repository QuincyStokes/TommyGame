using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource bgMusicSource;
    public AudioClip firstBgMusic;
    public AudioClip intenseBgMusic;
    public float initialDelay;
    public float transitionTime;
    private void Start()
    {
        bgMusicSource.clip = firstBgMusic;
        StartCoroutine(PlayBgMusic());
    }

    private IEnumerator PlayBgMusic()
    {
        bgMusicSource.Play();
        yield return new WaitForSeconds(initialDelay);
        StartCoroutine(SwapBGMusic(intenseBgMusic));
    }
    public IEnumerator SwapBGMusic(AudioClip newSong)
    {
        float elapsed = 0f;
        float originalVolume = bgMusicSource.volume;
        //Fade out old song
        while (elapsed < transitionTime)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / transitionTime;
            bgMusicSource.volume = (1 - t) * originalVolume;
            yield return null;
        }
        bgMusicSource.Stop();
        bgMusicSource.clip = newSong;

        //Fade in new song
        elapsed = 0f;

        while (elapsed < transitionTime)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / transitionTime;
            bgMusicSource.volume = t * originalVolume;
            yield return null;
        }
        bgMusicSource.Play();
    }
}
