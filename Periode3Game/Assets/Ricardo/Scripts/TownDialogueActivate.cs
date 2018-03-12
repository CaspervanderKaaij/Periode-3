using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownDialogueActivate : MonoBehaviour {

private TownManager manager;
[HideInInspector]
public bool on = true;
public string[] txt;
	void Start () {
		manager = GameObject.FindObjectOfType<TownManager>();
	}
	
	void Update () {
		
	}

	 void OnTriggerStay(Collider other) {
		  if(on == true){
			  if(other.tag == "Player"){
				if(Input.GetButtonDown("A_Button")){
					manager.dialogue.SetActive(true);
					manager.dialogue.transform.GetChild(3).GetComponent<TownDialogue>().text.Clear();
					manager.dialogue.transform.GetChild(3).GetComponent<TownDialogue>().text.AddRange(txt);
					manager.dialogue.transform.GetChild(3).GetComponent<TownDialogue>().first = true;
					on = false;
				}
			}
		}
	}

	void OnTriggerExit(){
		on = true;
	}
}
