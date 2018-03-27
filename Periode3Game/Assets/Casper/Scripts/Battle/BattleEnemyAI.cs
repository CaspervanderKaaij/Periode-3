using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleEnemyAI : MonoBehaviour
{

    //public string state = "normal";
    public float timer = 0;
    public float chargeFinishTime = 5;
    public float thinkTime = 1;
    public int target;
    private BattleManager manager;
    public string atackName = "Starlight Kick";
    public GameObject cam;
    private bool boolHelper = false;
    public int agro = 0;
    //[HideInInspector]
    public List<int> agroList;
    public AudioClip moanSFX;
    public AudioClip hitSFX;
    //public AudioClip chargeSFX;
    private bool agroActivate;
    private Animator anim;
    public GameObject thinkParticle;
    [HideInInspector]
    public float damageMultipier = 1;
    public float normalDamageMult = 1;
    public float ThinkDamageMult = 5f;
    public float ToppleDamageMult = 2;
    public float damage = 30;
    public float impactTime = 0.5f;
    public float attackLength = 2.5f;

    public enum State
    {
        Normal,
        Think,
        Attack,
        Topple
    }

    public State curState;

    void Start()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
        agroActivate = false;
        agroList.Clear();
        manager = GameObject.FindObjectOfType<BattleManager>();
        agro = Random.Range(0, manager.players.Count);
        cam.SetActive(false);
        curState = State.Normal;
    }

    public void CheckState()
    {
        switch (curState)
        {
            case State.Attack:
                Attack();
                anim.SetInteger("state", 1);
                break;
            case State.Normal:
                Normal();
                anim.SetInteger("state", 0);
                break;
            case State.Topple:
                Topple();
                anim.SetInteger("state", 2);
                break;
            case State.Think:
                Think();
                anim.SetInteger("state", 0);
                break;

        }
    }


    void Attack()
    {
        thinkParticle.SetActive(false);
        anim.speed = 1;
        if (manager.curState != BattleManager.State.EnemyAttack)
        {
            manager.PlaySound(moanSFX, 1, 0.75f, transform.position, 1);
            manager.PlaySound(manager.chargeSFX, 0.5f, 0, transform.position, 1);
        }
        agroList.Clear();
        manager.curState = BattleManager.State.EnemyAttack;//enemyAttack
                                                           // cam.SetActive(true);
        GameObject.FindObjectOfType<BattleCamera>().SetExtaPos(cam.transform);
        manager.atackNameUI.transform.GetChild(1).GetComponent<Text>().text = atackName;
        timer += Time.deltaTime;
        if (timer > impactTime)
        {
            if (GameObject.FindObjectOfType<BattleCamera>().shaking == false)
            {
                manager.Vibrate(0.1f, 1);
                GameObject.FindObjectOfType<BattleCamera>().StartShake(attackLength - impactTime, 0.5f);
                manager.DoDamage(manager.players[agro], damage, Random.Range(0.9f, 1.1f), false);
            }
        }
        if (timer > attackLength)
        {
            //  manager.Vibrate(0.1f, 1);
            //cam.SetActive (false);
            timer = 0;
            curState = State.Normal;
            manager.BackToNormal(true);
        }
    }

    void Normal()
    {
        damageMultipier = normalDamageMult;
        anim.speed = 1;
        thinkParticle.SetActive(false);
        if (manager.curState == BattleManager.State.Normal)
        {//normal
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
        }
        if (manager.atackNameUI.activeSelf == true)
        {
            timer = 0;
        }
        if (timer > chargeFinishTime)
        {
            timer = 0;
            curState = State.Think;
        }
        Agro();

    }

    void Think()
    {
        damageMultipier = ThinkDamageMult;
        anim.speed = 0.1f;
        GameObject.FindObjectOfType<BattleCamera>().StartShake(Time.deltaTime, 0.05f);
        if (thinkParticle.activeSelf == false)
        {
            thinkParticle.GetComponent<AudioSource>().pitch = 0f;
        }
        else
        {
            // thinkParticle.GetComponent<AudioSource>().pitch += 1.5f * Time.deltaTime;
        }
        thinkParticle.SetActive(true);
        // timer += Time.deltaTime;
        // thinkParticle.GetComponent<AudioSource>().pitch += 1.5f * Time.deltaTime;
        if (manager.curState != BattleManager.State.Cooldown)
        {
            //timer = 0;
            timer += Time.deltaTime;
            thinkParticle.GetComponent<AudioSource>().pitch += 5.5f / thinkTime * Time.deltaTime;
        }
        else if (manager.atackNameUI.activeSelf == true)
        {
            // timer = 0;
        }
        // Debug.Log(timer);
        if (timer > thinkTime)
        {
            bool can = true;
            if (Input.GetButtonDown("DPadUpDown"))
            {
                can = false;
            }
            if (Input.GetButtonDown("DPadLeftRight"))
            {
                can = false;
            }
            if (manager.curState != BattleManager.State.Normal)
            {
                can = false;
            }
            if (can == true)
            {
                timer = 0;
                curState = State.Attack;
            }
        }

    }

    void Topple()
    {
        damageMultipier = ToppleDamageMult;
        anim.speed = 1;
        if (thinkParticle.activeSelf == true)
        {
            manager.PlaySound(moanSFX, 1, 0.75f, transform.position, 1.7f);
            manager.PlaySound(hitSFX, 0.5f, 0.75f, transform.position, 1);
        }
        thinkParticle.SetActive(false);
        agroList.Clear();
        if (boolHelper == false)
        {
            StartCoroutine(ToppleTimer(15));
            boolHelper = true;
        }

    }

    void Agro()
    {
        if (agroActivate == false)
        {
            StartCoroutine(AgroTimer());
            agroActivate = true;
        }
    }
    private IEnumerator AgroTimer()
    {
        yield return new WaitForSeconds(5);
        agroActivate = false;
        if (agroList.Count != 0)
        {
            agro = agroList[Random.Range(0, agroList.Count)];
            agroList.Clear();
        }
    }

    void Update()
    {
        if (manager.playerHealth[agro] <= 0)
        {
            for (int i = 0; i < 4; i++)
            {
                if (manager.playerHealth[i] != 0)
                {
                    agro = i;
                    i = 3;
                }
            }
            StopCoroutine(AgroTimer());
            agroActivate = false;
            agroList.Clear();
        }
        CheckState();
        Vector3 agroPos = manager.players[agro].transform.position;
        transform.LookAt(new Vector3(agroPos.x, transform.position.y, agroPos.z));
        transform.eulerAngles += new Vector3(0, 180, 0);
    }
    private IEnumerator ToppleTimer(float time)
    {
        yield return new WaitForSeconds(time);
        if (manager.curState != BattleManager.State.Victory)
        {
            if (manager.curState != BattleManager.State.End)
            {
                manager.PlaySound(hitSFX, 0.5f, 0.75f, transform.position, 1);
                manager.PlaySound(moanSFX, 0.5f, 0.75f, transform.position, 1);
                curState = State.Normal;
                boolHelper = false;
                timer = 0;
            }
        }
    }
}
