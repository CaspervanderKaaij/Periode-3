using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{

    public bool jumpDeath = true;
    public bool jumpable = false;
    public bool kill = true;
    [HideInInspector]
    public bool gotHit = false;
    public bool invincile = false;
    public AudioClip sfx;

    void OnTriggerEnter(Collider other)
    {
        if (transform.tag == "Enemy")
        {
            if (other.tag == "Player")
            {
                Mario mario = other.GetComponent<Mario>();
                if (mario.cc.velocity.y < 0)
                {
                    if (jumpDeath == true)
                    {
                        mario.gravity = mario.jumpStength;
                        //Destroy(gameObject);
                        if (sfx != null)
                        {
                            FindObjectOfType<Mario>().PlaySound(sfx, 1, 0, transform.position, 1);
                        }
                        Death();
                    }
                    else if (jumpable == false)
                    {
                        if (kill == true)
                        {
                            mario.Dead();
                        }
                    }
                    else
                    {
                        mario.gravity = mario.jumpStength;
                        if (sfx != null)
                        {
                            FindObjectOfType<Mario>().PlaySound(sfx, 1, 0, transform.position, 1);
                        }
                    }
                }
                else if (kill == true)
                {
                    mario.Dead();
                }
                transform.GetComponent<Collider>().enabled = false;
                gotHit = true;
                StartCoroutine(ColOn());
            }
        }
        else if (transform.tag == "Coin")
        {
            FindObjectOfType<ScoreBoard>().coins += 1;
            FindObjectOfType<ScoreBoard>().score += 200;
            //Destroy(gameObject);
            if (sfx != null)
            {
                FindObjectOfType<Mario>().PlaySound(sfx, 1, 0, transform.position, 1);
            }
            Death();
        }
    }

    public void Death()
    {
        if (invincile == false)
        {
            Destroy(gameObject);
            if (sfx != null)
            {
                FindObjectOfType<Mario>().PlaySound(sfx, 1, 0, transform.position, 1);
            }
        }
    }

    IEnumerator ColOn()
    {
        yield return new WaitForSeconds(0.1f);
        gotHit = false;
        transform.GetComponent<Collider>().enabled = true;
    }
}
