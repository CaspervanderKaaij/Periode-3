using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour {
    public float speed;
    public Vector3 move;
    public float x;
    public float z;
    public Rigidbody r;
    public bool b;
    public Vector3 height;
    public Vector3 view;
    public Vector3 camlook;
    public Vector3 reset;
    public GameObject player;
    public GameObject cam;
    public GameObject button1;
    public GameObject button2;
    private Animator anim;
    // Use this for initialization
    void Start () {
        anim = transform.GetChild(0).GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        Mousebehaviour();
        Walking();
        Sprint();
        
        if (transform.position.y <= -20)
        {
            transform.Translate(move * Time.deltaTime * speed);
            reset.y = 10;
            transform.position = reset;
            print(transform.position);
        }

    }
   public void Walking()
   {
        x = Input.GetAxis("Vertical");
        z = Input.GetAxis("Horizontal");
        float angle = Mathf.Atan2(x, -z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y - 90;
        anim.SetFloat("anal",Vector2.SqrMagnitude(new Vector2(x,z)));
        print(angle);
        move.x = z;
        move.z = x;
        //make the player move relative to the cam
        //use the forward of the cam + pos player

        if (x != 0)
        {
            gameObject.transform.position += cam.transform.forward * Time.deltaTime * speed;
        }
        if (z != 0)
        {
            gameObject.transform.position += cam.transform.forward * Time.deltaTime * speed;
        }
        if (Vector2.SqrMagnitude(new Vector2(x, z)) > 0)
        {
             transform.eulerAngles = new Vector3(transform.eulerAngles.x, angle, transform.eulerAngles.z);
        }
   }

    public void Mousebehaviour()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (Input.GetButton("Escape"))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            player.SetActive(false);
        }
    }

    public void Sprint()
    {
        if (b == true)
        {
            if (Input.GetButtonDown("Jump") == true)
            {
                r.velocity = height;
                b = false;
            }
        }
        if (Input.GetButtonDown("Shift"))
        {
            speed *= 2f;
        }
        if (Input.GetButtonUp("Shift"))
        {
            speed /= 2f;
        }
    }

}
    


