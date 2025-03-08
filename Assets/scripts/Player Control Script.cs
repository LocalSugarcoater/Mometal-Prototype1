using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlScript : MonoBehaviour
{
        [SerializeField] float PlayerSpeed = 6f;
    float UpDowninput;
    public bool playercanmove;
    float LeftRightinput;
    [SerializeField] Rigidbody Playerbody;
    [SerializeField] Transform Enemy;
    [SerializeField] Transform cam;
    Vector3 forwarddirection;
    Vector3 rightdirection;
    Quaternion player_rotation;
    Vector3 directiontoenemy;
    public Vector3 direction;
    public float targetangle;

    int frameinput = 5;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update() 
    {
        frameinput = 5;
        UpDowninput = Input.GetAxisRaw("Horizontal");
        LeftRightinput = Input.GetAxisRaw("Vertical");

        if (UpDowninput > 0.1f){
            frameinput = 6;
        }
        else if (UpDowninput < -0.1f){
            frameinput = 4;
        }
        if (LeftRightinput > 0.1f){
            frameinput = 2;
        }
        else if (LeftRightinput < -0.1f){
            frameinput = 8;
        }

        switch (frameinput)
        {
            case 6:
                if (LeftRightinput > 0.1f){
                    frameinput = 3;
                }
                else if(LeftRightinput < -0.1f){
                    frameinput = 9;
                }
            break;

            case 4:
                if (LeftRightinput > 0.1f){
                    frameinput = 1;
                }
                else if(LeftRightinput < -0.1f){
                    frameinput = 7;
                }
            break;
        }


        if (Playerbody.velocity.x > 30f)
        {
            UpDowninput = 0;
            LeftRightinput = 0;
        }

        if (UpDowninput > 0.1f || UpDowninput < -0.1f){
            LeftRightinput = 0;
        }
        if (LeftRightinput > 0.1f || LeftRightinput < -0.1f){
            UpDowninput = 0;
        }

        direction = new Vector3(UpDowninput, 0f, LeftRightinput);
        targetangle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

        directiontoenemy = (Enemy.position - transform.position).normalized;
        directiontoenemy.y = 0;
        

        player_rotation = Quaternion.LookRotation(directiontoenemy);
        transform.rotation = player_rotation;

        if (UpDowninput > 0.1f || UpDowninput < -0.1f){
            forwarddirection = transform.forward;
            transform.position += forwarddirection * UpDowninput * PlayerSpeed* Time.deltaTime;
        }
        else if (LeftRightinput > 0.1f || LeftRightinput < -0.1f){
            rightdirection = transform.right;
            LeftRightinput = LeftRightinput * -1f;
            transform.position += rightdirection * LeftRightinput * PlayerSpeed* Time.deltaTime;
        }

    }
}
