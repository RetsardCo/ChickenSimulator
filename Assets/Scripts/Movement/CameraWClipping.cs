using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWClipping : MonoBehaviour
{
    public Transform target;
    public float distance = 5.0f;
    public float height = 2.0f;
    public float rotationDamping = 3.0f;
    public float zoomSpeed = 2.0f;
    public float minZoomDistance = 2.0f;
    public float maxZoomDistance = 10.0f;
    public float collisionOffset = 0.2f; // Offset to avoid clipping
    public LayerMask collisionLayers; // Layers to check for collisions

    private float currentX = 0f;
    private float currentY = 0f;
    private float rotationSpeed = 5f;

    void Update()
    {
        if (Input.GetMouseButton(1)) // Right mouse button to rotate the camera
        {
            currentX += Input.GetAxis("Mouse X") * rotationSpeed;
            currentY -= Input.GetAxis("Mouse Y") * rotationSpeed;
            currentY = Mathf.Clamp(currentY, -20f, 60f);
        }

        distance -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        distance = Mathf.Clamp(distance, minZoomDistance, maxZoomDistance);
    }

    void LateUpdate()
    {
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 desiredPosition = target.position - (rotation * Vector3.forward * distance + new Vector3(0, -height, 0));

        // Check for collisions
        RaycastHit hit;
        if (Physics.Linecast(target.position + new Vector3(0, height, 0), desiredPosition, out hit, collisionLayers))
        {
            // Adjust the camera position to avoid clipping through objects
            desiredPosition = hit.point + hit.normal * collisionOffset;
        }

        transform.position = desiredPosition;
        transform.LookAt(target.position + new Vector3(0, height, 0));
    }
}
