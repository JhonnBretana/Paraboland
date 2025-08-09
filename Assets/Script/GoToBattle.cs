using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToBattle : MonoBehaviour
{
    public string sceneToLoad = "EasyQuestion2";
    public int questionIndex; // Set this in the Inspector for each house

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPrefs.SetInt("CurrentQuestionIndex", questionIndex);
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}