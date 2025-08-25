using UnityEngine;

public static class ChapterProgress
{
    public static int CountCorrectAnswers(int chapter, string difficulty, int totalQuestions)
    {
        int correct = 0;
        for (int i = 0; i < totalQuestions; i++)
        {
            string key = $"Chapter{chapter}_{difficulty}_Q{i}_Correct";
            if (PlayerPrefs.GetInt(key, 0) == 1)
                correct++;
        }
        return correct;
    }

    public static void UnlockChapter(int chapter)
    {
        PlayerPrefs.SetInt($"Chapter{chapter}_Unlocked", 1);
        PlayerPrefs.Save();
    }

    public static bool IsChapterUnlocked(int chapter)
    {
        return PlayerPrefs.GetInt($"Chapter{chapter}_Unlocked", chapter == 1 ? 1 : 0) == 1; // Chapter 1 always unlocked
    }
}