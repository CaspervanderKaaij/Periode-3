using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCamera : MonoBehaviour
{

    private Transform cam;
    public List<Transform> pathOrder;
    public int curPathPoint = 0;
    public float speed = 10;
    public bool loop = true;
    private BattleManager manager;
    public bool lookAtEnemy = true;
    public Vector3 hardStartPos;
    public Vector3 hardStartEuler;
    private Vector3 center;
    public float shakeStrength = 1;
    private bool shaking = false;
    private float speedSave;
    private bool attackState = false;
    public GameObject posHelp;


    void Start()
    {
        posHelp = GameObject.Find("RealCamPos");
        transform.position = hardStartPos;
        transform.eulerAngles = hardStartEuler;
        for (int i = 0; i < transform.childCount; i++)
        {
            pathOrder.Add(transform.GetChild(i));
        }
        cam = posHelp.transform;
        manager = GameObject.FindObjectOfType<BattleManager>();
        posHelp.transform.position = pathOrder[0].position;
        cam.eulerAngles = pathOrder[0].eulerAngles;
        speedSave = speed;
        center = posHelp.transform.position;
    }

    void Update()
    {
        if (attackState == false)
        {
            if (Vector3.Distance(posHelp.transform.position, pathOrder[curPathPoint].position) > 0.3f)
            {
                posHelp.transform.position = Vector3.MoveTowards(posHelp.transform.position, pathOrder[curPathPoint].position, Time.unscaledDeltaTime * speed);
                if (lookAtEnemy == false)
                {
                    cam.eulerAngles = Vector3.Lerp(cam.eulerAngles, pathOrder[curPathPoint - 1].eulerAngles, Time.unscaledDeltaTime * 2);
                }
                else
                {
                    cam.LookAt(manager.enemies[0].transform.position);
                }
            }
            else if (curPathPoint < pathOrder.Count - 1)
            {
                curPathPoint++;
            }
            else if (loop == true)
            {
                curPathPoint = 0;
            }
            else
            {
                manager.SpawnRandomCam();
                Destroy(gameObject);
            }
            if (shaking == true)
            {
                speed = 0;
                CamShake();
            }
            else
            {
                speed = speedSave;
                center = posHelp.transform.position;
            }
            Camera.main.transform.position = posHelp.transform.position;
            Camera.main.transform.eulerAngles = posHelp.transform.eulerAngles;
        }
    }

    void CamShake()
    {
        posHelp.transform.position = center + new Vector3(Random.Range(shakeStrength, -shakeStrength), Random.Range(shakeStrength, -shakeStrength), Random.Range(shakeStrength, -shakeStrength));
    }

    public void StartShake(float time, float strength)
    {
        if (attackState == false)
        {
			if(posHelp != null){
            	center = posHelp.transform.position;
			}
        }
        shaking = true;
        StopCoroutine(StopShake(time));
        shakeStrength = strength;
        StartCoroutine(StopShake(time));
    }

    IEnumerator StopShake(float time)
    {
        yield return new WaitForSeconds(time);
        shaking = false;
        posHelp.transform.position = center;
    }

    public void SetExtaPos(Transform newPos)
    {
        StopCoroutine(CamBack());
        attackState = true;
        Camera.main.transform.position = newPos.position;
        Camera.main.transform.eulerAngles = newPos.eulerAngles;
        StartCoroutine(CamBack());
    }

    IEnumerator CamBack()
    {
        yield return new WaitForEndOfFrame();
        attackState = false;
        Camera.main.transform.position = center;
    }
}
