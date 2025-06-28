using UnityEngine;
using UnityEngine.UI;

public class ChoiceSelector : MonoBehaviour
{
    public Button[] choiceButtons; // Assign your 4 choice buttons in the Inspector
    private int selectedIndex = 0;

    public GameObject resultModalWin;
    public GameObject resultModalLost;
    public int correctAnswerIndex = 0; // Set this in the Inspector to the correct choice index (0-3)

    public GameObject[] objectsToHide; // Assign Character, Patient, Dialog Box, Choices Box in Inspector

    void Start()
    {
        HighlightChoice();
        // Hide modals at start
        if (resultModalWin != null) resultModalWin.SetActive(false);
        if (resultModalLost != null) resultModalLost.SetActive(false);
    }

    void Update()
    {
        // Disable selection/highlight if a modal is active
        if ((resultModalWin != null && resultModalWin.activeSelf) ||
            (resultModalLost != null && resultModalLost.activeSelf))
        {
            // Remove highlight from all buttons
            UnhighlightAllChoices();
            // Hide specified objects
            SetObjectsToHideActive(false);
            return;
        }
        else
        {
            // Show specified objects if modals are not active
            SetObjectsToHideActive(true);
        }

        // Assuming your choices are arranged in a 2x2 grid:
        // [0] [1]
        // [2] [3]
        int rowCount = 2;
        int colCount = 2;
        int row = selectedIndex / colCount;
        int col = selectedIndex % colCount;

        if (Input.GetKeyDown(KeyCode.W)) // Up
        {
            row = (row + rowCount - 1) % rowCount;
            selectedIndex = row * colCount + col;
            HighlightChoice();
        }
        else if (Input.GetKeyDown(KeyCode.S)) // Down
        {
            row = (row + 1) % rowCount;
            selectedIndex = row * colCount + col;
            HighlightChoice();
        }
        else if (Input.GetKeyDown(KeyCode.A)) // Left
        {
            col = (col + colCount - 1) % colCount;
            selectedIndex = row * colCount + col;
            HighlightChoice();
        }
        else if (Input.GetKeyDown(KeyCode.D)) // Right
        {
            col = (col + 1) % colCount;
            selectedIndex = row * colCount + col;
            HighlightChoice();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckAnswer(selectedIndex);
        }
    }

    void HighlightChoice()
    {
        for (int i = 0; i < choiceButtons.Length; i++)
        {
            var button = choiceButtons[i];
            var colors = button.colors;
            if (i == selectedIndex)
            {
                colors.normalColor = Color.yellow;
                button.transform.localScale = new Vector3(1.1f, 1.1f, 1f);
            }
            else
            {
                colors.normalColor = Color.white;
                button.transform.localScale = Vector3.one;
            }
            button.colors = colors;
        }
    }

    void UnhighlightAllChoices()
    {
        for (int i = 0; i < choiceButtons.Length; i++)
        {
            var button = choiceButtons[i];
            var colors = button.colors;
            colors.normalColor = Color.white;
            button.transform.localScale = Vector3.one;
            button.colors = colors;
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

    void SetObjectsToHideActive(bool isActive)
    {
        if (objectsToHide == null) return;
        foreach (var obj in objectsToHide)
        {
            if (obj != null) obj.SetActive(isActive);
        }
    }

    public void RetryQuestion()
    {
        if (resultModalWin != null) resultModalWin.SetActive(false);
        if (resultModalLost != null) resultModalLost.SetActive(false);
        SetObjectsToHideActive(true);
        selectedIndex = 0;
        HighlightChoice();
    }
}