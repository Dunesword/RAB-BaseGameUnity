using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Target")]
    public Transform player;
    public Vector3 targetOffset = new Vector3(0f, 1.5f, 0f);

    [Header("Orbit")]
    public float distance = 8f;
    public float sensitivity = 3f;
    public float minYAngle = -30f;
    public float maxYAngle = 70f;

    [Header("Collision")]
    public LayerMask collisionLayers;
    public float cameraRadius = 0.3f;
    public float collisionOffset = 0.2f;

    private float yaw;
    private float pitch;

    void LateUpdate()
    {
        if (player == null) return;

        if (Input.GetMouseButtonDown(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (Input.GetMouseButtonUp(1))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Input.GetMouseButton(1))
        {
            yaw += Input.GetAxis("Mouse X") * sensitivity;
            pitch -= Input.GetAxis("Mouse Y") * sensitivity;
            pitch = Mathf.Clamp(pitch, minYAngle, maxYAngle);
        }

        Vector3 targetPosition = player.position + targetOffset;

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);

        Vector3 desiredOffset = rotation * new Vector3(0f, 0f, -distance);
        Vector3 desiredCameraPosition = targetPosition + desiredOffset;

        Vector3 directionToCamera = desiredCameraPosition - targetPosition;
        float desiredDistance = directionToCamera.magnitude;

        directionToCamera.Normalize();

        float finalDistance = desiredDistance;

        if (Physics.SphereCast(
            targetPosition,
            cameraRadius,
            directionToCamera,
            out RaycastHit hit,
            desiredDistance,
            collisionLayers
        ))
        {
            finalDistance = hit.distance - collisionOffset;
        }

        finalDistance = Mathf.Clamp(finalDistance, 0.5f, distance);

        transform.position = targetPosition + directionToCamera * finalDistance;
        transform.LookAt(targetPosition);
    }
}