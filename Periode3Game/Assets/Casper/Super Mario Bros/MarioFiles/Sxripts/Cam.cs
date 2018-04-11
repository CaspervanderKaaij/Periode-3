using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{

    public GameObject player;
    public float speed;
    public float max = 100;
    public float min = 0;

	public Vector2 minMaxSave;
    void Start()
    {
		minMaxSave = new Vector2(min,max);
		if(player.transform.position.x > min){
        	transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
		} else {
			transform.position = new Vector3(min, transform.position.y, transform.position.z);
		}
    }

    void Update()
    {
        if(player == null){
            player = FindObjectOfType<Mario>().gameObject;
        }
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, player.transform.position.x, Time.deltaTime * speed), transform.position.y, transform.position.z);
        if (transform.position.x > max)
        {
			if(player.transform.position.x > transform.position.x){
            	transform.position = new Vector3(max, transform.position.y, transform.position.z);
			}
        }
        
		if (transform.position.x < min)
        {
			if(player.transform.position.x < transform.position.x){
           		 transform.position = new Vector3(min, transform.position.y, transform.position.z);
			}
        }
		min = transform.position.x;
    }
}
