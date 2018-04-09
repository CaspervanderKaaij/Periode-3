using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public Vector3 mov;
    public float speed;
    public Transform c;
    public Transform c2;
    public Transform t;
    public Vector3 tL;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        mov.x = Input.GetAxis("Horizontal");
        mov.z = Input.GetAxis("Vertical");

        if (transform.position.z < 4.5)
        {
            t = c;
        }
        else
        {
            if (transform.position.z > 4.5)
            {
                t = c2;
            }
        }

        transform.Translate(mov * Time.deltaTime * speed);

        tL = new Vector3(t.position.x, transform.position.y, t.position.z);

        transform.LookAt(tL);
        transform.Rotate(0, 180, 0);
    }
}
