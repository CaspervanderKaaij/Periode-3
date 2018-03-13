using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownYesNo : MonoBehaviour {

public bool yes = true;
public Vector3 posYes;
public Vector3 posNo;
private bool yesChange = true;
public TownDialogue yesDialogue;
public TownDialogue noDialogue;
	void Start () {
		
	}
	
	void Update () {
		SetLoc();
		if(Input.GetButtonDown("A_Button")){
		SelectYesNo();
		}
	}

	void SetLoc(){

		if(Input.GetButtonDown("Vertical")){
			//yes = !yes;
		} else if(Input.GetAxis("Vertical") != 0){
			if(yesChange == true){
				yes = !yes;
				yesChange = false;
			}
		} else {
			yesChange = true;
		}

		if(yes == true){
			transform.localPosition = Vector3.Lerp(transform.localPosition, posYes,Time.deltaTime * 20);
		} else {
			transform.localPosition = Vector3.Lerp(transform.localPosition, posNo,Time.deltaTime * 20);
		}
	}

	void SelectYesNo(){
		if(yes == true){
			Debug.Log("yes");
		} else {
			Debug.Log("no");
		}
	}
}
