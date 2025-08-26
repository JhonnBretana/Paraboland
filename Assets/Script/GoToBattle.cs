// // using UnityEngine;
// // using UnityEngine.SceneManagement;

// // public class GoToBattle : MonoBehaviour
// // {
// //     public string sceneToLoad = "EasyQuestion2";
// //     public int questionIndex; // Set this in the Inspector for each house

// //     private void OnTriggerEnter2D(Collider2D other)
// //     {
// //         if (other.CompareTag("Player"))
// //         {
// //             PlayerPrefs.SetInt("CurrentQuestionIndex", questionIndex);
// //             SceneManager.LoadScene(sceneToLoad);
// //         }
// //     }
// // }


// using UnityEngine;
// using UnityEngine.SceneManagement;
// using System.Collections.Generic;

// [RequireComponent(typeof(Collider2D))]
// public class GoToBattle : MonoBehaviour
// {
//     [Header("Target")]
//     public string sceneToLoad = "EasyQuestion2";
//     public int questionIndex; // set per house in Inspector

//     [Header("Transition")]
//     public float fadeToBlack = 0.45f;
//     public float fadeFromBlack = 0.45f;

//     [Header("Question Data")]
//     public int chapter = 1; // 1 to 5, set in Inspector
//     public string difficulty = "Easy"; // "Easy", "Average", "Difficult", set in Inspector

//     private bool isLoading;

//     void Reset()
//     {
//         // make sure this collider is a trigger
//         var col = GetComponent<Collider2D>();
//         if (col) col.isTrigger = true;
//     }

//     private string GetHouseId()
//     {
//         return $"House_{chapter}_{difficulty}_{questionIndex}";
//     }

//     private void OnTriggerEnter2D(Collider2D other)
//     {
//         if (isLoading) return;
//         if (!other.CompareTag("Player")) return;

//         // Prevent entry if healed
//         string houseId = GetHouseId();
//         if (PlayerPrefs.GetInt(houseId + "_Healed", 0) == 1) return;

//         isLoading = true;
//         PlayerPrefs.SetInt("CurrentQuestionIndex", questionIndex);
//         PlayerPrefs.SetInt("CurrentChapter", chapter);
//         PlayerPrefs.SetString("CurrentDifficulty", difficulty);

//         // // --- Randomize questions for this chapter/difficulty ---
//         // int totalQuestions = 5; // Adjust if needed
//         // int[] randomizedIndices = GenerateRandomIndices(totalQuestions);
//         // // if (randomizedIndices.Length == 0)
//         // // {
//         // //     // Optionally show a healed sign or message here
//         // //     return;
//         // // }

//         // Save as comma-separated string
//         // PlayerPrefs.SetString("RandomizedQuestionOrder", string.Join(",", randomizedIndices));
//         PlayerPrefs.SetString("RandomizedQuestionOrder", questionIndex.ToString());
//         PlayerPrefs.Save();
//         // -------------------------------------------------------

//         PlayerPrefs.SetString("PreviousMapScene", UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
//         PlayerPrefs.SetFloat("PlayerSpawnX", other.transform.position.x);
//         PlayerPrefs.SetFloat("PlayerSpawnY", other.transform.position.y);

//         // var col = GetComponent<Collider2D>();
//         // if (col) col.enabled = false;

//         if (TransitionManager.I != null)
//         {
//             TransitionManager.I.LoadSceneWithTransition(sceneToLoad, fadeToBlack, fadeFromBlack);
//         }
//         else
//         {
//             SceneManager.LoadScene(sceneToLoad);
//         }
//     }

//     // Add this helper function:
//     private int[] GenerateRandomIndices(int count)
//     {
//         List<int> availableIndices = new List<int>();
//         for (int i = 0; i < count; i++)
//         {
//             string key = $"Chapter{chapter}_{difficulty}_Q{i}_Correct";
//             if (PlayerPrefs.GetInt(key, 0) == 0) // Only add unanswered questions
//                 availableIndices.Add(i);
//         }

//         // Shuffle available indices
//         for (int i = availableIndices.Count - 1; i > 0; i--)
//         {
//             int j = Random.Range(0, i + 1);
//             int temp = availableIndices[i];
//             availableIndices[i] = availableIndices[j];
//             availableIndices[j] = temp;
//         }

//         return availableIndices.ToArray();
//     }
// }
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class GoToBattle : MonoBehaviour
{
    [Header("Target Scene")]
    public string sceneToLoad = "EasyQuestion2";

    [Header("Question Data (must match your question banks)")]
    public int chapter = 1;                 // 1..5
    public string difficulty = "Easy";      // "Easy", "Average", "Hard"
    public int questionIndex = 0;           // 0..N per chapter/difficulty

    [Header("Transition (optional)")]
    public float fadeToBlack = 0.45f;
    public float fadeFromBlack = 0.45f;

    private bool isLoading;
    private Collider2D doorTrigger;

    private string HealedKey => $"House_{chapter}_{difficulty}_{questionIndex}_Healed";

    void Awake()
    {
        doorTrigger = GetComponent<Collider2D>();
        if (doorTrigger) doorTrigger.isTrigger = true; // door is a trigger
    }

    void Start()
    {
        // Hard-lock the door if healed (can't re-enter)
        LockIfHealed();
    }

    void OnEnable()
    {
        // Also handle cases where the object gets re-enabled
        LockIfHealed();
    }

    private void LockIfHealed()
    {
        bool healed = PlayerPrefs.GetInt(HealedKey, 0) == 1;
        if (doorTrigger) doorTrigger.enabled = !healed; // disable collider once healed
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isLoading) return;
        if (!other.CompareTag("Player")) return;

        // Safety: do nothing if somehow healed state is reached
        if (PlayerPrefs.GetInt(HealedKey, 0) == 1) return;

        isLoading = true;

        // Save context for the question scene
        PlayerPrefs.SetInt("CurrentQuestionIndex", questionIndex);
        PlayerPrefs.SetInt("CurrentChapter", chapter);
        PlayerPrefs.SetString("CurrentDifficulty", difficulty);

        // Save map return point
        PlayerPrefs.SetString("PreviousMapScene", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetFloat("PlayerSpawnX", other.transform.position.x);
        PlayerPrefs.SetFloat("PlayerSpawnY", other.transform.position.y);
        PlayerPrefs.Save();

        // Load with your transition manager if you have one
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
