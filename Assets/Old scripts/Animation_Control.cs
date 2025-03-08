using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Control : MonoBehaviour
{
    [SerializeField] GameObject playerobject;
    [SerializeField] GameObject enemysensor;
    [SerializeField] Animator playeranimator;
    PlayerMovement playerscript;
    float directionx;
    float directiony;
    EnemySensor lockonscript;
    bool Playermoving = false;
    int stringcounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        playerscript = playerobject.GetComponent<PlayerMovement>();
        lockonscript = enemysensor.GetComponent<EnemySensor>();
        playeranimator = GetComponent<Animator>();
        AnimatorStateInfo currentanimationstate;
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo currentanimationstate = playeranimator.GetCurrentAnimatorStateInfo(0);
        directionx = Input.GetAxisRaw("Horizontal");
        directiony = Input.GetAxisRaw("Vertical");

        if (playerscript.direction.magnitude > 0.1f)
        {
            Playermoving = true;
        }
        else
        {
            Playermoving = false;
        }


        if (stringcounter == 3 && !(currentanimationstate.IsName("punch2")))
        {
            stringcounter = 0;
        }

        if (currentanimationstate.IsName("Idle"))
        {
            stringcounter = 0;
        }

        if ( stringcounter < 3 &&    currentanimationstate.IsName("punch" + (stringcounter - 1)))
        {
            if (currentanimationstate.normalizedTime > 0.8f && Input.GetButtonDown("Fire1"))
            {
                
                 playeranimator.Play("punch" + stringcounter);
                 stringcounter = stringcounter + 1;
                
            }
        }

        else if (stringcounter == 0 && Input.GetButtonDown("Fire1"))
        {
            
             playeranimator.Play("punch" + stringcounter);
             stringcounter = stringcounter + 1;
            
        }
       


        Debug.Log(playeranimator.GetCurrentAnimatorStateInfo(0).normalizedTime);


        playeranimator.SetBool("playermoving", Playermoving);
        playeranimator.SetFloat("directionx", directionx);
        playeranimator.SetFloat("directiony", directiony);
        playeranimator.SetBool("directionxis0", directionx > -0.1f && directionx < 0.1f);
        playeranimator.SetBool("Cameralockedon", lockonscript.cameralockedon);
    }
}
