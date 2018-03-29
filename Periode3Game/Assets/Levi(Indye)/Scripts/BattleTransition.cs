using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleTransition : MonoBehaviour {
    public int EnemyID;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyID = collision.gameObject.GetComponent<ID>().slimeId;
            PlayerPrefs.SetInt("enemy", EnemyID);
            Application.LoadLevel("Scene");
        }
    }
}
