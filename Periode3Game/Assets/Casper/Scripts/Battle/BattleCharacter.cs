using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCharacter : MonoBehaviour {

	BattleManager manager;
	public int playerNumber = 0;
	public int atackNumber = 0;
	public GameObject cam;
	public float timer = 0;
	private bool hasAtacked = false;
	//public string state = "normal";

	void Start () {
		manager = GameObject.FindObjectOfType<BattleManager> ();
		timer = 0;
	}

	void Update () {
		if (manager.curState == BattleManager.State.Attack) {//Attack
			if (manager.atackingPlayer == playerNumber) {
				cam.SetActive (true);
				//Debug.Log ("Player" + playerNumber + " is using atack number: " + atackNumber);

				if(timer > 0.5f){
					if(hasAtacked == false){
						hasAtacked = true;
						manager.DoDamage (manager.enemies[0],8001,Random.Range(0.9f,1.05f),true);
					}
				}

				if(timer > 1){
					timer = 0;
					hasAtacked = false;
					manager.BackToNormal (true);
				}
				timer += Time.deltaTime;
			} else {
				cam.SetActive (false);
				timer = 0;
			}
		} else {
			cam.SetActive (false);
			timer = 0;
		}
	}
}
