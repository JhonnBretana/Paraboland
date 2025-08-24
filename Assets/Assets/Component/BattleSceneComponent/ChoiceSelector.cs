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
        int chapter = PlayerPrefs.GetInt("CurrentChapter", 1);
        string difficulty = PlayerPrefs.GetString("CurrentDifficulty", "Easy");

        string[] questions = null;
        string[,] choices = null;
        int[] answers = null;

        switch (chapter)
        {
            case 1:
                switch (difficulty)
                {
                    case "Easy":
                        questions = Chapter1Questions.Easy;
                        choices = Chapter1Choices.Easy;
                        answers = Chapter1Answers.Easy;
                        break;
                    case "Average":
                        questions = Chapter1Questions.Average;
                        choices = Chapter1Choices.Average;
                        answers = Chapter1Answers.Average;
                        break;
                    case "Difficult":
                        questions = Chapter1Questions.Difficult;
                        choices = Chapter1Choices.Difficult;
                        answers = Chapter1Answers.Difficult;
                        break;
                    default:
                        questions = Chapter1Questions.Easy;
                        choices = Chapter1Choices.Easy;
                        answers = Chapter1Answers.Easy;
                        break;
                }
                break;
            case 2:
                switch (difficulty)
                {
                    case "Easy":
                        questions = Chapter2Questions.Easy;
                        choices = Chapter2Choices.Easy;
                        answers = Chapter2Answers.Easy;
                        break;
                    case "Average":
                        questions = Chapter2Questions.Average;
                        choices = Chapter2Choices.Average;
                        answers = Chapter2Answers.Average;
                        break;
                    case "Difficult":
                        questions = Chapter2Questions.Difficult;
                        choices = Chapter2Choices.Difficult;
                        answers = Chapter2Answers.Difficult;
                        break;
                    default:
                        questions = Chapter2Questions.Easy;
                        choices = Chapter2Choices.Easy;
                        answers = Chapter2Answers.Easy;
                        break;
                }
                break;
            case 3:
                switch (difficulty)
                {
                    case "Easy":
                        questions = Chapter3Questions.Easy;
                        choices = Chapter3Choices.Easy;
                        answers = Chapter3Answers.Easy;
                        break;
                    case "Average":
                        questions = Chapter3Questions.Average;
                        choices = Chapter3Choices.Average;
                        answers = Chapter3Answers.Average;
                        break;
                    case "Difficult":
                        questions = Chapter3Questions.Difficult;
                        choices = Chapter3Choices.Difficult;
                        answers = Chapter3Answers.Difficult;
                        break;
                    default:
                        questions = Chapter3Questions.Easy;
                        choices = Chapter3Choices.Easy;
                        answers = Chapter3Answers.Easy;
                        break;
                }
                break;
            case 4:
                switch (difficulty)
                {
                    case "Easy":
                        questions = Chapter4Questions.Easy;
                        choices = Chapter4Choices.Easy;
                        answers = Chapter4Answers.Easy;
                        break;
                    case "Average":
                        questions = Chapter4Questions.Average;
                        choices = Chapter4Choices.Average;
                        answers = Chapter4Answers.Average;
                        break;
                    case "Difficult":
                        questions = Chapter4Questions.Difficult;
                        choices = Chapter4Choices.Difficult;
                        answers = Chapter4Answers.Difficult;
                        break;
                    default:
                        questions = Chapter4Questions.Easy;
                        choices = Chapter4Choices.Easy;
                        answers = Chapter4Answers.Easy;
                        break;
                }
                break;
            case 5:
                switch (difficulty)
                {
                    case "Easy":
                        questions = Chapter5Questions.Easy;
                        choices = Chapter5Choices.Easy;
                        answers = Chapter5Answers.Easy;
                        break;
                    case "Average":
                        questions = Chapter5Questions.Average;
                        choices = Chapter5Choices.Average;
                        answers = Chapter5Answers.Average;
                        break;
                    case "Difficult":
                        questions = Chapter5Questions.Difficult;
                        choices = Chapter5Choices.Difficult;
                        answers = Chapter5Answers.Difficult;
                        break;
                    default:
                        questions = Chapter5Questions.Easy;
                        choices = Chapter5Choices.Easy;
                        answers = Chapter5Answers.Easy;
                        break;
                }
                break;
        }

        questionText.text = questions[questionIndex];
        for (int i = 0; i < choiceButtons.Length; i++)
        {
            TMP_Text btnText = choiceButtons[i].GetComponentInChildren<TMP_Text>();
            if (btnText != null)
                btnText.text = choices[questionIndex, i];
        }
        correctAnswerIndex = answers[questionIndex];

        ShowDialog();

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
            SetObjectsToHideActive(false);
            return;
        }
    }

    void CheckAnswer(int index)
    {
        if (index == correctAnswerIndex)
        {
            SaveCorrectAnswer();
            if (resultModalWin != null) resultModalWin.SetActive(true);
            MusicEffectsManager.PlayWinBGM(); // Play win BGM
        }
        else
        {
            if (resultModalLost != null) resultModalLost.SetActive(true);
            MusicEffectsManager.PlayLoseBGM(); // Play lose BGM
        }
    }

    void SaveCorrectAnswer()
    {
        int chapter = PlayerPrefs.GetInt("CurrentChapter", 1);
        string difficulty = PlayerPrefs.GetString("CurrentDifficulty", "Easy");
        int questionIndex = PlayerPrefs.GetInt("CurrentQuestionIndex", 0);

        string key = $"Chapter{chapter}_{difficulty}_Q{questionIndex}_Correct";
        PlayerPrefs.SetInt(key, 1); // 1 = correct
        PlayerPrefs.Save();
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