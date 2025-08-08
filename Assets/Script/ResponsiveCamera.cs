using UnityEngine;

public class ResponsiveCamera : MonoBehaviour
{
    public float targetWidth = 1920f;
    public float targetHeight = 1080f;
    public float pixelsPerUnit = 100f; // match this with your imported sprites' PPU

    void Start()
    {
        float targetAspect = targetWidth / targetHeight;
        float screenAspect = (float)Screen.width / Screen.height;

        float scaleFactor = screenAspect / targetAspect;

        Camera cam = GetComponent<Camera>();

        if (scaleFactor < 1f)
        {
            // Narrow screen (e.g. phone)
            cam.orthographicSize = (targetHeight / 2f) / pixelsPerUnit / scaleFactor;
        }
        else
        {
            // Wide screen (e.g. tablet)
            cam.orthographicSize = (targetHeight / 2f) / pixelsPerUnit;
        }
    }
}