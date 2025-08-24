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

        // --- Randomize questions for this chapter/difficulty ---
        int totalQuestions = 5; // Adjust if needed
        int[] randomizedIndices = GenerateRandomIndices(totalQuestions);
        // Save as comma-separated string
        PlayerPrefs.SetString("RandomizedQuestionOrder", string.Join(",", randomizedIndices));
        PlayerPrefs.Save();
        // -------------------------------------------------------

        PlayerPrefs.SetString("PreviousMapScene", UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);

        PlayerPrefs.SetFloat("PlayerSpawnX", other.transform.position.x);
        PlayerPrefs.SetFloat("PlayerSpawnY", other.transform.position.y);

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

    // Add this helper function:
    private int[] GenerateRandomIndices(int count)
    {
        int[] indices = new int[count];
        for (int i = 0; i < count; i++) indices[i] = i;
        for (int i = count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            int temp = indices[i];
            indices[i] = indices[j];
            indices[j] = temp;
        }
        return indices;
    }
}
