using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    private static SoundManager instance = null;
    private float currentBackgroundTime = 0;
    public AudioSource zoomInBackgroundAudioSource;
    public AudioSource zoomOutBackgroundAudioSource;
    public AudioSource soundEffectAudioSource;
    public AudioClip zoomInBackgroundMusic;
    public AudioClip zoomOutBackgroundMusic;
    public AudioClip zoomInSoundEffect;
    public AudioClip zoomOutSoundEffect;
    public AudioClip rotateSoundEffect;
    private float zoomInBackgroundMusicVolumeTarget;
    private float zoomOutBackgroundMusicVolumeTarget;
    private float zoomActionSoundEffectLength;
    public static SoundManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Start()
    {
        //StartCoroutine(playZoomInAction());
        zoomInBackgroundAudioSource.clip = zoomInBackgroundMusic;
        zoomOutBackgroundAudioSource.clip = zoomOutBackgroundMusic;
        zoomActionSoundEffectLength = (zoomInSoundEffect.length + zoomOutSoundEffect.length) / 2;
        initPlayBackgroundMusic();
    }

    private void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if(zoomInBackgroundAudioSource.clip == zoomInBackgroundMusic || zoomInBackgroundAudioSource.clip == zoomOutBackgroundMusic)
        {
            currentBackgroundTime = zoomInBackgroundAudioSource.time;
            Debug.Log(currentBackgroundTime);
        }
    }

    private void initPlayBackgroundMusic()
    {
        zoomOutBackgroundAudioSource.volume = 0;
        zoomInBackgroundAudioSource.volume = 1;
        zoomInBackgroundAudioSource.Play();
        zoomOutBackgroundAudioSource.Play();
    }

    private void playZoomInAction()
    {
        soundEffectAudioSource.PlayOneShot(zoomInSoundEffect);
        StartCoroutine(FadeIn(zoomOutBackgroundAudioSource));
        StartCoroutine(FadeOut(zoomInBackgroundAudioSource));
        /*
        soundEffectAudioSource.clip = zoomInSoundEffect;
        soundEffectAudioSource.Play();    
        zoomInBackgroundAudioSource.clip = zoomInBackgroundMusic;
        zoomInBackgroundAudioSource.time = currentBackgroundTime;
        zoomInBackgroundAudioSource.Play();
        */
    }

    private void playZoomOutAction()
    {
        soundEffectAudioSource.PlayOneShot(zoomOutSoundEffect);
        StartCoroutine(FadeIn(zoomInBackgroundAudioSource));
        StartCoroutine(FadeOut(zoomOutBackgroundAudioSource));
        /*
        soundEffectAudioSource.clip = zoomOutSoundEffect;
        soundEffectAudioSource.Play();
        zoomInBackgroundAudioSource.clip = zoomOutBackgroundMusic;
        zoomInBackgroundAudioSource.time = currentBackgroundTime;
        zoomInBackgroundAudioSource.Play();
        */
    }

    public void toggleZoomSoundAction(bool zoomIn)
    {
        if(zoomIn)
        {
            playZoomInAction();
        }
        else
        {
            playZoomOutAction();
        }
    }
    /*
    IEnumerator playZoomInActionCoroutine()
    {
        zoomInBackgroundAudioSource.clip = zoomInSoundEffect;
        zoomInBackgroundAudioSource.Play();
        yield return new WaitForSeconds(zoomInSoundEffect.length);
        zoomInBackgroundAudioSource.clip = zoomInBackgroundMusic;
        zoomInBackgroundAudioSource.time = currentBackgroundTime;
        zoomInBackgroundAudioSource.Play();
    }

    IEnumerator playZoomOutActionCoroutine()
    {
        zoomInBackgroundAudioSource.clip = zoomInSoundEffect;
        zoomInBackgroundAudioSource.Play();
        yield return new WaitForSeconds(zoomInSoundEffect.length);
        zoomInBackgroundAudioSource.clip = zoomOutBackgroundMusic;
        zoomInBackgroundAudioSource.time = currentBackgroundTime;
        zoomInBackgroundAudioSource.Play();
    }
    */

    IEnumerator FadeOut(AudioSource audioSource)
    {
        //float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= Time.deltaTime / zoomActionSoundEffectLength;

            yield return null;
        }

        audioSource.volume = 0;
    }

    IEnumerator FadeIn(AudioSource audioSource)
    {
        //float startVolume = audioSource.volume;

        while (audioSource.volume < 1)
        {
            audioSource.volume += Time.deltaTime / zoomActionSoundEffectLength;

            yield return null;
        }

        audioSource.volume = 1;
    }

}
