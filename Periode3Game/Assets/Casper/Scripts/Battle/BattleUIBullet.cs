using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUIBullet : MonoBehaviour
{

	public float speed = 1;
	//public Collider hitter;
	//public Collider destroyer;
	private string buttonName;
	private float bufferButton;
	private BattleManager manager;
	private int player = 0;
	private bool buttonDown = false;
	public GameObject effect;
	//private bool abxyCooldownNextFrame = false;

	void Start ()
	{
		if (transform.gameObject.layer == 8) {
			buttonName = "Y_Button";
			player = 3;
		} else if (transform.gameObject.layer == 9) {
			buttonName = "X_Button";
			player = 2;
		} else if (transform.gameObject.layer == 10) {
			buttonName = "B_Button";
			player = 1;
		} else if (transform.gameObject.layer == 11) {
			buttonName = "A_Button";
			player = 0;
		}

		manager = GameObject.FindObjectOfType<BattleManager> ();
	}

	void Update ()
	{
		
		if(Input.GetKeyDown(KeyCode.A)){
			//transform.localScale = new Vector3(startScale.x + 1f,startScale.y + 1f,1);
		}

		if (manager.curState == BattleManager.State.Normal) {
			transform.Translate (-speed * Time.deltaTime, 0, 0);
		}
		if (bufferButton > 0) {
			bufferButton -= Time.deltaTime;
		}
		if (Input.GetAxisRaw (buttonName) == 1) {
			if (buttonDown == false) {
				if (manager.abxyCooldown [player] == 0) {
					buttonDown = true;
					//bufferButton = 0.05f;
					manager.buttonObjects [player].transform.localScale = new Vector3 (1.25f, 1.25f, 1.3f);
				}
			}
		} else {
			buttonDown = false;
		}
	}

	void OnTriggerStay (Collider col)
	{
		if (manager.curState == BattleManager.State.Normal) {//normal
			if (col.tag == "BulletHit") {
				//if(bufferButton > 0){
				if (manager.buttonObjects [player].transform.localScale.x > 1) {
					bufferButton = 0;
					manager.DoDamage (manager.enemies [0].gameObject, 100, Random.Range (0.85f, 1.15f),false);
					manager.charge [player] += 15 * Random.Range (0.85f, 1.15f);
					Vector3 scale = manager.buttonObjects[player].transform.localScale;
					manager.buttonObjects [player].transform.localScale = new Vector3 (scale.x,scale.y,1);
					manager.abxyCooldown [player] = 0;
					GameObject g = GameObject.Instantiate (effect,manager.buttonObjects[player].transform);
					g.transform.position = g.transform.parent.position;
					g.transform.SetParent(GameObject.FindGameObjectWithTag("UIBulletEffect").transform);
					manager.Vibrate (0.1f,0.45f);
					Destroy (gameObject);
				}
			} else if (col.tag == "BulletMiss") {
				manager.charge[player] -= 5 * Random.Range (0.85f, 1.15f);
				Destroy (gameObject);
			}
		}
	}
}
