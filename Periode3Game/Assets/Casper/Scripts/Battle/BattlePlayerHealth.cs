using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattlePlayerHealth : MonoBehaviour
{

    private Text txt;
    private BattleManager manager;
    public int maxHealth = 100;
    public int health = 100;
    Animator anim;

    void Start()
    {
        txt = transform.GetComponent<Text>();
        manager = GameObject.FindObjectOfType<BattleManager>();
        anim = manager.players[transform.GetSiblingIndex()].transform.GetChild(0).GetComponent<Animator>();
    }

    void Update()
    {
        if (manager.playerHealth[transform.GetSiblingIndex()] < 0)
        {
            manager.playerHealth[transform.GetSiblingIndex()] = 0;

        }
        else if (manager.playerHealth[transform.GetSiblingIndex()] > maxHealth)
        {
            manager.playerHealth[transform.GetSiblingIndex()] = maxHealth;
        }
        health = manager.playerHealth[transform.GetSiblingIndex()];
        if (health <= 0)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            if (anim.GetFloat("hit") != 1)
            {
                anim.Play("Idle", 0, 0);
                anim.SetFloat("hit", 1);
            }
            txt.text = "Dead";
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
            txt.text = "HP:" + health + " / " + maxHealth;
            if (anim.GetFloat("hit") == 1)
            {
                anim.SetFloat("hit", 0);
            }
        }
    }
}
