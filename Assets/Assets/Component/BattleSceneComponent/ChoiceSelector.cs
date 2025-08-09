using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChoiceSelector : MonoBehaviour
{
    public Button[] choiceButtons; // Assign your 4 choice buttons in the Inspector
    private int selectedIndex = 0;
    private int pendingIndex = -1; // Store tapped choice index

    public GameObject resultModalWin;
    public GameObject resultModalLost;
    public int correctAnswerIndex = 0; // Set this in the Inspector to the correct choice index (0-3)

    public GameObject[] objectsToHide; // Assign Character, Patient, Dialog Box, Choices Box in Inspector
    public GameObject dialogBox; // Assign Dialog Box in Inspector

    public GameObject confirmationDialog; // Assign Confirmation GameObject in Inspector

    public TMP_Text questionText; // Assign in Inspector

    void Start()
    {
        int questionIndex = PlayerPrefs.GetInt("CurrentQuestionIndex", 0);
        questionText.text = Chapter1Questions.Easy[questionIndex];
        for (int i = 0; i < choiceButtons.Length; i++)
        {
            // Use TMP_Text for button label
            TMP_Text btnText = choiceButtons[i].GetComponentInChildren<TMP_Text>();
            if (btnText != null)
                btnText.text = Chapter1Choices.Easy[questionIndex, i];
        }
        correctAnswerIndex = Chapter1Answers.Easy[questionIndex];

        ShowDialog();

        // HighlightChoice(); // Remove this line
        if (resultModalWin != null) resultModalWin.SetActive(false);
        if (resultModalLost != null) resultModalLost.SetActive(false);
        if (confirmationDialog != null) confirmationDialog.SetActive(false);

        for (int i = 0; i < choiceButtons.Length; i++)
        {
            int index = i;
            choiceButtons[i].onClick.AddListener(() => OnChoiceTapped(index));
        }
    }
    void Update()
    {
        if ((resultModalWin != null && resultModalWin.activeSelf) ||
            (resultModalLost != null && resultModalLost.activeSelf))
        {
            // UnhighlightAllChoices(); // Remove this line
            SetObjectsToHideActive(false);
            return;
        }
    }

    void CheckAnswer(int index)
    {
        if (index == correctAnswerIndex)
        {
            if (resultModalWin != null) resultModalWin.SetActive(true);
        }
        else
        {
            if (resultModalLost != null) resultModalLost.SetActive(true);
        }
    }

    public void SetObjectsToHideActive(bool isActive)
    {
        if (objectsToHide == null) return;
        foreach (var obj in objectsToHide)
        {
            if (obj != null)
            {
                obj.SetActive(isActive);
            }
        }
    }

    public void RetryQuestion()
    {
        if (resultModalWin != null) resultModalWin.SetActive(false);
        if (resultModalLost != null) resultModalLost.SetActive(false);
        SetObjectsToHideActive(true);
        selectedIndex = 0;
        // HighlightChoice(); // Remove this line
    }


    // Called when a choice is tapped
    void OnChoiceTapped(int index)
    {
        pendingIndex = index;
        if (confirmationDialog != null)
            confirmationDialog.SetActive(true);
    }

    // Called by the Submit button in the confirmation dialog
    public void SubmitConfirmedChoice()
    {
        if (pendingIndex >= 0)
        {
            selectedIndex = pendingIndex;
            // HighlightChoice(); // Remove this line
            if (confirmationDialog != null)
                confirmationDialog.SetActive(false);

            CheckAnswer(selectedIndex);
        }
    }

    // Optional: Called by a Cancel button in the confirmation dialog
    public void CancelConfirmation()
    {
        pendingIndex = -1;
        if (confirmationDialog != null)
            confirmationDialog.SetActive(false);
    }

    // --- Merged Dialog Methods ---
    public void ShowDialog()
    {
        if (dialogBox != null)
            dialogBox.SetActive(true);

        SetObjectsToHideActive(false); // Hide choices etc.
    }

    public void CloseDialog()
    {
        SetObjectsToHideActive(true); // Show choices etc. FIRST
        this.gameObject.SetActive(true);
        if (dialogBox != null)
            dialogBox.SetActive(false);
    }
}