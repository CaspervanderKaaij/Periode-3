using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCamera : MonoBehaviour
{

	private Transform cam;
	public List<Transform> pathOrder;
	public int curPathPoint = 0;
	public float speed = 10;
	public bool loop = true;
	private BattleManager manager;
	public bool lookAtEnemy = true;
	public Vector3 hardStartPos;
	public Vector3 hardStartEuler;
	private Vector3 center;
	public float shakeStrength = 1;
	private bool shaking = false;


	void Start ()
	{
		transform.position = hardStartPos;
		center = Camera.main.transform.position;
		//speed = 0;//DELETE THIS
		transform.eulerAngles = hardStartEuler;
		for (int i = 0; i < transform.childCount; i++) {
			pathOrder.Add (transform.GetChild (i));
		}
		cam = Camera.main.transform;
		manager = GameObject.FindObjectOfType<BattleManager> ();
		cam.position = pathOrder [0].position;
		cam.eulerAngles = pathOrder [0].eulerAngles;
	}

	void Update ()
	{
		//if (transform.position != pathOrder [curPathPoint]) {
		if (Vector3.Distance (cam.position, pathOrder [curPathPoint].position) > 0.3f) {
			cam.position = Vector3.MoveTowards (cam.position, pathOrder [curPathPoint].position, Time.unscaledDeltaTime * speed);
			if (lookAtEnemy == false) {
				//Debug.Log (curPathPoint);
				cam.eulerAngles = Vector3.Lerp (cam.eulerAngles, pathOrder [curPathPoint - 1].eulerAngles, Time.unscaledDeltaTime * 2);
			} else {
				cam.LookAt (manager.enemies [0].transform.position);
			}
		} else if (curPathPoint < pathOrder.Count - 1) {
			curPathPoint++;
		} else if (loop == true) {
			curPathPoint = 0;
		} else {
			manager.SpawnRandomCam ();
			Destroy (gameObject);
		}
		//if(Input.GetKeyDown(KeyCode.S)){
		//	StartShake(0.1f,0.2f);
		//}
		if(shaking == true){
			CamShake();
		}
	}

	void CamShake(){
		Camera.main.transform.position = center + new Vector3(Random.Range(shakeStrength,-shakeStrength),Random.Range(shakeStrength,-shakeStrength),Random.Range(shakeStrength,-shakeStrength));
	}

	public void StartShake(float time,float strength){
		shaking = true;
		StopCoroutine(StopShake(time));
		shakeStrength = strength;
		center = Camera.main.transform.position;
		StartCoroutine(StopShake(time));
	}

	IEnumerator StopShake(float time){
		yield return new WaitForSeconds(time);
		shaking = false;
		Camera.main.transform.position = center;
	}
}
