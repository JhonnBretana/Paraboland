using UnityEngine;

public class QuestionDialogTrigger : MonoBehaviour
{
    public GameObject dialogBox;
    public ChoiceSelector choiceSelector; // Assign in Inspector

    void Start()
    {
        ShowDialog();
    }

    public void ShowDialog()
    {
        if (dialogBox != null)
            dialogBox.SetActive(true);

        if (choiceSelector != null)
            choiceSelector.SetObjectsToHideActive(false); // Hide choices etc.
    }

    public void CloseDialog()
    {
        if (choiceSelector != null)
        {
            choiceSelector.SetObjectsToHideActive(true); // Show choices etc. FIRST
            choiceSelector.gameObject.SetActive(true);
        }
        if (dialogBox != null)
            dialogBox.SetActive(false);
    }
}