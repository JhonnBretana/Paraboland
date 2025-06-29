using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToBattle : MonoBehaviour
{
    [Tooltip("The name of the scene to load when the player collides with this object.")]
    public string sceneToLoad; // Set this in the Inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object colliding with this trigger is tagged as 'Player'
        if (other.CompareTag("Player"))
        {
            // Load the target scene
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
