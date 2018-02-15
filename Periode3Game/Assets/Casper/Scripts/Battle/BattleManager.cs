﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{

	[Header ("ObjectLists")]
	public List<GameObject> players;
	public List<GameObject> enemies;

	[Header ("Other object")]
	public GameObject damageUI;
	public GameObject[] camPathPrefabs;
	public BattleHealth[] enemyHealthUI;
	private GameObject camSlowMotionEffect;
	public GameObject[] selectAtackUI;
	public GameObject atackNameUI;
	public GameObject[] buttonObjects;

	[Header ("stats")]
	public float[] charge;
	public int[] playerHealth;
	public List<float> enemyHealth;

	[Header ("Handy's & helpers")]
	public float timeScale = 1;
	public bool turnAtack = false;
	//[HideInInspector]
	//public string state = "normal";
	public enum State{
		Normal,
		PlayerAttack,
		EnemyAttack,
		Victory,
		End,
		Cooldown,
		Attack
	}
	public State curState;
	public int atackingPlayer = 2;
	[HideInInspector]
	public float coolDownTimer = 0;
	public Image fadeIn;
	public Color fadeColor;
	[HideInInspector]
	public List<float> abxyCooldown;

	private GameObject victory;

	void Start ()
	{
		curState = State.Normal;
		abxyCooldown.Add (0);
		abxyCooldown.Add (0);
		abxyCooldown.Add (0);
		abxyCooldown.Add (0);
		fadeIn.color = Color.clear;
		victory = GameObject.Find ("Victory");
		victory.SetActive (false);
		atackingPlayer = 2;
		atackNameUI.SetActive (false);
		for (int i = 0; i < selectAtackUI.Count (); i++) {
			selectAtackUI [i].SetActive (false);
		}
		camSlowMotionEffect = Camera.main.transform.GetChild (0).gameObject;
		camSlowMotionEffect.SetActive (false);
		for (int i = 0; i < GameObject.FindGameObjectsWithTag ("Player").Length; i++) {
			players.Add (GameObject.FindGameObjectsWithTag ("Player") [i]);
		}
		players = players.OrderBy (go => go.name).ToList ();
		for (int i = 0; i < players.Count; i++) {
			players [i].GetComponent<BattleCharacter> ().playerNumber = i + 1;
		}

		for (int i = 0; i < GameObject.FindGameObjectsWithTag ("Enemy").Length; i++) {
			enemies.Add (GameObject.FindGameObjectsWithTag ("Enemy") [i]);
			enemyHealth.Add (enemyHealthUI [i].health);
		}
		enemies = enemies.OrderBy (go => go.name).ToList ();

		//DoDamage (enemies[0].gameObject,100,Random.Range(0.85f,1.15f));
	}

	public void DoDamage (GameObject sandBag, float damage, float random, bool topple)
	{
		GameObject uidmg = GameObject.Instantiate (damageUI, sandBag.transform.position, Quaternion.identity);
		uidmg.GetComponent<TextMesh> ().text = "" + Mathf.RoundToInt (damage * random);
		if (sandBag.tag == "Enemy") {
			enemyHealth [0] -= Mathf.RoundToInt (damage * random);
			if (topple == true) {
				BattleEnemyAI ai = sandBag.GetComponent<BattleEnemyAI> ();
				if (ai.curState ==  BattleEnemyAI.State.Think) {//think
					ai.curState = BattleEnemyAI.State.Topple;
				}
			}
		} else if (sandBag.tag == "Player") {
			for (int i = 0; i < players.Count; i++) {
				if (players [i] == sandBag) {
					playerHealth [i] -= Mathf.RoundToInt (damage * random);
				}
			}
		}
	}

	public void SpawnRandomCam ()
	{
		int i = Random.Range (0, camPathPrefabs.Count ());
		GameObject.Instantiate (camPathPrefabs [i], Vector3.zero, Quaternion.identity);
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.R)) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		}
		if (Input.GetKeyDown (KeyCode.F)) {
			fadeIn.color = fadeColor;
		}
		if (Input.GetKeyDown (KeyCode.C)) {
			charge [0] = 200;
			charge [1] = 200;
			charge [2] = 200;
			charge [3] = 200;
		}

		for (int i = 0; i < charge.Length; i++) {
			if (charge [i] > 200) {
				charge [i] = 200;
			} else if (charge [i] < 0) {
				charge [i] = 0;
			}
		}

		for (int i = 0; i < buttonObjects.Length; i++) {
			buttonObjects [i].transform.localScale = Vector3.MoveTowards (buttonObjects [i].transform.localScale, new Vector3 (1, 1, buttonObjects [i].transform.localScale.z), Time.deltaTime * 10);
			buttonObjects [i].transform.localScale = Vector3.MoveTowards (buttonObjects [i].transform.localScale, new Vector3 (buttonObjects [i].transform.localScale.x, buttonObjects [i].transform.localScale.y, 1), Time.deltaTime);
			abxyCooldown [i] -= Time.unscaledDeltaTime;
			if (abxyCooldown [i] < 0) {
				abxyCooldown [i] = 0;
			}
			if (buttonObjects [i].transform.localScale.z != 1) {
				abxyCooldown [i] = buttonObjects [i].transform.localScale.z - 1;
			}
		}

		if (fadeIn.color != Color.clear) {
			FadeIn ();
		}
		bool canPress;
		canPress = true;
		if (atackNameUI.activeSelf == true) {
			canPress = false;
		} else if (coolDownTimer != 0) {
			canPress = false;
		}
		if (canPress == true) {
			if (turnAtack == false) {
				if (Vector2.SqrMagnitude (new Vector2 (Mathf.Abs (Input.GetAxis ("DPadLeftRight")), Mathf.Abs (Input.GetAxis ("DPadUpDown")))) != 0) {
					if (Input.GetAxis ("DPadLeftRight") > 0) {
						atackingPlayer = 2;
					} else {
						atackingPlayer = 3;
					}
					if (Input.GetAxis ("DPadUpDown") != 0) {
						if (Input.GetAxis ("DPadUpDown") > 0) {
							atackingPlayer = 1;
						} else {
							atackingPlayer = 4;
						}
					}
				}
				if (curState == State.PlayerAttack) {
					curState = State.Normal;
				}
			} else {
				curState = State.PlayerAttack;
			}

			if (turnAtack == true) {
				TurnAtack ();
				camSlowMotionEffect.SetActive (true);
				selectAtackUI [atackingPlayer - 1].SetActive (true);
			} else {
				camSlowMotionEffect.SetActive (false);
				selectAtackUI [atackingPlayer - 1].SetActive (false);
				//}
				if (Input.GetAxis ("DPadUpDown") != 0) {
					bool canAtack = false;
					if (charge [atackingPlayer - 1] >= 100) {
						canAtack = true;
					}
					if (canAtack == true) {
						turnAtack = true;
					}
				}
				if (Input.GetAxis ("DPadLeftRight") != 0) {
					bool canAtack = false;
					if (charge [atackingPlayer - 1] >= 100) {
						canAtack = true;
					}
					if (canAtack == true) {
						turnAtack = true;
					}
				}
			}
		}
		if (curState == State.EnemyAttack) {//enemyAttack
			if (atackNameUI.activeSelf == false) {
				fadeIn.color = fadeColor;
			}
			atackNameUI.SetActive (true);
		}

		if (curState == State.Victory) {//Victory
			victory.SetActive (true);
			for (int i = 0; i < GameObject.Find ("Canvas").transform.childCount; i++) {
				if (GameObject.Find ("Canvas").transform.GetChild (i).name != "Victory") {
					Destroy (GameObject.Find ("Canvas").transform.GetChild (i).gameObject);
				}
			}
			curState = State.End;//fuck
		}

		if (curState == State.Cooldown) {//cooldown
			coolDownTimer = Mathf.MoveTowards (coolDownTimer, 0, Time.deltaTime);
			if (coolDownTimer == 0) {
				curState = State.Normal;//normal
			}
		}

		Time.timeScale = timeScale;
	}

	public void BackToNormal (bool coolDown)
	{
		fadeIn.color = fadeColor;
		if (coolDown == false) {
			curState = State.Normal;
		} else {
			curState = State.Cooldown;
			coolDownTimer = 0.5f;
		}
		//charge [atackingPlayer - 1] = 0;
		turnAtack = false;
		atackNameUI.SetActive (false);
		selectAtackUI [atackingPlayer - 1].SetActive (false);
	}

	void TurnAtack ()
	{
		if (Time.timeScale != 0) {
			fadeIn.color = fadeColor;
		}
		timeScale = 0;
	}

	void FadeIn ()
	{
		fadeIn.color = Color.Lerp (fadeIn.color, Color.clear, Time.unscaledDeltaTime * 3);
		if (fadeIn.color.a < 0.5f) {
			//fadeIn.color = Color.clear;
		}
	}
}
