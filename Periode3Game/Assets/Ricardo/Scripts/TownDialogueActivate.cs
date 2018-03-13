using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownDialogueActivate : MonoBehaviour {

private TownManager manager;
[HideInInspector]
public bool on = true;
public string[] txt;
public string[] talker;
public bool yesNo = false;
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
					manager.dialogue.transform.GetChild(3).GetComponent<TownDialogue>().talker.Clear();
					manager.dialogue.transform.GetChild(3).GetComponent<TownDialogue>().talker.AddRange(talker);
					manager.dialogue.transform.GetChild(3).GetComponent<TownDialogue>().yesNo = yesNo;
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
