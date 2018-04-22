using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChanger : MonoBehaviour {

    public GameObject c1;
    public GameObject c2;
    public GameObject c3;
    public GameObject c4;
    public GameObject movementHolder;
    public int character;

    void Start () {
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

    

    public void First()
    {
        c1.SetActive(true);
        c2.SetActive(false);
        c3.SetActive(false);
        c4.SetActive(false);
        transform.GetComponent<MovementScript>().anim = c1.GetComponent<Animator>();
        //c1.GetComponent<Animator>().Play("anal");
        character = 1;
    }

    public void Second()
    {
        c1.SetActive(false);
        c2.SetActive(true);
        c3.SetActive(false);
        c4.SetActive(false);
        transform.GetComponent<MovementScript>().anim = c2.GetComponent<Animator>();
        //c2.GetComponent<Animator>().Play("anal");
        character = 2;
    }

    public void Third()
    {
        c1.SetActive(false);
        c2.SetActive(false);
        c3.SetActive(true);
        c4.SetActive(false);
        transform.GetComponent<MovementScript>().anim = c3.GetComponent<Animator>();
       // c3.GetComponent<Animator>().Play("anal");
        character = 3;
    }

    public void Fourth()
    {
        c1.SetActive(false);
        c2.SetActive(false);
        c3.SetActive(false);
        c4.SetActive(true);
        transform.GetComponent<MovementScript>().anim = c4.GetComponent<Animator>();
        //c4.GetComponent<Animator>().Play("anal");
        character = 4;
    }
}
