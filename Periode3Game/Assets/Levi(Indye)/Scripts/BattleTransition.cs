using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleTransition : MonoBehaviour
{
    public int EnemyID;
    private bool hit = false;
    Camera mainCam;
    Rect camRect;
    public AudioSource music;
    [HideInInspector]
    public AudioClip battleMusic;
    // Use this for initialization
    void Start()
    {
        mainCam = Camera.main;
        camRect = mainCam.rect;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerPrefs.SetFloat("overworldLastX", transform.position.x); //deze
        PlayerPrefs.SetFloat("overworldLastY", transform.position.y); // deze
        PlayerPrefs.SetFloat("overworldLastZ", transform.position.z); // hey levi, deze 3 lijnen zijn misschien handig :D
        if (hit == true)
        {
            if (mainCam.fieldOfView < 170)
            {
                mainCam.fieldOfView += Time.unscaledDeltaTime * 200;
            }
            // camRect.width -= Time.fixedUnscaledDeltaTime * 2;
            camRect.height -= Time.fixedUnscaledDeltaTime * 2;
            mainCam.rect = camRect;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (hit == false)
            {
                EnemyID = collision.gameObject.GetComponent<ID>().battleId;
                battleMusic = collision.gameObject.GetComponent<ID>().battleMusic;
                GameObject.FindObjectOfType<NoDestroyLoad>().enemyDead[collision.gameObject.GetComponent<ID>().overworldId] = true;
                PlayerPrefs.SetInt("enemy", EnemyID);
                // Application.LoadLevel("Scene");
                StartCoroutine(Load());
                transform.GetComponent<AudioSource>().Play();
                music.clip = battleMusic;
                music.Stop();
                music.Play();
                hit = true;
            }
        }
    }

    //battle zoom in effect door een zekere jezus!

    IEnumerator Load()
    {
        yield return new WaitForSecondsRealtime(0.7f);
        Application.LoadLevel("Scene");
    }
}
