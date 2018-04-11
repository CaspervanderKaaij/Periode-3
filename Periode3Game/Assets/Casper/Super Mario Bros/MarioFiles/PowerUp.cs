using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : Move
{

    public bool turnAround = true;
    public AudioClip sfx;
    public AudioClip starmanTheme;
    public enum Type
    {
        Star,
        Mushroom,
        FireFlower
    }
    public Type type = Type.Star;
    public GameObject bigMario;
    void Start()
    {
        cc = transform.GetComponent<CharacterController>();
    }

    void Update()
    {
        Gravity();
        Activate();
        if (type == Type.Star)
        {
            if (cc.isGrounded)
            {
                Jump();
            }
        }
        if (type != Type.FireFlower)
        {
            LeftRight(Vector3.right);
        }
    }

    public override void Gravity()
    {
        gravity = Mathf.MoveTowards(gravity, gravitySTR, Time.deltaTime * 35f);
        moveVel = new Vector3(moveVel.x, gravity, moveVel.z);
        if (Mathf.Abs(cc.velocity.x) <= 0f)
        {
            speed = -speed;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<Mario>().PlaySound(sfx, 1, 0, transform.position, 1);
            if (type == Type.Star)
            {
                other.GetComponent<Mario>().starman = true;
                Camera.main.GetComponent<AudioSource>().Stop();
                Camera.main.GetComponent<AudioSource>().clip = starmanTheme;
                Camera.main.GetComponent<AudioSource>().time = 0;
                Camera.main.GetComponent<AudioSource>().Play();
                other.GetComponent<Mario>().StartCoroutine(other.GetComponent<Mario>().StarmanStop());
                Destroy(gameObject);
            }
            else if (type == Type.Mushroom)
            {
                Destroy(gameObject);
                Destroy(other.gameObject);
                Instantiate(bigMario, other.transform.position, other.transform.rotation);
            }
        }
    }
}
