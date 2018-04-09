using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldCamera : MonoBehaviour
{

    public GameObject pivot;
    public Transform player;
    public float speed = 100;
    public float distance = 10;
    public Vector3 pivotAdd = Vector3.zero;
    //CharacterController cc;

    void Start()
    {
        pivot.transform.position = player.position + transform.TransformDirection(pivotAdd);

    }
    void Update()
    {
        pivot.transform.position = Vector3.LerpUnclamped(pivot.transform.position, player.position + transform.TransformDirection(pivotAdd), Time.deltaTime * 5);
        SetPivot();
        CamFix();
    }

    void SetPivot()
    {
        pivot.transform.eulerAngles += new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X")) * speed * Time.deltaTime;
        transform.position = pivot.transform.position + pivot.transform.TransformDirection(0, 0, -distance);
        transform.LookAt(pivot.transform.position);
    }

    void CamFix()
    {
        RaycastHit hit;
        //Debug.DrawRay(pivot.transform.position,transform.position - pivot.transform.position);
        if (Physics.Raycast(new Ray(pivot.transform.position, transform.position - pivot.transform.position), out hit, Vector3.Distance(transform.position, pivot.transform.position)))
        {
            transform.position = hit.point;
            transform.position += transform.forward;
        }
    }

}
