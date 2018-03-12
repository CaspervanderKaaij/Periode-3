using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldCamera : MonoBehaviour {

public GameObject pivot;
public Transform player;
public float speed = 100;
public float distance = 10;
//CharacterController cc;

	
	void Update () {
		SetPivot();
		pivot.transform.position = Vector3.Lerp(pivot.transform.position,player.position,Time.deltaTime * 10);
		CamFix();
	}

	void SetPivot(){
		pivot.transform.eulerAngles += new Vector3(-Input.GetAxis("Mouse Y"),Input.GetAxis("Mouse X")) * speed * Time.deltaTime;
		transform.position = pivot.transform.position + pivot.transform.TransformDirection(0,0,-distance);
		transform.LookAt(pivot.transform.position);
	}

void CamFix(){
	RaycastHit hit;
	Debug.DrawRay(pivot.transform.position,transform.position - pivot.transform.position);
	if(Physics.Raycast(new Ray(pivot.transform.position,transform.position - pivot.transform.position),out hit,Vector3.Distance(transform.position,pivot.transform.position))){
		transform.position = hit.point;
		transform.position += transform.forward;
	}
}

}
