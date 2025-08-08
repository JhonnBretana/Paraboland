using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Camera))]
public class CameraFitToAllTilemaps : MonoBehaviour
{
    public Transform gridRoot; // Drag your Grid GameObject here

    void Start()
    {
        FitToAllTilemaps();
    }

    void FitToAllTilemaps()
    {
        if (gridRoot == null)
        {
            Debug.LogError("Grid root not assigned.");
            return;
        }

        Bounds combinedBounds = new Bounds();
        bool hasBounds = false;

        foreach (Tilemap tilemap in gridRoot.GetComponentsInChildren<Tilemap>())
        {
            if (!hasBounds)
            {
                combinedBounds = tilemap.localBounds;
                hasBounds = true;
            }
            else
            {
                combinedBounds.Encapsulate(tilemap.localBounds);
            }
        }

        if (!hasBounds)
        {
            Debug.LogWarning("No tilemaps found under grid root.");
            return;
        }

        Camera cam = GetComponent<Camera>();
        float screenAspect = (float)Screen.width / Screen.height;
        float mapWidth = combinedBounds.size.x;
        float mapHeight = combinedBounds.size.y;
        float mapAspect = mapWidth / mapHeight;

        if (screenAspect >= mapAspect)
        {
            cam.orthographicSize = mapHeight / 2f;
        }
        else
        {
            float difference = mapAspect / screenAspect;
            cam.orthographicSize = (mapHeight / 2f) * difference;
        }

        cam.transform.position = new Vector3(combinedBounds.center.x, combinedBounds.center.y, cam.transform.position.z);
    }
}
