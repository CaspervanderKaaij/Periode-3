using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleEnemySpawn : MonoBehaviour {

public GameObject[] enemies;
public int curEnemy = 0;
public Text UIEnemyName;

	void Start () {
		curEnemy = PlayerPrefs.GetInt("enemy");
		Instantiate(enemies[curEnemy],transform.position,Quaternion.identity);
		UIEnemyName.text = enemies[curEnemy].name;
	}
}
