using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRotator : MonoBehaviour
{
    public Vector3 rotate;
    public float x;
    public float z;
    public Vector3 view;
    public GameObject cam;
    public Vector3 camlook;
    public float sensetivety;
    public GameObject camPos;
    public Vector3 camPosStart;
    // Use this for initialization
    void Start()
    {
        camPosStart = camPos.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        camPos.transform.localPosition = camPosStart;
        view.y = Input.GetAxis("Mouse X");
        camlook.x = -Input.GetAxis("Mouse Y");
        transform.Rotate(view * sensetivety);
        cam.transform.Rotate(camlook * sensetivety);
        if (cam.transform.eulerAngles.x > 60)
        {
            if (cam.transform.eulerAngles.x < 180)
            {
                cam.transform.eulerAngles = new Vector3(59.9999f, cam.transform.eulerAngles.y, cam.transform.eulerAngles.z);
            }
        }
        if (cam.transform.eulerAngles.x < 300)
        {
            if (cam.transform.eulerAngles.x > 180)
            {
                cam.transform.eulerAngles = new Vector3(300.0001f, cam.transform.eulerAngles.y, cam.transform.eulerAngles.z);
            }
        }

        cam.transform.eulerAngles = new Vector3(cam.transform.eulerAngles.x, cam.transform.eulerAngles.y, 0);
        RaycastHit hit;
        if (Physics.Raycast(new Ray(transform.position, -(transform.position - camPos.transform.position)), out hit, Vector3.Distance(transform.transform.position, camPos.transform.position), ~(1 >> 10)))
        {
            if (hit.transform.gameObject.layer != 10)
            {
                if (hit.transform.gameObject.layer != 2)
                {
                    if (hit.transform.tag != "Player")
                    {
                        transform.position = hit.point;
                        transform.position += transform.forward;
                    }
                }
            }
        }
        if (Physics.Raycast(new Ray(camPos.transform.position, -(camPos.transform.position - transform.position)), out hit, Vector3.Distance(camPos.transform.transform.position, transform.position), ~(1 >> 10)))
        {
            if (hit.transform.gameObject.layer != 10)
            {
                if (hit.transform.gameObject.layer != 2)
                {
                    if (hit.transform.tag != "Player")
                    {
                        transform.position = hit.point;
                        transform.position += transform.forward;
                    }
                }
            }
        }
    }
}
    