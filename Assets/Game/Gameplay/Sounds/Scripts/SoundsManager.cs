using DoubleDTeam.Attributes;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundsManager : MonoBehaviour
{
    public static SoundsManager Instance;

    public AudioClip[] sounds;
    public SoundArrays[] randSound;

    [SerializeField, ReadOnlyProperty] private AudioSource audioSrc;

    [SerializeField] private float _soundsVolume = 1;

    private void OnValidate()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void PlaySound(int i, Vector3 pos = new(), bool random = false, bool destroyed = false, float p1 = 0.85f,
        float p2 = 1.2f)
    {
        AudioClip clip = random ? randSound[i].soundArray[Random.Range(0, randSound[i].soundArray.Length)] : sounds[i];
        audioSrc.pitch = Random.Range(p1, p2);

        if (destroyed)
            AudioSource.PlayClipAtPoint(clip, pos, _soundsVolume);
        else
            audioSrc.PlayOneShot(clip, _soundsVolume);
    }

    public void PlaySound(AudioClip sound, Vector3 pos = new(), bool destroyed = false, float p1 = 0.85f,
        float p2 = 1.2f)
    {
        AudioClip clip = sound;
        audioSrc.pitch = Random.Range(p1, p2);

        if (destroyed)
            AudioSource.PlayClipAtPoint(clip, pos, _soundsVolume);
        else
            audioSrc.PlayOneShot(clip, _soundsVolume);
    }

    public void PlayFootstepSound(Vector3 pos) =>
        PlaySound(0, pos);
    public void PlayMeat1(Vector3 pos) =>
        PlaySound(1, pos);
    public void PlayMeat2(Vector3 pos) =>
        PlaySound(2, pos);
    public void PlayMeat3(Vector3 pos) =>
        PlaySound(3, pos);
    public void PlayGetMoch(Vector3 pos) =>
        PlaySound(4, pos);
    
    public void PlayNewbornOlen(Vector3 pos) =>
        PlaySound(5, pos);
    public void PlayKillOlen(Vector3 pos) =>
        PlaySound(6, pos);
    
    public void PlayDeerEat(Vector3 pos) =>
        PlaySound(7, pos);

    [System.Serializable]
    public class SoundArrays
    {
        public AudioClip[] soundArray;
    }
}