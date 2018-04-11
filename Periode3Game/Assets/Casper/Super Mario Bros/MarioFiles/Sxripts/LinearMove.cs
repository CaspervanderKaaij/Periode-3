using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearMove : Move
{

    public Vector2 moveVector;
    public bool hasGravity = true;
    public bool turnAround = true;
    public AudioClip sfx;
    public bool staticOffScreen = true;
    void Start()
    {
        cc = transform.GetComponent<CharacterController>();
    }

    void Update()
    {
        if(staticOffScreen == false){
            Activate();
        } else if(transform.GetChild(0).GetComponent<Renderer>().isVisible == true){
            Activate();
        }
     //  Activate();
      // if(transform.GetChild(0).GetComponent<Renderer>().isVisible == true){
          // Debug.Log(transform.name);
      // }
    }

    public override void Activate()
    {
        if (hasGravity == true)
        {
            Gravity();
        }
        LeftRight(new Vector3(moveVector.x, moveVel.y + moveVector.y, 0));
        cc.Move(moveVel * Time.deltaTime);

        RaycastHit rayHit;
        Debug.DrawRay(transform.position, new Vector3(Mathf.Abs(moveVector.x), moveVector.y, 0));
        if (Physics.Raycast(new Ray(transform.position, -moveVector), out rayHit))
        {
            if (rayHit.transform.tag == "Untagged" | rayHit.transform.tag == transform.tag)
            {
                if (turnAround == true)
                {
                    if (Vector3.Distance(rayHit.point, transform.position) < Mathf.Abs(transform.localScale.x) * 1.01f)
                    {
                        moveVector = -moveVector;
                    }
                }
            }
        }
    }

    public void Death()
    {
        transform.GetComponent<Hitbox>().Death();
    }
}
