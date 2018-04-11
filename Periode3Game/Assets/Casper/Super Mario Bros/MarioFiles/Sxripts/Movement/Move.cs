using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    [HideInInspector]
    public CharacterController cc;
    [HideInInspector]
    public Vector3 moveVel = Vector3.zero;
    [HideInInspector]
    public float gravity = -9.81f;
    public float gravitySTR = -9.81f;
    public float jumpStength = 5;
    [HideInInspector]
    public Vector2 horVertInput;
    public float speed = 1;
    public bool hasLowJump = false;
    private bool lowJump = false;

    void Start()
    {
        cc = transform.GetComponent<CharacterController>();
    }

    public virtual void Activate()
    {
        //horVertInput = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
        //Gravity();
        //	if(Input.GetButtonDown("Jump")){
        //	if(cc.isGrounded){
        //	Jump();
        //	}
        //}
        //LeftRight(new Vector3(horVertInput.x,moveVel.y,horVertInput.y));
        //Debug.Log(moveVel);
        cc.Move(moveVel * Time.deltaTime);
    }

    public virtual void Gravity()
    {

        if (gravity > 0)
        {
            if (hasLowJump == true)
            {
                if (Input.GetAxis("SMBJump") == 0)
                {
                    lowJump = true;
                }
            }
            if (lowJump == false)
            {
                gravity = Mathf.MoveTowards(gravity, gravitySTR, Time.deltaTime * 5);
            }
            else if(cc.isGrounded == false)
            {
                //gravity = 0;
				gravity = Mathf.MoveTowards(gravity, gravitySTR, Time.deltaTime * 12.5f);
            } else {
                gravity = 0;
            }
        }
        else
        {
            lowJump = false;
            gravity = Mathf.MoveTowards(gravity, gravitySTR, Time.deltaTime * 12.5f);
        }
        moveVel = new Vector3(moveVel.x, gravity, moveVel.z);
    }
    public virtual void Jump()
    {
        gravity = jumpStength;
    }

    public virtual void LeftRight(Vector3 vel)
    {
        moveVel = new Vector3(vel.x * speed,vel.y * 20,0);
    }
}
