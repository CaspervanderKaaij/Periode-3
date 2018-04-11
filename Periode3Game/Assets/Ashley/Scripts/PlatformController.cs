using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour {
    public Vector3 route;
    public float moveSpeedUp;
    public float moveSpeedDown;
    public bool upQuestionmark;
    public int waitSec;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (upQuestionmark == false)
        {
            StartCoroutine("Up");
        }
        if(upQuestionmark == true)
        {
            StartCoroutine("Down");
        }
    }
    IEnumerator Up()
    {
        transform.Translate(route * Time.deltaTime * moveSpeedUp);
        yield return new WaitForSeconds(waitSec);
        upQuestionmark = true;
    }
    IEnumerator Down()
    {
        transform.Translate(route * Time.deltaTime * moveSpeedDown);
        yield return new WaitForSeconds(waitSec);
        upQuestionmark = false;
    }
}