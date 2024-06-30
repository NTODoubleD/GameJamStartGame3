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

    private void Awake()
    {
        _musicSlider.value = 0.3f;
        _soundSlider.value = 0.3f;
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
        _audioMixer.SetFloat(SoundVolumeCode, ToMixerValue(newValue));
    }

    private void OnMusicValueChanged(float newValue)
    {
        _audioMixer.SetFloat(MusicVolumeCode, ToMixerValue(newValue));
    }
}