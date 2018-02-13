using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleAtackSelecter : MonoBehaviour
{

	public GameObject[] positions;
	public int curPos = 0;
	private bool canPress = false;
	BattleManager manager;
	public string[] atackName;

	void Start ()
	{
		canPress = false;
		manager = GameObject.FindObjectOfType<BattleManager> ();
	}

	void Update ()
	{
		transform.position = positions [curPos].transform.position;
		if (Input.GetAxisRaw ("DPadLeftRight") != 0) {
			if (canPress == true) {
				if (curPos + Mathf.RoundToInt (Input.GetAxisRaw ("DPadLeftRight")) > -1) {
					if (curPos + Mathf.RoundToInt (Input.GetAxisRaw ("DPadLeftRight")) < positions.Length) {
						curPos += Mathf.RoundToInt (Input.GetAxisRaw ("DPadLeftRight"));
					}
				}
			}
			canPress = false;
		} else if (Input.GetAxisRaw ("DPadUpDown") == 0) {
			canPress = true;
		}

		if (Input.GetAxisRaw ("DPadUpDown") != 0) {
			if (canPress == true) {
				if (curPos < 4) {
					curPos += (positions.Length / 2);
				} else {
					curPos -= (positions.Length / 2);
				}
			}
			canPress = false;
		} else if (Input.GetAxisRaw ("DPadLeftRight") == 0) {
			canPress = true;
		}

		if (Input.GetButtonDown ("A_Button")) {
			bool canSelect = false;
			if (curPos < 3) {
				canSelect = true;
			} else if (manager.charge [manager.atackingPlayer - 1] >= 200) {
				canSelect = true;
			}
			if (canSelect == true) {
				manager.state = "atack";
				manager.fadeIn.color = Color.white;
				//Debug.Log (manager.atackingPlayer - 1);
				//Debug.Log (manager.players[manager.atackingPlayer - 1].name);
				GameObject atackNameUI = manager.atackNameUI;
				atackNameUI.SetActive (true);
				atackNameUI.transform.GetChild (1).GetComponent<Text> ().text = atackName [curPos];
				manager.players [manager.atackingPlayer - 1].GetComponent<BattleCharacter> ().atackNumber = curPos;
				manager.turnAtack = false;
				manager.timeScale = 1;
				manager.charge [manager.atackingPlayer - 1] = 0;
			}
		}
	}
}
