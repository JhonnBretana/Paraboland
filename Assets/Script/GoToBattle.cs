// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class GoToBattle : MonoBehaviour
// {
//     public string sceneToLoad = "EasyQuestion2";
//     public int questionIndex; // Set this in the Inspector for each house

//     private void OnTriggerEnter2D(Collider2D other)
//     {
//         if (other.CompareTag("Player"))
//         {
//             PlayerPrefs.SetInt("CurrentQuestionIndex", questionIndex);
//             SceneManager.LoadScene(sceneToLoad);
//         }
//     }
// }


using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class GoToBattle : MonoBehaviour
{
    [Header("Target")]
    public string sceneToLoad = "EasyQuestion2";
    public int questionIndex; // set per house in Inspector

    [Header("Transition")]
    public float fadeToBlack = 0.45f;
    public float fadeFromBlack = 0.45f;

    private bool isLoading;

    void Reset()
    {
        // make sure this collider is a trigger
        var col = GetComponent<Collider2D>();
        if (col) col.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isLoading) return;
        if (!other.CompareTag("Player")) return;

        isLoading = true;
        PlayerPrefs.SetInt("CurrentQuestionIndex", questionIndex);

        // prevent re-entry while loading
        var col = GetComponent<Collider2D>();
        if (col) col.enabled = false;

        // Use your TransitionManager (must be present in a scene and have screenTransitionPrefab assigned)
        if (TransitionManager.I != null)
        {
            TransitionManager.I.LoadSceneWithTransition(sceneToLoad, fadeToBlack, fadeFromBlack);
        }
        else
        {
            // Fallback: direct load if TransitionManager wasn't placed in the scene
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
