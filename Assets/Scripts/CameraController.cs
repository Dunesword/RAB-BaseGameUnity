using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;

    private Vector3 offset;

    public float distance = 10f;
    public float sensitivity = 3f;
    public float minYAngle = -30f;
    public float maxYAngle = 70f;

    private float yaw;
    private float pitch;


    void Start ()
    {
        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;
    }
	
	
	void LateUpdate ()
    {

        if (Input.GetMouseButton(0)) // Left click held
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            yaw += Input.GetAxis("Mouse X") * sensitivity;
            pitch -= Input.GetAxis("Mouse Y") * sensitivity;
            pitch = Mathf.Clamp(pitch, minYAngle, maxYAngle);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);

        offset = rotation * new Vector3(0f, 0f, -distance);

        transform.position = player.transform.position + offset;
        transform.LookAt(player.transform.position);

    }
}
