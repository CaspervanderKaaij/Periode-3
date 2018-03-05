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
	public Animator anim;
	//public string state = "normal";

	void Start () {
		manager = GameObject.FindObjectOfType<BattleManager> ();
		timer = 0;
		anim = transform.GetChild(0).GetComponent<Animator>();
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
						manager.Vibrate (0.1f,1);
						manager.enemies[0].GetComponent<BattleEnemyAI>().agroList.Add (playerNumber - 1);
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
			if(Input.GetKeyDown(KeyCode.Space)){
			//	Animate();
			}
		}
	}
	void Animate(){
		int r = Random.Range(1,6);
		Debug.Log(r);
		anim.Play("Attack");
		anim.SetBool("attacking",true);
		anim.SetFloat("attackNumber",r);
		StartCoroutine(StopAnim());
	}

	IEnumerator StopAnim(){
		yield return new WaitForSeconds(Time.deltaTime);
		anim.SetBool("attacking",false);
	}
}

