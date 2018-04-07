using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour {

public Transform mainCam;

	void Start () {
		mainCam = Camera.main.transform;
	}
	
	void Update () {
		transform.LookAt(mainCam.position);
	}
}
