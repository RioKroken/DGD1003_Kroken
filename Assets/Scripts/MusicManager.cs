using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("SFX Clips")]
    public AudioClip coinCollectSound;

    private bool isMuted = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        if (musicSource != null && !musicSource.isPlaying)
        {
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    // ----------- SFX METHODS -----------

    public void PlayCoinSound()
    {
        if (sfxSource != null && coinCollectSound != null)
        {
            sfxSource.PlayOneShot(coinCollectSound);
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (sfxSource != null && clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public void ToggleMute()
    {
        isMuted = !isMuted;

        PlayerPrefs.SetInt("Muted", isMuted ? 1 : 0);
        PlayerPrefs.Save();

        ApplyMuteState();
    }

    private void ApplyMuteState()
    {
        if (musicSource != null)
            musicSource.mute = isMuted;

        if (sfxSource != null)
            sfxSource.mute = isMuted;
    }

    public bool IsMuted()
    {
        return isMuted;
    }
}
