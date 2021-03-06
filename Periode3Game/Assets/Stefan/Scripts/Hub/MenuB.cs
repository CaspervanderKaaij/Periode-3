﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuB : MonoBehaviour {
    //Player GameObject
    public GameObject player;

    //Vibrations Toggle
    public Toggle checker;
    public int vibrate;
    public bool vT;

    //Sound slider
    public Slider slider;
    public float maxV = 1;
    public float minV = 0;

    //These are the continue/exit buttons
    public Button c;
    public Button e;
    
    //These are all the player buttons
    public Button one;
    public Button two;
    public Button three;
    public Button four;

    public GameObject c1;
    public GameObject c2;
    public GameObject c3;
    public GameObject c4;
    public GameObject movementHolder;

    public GameObject menu;
    public GameObject conv;
    public bool b;
    public Renderer r;
    public GameObject p;

    //this is the int that goes to the other scenes
    //This is the character which was chosen by the player
    public int character;

	// Use this for initialization
	void Start () {
        r = p.gameObject.GetComponent<Renderer>();

        Button on = one.GetComponent<Button>();
        Button tw = two.GetComponent<Button>();
        Button th = three.GetComponent<Button>();
        Button fo = four.GetComponent<Button>();
        on.onClick.AddListener(First);
        tw.onClick.AddListener(Second);
        th.onClick.AddListener(Third);
        fo.onClick.AddListener(Fourth);

        Button co = c.GetComponent<Button>();
        Button ex = e.GetComponent<Button>();
        co.onClick.AddListener(Continue);
        ex.onClick.AddListener(Exit);
        menu.SetActive(false);

        if (PlayerPrefs.GetInt("Vibrate") == 1)
        {
            vT = true;
            checker.isOn = true;
        } else
        {
            if (PlayerPrefs.GetInt("Vibrate") == 0)
            {
                vT = false;
                checker.isOn = false;
            }
        }

        slider.value = PlayerPrefs.GetFloat("Volume");

        if (PlayerPrefs.GetInt("Character") == 1)
        {
            First();
        }
        else
        {
            if (PlayerPrefs.GetInt("Character") == 2)
            {
                Second();
            }
            else
            {
                if (PlayerPrefs.GetInt("Character") == 3)
                {
                    Third();
                }
                else
                {
                    if (PlayerPrefs.GetInt("Character") == 4)
                    {
                        Fourth();
                    }
                }
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        PlayerPrefs.SetInt("Character", character);
        PlayerPrefs.SetFloat("Volume", slider.value);
        if (checker.isOn == true)
        {
            PlayerPrefs.SetInt("Vibrate", 1);
            vT = true;
        }
        else
        {
            if(checker.isOn == false)
            {
                PlayerPrefs.SetInt("Vibrate", 0);
                vT = false;
            }
        }

        slider.maxValue = maxV;
        slider.minValue = minV;
        AudioListener.volume = slider.value;

        if (Input.GetButtonDown("Menu"))
        {
            if(b == false)
            {
                b = true;
                menu.SetActive(true);
                //player.GetComponent<Movement>().enabled = false;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                if(b == true)
                {
                    b = false;
                    menu.SetActive(false);
                    //player.GetComponent<Movement>().enabled = true;
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                }
            }
            
        }
    }

    public void Continue()
    {
        menu.SetActive(false);
        b = false;
        //player.GetComponent<Movement>().enabled = true;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void First()
    {
        movementHolder.GetComponent<Movement>().character = c1;
        c1.SetActive(true);
        c2.SetActive(false);
        c3.SetActive(false);
        c4.SetActive(false);
        character = 1;
    }

    public void Second()
    {
        movementHolder.GetComponent<Movement>().character = c2;
        c1.SetActive(false);
        c2.SetActive(true);
        c3.SetActive(false);
        c4.SetActive(false);
        character = 2;
    }

    public void Third()
    {
        movementHolder.GetComponent<Movement>().character = c3;
        c1.SetActive(false);
        c2.SetActive(false);
        c3.SetActive(true);
        c4.SetActive(false);
        character = 3;
    }

    public void Fourth()
    {
        movementHolder.GetComponent<Movement>().character = c4;
        c1.SetActive(false);
        c2.SetActive(false);
        c3.SetActive(false);
        c4.SetActive(true);
        character = 4;
    }

    public void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }
}
