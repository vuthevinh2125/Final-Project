using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Mục tiêu theo dõi")]
    public Transform target; 

    [Header("Thông số Camera")]
    public Vector3 offset = new Vector3(0f, 8f, -8f); 
    public float smoothSpeed = 5f; 

    void LateUpdate()
    {
        if (target == null) return; 

        Vector3 desiredPosition = target.position + offset;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        transform.position = smoothedPosition;
    }
}