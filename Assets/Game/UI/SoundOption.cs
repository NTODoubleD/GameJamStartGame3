using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundOption : MonoBehaviour
{
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _soundSlider;
    [SerializeField] private AudioMixer _audioMixer;

    private const string MusicVolumeCode = "MusicVolume";
    private const string SoundVolumeCode = "SoundVolume";

    private const string MusicPrefKey = "MusicVolumePref";
    private const string SoundPrefKey = "SoundVolumePref";

    private void Awake()
    {
        var musicVolume = PlayerPrefs.GetFloat(MusicPrefKey, 0.3f);
        var soundVolume = PlayerPrefs.GetFloat(SoundPrefKey, 0.3f);

        _musicSlider.value = musicVolume;
        _soundSlider.value = soundVolume;
        
        OnMusicValueChanged(musicVolume);
        OnSoundValueChanged(soundVolume);
    }

    private void OnEnable()
    {
        _musicSlider.onValueChanged.AddListener(OnMusicValueChanged);
        _soundSlider.onValueChanged.AddListener(OnSoundValueChanged);
    }

    private void OnDisable()
    {
        _musicSlider.onValueChanged.RemoveListener(OnMusicValueChanged);
        _soundSlider.onValueChanged.RemoveListener(OnSoundValueChanged);
    }

    private float ToMixerValue(float value)
        => Mathf.Log10(value) * 20;

    private void OnSoundValueChanged(float newValue)
    {
        var calculatedValue = newValue == _soundSlider.minValue ? -80 : newValue;
        _audioMixer.SetFloat(SoundVolumeCode, ToMixerValue(calculatedValue));

        PlayerPrefs.SetFloat(SoundPrefKey, newValue);
    }

    private void OnMusicValueChanged(float newValue)
    {
        var calculatedValue = newValue == _musicSlider.minValue ? -80 : newValue;
        _audioMixer.SetFloat(MusicVolumeCode, ToMixerValue(calculatedValue));

        PlayerPrefs.SetFloat(MusicPrefKey, newValue);
    }
}