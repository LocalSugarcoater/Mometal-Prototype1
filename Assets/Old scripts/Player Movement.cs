using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float PlayerSpeed = 6f;
    [SerializeField] Rigidbody Playerbody;
    [SerializeField] Transform Enemy;
    [SerializeField] Transform cam;
    Quaternion player_rotation;
    Vector3 directiontoenemy;
    public Vector3 direction;
    public float targetangle;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update() 
    {
        float UpDowninput = Input.GetAxisRaw("Horizontal");
        float LeftRightinput = Input.GetAxisRaw("Vertical");

        print("Left Right movement" + UpDowninput);
        print("\nUp down movement" + LeftRightinput);
        
        if (Playerbody.velocity.x > 30f)
        {
            UpDowninput = 0;
            LeftRightinput = 0;
        }

        direction = new Vector3(UpDowninput, 0f, LeftRightinput);
        targetangle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

        directiontoenemy = (Enemy.position - transform.position).normalized;
        directiontoenemy.y = 0;
        

        player_rotation = Quaternion.LookRotation(directiontoenemy);
        transform.rotation = player_rotation;
        if (UpDowninput > 0.1f || UpDowninput < -0.1f){
            Vector3 forwarddirection = transform.forward;
            transform.position += forwarddirection * UpDowninput * PlayerSpeed* Time.deltaTime;
        }
        else if (LeftRightinput > 0.1f || LeftRightinput < -0.1f){
            Vector3 rightdirection = transform.right;
            LeftRightinput = LeftRightinput * -1f;
            transform.position += rightdirection * LeftRightinput * PlayerSpeed* Time.deltaTime;
        }
        /*if (direction.magnitude >= 0.1f){
            Vector3 movedirection = Quaternion.Euler(0f, targetangle, 0f) * Vector3.forward;
            
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + (movedirection.z * PlayerSpeed * Time.deltaTime));
            transform.position = new Vector3(transform.position.x + (movedirection.x * PlayerSpeed * Time.deltaTime), transform.position.y, transform.position.z);

        }*/
        
        
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "normalhit")
        {
            Playerbody.AddForce(new Vector3(2000, 0, 0));
            
        }
    }
}
