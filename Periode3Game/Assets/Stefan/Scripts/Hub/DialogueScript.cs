using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueScript : MonoBehaviour {

    public ParticleSystem p;
    public GameObject ship;

    public string[] dia;
    public float textSpeed;
    public Text text;
    public int lineNumber = 0;

    public Camera cam;
    public bool screenshake;
    public Vector3 center;
    public float shakeStrength;

	// Use this for initialization
	void Start () {
        StartCoroutine(DiaTimer(1));
        center = cam.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
		if(screenshake == true)
        {
            cam.transform.position = center + new Vector3(Random.Range(shakeStrength, -shakeStrength), Random.Range(shakeStrength, -shakeStrength), Random.Range(shakeStrength, -shakeStrength));
            shakeStrength += 0.001f;
        }
	}

    public IEnumerator DiaTimer(float t)
    {
        yield return new WaitForSeconds(t);
        text.text = dia[lineNumber];
        lineNumber++;
        StartCoroutine(DiaTimer(textSpeed));
        if(lineNumber >= (dia.Length / 3))
        {
            screenshake = true;
        }
        if(lineNumber >= (dia.Length - (dia.Length / 4)))
        {
            p.Play(true);
        }
        if (lineNumber >= (dia.Length - (dia.Length / 8)))
        {
            ship.SetActive(false);
        }
        if (lineNumber >= dia.Length)
        {
            SceneManager.LoadScene("SpaceHubScene");
        }
    }
}
