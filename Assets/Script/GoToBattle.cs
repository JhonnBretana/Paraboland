using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management functions


public class GoToBattle : MonoBehaviour
{
    [Header("Scene Configuration")]
    [Tooltip("The name of the scene to load when the player collides with this object.")]
    public string sceneToLoad; // This will appear in the Inspector - drag and type the exact scene name


    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object colliding with this trigger has the "Player" tag
        // Unity's CompareTag() is more efficient than using other.tag == "Player"
        if (other.CompareTag("Player"))
        {
            // SceneManager.LoadScene() loads a new scene and unloads the current one
            // This is a simple scene transition - the current scene is completely replaced
            SceneManager.LoadScene(sceneToLoad);
            
            // NOTE: Code after LoadScene may not execute as the current scene is being destroyed
        }
    }
}
