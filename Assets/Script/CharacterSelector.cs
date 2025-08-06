using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelector : MonoBehaviour
{
    [Header("Character Selection")]
    public SpriteRenderer[] characterSprites; // Assign your 5 character sprites in the Inspector
    private int selectedIndex = 0;

    [Header("UI Elements")]
    public Button selectButton; // The "SELECT" button
    public GameObject[] selectionIndicators; // Optional: visual indicators for selection

    [Header("Scene Management")]
    public string nextSceneName = "ChapterSelection"; // Scene to load after character selection

    [Header("Visual Feedback")]
    public Color selectedColor = Color.yellow;
    public Color normalColor = Color.white;
    public float selectedScale = 1.1f;
    public float normalScale = 1.0f;

    void Start()
    {
        HighlightCharacter();
        // Set up button click event
        if (selectButton != null)
        {
            selectButton.onClick.AddListener(OnSelectCharacter);
        }
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        // Left arrow or A key
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            selectedIndex = (selectedIndex - 1 + characterSprites.Length) % characterSprites.Length;
            HighlightCharacter();
        }
        // Right arrow or D key
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            selectedIndex = (selectedIndex + 1) % characterSprites.Length;
            HighlightCharacter();
        }
        // Space or Enter to select
        else if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            OnSelectCharacter();
        }
    }

    void HighlightCharacter()
    {
        for (int i = 0; i < characterSprites.Length; i++)
        {
            if (characterSprites[i] != null)
            {
                // Change color
                characterSprites[i].color = (i == selectedIndex) ? selectedColor : normalColor;

                // Change scale
                float scale = (i == selectedIndex) ? selectedScale : normalScale;
                characterSprites[i].transform.localScale = Vector3.one * scale;
            }
        }

        // Update selection indicators if they exist
        if (selectionIndicators != null)
        {
            for (int i = 0; i < selectionIndicators.Length; i++)
            {
                if (selectionIndicators[i] != null)
                {
                    selectionIndicators[i].SetActive(i == selectedIndex);
                }
            }
        }
    }

    public void OnSelectCharacter()
    {
        // Save the selected character index (you can use PlayerPrefs or a static variable)
        PlayerPrefs.SetInt("SelectedCharacter", selectedIndex);
        PlayerPrefs.Save();

        Debug.Log($"Character {selectedIndex + 1} selected!");

        // Load the next scene
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("Next scene name is not set!");
        }
    }

    // Public method to get the currently selected character index
    public int GetSelectedCharacterIndex()
    {
        return selectedIndex;
    }

    // Public method to set the selected character (useful for testing)
    public void SetSelectedCharacter(int index)
    {
        if (index >= 0 && index < characterSprites.Length)
        {
            selectedIndex = index;
            HighlightCharacter();
        }
    }
}
