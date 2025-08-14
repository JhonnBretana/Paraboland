using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class GoToMap : MonoBehaviour
{
    [Header("Target Map")]
    public string mapSceneToLoad = "MapScene2"; // Set this in Inspector

    [Header("Transition")]
    public float fadeToBlack = 0.45f;
    public float fadeFromBlack = 0.45f;

    private bool isLoading;

    void Reset()
    {
        var col = GetComponent<Collider2D>();
        if (col) col.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isLoading) return;
        if (!other.CompareTag("Player")) return;

        isLoading = true;

        // Store the current scene name as the previous map
        PlayerPrefs.SetString("PreviousMapScene", SceneManager.GetActiveScene().name);

        // Prevent re-entry while loading
        var col = GetComponent<Collider2D>();
        if (col) col.enabled = false;

        if (TransitionManager.I != null)
        {
            TransitionManager.I.LoadSceneWithTransition(mapSceneToLoad, fadeToBlack, fadeFromBlack);
        }
        else
        {
            SceneManager.LoadScene(mapSceneToLoad);
        }
    }
}