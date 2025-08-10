using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TransitionManager : MonoBehaviour
{
    // Drag your ScreenTransition prefab here in the Inspector
    public ScreenFader screenTransitionPrefab;

    private static TransitionManager _instance;
    public static TransitionManager I => _instance;

    private ScreenFader fader;

    void Awake()
    {
        if (_instance != null && _instance != this) { Destroy(gameObject); return; }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void EnsureFader()
    {
        if (fader) return;

        if (!screenTransitionPrefab)
        {
            Debug.LogError("TransitionManager: Please assign 'screenTransitionPrefab' in the Inspector.");
            return;
        }

        var inst = Instantiate(screenTransitionPrefab.gameObject);
        fader = inst.GetComponent<ScreenFader>();
        if (!fader) Debug.LogError("ScreenTransition prefab is missing ScreenFader on the root.");
    }

    public void LoadSceneWithTransition(string sceneName, float? fadeIn = null, float? fadeOut = null)
    {
        EnsureFader(); if (!fader) return;
        StartCoroutine(CoLoad(sceneName, fadeIn, fadeOut));
    }

    IEnumerator CoLoad(string sceneName, float? fadeIn, float? fadeOut)
    {
        yield return fader.FadeToBlack(fadeIn);
        var op = SceneManager.LoadSceneAsync(sceneName);
        while (!op.isDone) yield return null;
        yield return fader.FadeFromBlack(fadeOut);
    }

    // Optional utility: fade around any action without scene load
    public void DoTransition(System.Action midAction, float? fadeIn = null, float? fadeOut = null)
    {
        EnsureFader(); if (!fader) return;
        StartCoroutine(CoDo(midAction, fadeIn, fadeOut));
    }
    IEnumerator CoDo(System.Action midAction, float? fadeIn, float? fadeOut)
    {
        yield return fader.FadeToBlack(fadeIn);
        midAction?.Invoke();
        yield return fader.FadeFromBlack(fadeOut);
    }
}
