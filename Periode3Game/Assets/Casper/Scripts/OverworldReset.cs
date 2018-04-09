using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldReset : MonoBehaviour
{
    void Start()
    {
		//startPos = GameObject.FindWithTag("Player").transform.position;
		if(new Vector3(PlayerPrefs.GetFloat("overworldLastX"), PlayerPrefs.GetFloat("overworldLastY"), PlayerPrefs.GetFloat("overworldLastZ")) != Vector3.zero){
        GameObject.FindWithTag("Player").transform.position = new Vector3(PlayerPrefs.GetFloat("overworldLastX"), PlayerPrefs.GetFloat("overworldLastY"), PlayerPrefs.GetFloat("overworldLastZ"));
		}
    }
}
