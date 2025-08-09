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

        // Hide choices etc. at start
        SetObjectsToHideActive(false);

        // Add tap/click listeners to each button
        for (int i = 0; i < choiceButtons.Length; i++)
        {
            int index = i; // Capture index for the lambda
            choiceButtons[i].onClick.AddListener(() => OnChoiceSelected(index));
        }
    }

    void Update()
    {
        // Disable selection/highlight if a modal is active
        if ((resultModalWin != null && resultModalWin.activeSelf) ||
            (resultModalLost != null && resultModalLost.activeSelf))
        {
            UnhighlightAllChoices();
            SetObjectsToHideActive(false);
            return;
        }
        // Remove: SetObjectsToHideActive(true);
        // No keyboard input needed for tap version
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

    public void SetObjectsToHideActive(bool isActive)
    {
        Debug.Log("SetObjectsToHideActive called with: " + isActive);
        if (objectsToHide == null) return;
        foreach (var obj in objectsToHide)
        {
            if (obj != null)
            {
                obj.SetActive(isActive);
                Debug.Log("SetActive " + obj.name + " to " + isActive);
            }
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

    void OnChoiceSelected(int index)
    {
        selectedIndex = index;
        HighlightChoice();
        // Do NOT check answer here
    }

    // Call this from the Submit button's OnClick event
    public void SubmitAnswer()
    {
        CheckAnswer(selectedIndex);
    }
}