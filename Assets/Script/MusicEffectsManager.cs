using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicEffectsManager : MonoBehaviour
{
    public AudioClip buttonClickSFX;
    public AudioClip winSFX;
    public AudioClip loseSFX;

    [Header("Background Music")]
    public AudioClip mainMenuBGM;
    public AudioClip chapterSelectionBGM;
    public AudioClip characterSelectionBGM;
    public AudioClip mapBGM;
    public AudioClip battleBGM;

    private AudioSource audioSource;
    private static MusicEffectsManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = true;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayBackgroundMusicForScene(scene.name);
    }

        public static float SFXVolume
    {
        get => instance != null ? instance.audioSource.volume : 1f;
        set
        {
            if (instance != null)
                instance.audioSource.volume = Mathf.Clamp01(value);
        }
    }

    private void PlayBackgroundMusicForScene(string sceneName)
    {
        AudioClip bgmToPlay = null;

        if (sceneName == "MainMenu")
            bgmToPlay = mainMenuBGM;
        else if (sceneName == "ChapterSelection")
            bgmToPlay = chapterSelectionBGM;
        else if (sceneName == "CharacterCreation")
            bgmToPlay = characterSelectionBGM;
        else if (sceneName.StartsWith("Chapter")) // e.g., Chapter1, Chapter2, etc.
            bgmToPlay = mapBGM;
        else if (sceneName == "EasyQuestion2")
            bgmToPlay = battleBGM;

        if (audioSource.clip != bgmToPlay)
        {
            audioSource.Stop();
            audioSource.clip = bgmToPlay;
            if (bgmToPlay != null)
                audioSource.Play();
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

    public static void PlayWinBGM()
    {
        if (instance != null && instance.winSFX != null)
        {
            instance.audioSource.Stop();
            instance.audioSource.clip = instance.winSFX;
            instance.audioSource.loop = false;
            instance.audioSource.Play();
        }
    }

    public static void PlayLoseBGM()
    {
        if (instance != null && instance.loseSFX != null)
        {
            instance.audioSource.Stop();
            instance.audioSource.clip = instance.loseSFX;
            instance.audioSource.loop = false;
            instance.audioSource.Play();
        }
    }
}