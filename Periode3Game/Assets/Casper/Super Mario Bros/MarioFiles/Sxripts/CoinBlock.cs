using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBlock : MonoBehaviour {

float speed = 30;
public AudioClip sfx;
	void Start () {
		StartCoroutine(Collect());
		FindObjectOfType<Mario>().PlaySound(sfx,1,0,transform.position,1);
	}

	void Update() {
		speed -= 100 * Time.deltaTime;
		transform.position += Vector3.up * speed * Time.deltaTime;
	}
	
	IEnumerator Collect(){
		yield return new WaitForSeconds(0.3f);
		Destroy(gameObject);
		FindObjectOfType<ScoreBoard>().coins += 1;
		FindObjectOfType<ScoreBoard>().score += 200;
	}
}
