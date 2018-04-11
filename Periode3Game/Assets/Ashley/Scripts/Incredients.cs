using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Incredients : MonoBehaviour {
    public int buff;
    public bool incredientOne;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void PickUp()
    {
        Destroy(gameObject);
        incredientOne = true;
    }

}
