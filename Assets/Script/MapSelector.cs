using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSelector : MonoBehaviour
{
    public SwipeSnap swipeSnap;           // Drag your CarouselManager here
    public string[] sceneNames;           // Set up in Inspector

    public void OnSelectMap()
    {
        int index = swipeSnap.currentIndex;

        if (index >= 0 && index < sceneNames.Length)
        {
            Debug.Log("Loading scene: " + sceneNames[index]);
            SceneManager.LoadScene(sceneNames[index]);
        }
        else
        {
            Debug.LogWarning("Invalid map index or missing scene name.");
        }
    }
}
