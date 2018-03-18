using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBulletSpawner : MonoBehaviour
{

    private Transform child;
    public GameObject[] toSpawn;
    private BattleManager manager;
    public List<GameObject> toDestroy;
    private Transform bulHit;

    void Start()
    {
        child = transform.GetChild(0).transform;
        manager = GameObject.FindObjectOfType<BattleManager>();
        bulHit = GameObject.FindGameObjectWithTag("BulletHit").transform;
    }

    void Update()
    {
        if (child.childCount == 0)
        {
            ActualCode();
        }
        if (child.childCount != 0)
        {
            if (Vector3.Distance(bulHit.position, child.GetChild(child.childCount - 1).transform.position) < 160)
            {
                ActualCode();
            }
        }
        else
        {
            ActualCode();
        }
    }


    void ActualCode()
    {
        GameObject g = GameObject.Instantiate(toSpawn[Random.Range(0, toSpawn.Length)], transform.position, Quaternion.identity);
        g.transform.parent = transform;
        for (int i = 0; i < child.childCount; i++)
        {
            child.GetChild(i).tag = "Untagged";
        }
        child.DetachChildren();
        Destroy(child.gameObject);
        child = g.transform;

    }

    void DestroyDead()
    {
        toDestroy.Clear();
        toDestroy.AddRange(GameObject.FindGameObjectsWithTag("Bullet"));
        for (int i = 0; i < toDestroy.Count; i++)
        {
            if (toDestroy[i].transform.parent != child)
            {
                toDestroy.Remove(toDestroy[i]);
            }
        }
        for (int kaas = 0; kaas < 3; kaas++)
        {
            if (manager.playerHealth[kaas] <= 0)
            {
                for (int i = 0; i < toDestroy.Count; i++)
                {
                    if (toDestroy[i].layer == 11)
                    {
                        Destroy(toDestroy[i]);
                    }
                }
            }


            toDestroy.Clear();
        }
    }

}
