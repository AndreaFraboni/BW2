using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public AudioMixer _mixer;

    [Header("Settings")]
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _SFXSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip != null)
        {
            _musicSource.clip = clip;
            _musicSource.Play();
        }
    }
    
    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            _SFXSource.PlayOneShot(clip);
        }
    }

    public void StopAudio()
    {
        _musicSource?.Stop();
    }

    public void StopAllAudioSource()
    {
        if (_musicSource.isPlaying) _musicSource.Stop();
        if (_SFXSource.isPlaying) _SFXSource.Stop();
    }

    public void SetSliderValue(Slider slider, string group)
    {
        if (_mixer.GetFloat(group, out float decibel))
        {
            float percentage = Mathf.Pow(10, decibel / 20);
            slider.value = percentage;
        }
    }

    public float GetSliderValue(Slider slider)
    {
        float value = Mathf.Max(slider.value, 0.0001f);
        float decibel = Mathf.Log10(value) * 20f;

        return decibel;
    }

    public void SetVolume(float value, string group)
    {
        if (value > 0.01f)
        {
            float volume = Mathf.Log10(value) * 20;
            _mixer.SetFloat(group, volume);
        }
        else
        {
            _mixer.SetFloat(group, -80f);
        }
    }
    public void SetMasterVolume(float value)
    {
        SetVolume(value, "Master");
    }

    public void SetMusicVolume(float value)
    {
        SetVolume(value, "Music");
    }

    public void SetSFXVolume(float value)
    {
        SetVolume(value, "SFX");
    }


}
