using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public AudioClip landingPageMusic;
    public AudioClip battleMusic;
    public AudioClip mapMusic;

    private AudioSource audioSource;
    private static MusicManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (IsLandingPageScene(scene.name))
        {
            PlayMusic(landingPageMusic);
        }
        else if (scene.name == "EasyQuestion2")
        {
            PlayMusic(battleMusic);
        }
        else if (IsMapScene(scene.name))
        {
            PlayMusic(mapMusic);
        }
        else
        {
            audioSource.Stop();
        }
    }

    bool IsLandingPageScene(string sceneName)
    {
        return sceneName == "LandingPage" ||
               sceneName == "MainMenu" ||
               sceneName == "CharacterCreation" ||
               sceneName == "ChapterSelection";
    }


    public static float MusicVolume
    {
        get => instance != null ? instance.audioSource.volume : 1f;
        set
        {
            if (instance != null)
                instance.audioSource.volume = Mathf.Clamp01(value);
        }
    }


    bool IsMapScene(string sceneName)
    {
        // Checks if the scene name matches your map naming convention
        return sceneName.StartsWith("Chapter") &&
               (sceneName.Contains("_Easy") || sceneName.Contains("_Medium") || sceneName.Contains("_Hard"));
    }

    void PlayMusic(AudioClip clip)
    {
        if (audioSource.clip == clip) return;
        audioSource.clip = clip;
        audioSource.Play();
    }
}