using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonTexture : MonoBehaviour {

	public Sprite gamePadTexture;
	public Sprite keyBoardTexture;
	private Image rend;

	void Start(){
		rend = transform.GetComponent<Image> ();
	}

	void Update () {
		//Debug.Log (Input.GetJoystickNames ().Length);
		string[] joyNames = Input.GetJoystickNames ();

		if (joyNames.Length == 0) {
			rend.sprite = keyBoardTexture;
		} else if (joyNames[0] != "") {
			rend.sprite = gamePadTexture;
		} else {
			rend.sprite = keyBoardTexture;
		}

	}
}
