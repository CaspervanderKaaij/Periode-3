using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ID : MonoBehaviour
{
    public int battleId;
    public int overworldId;
    public AudioClip battleMusic;
    // Use this for initialization
    void Start()
    {
        //slimeId = 0; Casper hier, dit is gewoon fucking dom lol. Dit is zeg maar een int die de goede enemy laad. 0 = Slime; 1 = Talus etc. Nu laad je altijd de slime.. WTF
		if(GameObject.FindGameObjectWithTag("Music").GetComponent<NoDestroyLoad>().enemyDead[overworldId] == true){
			Destroy(gameObject);
		} else {
			transform.GetComponent<Collider>().enabled = true;
		}
    }

    // Update is called once per frame
    //void Update () {
    // ook er uit geslashed door een zekere jezus	
    //}
}
