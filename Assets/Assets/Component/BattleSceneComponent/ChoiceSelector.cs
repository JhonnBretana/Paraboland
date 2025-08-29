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

    public GameObject[] objectsToHide; // Assign Character, Patient, Dialog Box, Choices Box in Inspector
    public GameObject dialogBox; // Assign Dialog Box in Inspector

    public GameObject confirmationDialog; // Assign Confirmation GameObject in Inspector

    public TMP_Text questionText; // Assign in Inspector

    public GameObject[] heartIcons; // Assign Heart1, Heart2, Heart3 in Inspector

    public GameObject gameOverPanel; // Assign your Game Over Panel in Inspector
    public Button gameOverOkButton;  // Assign OK Button in Inspector

    private int currentHearts = 3; // Default to 3 hearts
    private const string HeartsKey = "CurrentHearts";

    private int chapter;
    private string difficulty;
    private int questionIndex;
    private int[] randomizedOrder;

    private string correctAnswerString;

    void Start()
    {
        chapter = PlayerPrefs.GetInt("CurrentChapter", 1);
        difficulty = PlayerPrefs.GetString("CurrentDifficulty", "Easy");
        questionIndex = PlayerPrefs.GetInt("CurrentQuestionIndex", 0);

        // // Get randomized order from PlayerPrefs
        // string orderStr = PlayerPrefs.GetString("RandomizedQuestionOrder", "");
        // if (!string.IsNullOrEmpty(orderStr))
        // {
        //     string[] parts = orderStr.Split(',');
        //     randomizedOrder = new int[parts.Length];
        //     for (int i = 0; i < parts.Length; i++)
        //         randomizedOrder[i] = int.Parse(parts[i]);
        // }
        // else
        // {
        //     randomizedOrder = new int[] { 0, 1, 2, 3, 4 };
        // }

        // Now you can use chapter, difficulty, questionIndex, and randomizedOrder in your logic
        // Example:
        // int questionIdx = randomizedOrder[currentQuestionIndex];
        // string question = Chapter1Questions.Easy[questionIdx];
        // string[] choices = new string[] {
        //     Chapter1Choices.Easy[questionIdx, 0],
        //     Chapter1Choices.Easy[questionIdx, 1],
        //     Chapter1Choices.Easy[questionIdx, 2],
        //     Chapter1Choices.Easy[questionIdx, 3]
        // };
        // int answerIdx = Chapter1Answers.Easy[questionIdx];
        string[] questions = null;
        string[,] choices = null;
        string[] answers = null;

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
                    case "Hard":
                        questions = Chapter1Questions.Hard;
                        choices = Chapter1Choices.Hard;
                        answers = Chapter1Answers.Hard;
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
                    case "Hard":
                        questions = Chapter2Questions.Hard;
                        choices = Chapter2Choices.Hard;
                        answers = Chapter2Answers.Hard;
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
                    case "Hard":
                        questions = Chapter3Questions.Hard;
                        choices = Chapter3Choices.Hard;
                        answers = Chapter3Answers.Hard;
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
                    case "Hard":
                        questions = Chapter4Questions.Hard;
                        choices = Chapter4Choices.Hard;
                        answers = Chapter4Answers.Hard;
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
                    case "Hard":
                        questions = Chapter5Questions.Hard;
                        choices = Chapter5Choices.Hard;
                        answers = Chapter5Answers.Hard;
                        break;
                    default:
                        questions = Chapter5Questions.Easy;
                        choices = Chapter5Choices.Easy;
                        answers = Chapter5Answers.Easy;
                        break;
                }
                break;
        }

        // // Check for available questions
        // if (randomizedOrder.Length == 0 || questionIndex >= randomizedOrder.Length)
        // {
        //     // No available questions, show a message or exit
        //     questionText.text = "All questions answered!";
        //     foreach (var btn in choiceButtons)
        //         btn.gameObject.SetActive(false);
        //     return;
        // }

        // Use questionIndex directly, not randomizedOrder
        if (questions == null || questionIndex < 0 || questionIndex >= questions.Length)
        {
            questionText.text = "All questions answered!";
            foreach (var btn in choiceButtons)
                btn.gameObject.SetActive(false);
            return;
        }

        // int actualIndex = randomizedOrder[questionIndex];
        questionText.text = questions[questionIndex];

        // --- Shuffle choices ---
        string[] originalChoices = new string[choiceButtons.Length];
        for (int i = 0; i < choiceButtons.Length; i++)
            originalChoices[i] = choices[questionIndex, i];

        string correctAnswer = answers[questionIndex]; // Now a string!
        string[] shuffledChoices;
        ShuffleChoices(originalChoices, correctAnswer, out shuffledChoices, out correctAnswerString);

        for (int i = 0; i < choiceButtons.Length; i++)
        {
            TMP_Text btnText = choiceButtons[i].GetComponentInChildren<TMP_Text>();
            if (btnText != null)
                btnText.text = shuffledChoices[i];
        }

        ShowDialog();


        if (resultModalWin != null) resultModalWin.SetActive(false);
        if (resultModalLost != null) resultModalLost.SetActive(false);
        if (confirmationDialog != null) confirmationDialog.SetActive(false);

        for (int i = 0; i < choiceButtons.Length; i++)
        {
            int index = i;
            choiceButtons[i].onClick.AddListener(() => OnChoiceTapped(index));
        }

        currentHearts = PlayerPrefs.GetInt(HeartsKey, 3); // Load hearts from PlayerPrefs
        UpdateHeartsUI();

        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (gameOverOkButton != null)
            gameOverOkButton.onClick.AddListener(OnGameOverOkClicked);

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
        string chosen = choiceButtons[index].GetComponentInChildren<TMP_Text>().text;
        if (chosen == correctAnswerString)
        {
            SaveCorrectAnswer();
            string houseId = $"House_{chapter}_{difficulty}_{questionIndex}";
            PlayerPrefs.SetInt(houseId + "_Healed", 1);
            PlayerPrefs.Save();

            if (resultModalWin != null) resultModalWin.SetActive(true);
            MusicEffectsManager.PlayWinBGM();
        }
        else
        {
            if (resultModalLost != null) resultModalLost.SetActive(true);
            MusicEffectsManager.PlayLoseBGM();
            LoseHeart();
        }
    }

    void LoseHeart()
    {
        if (currentHearts > 0)
        {
            currentHearts--;
            PlayerPrefs.SetInt(HeartsKey, currentHearts); // Save hearts
            PlayerPrefs.Save();
            UpdateHeartsUI();

            if (currentHearts == 0)
            {
                ShowGameOverPanel();
            }
        }
    }

    void ShowGameOverPanel()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        SetObjectsToHideActive(false);
        if (resultModalLost != null) resultModalLost.SetActive(false);
        if (resultModalWin != null) resultModalWin.SetActive(false);
        if (confirmationDialog != null) confirmationDialog.SetActive(false);
    }

    void OnGameOverOkClicked()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        // Optionally reset hearts
        currentHearts = 3;
        PlayerPrefs.SetInt(HeartsKey, currentHearts);
        PlayerPrefs.Save();

        // Go to Main Menu scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    void UpdateHeartsUI()
    {
        for (int i = 0; i < heartIcons.Length; i++)
        {
            if (heartIcons[i] != null)
                heartIcons[i].SetActive(i < currentHearts);
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
        // Optionally reset hearts here if needed:
        // currentHearts = 3;
        // PlayerPrefs.SetInt(HeartsKey, currentHearts);
        // PlayerPrefs.Save();
        // UpdateHeartsUI();
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

    private void ShuffleChoices(string[] choices, string correctAnswer, out string[] shuffled, out string newCorrectAnswer)
    {
        shuffled = new string[choices.Length];
        choices.CopyTo(shuffled, 0);

        // Shuffle
        for (int i = choices.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            string temp = shuffled[i];
            shuffled[i] = shuffled[j];
            shuffled[j] = temp;
        }

        // Find new correct answer string (it doesn't change, just keep it)
        newCorrectAnswer = correctAnswer;
    }
}