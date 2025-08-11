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
}