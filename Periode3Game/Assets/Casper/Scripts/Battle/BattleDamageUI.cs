using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleDamageUI : MonoBehaviour {

	private Transform cam;
	private TextMesh txt;
	private float timer;
	private float randomizer;

	void Start () {
		cam = Camera.main.transform;
		txt = transform.GetComponent<TextMesh> ();
		int range = 5;
		transform.position += new Vector3 (Random.Range(-range,range),Random.Range(-range,range),Random.Range(-range,range));
		randomizer = Random.Range (0.5f,2);
	}

	void Update () {
		transform.eulerAngles = cam.eulerAngles;
		float scaleHelp = Vector3.Distance (transform.position,cam.position) * (randomizer / 1.5f) / 170;
		transform.localScale = new Vector3(scaleHelp,scaleHelp,scaleHelp);
		transform.position += cam.TransformDirection (Vector3.up * 7 * randomizer * Time.unscaledDeltaTime);
		timer += Time.unscaledDeltaTime * randomizer;
		if(timer > 0.5f){
			txt.color = Color.Lerp (txt.color,Color.clear,Time.unscaledDeltaTime * 3f);
		}
		if(timer > 1.7f){
			txt.color = Color.clear;
		}
		if(timer > 2.9f){
			Destroy (gameObject);
		}
	}
}
