// This line imports Unity's basic functions and classes
using UnityEngine;
// This line imports Unity's scene management functions (loading, switching scenes)
using UnityEngine.SceneManagement;

// This creates a public class called SceneController that inherits from MonoBehaviour
// MonoBehaviour allows this script to be attached to GameObjects in Unity
public class SceneController : MonoBehaviour
{
    // [Header] creates a title in the Unity Inspector window
    [Header("Scene Name Input")]
    // [SerializeField] makes this private variable visible in the Inspector
    // This string variable will hold the name of the scene we want to load
    [SerializeField] private string sceneToLoad = "";

    // #region helps organize code into collapsible sections
    #region Scene Loading

    /// <summary>
    /// This method loads a scene using the scene name you typed in the Inspector
    /// You can connect this to a button's OnClick event
    /// </summary>
    public void LoadSceneFromInput()
    {
        // Check if the sceneToLoad variable is not empty or null
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            // If there's a scene name, call the LoadScene method with that name
            LoadScene(sceneToLoad);
        }
        else // If the scene name is empty
        {
            // Print an error message to Unity's Console window
            Debug.LogError("Scene name is empty! Please enter a scene name in the inspector.");
        }
    }

    /// <summary>
    /// This method loads any scene by its name
    /// You can call this method directly from button events
    /// </summary>
    /// <param name="sceneName">The exact name of the scene you want to load</param>
    public void LoadScene(string sceneName)
    {
        // Check if the provided scene name is empty or null
        if (string.IsNullOrEmpty(sceneName))
        {
            // If empty, show error message and stop the method
            Debug.LogError("Scene name is null or empty!");
            return; // Exit the method early
        }

        // Print a message to the Console showing which scene is being loaded
        Debug.Log($"Loading scene: {sceneName}");
        // Actually load the scene using Unity's SceneManager
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// This method reloads/restarts the current scene
    /// Useful for "Restart Level" buttons
    /// </summary>
    public void ReloadCurrentScene()
    {
        // Get information about the currently active scene
        Scene currentScene = SceneManager.GetActiveScene();
        // Load the same scene again using its name (this restarts it)
        LoadScene(currentScene.name);
    }

    /// <summary>
    /// This method closes the game application
    /// Use this for "Quit Game" buttons
    /// </summary>
    public void QuitGame()
    {
        // #if UNITY_EDITOR means "only do this when testing in Unity Editor"
        #if UNITY_EDITOR
        // Stop playing the game in Unity Editor
        UnityEditor.EditorApplication.isPlaying = false;
        #else // #else means "do this when the game is built as an executable"
        // Close the application completely
        Application.Quit();
        #endif // End of the conditional compilation
    }

    // End of the Scene Loading region
    #endregion
} // End of the SceneController class