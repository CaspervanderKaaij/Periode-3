using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoDestroyLoad : MonoBehaviour
{
    public AudioClip destroyMusic;
    private bool timeBomb = false;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (timeBomb == false)
        {
            if (transform.GetComponent<AudioSource>().clip == destroyMusic)
            {
                StartCoroutine(DestroyTime(8));
                timeBomb = true;
            }
        }
    }
    public IEnumerator DestroyTime(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        Destroy(gameObject);
    }
}
