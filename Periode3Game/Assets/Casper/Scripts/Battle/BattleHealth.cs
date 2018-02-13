using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHealth : MonoBehaviour {

	public float health = 100;
	private float startX;
	private BattleManager manager;
	public int enemy;
	private float startHealth;

	void Start () {
		startX = transform.localScale.x;
		manager = GameObject.FindObjectOfType<BattleManager> ();
		startHealth = health;
	}

	void Update () {
		health = manager.enemyHealth [enemy];
		transform.localScale = new Vector3 ((health / startHealth) * startX,transform.localScale.y,transform.localScale.z);
		if(health <= 0){
			manager.enemies.Remove (manager.enemies[enemy]);
			if(manager.enemies.Count == 0){
				manager.state = "Victory";
			}
		}
	}
}
