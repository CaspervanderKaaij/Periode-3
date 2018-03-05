using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUIRhythm : MonoBehaviour {

	private BattleManager manager;
	private Vector3 startScale;

	void Start () {
		manager = GameObject.FindObjectOfType<BattleManager> ();
		startScale = transform.localScale;
	}

	void Update () {
		transform.localScale = Vector3.MoveTowards (transform.localScale,startScale,Time.unscaledDeltaTime * startScale.x * 3);
		if(manager.rhythmTime == true){
			transform.localScale = new Vector3(startScale.x * 1.2f,startScale.y * 1.2f,startScale.y * 1.2f);
		}
	}
}
