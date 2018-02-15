using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleEnemyAI : MonoBehaviour {

	//public string state = "normal";
	public float timer = 0;
	public float chargeFinishTime = 5;
	public float thinkTime = 1;
	public int target;
	private BattleManager manager;
	public string atackName = "Starlight Kick";
	public GameObject cam;
	private bool boolHelper = false;
	private bool damageHelper = false;
	public int agro = 0;

	public enum State{
		Normal,
		Think,
		Attack,
		Topple
	}

	public State curState;

	void Start () {
		manager = GameObject.FindObjectOfType<BattleManager> ();
		agro = Random.Range (0,manager.players.Count);
		Debug.Log (manager.players.Count);
		cam.SetActive (false);
		curState = State.Normal;
		//if (curState == State.Attack) {
			
		//}
	}

	/*public void CheckState(){
		switch (curState) {
		case curState == State.Attack :
			// do attack stuff;
			break;
		case curState == State.Normal :
			// do normal stuff;
			break;
		
			
		}
	}
*/



	void Update () {
		if (curState == State.Normal) {
			if (manager.curState == BattleManager.State.Normal) {//normal
				timer += Time.deltaTime;
			} else {
				timer = 0;
			}
			if(manager.atackNameUI.activeSelf == true){
				timer = 0;
			}
			if(timer > chargeFinishTime){
				timer = 0;
				curState = State.Think;
			}
		} else if(curState == State.Think){
			timer += Time.deltaTime;
			if(manager.coolDownTimer != 0){
				timer = 0;
			} else if(manager.atackNameUI.activeSelf == true){
				timer = 0;
			}
			if(timer > thinkTime){
				curState = State.Attack;
			}
		} else if(curState == State.Attack){
			manager.curState = BattleManager.State.EnemyAttack;//enemyAttack
			cam.SetActive (true);
			manager.atackNameUI.transform.GetChild (1).GetComponent<Text> ().text = atackName;
			timer += Time.deltaTime;
			if(timer > 1){
				if(damageHelper == false){
					damageHelper = true;
					manager.DoDamage (manager.players[agro],10,Random.Range(0.9f,1.1f),false);
				}
			}
			if(timer > 2.5f){
				cam.SetActive (false);
				timer = 0;
				curState = State.Normal;
				damageHelper = false;
				manager.BackToNormal (true);
			}
		}
		if (curState == State.Topple) {
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
		curState = State.Normal;
		boolHelper = false;
	}
}
