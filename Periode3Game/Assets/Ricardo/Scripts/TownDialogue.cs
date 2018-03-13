using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TownDialogue : MonoBehaviour {

public List<string> text;
public List<string> talker;
public Text talkerTxt;
private int curTextInt = 0;
private string textGoal;
private string curText;
private int curPage = 0;
Text txt;
TownManager manager;
public bool yesNo = false;
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
			if(manager.yesNo.activeSelf == false){
				NewText();
			}
		}
		if(manager.yesNo.activeSelf == false){
			TextUpdate();
		}
		txt.text = curText;
	}

	void NewText(){
		if(curPage != text.Count - 1){
		curPage += 1;
		curTextInt = 0;
		txt.text = "";
		textGoal = text[curPage];
		curText = "";
		talkerTxt.text = talker[curPage];
		} else if(first == true) {
			curPage = 0;
			curTextInt = 0;
			txt.text = "";
			textGoal = text[curPage];
			curText = "";
			talkerTxt.text = talker[curPage];
		} else {
			text.Clear();
			text.Add("graljsn");
			GameObject.FindObjectOfType<TownMovement>().curState = TownMovement.State.Normal;
			if(yesNo == false){
			manager.dialogue.SetActive(false);
			} else {
				manager.yesNo.SetActive(true);
			}
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
