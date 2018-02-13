using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleAtackCharge : MonoBehaviour {

	public int player = 0;
	public float percentage = 0;
	private float startX;
	private BattleManager manager;
	public float minPercantage = 0;
	public Image dPad;

	void Start () {
		startX = transform.parent.localScale.x;
		transform.parent.localScale = new Vector3 (0,transform.parent.localScale.y,transform.parent.localScale.z);
		manager = GameObject.FindObjectOfType<BattleManager> ();
	}

	void Update () {
		if (percentage >= minPercantage) {
			float realPercentage = percentage - minPercantage;
			transform.parent.localScale = Vector3.MoveTowards (transform.parent.localScale,new Vector3((realPercentage / 100) * startX, transform.parent.localScale.y, transform.parent.localScale.z),Time.deltaTime * 350);
		} else {
			transform.parent.localScale = new Vector3 (0, transform.parent.localScale.y, transform.parent.localScale.z);
		}
		percentage = manager.charge [player];
		if(percentage > 100){
			if(dPad != null){
				dPad.color = Color.white;
			}
		} else if(dPad != null){
			dPad.color = new Color(1,1,1,0.397f);
		}
	}
}
