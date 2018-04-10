using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public Button b;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if(Input.GetButtonDown("Fire1"))
        {
            b.Select();
        }
    }

    public void StartButton()
    {
        print("Start");
        SceneManager.LoadScene("CutSceneToOtherSceneScene");
    }

    public void OptionsButton()
    {
        print("Options");
    }

    public void ExitButton()
    {
        print("Exit");
        Application.Quit();
    }
}
