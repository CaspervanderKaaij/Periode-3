using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camer : MonoBehaviour {

    public GameObject t;
    public Camera c;
    public Camera c2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(t.transform);
        if(t.transform.position.z < 4.5)
        {
            c.enabled = true;
            c2.enabled = false;
        } else
        {
            if(t.transform.position.z > 4.5)
            {
                c.enabled = false;
                c2.enabled = true;
            }
        }
	}
}
