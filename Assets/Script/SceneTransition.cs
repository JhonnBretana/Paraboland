using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [Header("Scene to Load")]
    public string sceneName;

    // Call this method (e.g., from a Button OnClick)
    public void TransitionToScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("Scene name not set in the Inspector!");
        }
    }
}