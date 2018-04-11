using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagPole : MonoBehaviour {

	public AudioClip stageComplete;

	void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			Mario mario = other.GetComponent<Mario>();
			mario.curState = Mario.State.Finish;
			Camera.main.GetComponent<AudioSource>().Stop();
			Camera.main.GetComponent<AudioSource>().clip = stageComplete;
			Camera.main.GetComponent<AudioSource>().time = 0;
			Camera.main.GetComponent<AudioSource>().loop = false;
			Camera.main.GetComponent<AudioSource>().Play();
		}
	}
}
