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
        if (selectButton != null)
        {
            selectButton.onClick.AddListener(OnSelectCharacter);
        }
    }

    // Remove keyboard input, selection is now by tap/click only

    // Call this from each character's OnMouseDown or via an EventTrigger
    public void OnCharacterTapped(int index)
    {
        SetSelectedCharacter(index);
    }

    void HighlightCharacter()
    {
        for (int i = 0; i < characterSprites.Length; i++)
        {
            if (characterSprites[i] != null)
            {
                characterSprites[i].color = (i == selectedIndex) ? selectedColor : normalColor;
                float scale = (i == selectedIndex) ? selectedScale : normalScale;
                characterSprites[i].transform.localScale = Vector3.one * scale;
            }
        }

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
        PlayerPrefs.SetInt("SelectedCharacter", selectedIndex);
        PlayerPrefs.Save();

        Debug.Log($"Character {selectedIndex + 1} selected!");

        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("Next scene name is not set!");
        }
    }

    public int GetSelectedCharacterIndex()
    {
        return selectedIndex;
    }

    public void SetSelectedCharacter(int index)
    {
        if (index >= 0 && index < characterSprites.Length)
        {
            selectedIndex = index;
            HighlightCharacter();
        }
    }

    // Optional: For direct click support on SpriteRenderer
    void OnEnable()
    {
        for (int i = 0; i < characterSprites.Length; i++)
        {
            int idx = i;
            var collider = characterSprites[i].GetComponent<Collider2D>();
            if (collider == null)
                collider = characterSprites[i].gameObject.AddComponent<BoxCollider2D>();
            var clickHandler = characterSprites[i].gameObject.GetComponent<CharacterClickHandler>();
            if (clickHandler == null)
                clickHandler = characterSprites[i].gameObject.AddComponent<CharacterClickHandler>();
            clickHandler.selector = this;
            clickHandler.index = idx;
        }
    }
}

// Helper script for click/tap detection
public class CharacterClickHandler : MonoBehaviour
{
    public CharacterSelector selector;
    public int index;

    void OnMouseDown()
    {
        if (selector != null)
            selector.OnCharacterTapped(index);
    }
}
