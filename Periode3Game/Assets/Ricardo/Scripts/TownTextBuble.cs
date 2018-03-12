using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownTextBuble : MonoBehaviour {

	public Transform goal;
	public bool visible = true;

	void Update () {
		transform.LookAt(Camera.main.transform.position);
		transform.position = goal.position + Vector3.left;

		if(visible == true){
			transform.localScale = Vector3.Lerp(transform.localScale,new Vector3(95,95,95),Time.deltaTime * 30);
		} else {
			transform.localScale = Vector3.Lerp(transform.localScale,Vector3.zero,Time.deltaTime * 30);
		}
	}
}
