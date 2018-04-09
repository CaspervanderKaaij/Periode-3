using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class Interactable : MonoBehaviour {

    public GameObject t;
    public TextMesh txt;
    public bool a;
    public Color colorStart = Color.gray;
    public Color colorEnd = Color.blue;
    public float duration = 1.0F;
    public Renderer rend;
    public GameObject conv;

    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();
        rend.material.color = colorStart;
	}
	
	// Update is called once per frame
	void Update () {
        
		if(Vector3.Distance(gameObject.transform.position,t.transform.position) < 5)
        {
            txt.text = "Press [E]";
            a = true;
            gameObject.GetComponent<Renderer>().material.SetColor("green",Color.green);
        }
        else
        {
            if (Vector3.Distance(gameObject.transform.position, t.transform.position) > 5)
            {
                txt.text = "";
                a = false;
            }
        }

        if(a == true)
        {
            float lerp = Mathf.PingPong(Time.time, duration) / duration;
            rend.material.color = Color.Lerp(colorStart, colorEnd, lerp);
        } else
        {
            if(a == false)
            {
                rend.material.color = colorStart;
            }
        }

        if (Input.GetButtonDown("Interact"))
        {
            if(a == true)
            {
                TestFunction();
            }
        }
	}

    public void TestFunction()
    {
        conv.SetActive(true);
    }
}
