using UnityEngine;

public class MusicEffectsManager : MonoBehaviour
{
    public AudioClip buttonClickSFX;
    public AudioClip winSFX;
    public AudioClip loseSFX;

    private AudioSource audioSource;
    private static MusicEffectsManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = false;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void PlayButtonClick()
    {
        if (instance != null && instance.buttonClickSFX != null)
            instance.audioSource.PlayOneShot(instance.buttonClickSFX);
    }

    public static void PlayWin()
    {
        if (instance != null && instance.winSFX != null)
            instance.audioSource.PlayOneShot(instance.winSFX);
    }

    public static void PlayLose()
    {
        if (instance != null && instance.loseSFX != null)
            instance.audioSource.PlayOneShot(instance.loseSFX);
    }
}