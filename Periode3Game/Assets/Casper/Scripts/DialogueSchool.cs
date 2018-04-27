using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueSchool : MonoBehaviour
{

    public string[] dialogue;
    public int curText;
    public Text txt;

    void Start()
    {
		curText = 1;
        UpdateText();
    }
    void Update()
    {
		if (Input.anyKeyDown)
        {
			Answer();
		}
    }

	public virtual void Answer(){
            switch (Input.inputString)
            {

                case "2":
                    curText *= 2;
                    UpdateText();
                    break;
                case "1":
                    curText *= 2;
                    curText += 1;
                    UpdateText();
                    break;
            }
	}

    public void UpdateText()
    {
        txt.text = dialogue[curText - 1];
    }
}
