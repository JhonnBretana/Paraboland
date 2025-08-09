using UnityEngine;

public class ShowDialogOnQuestionClick : MonoBehaviour
{
    public GameObject dialogBox; // Assign your Dialog Box in the Inspector

    void Start()
    {
        if (dialogBox != null)
            dialogBox.SetActive(false); // Hide at start
    }

    public void ShowDialog()
    {
        if (dialogBox != null)
            dialogBox.SetActive(true); // Show on click
    }
}