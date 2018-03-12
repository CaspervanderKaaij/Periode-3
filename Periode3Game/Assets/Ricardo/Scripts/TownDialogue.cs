using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TownDialogue : MonoBehaviour {

public List<string> text;
private int curTextInt = 0;
private string textGoal;
private string curText;
private int curPage = 0;
Text txt;
TownManager manager;
[HideInInspector]
public bool first = true;
	void Start () {
		txt = transform.GetComponent<Text>();
		NewText();
		manager = GameObject.FindObjectOfType<TownManager>();
		curPage = -1;
	}
	
	void Update () {
		GameObject.FindObjectOfType<TownMovement>().curState = TownMovement.State.Dialogue;
		if(Input.GetButtonDown("A_Button")){
		NewText();
		}
		TextUpdate();
		txt.text = curText;
	}

	void NewText(){
		if(curPage != text.Count - 1){
		curPage += 1;
		curTextInt = 0;
		txt.text = "";
		textGoal = text[curPage];
		curText = "";
		} else if(first == true) {
			curPage = 0;
			curTextInt = 0;
			txt.text = "";
			textGoal = text[curPage];
			curText = "";
		} else {
			text.Clear();
			text.Add("graljsn");
			GameObject.FindObjectOfType<TownMovement>().curState = TownMovement.State.Normal;
			manager.dialogue.SetActive(false);
		}
		first = false;
	}

	void TextUpdate(){
		if(textGoal.Length != curTextInt){
		curText += textGoal[curTextInt];
		curTextInt += 1;
		}
	}
}
