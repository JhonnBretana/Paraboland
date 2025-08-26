using UnityEngine;

/// Attach this to each Signage object (Signage (1..N)).
/// Keep the Signage GameObject **inactive by default** in the scene.
public class SignageHealed : MonoBehaviour
{
    [Header("Must match the door's GoToBattle")]
    public int chapter = 1;              // 1..5
    public string difficulty = "Easy";   // "Easy", "Average", "Hard"
    public int questionIndex = 0;        // 0..N

    private string HealedKey => $"House_{chapter}_{difficulty}_{questionIndex}_Healed";

    // Called by SignageRefresher on scene load
    public void Refresh()
    {
        bool healed = PlayerPrefs.GetInt(HealedKey, 0) == 1;
        gameObject.SetActive(healed); // visible only if healed
    }
}
