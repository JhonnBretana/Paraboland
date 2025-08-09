// using UnityEngine;

// [RequireComponent(typeof(Camera))]
// public class CameraFixedHeight : MonoBehaviour
// {
//     [Tooltip("How many tiles tall should the camera show?")]
//     public float verticalTiles = 10f; // 10–12 feels great

//     Camera cam;

//     void LateUpdate()
//     {
//         if (!cam) cam = GetComponent<Camera>();
//         // 1 tile = 1 world unit (because PPU=16 and Grid cell size = (1,1,0))
//         cam.orthographicSize = verticalTiles * 0.5f;
//     }
// }


using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFixedHeight : MonoBehaviour
{
    public float verticalTiles = 10f;

    Camera cam; Vector2 lastRes;

    void Awake()
    {
        cam = GetComponent<Camera>();
        Apply();
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        if (!cam) cam = GetComponent<Camera>();
        Apply();
    }
#endif

    void Update()
    {
        // Re-apply only if resolution truly changed
        if (lastRes.x != Screen.width || lastRes.y != Screen.height)
            Apply();
    }

    void Apply()
    {
        cam.orthographicSize = verticalTiles * 0.5f; // 1 tile = 1 unit
        lastRes = new Vector2(Screen.width, Screen.height);
    }
}
