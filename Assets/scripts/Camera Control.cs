using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] Transform fighter1;
    [SerializeField] Transform fighter2;
    public float rotationspeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fighter1 == null || fighter2 == null) return;

        // Compute direction between fighters
        Vector3 direction = fighter2.position - fighter1.position;
        direction.y = 0; // Ignore vertical movement

        // Get perpendicular direction (rotate 90 degrees around Y-axis)
        Vector3 perpendicularDirection = Vector3.Cross(direction.normalized, Vector3.up);

        // Calculate target rotation
        Quaternion targetRotation = Quaternion.LookRotation(perpendicularDirection, Vector3.up);

        // Smoothly rotate the target group to match this angle
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationspeed);
    }
}
