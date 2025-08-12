using UnityEngine;

public static class CharacterManager
{
    public static Sprite[] CharacterSprites;
    public static Sprite[] WinCharacterSprites; // Add this line

    public static void SetSprites(Sprite[] sprites)
    {
        CharacterSprites = sprites;
    }

    public static void SetWinSprites(Sprite[] sprites) // Add this method
    {
        WinCharacterSprites = sprites;
    }

    public static Sprite GetSelectedCharacterSprite()
    {
        int idx = PlayerPrefs.GetInt("SelectedCharacter", 0);
        if (CharacterSprites != null && idx >= 0 && idx < CharacterSprites.Length)
            return CharacterSprites[idx];
        return null;
    }

    public static Sprite GetSelectedWinCharacterSprite() // Add this method
    {
        int idx = PlayerPrefs.GetInt("SelectedCharacter", 0);
        if (WinCharacterSprites != null && idx >= 0 && idx < WinCharacterSprites.Length)
            return WinCharacterSprites[idx];
        return null;
    }
}