using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattlePlayerHealth : MonoBehaviour {

	private Text txt;
	private BattleManager manager;
	public int maxHealth = 100;
	public int health = 100;

	void Start () {
		txt = transform.GetComponent<Text> ();
		manager = GameObject.FindObjectOfType<BattleManager> ();
	}

	void Update () {
		health = manager.playerHealth [transform.GetSiblingIndex ()];
		txt.text = "HP:" + health + " / " + maxHealth;
		if (health <= 0) {
			transform.GetChild (0).gameObject.SetActive (true);
		} else {
			transform.GetChild (0).gameObject.SetActive (false);
		}
	}
}
