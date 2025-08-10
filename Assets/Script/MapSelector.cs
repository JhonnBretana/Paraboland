using UnityEngine;
using UnityEngine.UI; // optional (only if you assign the button to lock clicks)

public class MapSelector : MonoBehaviour
{
    [Header("Inputs")]
    public SwipeSnap swipeSnap;       // drag your carousel/SwipeSnap here
    public string[] sceneNames;       // names match the carousel order

    [Header("Transition (seconds)")]
    public float fadeToBlack = 0.45f;
    public float fadeFromBlack = 0.45f;

    [Header("Optional")]
    public Button selectButton;       // assign to prevent double taps

    bool isLoading;

    public void OnSelectMap()
    {
        if (isLoading) return;

        int index = (swipeSnap != null) ? swipeSnap.currentIndex : -1;
        if (index >= 0 && index < sceneNames.Length && !string.IsNullOrEmpty(sceneNames[index]))
        {
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
