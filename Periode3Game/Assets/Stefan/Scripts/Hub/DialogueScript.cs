using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueScript : MonoBehaviour {

    public string[] dia;
    public float textSpeed;
    public Text text;
    public int lineNumber = 0;

    public Camera cam;
    public bool screenshake;

	// Use this for initialization
	void Start () {
        StartCoroutine(DiaTimer(1));
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("SpaceHubScene");
        }
    }

    public IEnumerator DiaTimer(float t)
    {
        yield return new WaitForSeconds(t);
        text.text = dia[lineNumber];
        lineNumber++;
        StartCoroutine(DiaTimer(textSpeed));
        if(lineNumber >= (dia.Length / 2))
        {
            screenshake = true;
        }
        if (lineNumber >= dia.Length)
        {
            SceneManager.LoadScene("SpaceHubScene");
        }
    }
}
