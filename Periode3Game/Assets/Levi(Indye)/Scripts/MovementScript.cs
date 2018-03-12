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
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(move * Time.deltaTime * speed);
        if (Input.GetButtonDown("Fire1"))
        {
            
        }
        
        if (Input.GetButton("Escape"))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            button1.SetActive(true);
            button2.SetActive(true);
            player.SetActive(false);
            cam.SetActive(true);
        }
        x = Input.GetAxis("Vertical");
        z = Input.GetAxis("Horizontal");
        move.x = z;
        move.z = x;
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
        if (transform.position.y <= -20)
        {
            reset.y = 10;
            transform.position = reset;
            print(transform.position);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        b = true;
    }
  
}


