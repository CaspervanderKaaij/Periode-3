using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUIBullet : MonoBehaviour
{

    public float speed = 1;
    //public Collider hitter;
    //public Collider destroyer;
    [HideInInspector]
    public string buttonName;
     [HideInInspector]
    public float bufferButton;
     [HideInInspector]
    public BattleManager manager;
     [HideInInspector]
    public int player = 0;
     [HideInInspector]
    public bool buttonDown = false;
    public GameObject effect;
     [HideInInspector]
    public bool startDone = false;
    public float destroyDistance = 0;
    private Vector3 destroyDistanceOrigin;
    [HideInInspector]
    public BattleCharacter btlchr;
    public AudioClip hitSFX;
    public float damage = 100;
    //private bool abxyCooldownNextFrame = false;

    void _Start()
    {
        if (transform.gameObject.layer == 8)
        {
            buttonName = "Y_Button";
            player = 3;
        }
        else if (transform.gameObject.layer == 9)
        {
            buttonName = "X_Button";
            player = 2;
        }
        else if (transform.gameObject.layer == 10)
        {
            buttonName = "B_Button";
            player = 1;
        }
        else if (transform.gameObject.layer == 11)
        {
            buttonName = "A_Button";
            player = 0;
        }

        manager = GameObject.FindObjectOfType<BattleManager>();
        btlchr = manager.players[player].GetComponent<BattleCharacter>();
    }

    public void Normal()
    {

        if (startDone == false)
        {
            _Start();
            startDone = true;
        }
        else
        {

            if (manager.curState == BattleManager.State.Normal)
            {
                transform.Translate(-speed * Time.deltaTime, 0, 0);
            }
            if (bufferButton > 0)
            {
                bufferButton -= Time.deltaTime;
            }
            if (Input.GetAxisRaw(buttonName) == 1)
            {
                if (buttonDown == false)
                {
                    if (manager.abxyCooldown[player] == 0)
                    {
                        buttonDown = true;
                        //manager.buttonObjects[player].transform.localScale = new Vector3(1.25f, 1.25f, 1.3f);
                    }
                }
            }
            else
            {
                buttonDown = false;
            }
        }
    }

    void OnTriggerStay(Collider col)
    {
		
	//	if(manager != null){
        manager = GameObject.FindObjectOfType<BattleManager>();
        if (manager.curState == BattleManager.State.Normal)
        {//normal
            if (col.tag == "BulletHit")
            {
               // Debug.Log("pussy");
                //if(bufferButton > 0){
                if (manager.buttonObjects[player].transform.localScale.x > 1)
                {
                    Hit();
                }
            }
            else if (col.tag == "BulletMiss")
            {
                if(destroyDistance == 0){
                manager.charge[player] -= 5 * Random.Range(0.85f, 1.15f);
                Destroy(gameObject);
                } else if(destroyDistanceOrigin == Vector3.zero) {
                    destroyDistanceOrigin = transform.position;
                } else if(Input.GetButton(buttonName) == true) {
                    if(Vector3.Distance(transform.position,destroyDistanceOrigin) > destroyDistance){
                        Destroy(gameObject);
                    }
                } else {
                    if(Vector3.Distance(transform.position,destroyDistanceOrigin) < destroyDistance / 4){
                    manager.charge[player] -= 5 * Random.Range(0.85f, 1.15f);
                    }
                        Destroy(gameObject);
                }
            }
        }
	//} else {
		//_Start();
	//}
    }

    public virtual void Hit()
    {
        btlchr.Animate();
        GameObject.FindObjectOfType<BattleCamera>().StartShake(0.1f,0.05f);
        manager.PlaySound(hitSFX,1,0.5f,manager.players[player].transform.position,2f);
        bufferButton = 0;
        manager.DoDamage(manager.enemies[0].gameObject, damage, Random.Range(0.85f, 1.15f), false);
        manager.charge[player] += 15 * Random.Range(0.85f, 1.15f);
        Vector3 scale = manager.buttonObjects[player].transform.localScale;
        manager.abxyCooldown[player] = 0;
        GameObject g = GameObject.Instantiate(effect, manager.buttonObjects[player].transform);
        g.transform.position = g.transform.parent.position;
        g.transform.SetParent(GameObject.FindGameObjectWithTag("UIBulletEffect").transform);
        manager.Vibrate(0.1f, 0.45f);
        Destroy(gameObject);
    }
}

