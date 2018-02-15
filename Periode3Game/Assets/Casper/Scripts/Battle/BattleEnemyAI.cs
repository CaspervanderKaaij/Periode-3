using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleEnemyAI : MonoBehaviour {

	public string state = "normal";
	public float timer = 0;
	public float chargeFinishTime = 5;
	public float thinkTime = 1;
	public int target;
	private BattleManager manager;
	public string atackName = "Starlight Kick";
	public GameObject cam;
	private bool boolHelper = false;
	public int agro = 0;

	void Start () {
		manager = GameObject.FindObjectOfType<BattleManager> ();
		agro = Random.Range (0,manager.players.Count);
		Debug.Log (manager.players.Count);
		cam.SetActive (false);
	}

	void Update () {
		if(state == "normal"){
			if (manager.state == "normal") {
				timer += Time.deltaTime;
			} else {
				timer = 0;
			}
			if(manager.atackNameUI.activeSelf == true){
				timer = 0;
			}
			if(timer > chargeFinishTime){
				timer = 0;
				state = "think";
			}
		} else if(state == "think"){
			timer += Time.deltaTime;
			if(manager.coolDownTimer != 0){
				timer = 0;
			} else if(manager.atackNameUI.activeSelf == true){
				timer = 0;
			}
			if(timer > thinkTime){
				state = "atack";
			}
		} else if(state == "atack"){
			manager.state = "enemyAtack";
			cam.SetActive (true);
			manager.atackNameUI.transform.GetChild (1).GetComponent<Text> ().text = atackName;
			timer += Time.deltaTime;
			if(timer > 2.5f){
				cam.SetActive (false);
				timer = 0;
				state = "normal";
				manager.BackToNormal (true);
			}
		}
		if (state == "topple") {
			if(boolHelper == false){
			StartCoroutine (ToppleTimer(15));
			boolHelper = true;
			}
		}
		Vector3 agroPos = manager.players [agro].transform.position;
		transform.LookAt (new Vector3(agroPos.x,transform.position.y,agroPos.z));
	}
	private IEnumerator ToppleTimer(float time){
		yield return new WaitForSeconds (time);
		state = "normal";
		boolHelper = false;
	}
}
