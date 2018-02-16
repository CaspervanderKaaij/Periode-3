using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEffect : MonoBehaviour {

	public float destoyTime = 1;
	public float scale = 0;
	public bool fadeOut = false;

	void Start () {
		
	}

	void Update () {
		if(destoyTime <= 0){
			Destroy (gameObject);
		}
		destoyTime -= Time.unscaledDeltaTime;
		transform.localScale += new Vector3(Time.unscaledDeltaTime * scale, Time.unscaledDeltaTime * scale, Time.unscaledDeltaTime * scale);
		if(fadeOut == true){
			Image clr = transform.GetComponent<Image> ();
			clr.color = new Color(clr.color.r,clr.color.g,clr.color.b,clr.color.a - 0.5f * Time.unscaledDeltaTime);
		}
	}
}
