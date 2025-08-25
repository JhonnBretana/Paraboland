using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelector : MonoBehaviour
{
    [Header("Character Selection")]
    public Image[] characterImages;   // Characters to click on
    public Image[] pedestalImages;    // Optional pedestals (same order as characters)

    private int selectedIndex = 0;

    [Header("UI Elements")]
    public Button selectButton;

    [Header("Scene Management")]
    public string nextSceneName = "ChapterSelection";

    [Header("Animation Settings")]
    public float moveUpDistance = 20f;   // How high they rise
    public float moveSpeed = 8f;         // How fast they move

    // Original positions
    private Vector3[] charOriginalPos;
    private Vector3[] pedOriginalPos;

    void Start()
    {
        // Store starting positions
        charOriginalPos = new Vector3[characterImages.Length];
        for (int i = 0; i < characterImages.Length; i++)
        {
            if (characterImages[i] != null)
                charOriginalPos[i] = characterImages[i].rectTransform.localPosition;
        }

        if (pedestalImages != null && pedestalImages.Length > 0)
        {
            pedOriginalPos = new Vector3[pedestalImages.Length];
            for (int i = 0; i < pedestalImages.Length; i++)
            {
                if (pedestalImages[i] != null)
                    pedOriginalPos[i] = pedestalImages[i].rectTransform.localPosition;
            }
        }

        if (selectButton != null)
            selectButton.onClick.AddListener(OnSelectCharacter);

        ApplyImmediate();
    }

    void OnEnable()
    {
        // Make character images clickable
        for (int i = 0; i < characterImages.Length; i++)
        {
            if (characterImages[i] == null) continue;

            int idx = i;
            var button = characterImages[i].GetComponent<Button>();
            if (button == null)
                button = characterImages[i].gameObject.AddComponent<Button>();

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => OnCharacterTapped(idx));
        }
    }

    void Update()
    {
        AnimateMovement();
    }

    // ========= Public =========
    public void OnCharacterTapped(int index)
    {
        SetSelectedCharacter(index);
    }

    public void OnSelectCharacter()
    {
        PlayerPrefs.SetInt("SelectedCharacter", selectedIndex);
        PlayerPrefs.Save();

        Debug.Log($"Character {selectedIndex + 1} selected!");

        if (!string.IsNullOrEmpty(nextSceneName))
            SceneManager.LoadScene(nextSceneName);
    }

    public void SetSelectedCharacter(int index)
    {
        if (index >= 0 && index < characterImages.Length)
            selectedIndex = index;
    }

    // ========= Animation =========
    private void AnimateMovement()
    {
        for (int i = 0; i < characterImages.Length; i++)
        {
            if (characterImages[i] == null) continue;

            Vector3 targetPos = charOriginalPos[i];
            if (i == selectedIndex)
                targetPos += Vector3.up * moveUpDistance;

            var current = characterImages[i].rectTransform.localPosition;
            characterImages[i].rectTransform.localPosition =
                Vector3.Lerp(current, targetPos, Time.deltaTime * moveSpeed);

            // Optional pedestal
            if (pedestalImages != null && i < pedestalImages.Length && pedestalImages[i] != null)
            {
                Vector3 pedTarget = pedOriginalPos[i];
                if (i == selectedIndex)
                    pedTarget += Vector3.up * moveUpDistance;

                var pedCurrent = pedestalImages[i].rectTransform.localPosition;
                pedestalImages[i].rectTransform.localPosition =
                    Vector3.Lerp(pedCurrent, pedTarget, Time.deltaTime * moveSpeed);
            }
        }
    }

    private void ApplyImmediate()
    {
        for (int i = 0; i < characterImages.Length; i++)
        {
            if (characterImages[i] == null) continue;

            Vector3 pos = charOriginalPos[i] + ((i == selectedIndex) ? Vector3.up * moveUpDistance : Vector3.zero);
            characterImages[i].rectTransform.localPosition = pos;

            if (pedestalImages != null && i < pedestalImages.Length && pedestalImages[i] != null)
            {
                Vector3 pedPos = pedOriginalPos[i] + ((i == selectedIndex) ? Vector3.up * moveUpDistance : Vector3.zero);
                pedestalImages[i].rectTransform.localPosition = pedPos;
            }
        }
    }
}
