using UnityEngine;

public class CharacterSpriteAssigner : MonoBehaviour
{
    public Sprite[] allCharacterSprites; // Assign in Inspector (battle sprites)
    public Sprite[] allWinCharacterSprites; // Assign in Inspector (win modal sprites)
    public Sprite[] allLoseCharacterSprites; // Assign in Inspector (lose modal sprites) // <-- Add this line

    void Awake()
    {
        CharacterManager.SetSprites(allCharacterSprites);
        CharacterManager.SetWinSprites(allWinCharacterSprites);
        CharacterManager.SetLoseSprites(allLoseCharacterSprites); // <-- Add this line
    }
}