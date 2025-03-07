using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField]public Transform targetGroup; // Assign the Target Group here
    public Vector3 offsetRotation; // Offset angle to match Target Group's orientation
    public float rotationSpeed = 5f; // Adjust for smooth rotation

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (targetGroup == null) return;

        // Get the target rotation (same as Target Group + offset)
        Quaternion targetRotation = targetGroup.rotation * Quaternion.Euler(offsetRotation);

        // Smoothly rotate the camera to match
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
}
