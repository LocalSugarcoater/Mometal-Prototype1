using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float PlayerSpeed = 6f;
    [SerializeField] Rigidbody Playerbody;
    [SerializeField] Transform cam;
    [SerializeField] GameObject enemysensor;
    EnemySensor enemysensorscript;
    Transform enemytransform;
    Quaternion player_rotation;
    Vector3 directiontoenemy;
    public Vector3 direction;
    float TurnSmoothTime = 0.1f;
    float turnsmoothvelocity;
    public float targetangle;
    // Start is called before the first frame update
    void Start()
    {
        enemysensorscript = enemysensor.GetComponent<EnemySensor>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalinput = Input.GetAxisRaw("Horizontal");
        float verticalinput = Input.GetAxisRaw("Vertical");
        
        if (Playerbody.velocity.x > 30f)
        {
            horizontalinput = 0;
            verticalinput = 0;
        }

        direction = new Vector3(horizontalinput, 0f, verticalinput);
        targetangle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

        if (enemysensorscript.cameralockedon)
        {
            directiontoenemy = enemysensorscript.CurrentlyLockedTarget.transform.position - transform.position;
            directiontoenemy.y = 0;

            player_rotation = Quaternion.LookRotation(directiontoenemy);
            transform.rotation = player_rotation;
        }

        if (direction.magnitude >= 0.1f && enemysensorscript.cameralockedon)
        {
            

            Vector3 movedirection = Quaternion.Euler(0f, targetangle, 0f) * Vector3.forward;
            transform.position = new Vector3(transform.position.x + (movedirection.x * PlayerSpeed * Time.deltaTime), transform.position.y, transform.position.z);
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + (movedirection.z * PlayerSpeed * Time.deltaTime));
        }
        else if (direction.magnitude >= 0.1f)
        {
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetangle, ref turnsmoothvelocity, TurnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 movedirection = Quaternion.Euler(0f, targetangle, 0f) * Vector3.forward;
            transform.position = new Vector3(transform.position.x + (movedirection.x * PlayerSpeed * Time.deltaTime), transform.position.y, transform.position.z);
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + (movedirection.z * PlayerSpeed * Time.deltaTime));
        }

    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Launcher")
        {
            Playerbody.AddForce(new Vector3(2000, 0, 0));
            
        }
    }
}
