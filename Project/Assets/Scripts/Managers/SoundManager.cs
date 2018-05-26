using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    private static SoundManager instance = null;
    private float currentBackgroundTime = 0;
    public AudioSource audioSource;
    public AudioClip zoomInBackgroundMusic;
    public AudioClip zoomOutBackgroundMusic;
    public AudioClip zoomInSoundEffect;
    public AudioClip zoomOutSoundEffect;
    public AudioClip rotateSoundEffect;

    public static SoundManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //StartCoroutine(playZoomInAction());
        PlayZoomInBackgroundMusic();
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
        if(audioSource.clip == zoomInBackgroundMusic || audioSource.clip == zoomOutBackgroundMusic)
        {
            currentBackgroundTime = audioSource.time;
            Debug.Log(currentBackgroundTime);
        }
    }

    public void PlayZoomInBackgroundMusic()
    {
        audioSource.clip = zoomInBackgroundMusic;
        audioSource.Play();
    }

    public void PlayZoomOutBackgroundMusic()
    {
        playZoomOutSoundEffect();
        audioSource.Stop();
        audioSource.clip = zoomOutBackgroundMusic;
        audioSource.Play();
    }

    private void playZoomInSoundEffect()
    {
        //StartCoroutine(playSound(zoomInSoundEffect));
    }

    private void playZoomOutSoundEffect()
    {
        audioSource.PlayOneShot(zoomOutSoundEffect);
    }

    public void playZoomInAction()
    {
        StartCoroutine(playZoomInActionCoroutine());
    }

    public void playZoomOutAction()
    {
        StartCoroutine(playZoomOutActionCoroutine());
    }

    IEnumerator playZoomInActionCoroutine()
    {
        audioSource.clip = zoomInSoundEffect;
        audioSource.Play();
        yield return new WaitForSeconds(zoomInSoundEffect.length);
        audioSource.clip = zoomInBackgroundMusic;
        audioSource.time = currentBackgroundTime;
        audioSource.Play();
    }

    IEnumerator playZoomOutActionCoroutine()
    {
        audioSource.clip = zoomInSoundEffect;
        audioSource.Play();
        yield return new WaitForSeconds(zoomInSoundEffect.length);
        audioSource.clip = zoomOutBackgroundMusic;
        audioSource.time = currentBackgroundTime;
        audioSource.Play();
    }

}
