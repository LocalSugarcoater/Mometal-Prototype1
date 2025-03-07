using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RyujaxAnimationScript : MonoBehaviour
{
    [SerializeField] GameObject playerobject;
    [SerializeField] Animator playeranimator;
    PlayerMovement playermovementscript;
    AnimatorStateInfo currentanimationstate;
    float directionx;
    float directiony;
    Vector3 direction;
    Vector3 playerdirection;
    bool playermoving = false;
    bool punch = false;
    bool alreadyinanimation = false;
    // Start is called before the first frame update
    void Start()
    {
        playermovementscript = playerobject.GetComponent<PlayerMovement>();
        playeranimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        currentanimationstate = playeranimator.GetCurrentAnimatorStateInfo(0);
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
        if (direction.x > 0.1f || direction.x < -0.1f){
            direction.y = 0;
        }
        if (direction.y > 0.1f || direction.y < -0.1f){
            direction.y = direction.y * -1f;
            direction.x = 0;
        }

        playermoving = direction.magnitude > 0.1f;

        playeranimator.SetBool("Punch", punch);
        playeranimator.SetBool("PlayerMoving", playermoving);
        playeranimator.SetFloat("UpDown", direction.y);
        playeranimator.SetFloat("LeftRight", direction.x);

    }
}
