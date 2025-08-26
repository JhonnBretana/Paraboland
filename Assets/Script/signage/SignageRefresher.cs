using UnityEngine;
using System.Linq;

public class SignageRefresher : MonoBehaviour
{
    void Start()
    {
        // Finds even inactive signage in the scene
        var all = Resources.FindObjectsOfTypeAll<SignageHealed>()
            // Filter to only objects that actually belong to the active scene (not prefabs)
            .Where(s => s.gameObject.scene.IsValid())
            .ToArray();

        Debug.Log($"[REFRESHER] Found {all.Length} signage objects");
        foreach (var s in all) s.Refresh();
    }
}
