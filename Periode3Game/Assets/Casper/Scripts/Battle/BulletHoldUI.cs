using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHoldUI : BattleUIBullet {

private bool holding = false;
private float timer = 0;

	void Update(){
		Normal ();
        if(holding == true){
            Hit();
        }
	}

	public override void Hit(){
        Debug.Log("ass");
		bufferButton = 0;
        timer += Time.deltaTime;
        if(timer > 0.3f){
            timer -= 0.3f;
            manager.DoDamage(manager.enemies[0].gameObject, 100, Random.Range(0.85f, 1.15f), false);
            GameObject g = GameObject.Instantiate(effect, manager.buttonObjects[player].transform);
            g.transform.position = g.transform.parent.position;
            g.transform.SetParent(GameObject.FindGameObjectWithTag("UIBulletEffect").transform);
        }
        manager.charge[player] += 4.5f * Random.Range(0.85f, 1.15f) * Time.deltaTime;
        Vector3 scale = manager.buttonObjects[player].transform.localScale;
        manager.abxyCooldown[player] = 0;
        manager.Vibrate(0.1f, 0.45f);
        if(Input.GetButton(buttonName) == true){
            holding = true;
        } else {
            holding = false;
        }
        //Destroy(gameObject);
	}
}
