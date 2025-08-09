using UnityEngine;

public class Retry : MonoBehaviour
{
    public ChoiceSelector choiceSelector; // Assign in Inspector

    public void RetryQuestion()
    {
        if (choiceSelector != null)
        {
            choiceSelector.RetryQuestion();
        }
    }
}
