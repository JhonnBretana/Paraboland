using UnityEngine;

public class FollowTarget2d : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.12f;
    private Vector3 velocity;

    void LateUpdate()
    {
        if (!target) return;
        var desired = new Vector3(target.position.x, target.position.y, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, desired, ref velocity, smoothTime);
    }
}
