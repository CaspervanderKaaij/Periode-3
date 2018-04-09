using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public Button start;
    public Button options;
    public Button exit;

	// Use this for initialization
	void Start () {
        start.onClick.AddListener(StartButton);
        options.onClick.AddListener(OptionsButton);
        exit.onClick.AddListener(ExitButton);
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public void StartButton()
    {
        print("Start");
    }

    public void OptionsButton()
    {
        print("Options");
    }

    public void ExitButton()
    {
        print("Exit");
    }
}
