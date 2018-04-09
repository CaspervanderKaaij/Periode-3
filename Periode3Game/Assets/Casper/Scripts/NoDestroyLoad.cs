using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoDestroyLoad : MonoBehaviour
{
    //behaviours are based of of the battle transition
    public AudioClip destroyMusic;
    private bool timeBomb = false;
    private AudioSource musicSource;
    public bool[] enemyDead;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        musicSource = transform.GetComponent<AudioSource>();
    }

    void Update()
    {
        /*
        if (timeBomb == false)
        {
            if (musicSource.clip == destroyMusic)
            {
                StartCoroutine(DestroyTime(8));
                musicSource.volume = 0.5f;
                timeBomb = true;
            }
        }
        */
        if (GameObject.FindGameObjectsWithTag("Music").Length > 1)
        {
           // Debug.Log("morethan1biatsh");
            if (musicSource.clip == destroyMusic)
            {
                for (int i = 0; i < GameObject.FindGameObjectsWithTag("Music").Length; i++)
                {
                  if(GameObject.FindGameObjectsWithTag("Music")[i] != gameObject){
                     // Debug.Log(GameObject.FindGameObjectsWithTag("Music")[i].transform.name);
                     for (int id = 0; id < enemyDead.Length; id++)
                     {
                          GameObject.FindGameObjectsWithTag("Music")[i].GetComponent<NoDestroyLoad>().enemyDead[id] = enemyDead[id];
                     }
                  }
                }
                Destroy(gameObject);
            }
        }
    }
    public IEnumerator DestroyTime(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        // Destroy(gameObject);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("overworldLastX", 0);
        PlayerPrefs.SetFloat("overworldLastY", 0);
        PlayerPrefs.SetFloat("overworldLastZ", 0);
    }
}
