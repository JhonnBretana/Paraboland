using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class GoToMap : MonoBehaviour
{
    [Header("Gate Objects")]
    public GameObject gateTransition; // Assign GateTransition in Inspector
    public GameObject gateClose;      // Assign GateClose in Inspector

    [Header("Target Map")]
    public string mapSceneToLoad = "MapScene2"; // Set this in Inspector

    [Header("Transition")]
    public float fadeToBlack = 0.45f;
    public float fadeFromBlack = 0.45f;

    [Header("Unlock Requirement")]
    public int chapter = 1; // Set in Inspector
    public string difficulty = "Easy"; // Set in Inspector
    public int requiredCorrectAnswers = 3;
    public int totalQuestions = 5; // Set to your actual question count

    private bool isLoading;

    void Start()
    {
        int correct = ChapterProgress.CountCorrectAnswers(chapter, difficulty, totalQuestions);

        if (gateTransition != null)
            gateTransition.SetActive(correct >= requiredCorrectAnswers);

        if (gateClose != null)
            gateClose.SetActive(correct < requiredCorrectAnswers);

        // Optionally, disable this object if not unlocked
        // gameObject.SetActive(correct >= requiredCorrectAnswers);
    }

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

        // Reset hearts to 3 when transitioning to another map/difficulty
        PlayerPrefs.SetInt("CurrentHearts", 3);
        PlayerPrefs.Save();

        PlayerPrefs.SetString("PreviousMapScene", SceneManager.GetActiveScene().name);

        var col = GetComponent<Collider2D>();
        if (col) col.enabled = false;

        // Unlock next chapter if on Hard difficulty and transitioning to ChapterSelection
        if (difficulty == "Hard" && mapSceneToLoad == "ChapterSelection")
        {
            ChapterProgress.UnlockChapter(chapter + 1);
        }

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