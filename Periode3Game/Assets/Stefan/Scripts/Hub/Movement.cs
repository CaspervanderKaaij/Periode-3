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
    public GameObject character;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        mov.x = Input.GetAxis("Horizontal");
        mov.z = Input.GetAxis("Vertical");

        if (transform.position.z < -0.63)
        {
            t = c;
        }
        else
        {
            if (transform.position.z > -0.63)
            {
                t = c2;
            }
        }

        transform.Translate(mov * Time.deltaTime * speed);

        tL = new Vector3(t.position.x, transform.position.y, t.position.z);

        if (Vector2.SqrMagnitude(new Vector2(mov.x, mov.z)) != 0)
        {
            float angle = Mathf.Atan2(mov.x, mov.z) * Mathf.Rad2Deg;
            character.transform.eulerAngles = new Vector3(character.transform.eulerAngles.x, angle+90 + Camera.main.transform.eulerAngles.y, character.transform.eulerAngles.z);    
        }


        transform.LookAt(tL);
        transform.Rotate(0, 180, 0);
    }
}
