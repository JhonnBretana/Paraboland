using UnityEngine;
using UnityEngine.UI;

public class SetWinCharacterSpriteFromSelection : MonoBehaviour
{
    public Image characterImage; // Assign the UI Image component in Inspector

    void Start()
    {
        var sprite = CharacterManager.GetSelectedWinCharacterSprite();
        if (sprite != null && characterImage != null)
            characterImage.sprite = sprite;
    }
}