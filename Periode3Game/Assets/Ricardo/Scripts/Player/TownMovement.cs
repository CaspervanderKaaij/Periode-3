using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownMovement : MonoBehaviour {

private CharacterController cc;
public float speed = 5;
private TownTextBuble buble;
private Animator anim;

public enum State
{
	Normal,
	Dialogue
}

public State curState = State.Normal;
	void Start () {
		cc = transform.GetComponent<CharacterController>();
		buble = GameObject.FindObjectOfType<TownTextBuble>();
		anim = transform.GetChild(0).GetComponent<Animator>();
	}
	
	void Update () {
		if(curState == State.Normal){
			Move();
		} else {
			anim.SetFloat("anal",0);
			buble.visible = false;
		}
	}

	void Move(){
		Vector2 horVert = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
		if(Vector2.SqrMagnitude(horVert) != 0){
			float angle = Mathf.Atan2(horVert.x,horVert.y) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
			transform.eulerAngles = new Vector3(transform.eulerAngles.x,angle,transform.eulerAngles.z);
		}
		cc.Move(transform.TransformDirection(0,-9.81f * Time.deltaTime,speed * Mathf.Min(Vector2.SqrMagnitude(horVert),1) * Time.deltaTime));
		anim.SetFloat("anal",Mathf.Min(Vector2.SqrMagnitude(horVert),1));
	}

	void OnTriggerStay(Collider other) {
		if(other.GetComponent<TownDialogueActivate>() != null){
			if(curState == State.Normal){
				if(other.GetComponent<TownDialogueActivate>().on == true){
					buble.visible = true;
				}
			}
		}
	}
	void OnTriggerExit(Collider other) {
		if(other.GetComponent<TownDialogueActivate>() != null){
			buble.visible = false;
		}
	}
}
