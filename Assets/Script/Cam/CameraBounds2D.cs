using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Camera))]
public class CameraBounds2D : MonoBehaviour
{
    public Tilemap worldTilemap;      // assign your main painted tilemap (e.g., Ground)
    public float padding = 0f;        // add if you want a little buffer inside the edge

    Camera cam;
    Bounds worldBounds;

    void Awake()
    {
        cam = GetComponent<Camera>();
        CacheBounds();
    }

    void CacheBounds()
    {
        if (!worldTilemap) return;
        // tighten bounds around painted tiles
        worldTilemap.CompressBounds();
        var b = worldTilemap.localBounds;
        // convert to world space
        Vector3 min = worldTilemap.transform.TransformPoint(b.min);
        Vector3 max = worldTilemap.transform.TransformPoint(b.max);
        worldBounds = new Bounds();
        worldBounds.SetMinMax(min, max);
    }

    void LateUpdate()
    {
        if (!worldTilemap) return;

        float vert = cam.orthographicSize;
        float horiz = vert * cam.aspect;

        // if map is smaller than camera view, just center
        float minX = worldBounds.min.x + horiz + padding;
        float maxX = worldBounds.max.x - horiz - padding;
        float minY = worldBounds.min.y + vert  + padding;
        float maxY = worldBounds.max.y - vert  - padding;

        float x = Mathf.Clamp(transform.position.x, minX, maxX);
        float y = Mathf.Clamp(transform.position.y, minY, maxY);
        transform.position = new Vector3(x, y, transform.position.z);
    }

    // Call this if you change the map at runtime
    public void Recalculate() => CacheBounds();
}
