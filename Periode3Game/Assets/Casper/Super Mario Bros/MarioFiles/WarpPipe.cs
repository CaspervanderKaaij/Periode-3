using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpPipe : MonoBehaviour
{

    public Transform newPos = null;
    public bool staticNewCam = false;
    public Transform newCamPos = null;
    public bool down = true;
    private bool press = false;
    public AudioClip sfx;
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (down == true)
            {
                if (Input.GetAxis("Vertical") < 0)
                {
                    press = true;
                }
                else
                {
                    press = false;
                }
            }
            else if (Input.GetAxis("Horizontal") > 0)
            {
                press = true;
            }
            else
            {
                press = false;
            }
            if (press == true)
            {
                FindObjectOfType<Mario>().PlaySound(sfx,1,0,transform.position,1);
                other.transform.position = newPos.position;
                Camera.main.transform.position = newCamPos.position;
                Cam cam = Camera.main.GetComponent<Cam>();
                cam.min = newCamPos.position.x;
                if (staticNewCam == true)
                {
                    cam.max = newCamPos.position.x;
                }
                else
                {
                    cam.max = cam.minMaxSave.y;
                }
            }
        }
    }
}
