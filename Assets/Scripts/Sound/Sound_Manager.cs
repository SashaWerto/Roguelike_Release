using UnityEngine;
using Random = UnityEngine.Random;

public class Sound_Manager : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource musicSource;
    private AudioClip startedMusic;
    private static Sound_Manager soundManager;
    public static Sound_Manager Instance => soundManager;

    private void Start()
    {
        soundManager = this;
        startedMusic = musicSource.clip;
    }

    public void ChangeMusicToMain()
    {
        musicSource.clip = startedMusic;
        musicSource.Play();
    }
    public void ChangeMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }
    public void PlayShot(AudioClip clip)
    {
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(clip);
    }
}
