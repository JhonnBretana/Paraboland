using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Add this

public class SettingsManager : MonoBehaviour
{
    [Header("References")]
    public GameObject settingsModal; // Assign your SettingsModal GameObject here

    // Call this from the Settings button OnClick()
    public void OpenSettings()
    {
        if (settingsModal != null)
            settingsModal.SetActive(true);
    }

    // Call this from the Close button inside the modal
    public void CloseSettings()
    {
        if (settingsModal != null)
            settingsModal.SetActive(false);
    }

    // Call this from the Clear Records button OnClick()
    public void ClearRecords()
    {
        PlayerPrefs.DeleteAll(); // Clears all PlayerPrefs (character selection, answers, etc.)
        PlayerPrefs.Save();
        Debug.Log("All records cleared!");
    }

    // Call this from the Exit App button OnClick()
    public void ExitApp()
    {
        Application.Quit();
        Debug.Log("Exiting app...");
    }

    // Call this from the Go Home button OnClick()
    public void GoHome()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}