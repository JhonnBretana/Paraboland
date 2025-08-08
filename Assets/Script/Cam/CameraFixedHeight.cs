using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFixedHeight : MonoBehaviour
{
    [Tooltip("How many tiles tall should the camera show?")]
    public float verticalTiles = 10f; // 10–12 feels great

    Camera cam;

    void LateUpdate()
    {
        if (!cam) cam = GetComponent<Camera>();
        // 1 tile = 1 world unit (because PPU=16 and Grid cell size = (1,1,0))
        cam.orthographicSize = verticalTiles * 0.5f;
    }
}
