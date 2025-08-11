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

    [Header("Question Data")]
    public int chapter = 1; // 1 to 5, set in Inspector
    public string difficulty = "Easy"; // "Easy", "Average", "Difficult", set in Inspector

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
        PlayerPrefs.SetInt("CurrentChapter", chapter);
        PlayerPrefs.SetString("CurrentDifficulty", difficulty);

        // Store the current scene name as the previous map
        PlayerPrefs.SetString("PreviousMapScene", UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);

        // prevent re-entry while loading
        var col = GetComponent<Collider2D>();
        if (col) col.enabled = false;

        if (TransitionManager.I != null)
        {
            TransitionManager.I.LoadSceneWithTransition(sceneToLoad, fadeToBlack, fadeFromBlack);
        }
        else
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
