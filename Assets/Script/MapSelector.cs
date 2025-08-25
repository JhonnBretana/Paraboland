using UnityEngine;
using UnityEngine.UI;

public class MapSelector : MonoBehaviour
{
    [Header("Inputs")]
    public SwipeSnap swipeSnap;       // drag your carousel/SwipeSnap here
    public string[] sceneNames;       // names match the carousel order

    [Header("Transition (seconds)")]
    public float fadeToBlack = 0.45f;
    public float fadeFromBlack = 0.45f;

    [Header("UI References")]
    public Image[] mapImages;      // Assign each map card's Image in Inspector (order matches sceneNames)

    [Header("Optional")]
    public Button selectButton;       // assign to prevent double taps

    bool isLoading;

    void Start()
    {
        // Apply unlocked/locked color
        for (int i = 0; i < sceneNames.Length; i++)
        {
            int chapterNum = i + 1;
            bool unlocked = ChapterProgress.IsChapterUnlocked(chapterNum);

            if (mapImages != null && i < mapImages.Length && mapImages[i] != null)
            {
                var color = mapImages[i].color;
                color.a = unlocked ? 1f : 0.5f; // 1 = fully visible, 0.5 = greyed out
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
