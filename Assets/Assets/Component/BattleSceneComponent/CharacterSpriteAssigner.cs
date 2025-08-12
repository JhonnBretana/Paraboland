using UnityEngine;

public class CharacterSpriteAssigner : MonoBehaviour
{
    public Sprite[] allCharacterSprites; // Assign in Inspector (battle sprites)
    public Sprite[] allWinCharacterSprites; // Assign in Inspector (win modal sprites)

    void Awake()
    {
        CharacterManager.SetSprites(allCharacterSprites);
        CharacterManager.SetWinSprites(allWinCharacterSprites); // Add this line
    }
}