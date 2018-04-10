using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour {

    public GameObject canvas;

	// Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        canvas.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKeyDown)
        {
            canvas.SetActive(true);
            gameObject.GetComponent<FadeIn>().enabled = false;
        }
	}
}
