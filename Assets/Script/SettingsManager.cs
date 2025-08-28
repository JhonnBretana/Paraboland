using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    [Header("References")]
    public GameObject settingsModal; // Assign your SettingsModal GameObject here
    public GameObject portalGuideModal; // Assign your PortalGuideModal GameObject here

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
        PlayerPrefs.DeleteAll();
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
        SceneManager.LoadScene("MainMenu");
    }

    // Call this to open the portal guide modal
    public void OpenPortalGuide()
    {
        if (portalGuideModal != null)
            portalGuideModal.SetActive(true);
    }

    // Call this to close the portal guide modal
    public void ClosePortalGuide()
    {
        if (portalGuideModal != null)
            portalGuideModal.SetActive(false);
    }
}