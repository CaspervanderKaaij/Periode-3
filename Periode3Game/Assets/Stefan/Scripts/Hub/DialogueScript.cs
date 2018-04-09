using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueScript : MonoBehaviour {

    public string[] dia;
    public float textSpeed;
    public Text text;
    public int lineNumber = 0;

	// Use this for initialization
	void Start () {
        StartCoroutine(DiaTimer(2));
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator DiaTimer(float t)
    {
        yield return new WaitForSeconds(t);
        text.text = dia[lineNumber];
        lineNumber++;
        StartCoroutine(DiaTimer(textSpeed));
    }
}
