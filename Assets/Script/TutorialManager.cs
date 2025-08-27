using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialModal;
    public TMP_Text titleText;
    public TMP_Text[] pageTexts; // Assign Page1, Page2, Page3 in Inspector

    private int currentPage = 0;
    private string[] title;
    private string[][] pages;

    // Call this from the Tutorial button/icon OnClick()
    public void OpenTutorial()
    {
        int chapter = PlayerPrefs.GetInt("CurrentChapter", 1);

        switch (chapter)
        {
            case 1:
                title = Chapter1Tutorial.Title;
                pages = new string[][] { Chapter1Tutorial.Page1, Chapter1Tutorial.Page2, Chapter1Tutorial.Page3 };
                break;
            case 2:
                title = Chapter2Tutorial.Title;
                pages = new string[][] { Chapter2Tutorial.Page1, Chapter2Tutorial.Page2, Chapter2Tutorial.Page3 };
                break;
            // Add more chapters as needed
            default:
                title = Chapter1Tutorial.Title;
                pages = new string[][] { Chapter1Tutorial.Page1, Chapter1Tutorial.Page2, Chapter1Tutorial.Page3 };
                break;
        }

        currentPage = 0;
        UpdateTutorialUI();

        if (tutorialModal != null)
            tutorialModal.SetActive(true);
    }

    // Call this from the Close button inside the modal
    public void CloseTutorial()
    {
        if (tutorialModal != null)
            tutorialModal.SetActive(false);
    }

    public void NextPage()
    {
        if (currentPage < pages.Length - 1)
        {
            currentPage++;
            UpdateTutorialUI();
        }
    }

    public void PreviousPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            UpdateTutorialUI();
        }
    }

    private void UpdateTutorialUI()
    {
        if (titleText != null)
            titleText.text = title[0];

        for (int i = 0; i < pageTexts.Length; i++)
        {
            pageTexts[i].gameObject.SetActive(i == currentPage);
            if (i == currentPage)
                pageTexts[i].text = pages[i][0];
        }
    }
}