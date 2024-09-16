using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    //references
    [Header ("Audio Mixer")]
    [SerializeField] private AudioMixer audioMixer;

    [Header ("Sliders")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider SFXSlider;
    [SerializeField] private Slider musicSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("MasterVolume"))  LoadAudioPrefs(); else  SetAudioPrefs();
    }
    #region Audio Slider

    //Setting up Master Audio
    public void SetMasterAudio()
    {
        float masterVolume = masterSlider.value;
        audioMixer.SetFloat("Master", Mathf.Log10(masterVolume) * 20);
        PlayerPrefs.SetFloat("MasterVolume", masterVolume);
    }

    //Setting up SFX Audio
    public void SetSFXAudio()
    {
        float SFXvolume = SFXSlider.value;
        audioMixer.SetFloat("SFX", Mathf.Log10(SFXvolume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", SFXvolume);
    }

    //Setting up Music Audio
    public void SetMusicAudio()
    {
        float musicVolume = musicSlider.value;
        audioMixer.SetFloat("Music", Mathf.Log10(musicVolume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
    }

    #endregion

    //Loading up player prefs audio 
    private void LoadAudioPrefs()
    {
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        SetMasterAudio();

        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        SetMusicAudio();

        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        SetSFXAudio();
    }

    //Setting up player prefs audio
    private void SetAudioPrefs()
    {
        SetMasterAudio();
        SetMusicAudio();
        SetSFXAudio();
    }
}
