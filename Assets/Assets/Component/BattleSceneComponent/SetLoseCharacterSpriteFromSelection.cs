using UnityEngine;
using UnityEngine.UI;

public class SetLoseCharacterSpriteFromSelection : MonoBehaviour
{
    public Image characterImage; // Assign the UI Image component in Inspector

    void Start()
    {
        var sprite = CharacterManager.GetSelectedLoseCharacterSprite();
        if (sprite != null && characterImage != null)
            characterImage.sprite = sprite;
    }
}