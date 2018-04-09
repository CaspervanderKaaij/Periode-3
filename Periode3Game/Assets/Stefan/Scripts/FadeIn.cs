using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour {

    public GameObject options;
    public GameObject canvas;
    public GameObject[] faders;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < faders.Length; i++)
        {
            faders[i].GetComponent<Image>().enabled = false;
            faders[i].GetComponent<Text>().enabled = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKeyDown)
        {
            for (int i = 0; i < faders.Length; i++)
            {
                faders[i].GetComponent<Image>().enabled = true;
                faders[i].GetComponent<Text>().enabled = true;
            }

            gameObject.GetComponent<FadeIn>().enabled = false;
        }
	}
}
