using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCharacter : MonoBehaviour
{

    BattleManager manager;
    public int playerNumber = 0;
	[HideInInspector]
    public int atackNumber = 0;
    public GameObject cam;
	[HideInInspector]
    public float timer = 0;
    private bool hasAtacked = false;
    public Animator anim;
    public enum AttackType
    {
        Damage,
        Heal,
        EnemyDebuff,
        PlayerBuff
    }

    public AttackType[] atkType;
    public float[] damage;
	[HideInInspector]
    public int attackNumber = 0;
	public int toppleNumber = 1;
	public int highLevelNumber = 2;

    void Start()
    {
        manager = GameObject.FindObjectOfType<BattleManager>();
        timer = 0;
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    void Special()
    {
        if (atkType[attackNumber] == AttackType.Damage)
        {
			bool tplATK = false;
			if(toppleNumber == attackNumber){
				tplATK = true;
			}

            manager.DoDamage(manager.enemies[0], damage[attackNumber], Random.Range(0.9f, 1.05f), tplATK);
        }

        if (atkType[attackNumber] == AttackType.Heal)
        {
            for (int i = 0; i < 4; i++)
            {
				manager.playerHealth[i] += Mathf.RoundToInt(damage[attackNumber]);
            }
        }
    }
    void Update()
    {
        if (manager.curState == BattleManager.State.Attack)
        {//Attack
            if (manager.atackingPlayer == playerNumber)
            {
                //cam.SetActive (true);
                GameObject.FindObjectOfType<BattleCamera>().SetExtaPos(cam.transform);

                if (timer > 0.5f)
                {
                    if (hasAtacked == false)
                    {
                        hasAtacked = true;
                        //manager.DoDamage (manager.enemies[0],damage[attackNumber],Random.Range(0.9f,1.05f),true);
                        Special();
                        manager.Vibrate(0.1f, 1);
                        manager.enemies[0].GetComponent<BattleEnemyAI>().agroList.Add(playerNumber - 1);
                    }
                }

                if (timer > 1)
                {
                    timer = 0;
                    hasAtacked = false;
                    manager.BackToNormal(true);
                }
                timer += Time.deltaTime;
            }
            else
            {
                //cam.SetActive (false);
                timer = 0;
            }
        }
        else
        {
            //cam.SetActive (false);
            //GameObject.FindObjectOfType<BattleCamera>().SetExtaPos(cam.transform);
            timer = 0;
        }
    }
    public void Animate()
    {
        int r = Random.Range(1, 6);
        anim.Play("Attack");
        anim.SetBool("attacking", true);
        anim.SetFloat("attackNumber", r);
        StartCoroutine(StopAnim());
    }

    IEnumerator StopAnim()
    {
        yield return new WaitForSeconds(Time.deltaTime);
        anim.SetBool("attacking", false);
    }
}

