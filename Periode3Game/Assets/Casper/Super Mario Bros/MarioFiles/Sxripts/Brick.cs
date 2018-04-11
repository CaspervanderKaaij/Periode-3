using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [HideInInspector]
    public Vector3 origin;
    public bool deadByBig = false;
    public AudioClip sfx;

    void Start()
    {
        origin = transform.parent.position;
    }
    public virtual void Hit()
    {
        Destroy(transform.parent.gameObject);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Mario mario = other.GetComponent<Mario>();
            if (sfx != null)
            {
                mario.PlaySound(sfx, 1, 0, transform.position, 1);
            }
            if (mario.hit != 2)
            {
                if (mario.hit == 1)
                {
                    //mario.gravity = 0;
                    //mario.hit = false;
                    mario.hit = 2;
                    if (deadByBig == true)
                    {
                        if (mario.big == true)
                        {
                            Destroy(transform.parent.gameObject);
                        }
                    }
                    Hit();
                }
                else if (mario.cc.velocity.y >= 0)
                {
                    Hit();
                    if (deadByBig == true)
                    {
                        if (mario.big == true)
                        {
                            Destroy(transform.parent.gameObject);
                        }
                    }
                    mario.hit = 2;
                    mario.gravity = 0;
                }
            }
        }
    }

    public void backToOrigin()
    {
        transform.parent.position = Vector3.MoveTowards(transform.parent.position, origin, Time.deltaTime * 10);
    }
}
