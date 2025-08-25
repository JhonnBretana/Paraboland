using UnityEngine;
using TMPro;

public class ScoreHandler : MonoBehaviour
{
    public TMP_Text[] chapterScoreTexts; // [0] = Chapter 1, [1] = Chapter 2, etc.

    private int chapters = 5;
    private int questionsPerDifficulty = 5;
    private string[] difficulties = { "Easy", "Average", "Difficult" };

    void Start()
    {
        UpdateScoresDisplay();
    }

    void OnEnable()
    {
        UpdateScoresDisplay();
    }

    private void UpdateScoresDisplay()
    {
        for (int chapter = 1; chapter <= chapters; chapter++)
        {
            int totalCorrect = 0;
            int totalQuestions = 0;
            foreach (var diff in difficulties)
            {
                totalCorrect += ChapterProgress.CountCorrectAnswers(chapter, diff, questionsPerDifficulty);
                totalQuestions += questionsPerDifficulty;
            }
            if (chapterScoreTexts != null && chapterScoreTexts.Length >= chapter)
            {
                chapterScoreTexts[chapter - 1].text = $"{totalCorrect} / {totalQuestions}";
            }
        }
    }
}