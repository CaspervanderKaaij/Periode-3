using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QBlock : Brick
{

    public Sprite newSprite;
    public GameObject toSpawn;
    public int uses = 1;

    void Start()
    {
        origin = transform.parent.position;
    }
    public override void Hit()
    {
        transform.parent.position += Vector3.up;
        if (uses <= 1)
        {
            // transform.GetComponent<Collider>().enabled = false;
			if(newSprite != null){
            	transform.parent.GetComponent<SpriteRenderer>().sprite = newSprite;
			}
            if (transform.parent.GetComponent<Animator>() != null)
            {
                transform.parent.GetComponent<Animator>().enabled = false;
            }
            transform.parent.GetComponent<Collider>().enabled = true;
            Vector3 pos = transform.parent.position;
			if(toSpawn != null){
            	Instantiate(toSpawn, new Vector3(pos.x, pos.y + transform.parent.localScale.y, pos.z), Quaternion.identity);
			}
			toSpawn = null;
            //transform.parent.position = origin;
        }
        else
        {
            uses -= 1;
            Vector3 pos = transform.parent.position;
            Instantiate(toSpawn, new Vector3(pos.x, pos.y + transform.parent.localScale.y, pos.z), Quaternion.identity);
        }
    }

    void Update()
    {
        backToOrigin();
    }

}
