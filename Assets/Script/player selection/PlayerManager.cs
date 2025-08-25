using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Available Characters")]
    public GameObject[] playerPrefabs;   // assign 5 prefabs in inspector (order must match selection)

    void Start()
    {
        // 1) Read selected character
        int selectedIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        if (selectedIndex < 0 || selectedIndex >= playerPrefabs.Length) selectedIndex = 0;

        // 2) Spawn chosen prefab
        GameObject player = Instantiate(playerPrefabs[selectedIndex], transform.position, Quaternion.identity);

        // 3) Camera follow (your existing script on Main Camera)
        var follow = Camera.main.GetComponent<FollowTarget2d>();
        if (follow != null) follow.target = player.transform;

        // 4) Hook D-Pad to the spawned player
        var pm = player.GetComponent<PlayerMovement>();
        var dpad = FindObjectOfType<DPadController>(true); // include inactive
        if (pm && dpad) dpad.SetPlayer(pm);
    }
}

