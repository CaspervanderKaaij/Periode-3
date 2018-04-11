using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoopaShell : LinearMove {

public enum State
{
	Normal,
	ShellStill,
	ShellMove
}
public State curState = State.Normal;
private float speedSave;
Hitbox hitbox;
public GameObject[] sprites;

	void Start () {
		cc = transform.GetComponent<CharacterController>();
		speedSave = speed;
		hitbox = transform.GetComponent<Hitbox>();
	}
	
	void Update () {
		Activate();
		switch (curState)
		{
			case State.Normal:
			Normal();
			break;
			case State.ShellStill:
			ShellStill();
			break;
			case State.ShellMove:
			ShellMove();
			break;
		}
	}

	void Normal(){
		transform.GetChild(2).gameObject.SetActive(false);
		speed = speedSave / 5;
		sprites[0].SetActive(false);
		sprites[1].SetActive(true);
		if(moveVector.x > 0){
			sprites[1].GetComponent<SpriteRenderer>().flipX = true;
		} else {
			sprites[1].GetComponent<SpriteRenderer>().flipX = false;
		}
		//hitbox.kill = true;
		if(hitbox.gotHit == true){
			hitbox.gotHit = false;
			curState = State.ShellStill;
		}
	}

	void ShellStill(){
		transform.GetChild(2).gameObject.SetActive(false);
		speed = 0;
		sprites[1].SetActive(false);
		sprites[0].SetActive(true);
		//hitbox.kill = false;
		if(hitbox.gotHit == true){
			hitbox.gotHit = false;
			if(GameObject.FindGameObjectWithTag("Player").transform.position.x > transform.position.x){
				moveVector = new Vector2(Mathf.Abs(moveVector.x),moveVector.y);
			} else {
				moveVector = new Vector2(-Mathf.Abs(moveVector.x),moveVector.y);
			}
			curState = State.ShellMove;
		}
	}

	void ShellMove(){
		speed = speedSave;
		transform.GetChild(2).gameObject.SetActive(true);
		sprites[1].SetActive(false);
		sprites[0].SetActive(true);
		//hitbox.kill = true;
		if(hitbox.gotHit == true){
			hitbox.gotHit = false;
			curState = State.ShellStill;
		}
	}
}
