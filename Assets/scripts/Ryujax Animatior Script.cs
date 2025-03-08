using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RyujaxAnimatiorScript : MonoBehaviour
{
    [SerializeField] GameObject playerobject;
    [SerializeField] Animator playeranimator;
    PlayerControlScript playermovementscript;
    AnimatorStateInfo currentanimationstate;
    int playerframeinput = 0;
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
        playermovementscript = playerobject.GetComponent<PlayerControlScript>();
        playeranimator = GetComponent<Animator>();
    }

    void Update()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        currentanimationstate = playeranimator.GetCurrentAnimatorStateInfo(0);
        playerframeinput = playermovementscript.frameinput;


        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");

        playermoving = direction.magnitude > 0.1f;

        playeranimator.SetBool("PlayerMoving", playermoving);
        playeranimator.SetBool("Punch", punch);
        playeranimator.SetInteger("PlayerMovementNotation", playerframeinput);

    }
}
