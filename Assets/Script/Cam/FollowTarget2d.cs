// using UnityEngine;

// public class FollowTarget2d : MonoBehaviour
// {
//     public Transform target;
//     public float smoothTime = 0.12f;
//     private Vector3 velocity;

//     void LateUpdate()
//     {
//         if (!target) return;
//         var desired = new Vector3(target.position.x, target.position.y, transform.position.z);
//         transform.position = Vector3.SmoothDamp(transform.position, desired, ref velocity, smoothTime);
//     }
// }

using UnityEngine;

[DefaultExecutionOrder(50)] // run before clamping
public class FollowTarget2d : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.12f;
    public float pixelsPerUnit = 16f; // match your PPU
    private Vector3 velocity;

    void LateUpdate()
    {
        if (!target) return;

        // Smooth follow
        var desired = new Vector3(target.position.x, target.position.y, transform.position.z);
        var pos = Vector3.SmoothDamp(transform.position, desired, ref velocity, smoothTime);

        // Pixel-round to avoid shimmer
        float unitsPerPixel = 1f / Mathf.Max(1f, pixelsPerUnit);
        pos.x = Mathf.Round(pos.x / unitsPerPixel) * unitsPerPixel;
        pos.y = Mathf.Round(pos.y / unitsPerPixel) * unitsPerPixel;

        transform.position = pos;
    }
}

