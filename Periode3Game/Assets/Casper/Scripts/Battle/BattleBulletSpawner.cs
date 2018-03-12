using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBulletSpawner : MonoBehaviour
{

	private Transform child;
	public GameObject[] toSpawn;
	private BattleManager manager;
	public List<GameObject> toDestroy;

	void Start ()
	{
		child = transform.GetChild (0).transform;
		manager = GameObject.FindObjectOfType<BattleManager> ();
	}

	void Update ()
	{
		if(transform.name != transform.name){
		if (child.childCount == 0) {
				ActualCode ();
		}
	}
		if (child.childCount != 0) {
			if (Vector3.Distance (GameObject.FindGameObjectWithTag ("BulletHit").transform.position, child.GetChild (child.childCount - 1).transform.position) < 160) {
				ActualCode ();
			}
		} else {
			ActualCode ();
		}

		/*
		 * 
		 * 
		*/ 
	}


	void ActualCode(){
		//if(child.GetChild(child.childCount - 1).GetComponent<Renderer>().isVisible == true){
		GameObject g = GameObject.Instantiate (toSpawn [Random.Range (0, toSpawn.Length)], transform.position, Quaternion.identity);
		g.transform.parent = transform;
		for (int i = 0; i < child.childCount; i++) {
			child.GetChild (i).tag = "Untagged";
		}
		child.DetachChildren ();
		Destroy (child.gameObject);
		child = g.transform;
		//List<GameObject> toDestroy = GameObject.FindGameObjectsWithTag("A_Only");

	}

	void DestroyDead(){
		toDestroy.Clear ();
		toDestroy.AddRange (GameObject.FindGameObjectsWithTag ("Bullet"));
		for (int i = 0; i < toDestroy.Count; i++) {
			if (toDestroy [i].transform.parent != child) {
				toDestroy.Remove (toDestroy [i]);
			}
		}

		if (manager.playerHealth [0] <= 0) {
			for (int i = 0; i < toDestroy.Count; i++) {
				if (toDestroy [i].layer == 11) {
					Destroy (toDestroy [i]);
				}
			}
		}
		if (manager.playerHealth [1] <= 0) {
			for (int i = 0; i < toDestroy.Count; i++) {
				if (toDestroy [i].layer == 10) {
					Destroy (toDestroy [i]);
				}
			}
		}
		if (manager.playerHealth [2] <= 0) {
			for (int i = 0; i < toDestroy.Count; i++) {
				if (toDestroy [i].layer == 9) {
					Destroy (toDestroy [i]);
				}
			}
		}
		if (manager.playerHealth [3] <= 0) {
			for (int i = 0; i < toDestroy.Count; i++) {
				if (toDestroy [i].layer == 8) {
					Destroy (toDestroy [i]);
				}
			}
		}
		//if (child.GetChild (child.childCount - 1).GetComponent<Renderer> ().isVisible == true) {
		//for (int i = 0; i < child.childCount; i++) {
		//child.GetChild (i).tag = "Untagged";
		//child.GetChild (i).parent = null;
		//}
		//}
		toDestroy.Clear ();
	}

}
