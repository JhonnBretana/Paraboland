using UnityEngine;
using UnityEngine.UI;

public class SetCharacterSpriteFromSelection : MonoBehaviour
{
    public Image characterImage; // Assign the UI Image component in Inspector

    void Start()
    {
        var sprite = CharacterManager.GetSelectedCharacterSprite();
        if (sprite != null && characterImage != null)
            characterImage.sprite = sprite;
    }
}