using UnityEngine;
using UnityEngine.UI;

public class MapSelector : MonoBehaviour
{
    [Header("Inputs")]
    public SwipeSnap swipeSnap;
    public string[] sceneNames;

    [Header("Transition (seconds)")]
    public float fadeToBlack = 0.45f;
    public float fadeFromBlack = 0.45f;

    [Header("UI References")]
    public Image[] mapImages;      // Assign each map card's Image in Inspector (order matches sceneNames)
    public GameObject[] checkUIs;  // Assign each chapter's check UI in Inspector (order matches sceneNames)

    [Header("Optional")]
    public Button selectButton;

    bool isLoading;

    void Start()
    {
        for (int i = 0; i < sceneNames.Length; i++)
        {
            int chapterNum = i + 1;
            bool unlocked = ChapterProgress.IsChapterUnlocked(chapterNum);

            // Check if completed (Hard difficulty transition reached)
            bool completed = PlayerPrefs.GetInt($"Chapter{chapterNum}_Hard_Completed", 0) == 1;

            // Set check UI active if completed
            if (checkUIs != null && i < checkUIs.Length && checkUIs[i] != null)
                checkUIs[i].SetActive(completed);

            // Set opacity
            if (mapImages != null && i < mapImages.Length && mapImages[i] != null)
            {
                var color = mapImages[i].color;
                if (!unlocked)
                    color.a = 0.5f; // locked
                else if (completed)
                    color.a = 0.7f; // completed
                else
                    color.a = 1f;   // normal
                mapImages[i].color = color;
            }
        }
    }

    public void OnSelectMap()
    {
        if (isLoading) return;

        int index = (swipeSnap != null) ? swipeSnap.currentIndex : -1;
        if (index >= 0 && index < sceneNames.Length && !string.IsNullOrEmpty(sceneNames[index]))
        {
            // Only allow selection if unlocked
            if (!ChapterProgress.IsChapterUnlocked(index + 1))
            {
                Debug.Log("Chapter locked!");
                return;
            }

            isLoading = true;
            if (selectButton) selectButton.interactable = false;

            string scene = sceneNames[index];
            Debug.Log($"Transition → {scene}");
            TransitionManager.I.LoadSceneWithTransition(scene, fadeToBlack, fadeFromBlack);
        }
        else
        {
            Debug.LogWarning("Invalid map index or missing scene name.");
        }
    }
}
