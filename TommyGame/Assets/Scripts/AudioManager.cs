using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource bgMusicSource;
    public AudioClip firstBgMusic;
    public AudioClip intenseBgMusic;
    public float initialDelay;
    public float transitionTime;

    private List<AudioSource> pool = new List<AudioSource>();
    [SerializeField] private int poolSize = 10;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        InitializePool();   
    }

    private void Start()
    {
        bgMusicSource.clip = firstBgMusic;
        StartCoroutine(PlayBgMusic());
    }

    private IEnumerator PlayBgMusic()
    {
        bgMusicSource.Play();
        yield return new WaitForSeconds(initialDelay);
        if(intenseBgMusic != null)
        {
            StartCoroutine(SwapBGMusic(intenseBgMusic));
        }        
    }

    public IEnumerator StopMusic()
    {
        if(bgMusicSource.isPlaying == false)
        {
            yield break;
        }
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

    public void PlaySFX(AudioClip clip, float volume)
    {
        foreach (AudioSource source in pool)
        {
            if (!source.isPlaying)
            {
                source.clip = clip;
                source.volume = volume;
                source.Play();
                return;
            }
        }
    }

    private void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = new GameObject("AudioSource_" + i);
            obj.transform.parent = this.transform;
            AudioSource audioSource = obj.AddComponent<AudioSource>();
            pool.Add(audioSource);
        }
    }
}
