using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour {
    public Vector3 v;
    public float hor;
    public float ver;
    public float walkSpeed;
    public Vector3 vectorRotate;
    public float rotateSpeed;
    public float jumpSpeed;
    public bool mayJump;
    // Use this for initialization
    void Start () {

	}

    // Update is called once per frame
    void Update()
    {   
        vectorRotate.y = Input.GetAxis("Mouse X");

        transform.Rotate(vectorRotate * Time.deltaTime * rotateSpeed);

        transform.Translate(v / walkSpeed * Time.deltaTime);
        hor = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical");
        v.z = hor;
        v.x = -ver;
        if (Input.GetButtonDown("Jump"))
        {
            GetComponent<Rigidbody>().velocity += jumpSpeed * Vector3.up;
        }
    }
}
