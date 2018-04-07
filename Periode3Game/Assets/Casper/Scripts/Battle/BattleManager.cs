using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using XInputDotNetPure;

public class BattleManager : MonoBehaviour
{

    [Header("ObjectLists")]
    public List<GameObject> players;
    public List<GameObject> enemies;

    [Header("Other object")]
    public GameObject blankAudioObject;
    public GameObject damageUI;
    public GameObject[] camPathPrefabs;
    private int lastCamPath = -1;
    public BattleHealth[] enemyHealthUI;
    private GameObject camSlowMotionEffect;
    public GameObject[] selectAtackUI;
    public GameObject atackNameUI;
    public GameObject[] buttonObjects;
    public GameObject victory;

    [Header("stats")]
    public float[] charge;
    public int[] playerHealth;
    public List<float> enemyHealth;

    [Header("Handy's & helpers")]
    public bool vibration = true;
    public GameObject slashEffect;
    public Image startFade;
    public float timeScale = 1;
    private float rhythmTimer = 0;
    public bool rhythmTime = false;
    private Coroutine vib;
    public bool turnAtack = false;
    public AudioClip chargeSFX;
    public AudioClip pressSFX;
    public GameObject[] victoryOut;
    public AudioClip victoryMusic;
    //[HideInInspector]
    //public string state = "normal";
    public enum State
    {
        Normal,
        PlayerAttack,
        EnemyAttack,
        Victory,
        End,
        Cooldown,
        Attack
    }

    public State curState;
    public int atackingPlayer = 2;
    [HideInInspector]
    public float coolDownTimer = 0;
    public Image fadeIn;
    public Color fadeColor;
    [HideInInspector]
    public List<float> abxyCooldown;


    void Start()
    {
        curState = State.Normal;
        abxyCooldown.Add(0);
        abxyCooldown.Add(0);
        abxyCooldown.Add(0);
        abxyCooldown.Add(0);
        startFade.color = Color.black;
        fadeIn.color = Color.clear;
        victory.SetActive(false);
        atackingPlayer = 2;
        atackNameUI.SetActive(false);
        for (int i = 0; i < selectAtackUI.Count(); i++)
        {
            selectAtackUI[i].SetActive(false);
        }
        camSlowMotionEffect = Camera.main.transform.GetChild(0).gameObject;
        camSlowMotionEffect.SetActive(false);
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Player").Length; i++)
        {
            players.Add(GameObject.FindGameObjectsWithTag("Player")[i]);
        }
        players = players.OrderBy(go => go.name).ToList();
        for (int i = 0; i < players.Count; i++)
        {
            players[i].GetComponent<BattleCharacter>().playerNumber = i + 1;
        }

        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Enemy").Length; i++)
        {
            enemies.Add(GameObject.FindGameObjectsWithTag("Enemy")[i]);
            enemyHealth.Add(enemyHealthUI[i].health);
        }
        enemies = enemies.OrderBy(go => go.name).ToList();

        //DoDamage (enemies[0].gameObject,100,Random.Range(0.85f,1.15f));
    }

    public void ToggleVibration()
    {
        vibration = !vibration;
    }

    public void DoDamage(GameObject sandBag, float damage, float random, bool topple)
    {
        GameObject uidmg = GameObject.Instantiate(damageUI, sandBag.transform.position, Quaternion.identity);
        uidmg.GetComponent<TextMesh>().text = "" + Mathf.RoundToInt(damage * random * enemies[0].GetComponent<BattleEnemyAI>().damageMultipier);
        if (sandBag.tag == "Enemy")
        {
            enemyHealth[0] -= Mathf.RoundToInt(damage * random * enemies[0].GetComponent<BattleEnemyAI>().damageMultipier);
            Instantiate(slashEffect, Camera.main.transform);
            if (topple == true)
            {
                BattleEnemyAI ai = sandBag.GetComponent<BattleEnemyAI>();
                if (ai.curState == BattleEnemyAI.State.Think)
                {//think
                    ai.curState = BattleEnemyAI.State.Topple;
                }
            }
        }
        else if (sandBag.tag == "Player")
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i] == sandBag)
                {
                    playerHealth[i] -= Mathf.RoundToInt(damage * random);
                    players[i].transform.GetChild(0).GetComponent<Animator>().Play("Idle", 0, 0);
                    players[i].transform.GetChild(0).GetComponent<Animator>().SetFloat("hit", 0.5f);
                }
            }
        }
    }

    public void SpawnRandomCam()
    {
        //Debug.Log("spawn");
        int i = Random.Range(0, camPathPrefabs.Count() - 1);
        if (i == lastCamPath)
        {
            i += 1;
        }
        if (i > camPathPrefabs.Count() - 1)
        {
            i = 0;
        }
        GameObject.Instantiate(camPathPrefabs[i], Vector3.zero, Quaternion.identity);
        lastCamPath = i;
    }

    public void Vibrate(float time, float strength)
    {
        if (vibration == true)
        {
            GamePad.SetVibration(0, strength, strength);
            if (vib != null)
            {
                StopCoroutine(vib);
            }
            vib = StartCoroutine(VibrateStop(time));
        }
    }

    IEnumerator VibrateStop(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        GamePad.SetVibration(0, 0, 0);
    }

    public void PlaySound(AudioClip clip, float volume, float spatialBlend, Vector3 pos, float pitch)
    {
        GameObject p;
        p = Instantiate(blankAudioObject);
        p.transform.position = pos;
        AudioSource pAudio = p.GetComponent<AudioSource>();
        pAudio.pitch = pitch;
        pAudio.clip = clip;
        pAudio.volume = volume;
        pAudio.Play();
        pAudio.spatialBlend = spatialBlend;
        Destroy(p, clip.length);
    }

    public void CheckState()
    {
        switch (curState)
        {
            case State.Attack:
                //Attack();
                break;
            case State.Normal:
                //Normal ();
                break;
            case State.Cooldown:
                //Topple ();
                break;
            case State.Victory:
                //Think ();
                break;

        }
    }

    void Normal()
    {
        if (Input.GetButtonDown("A_Button"))
        {
            if (playerHealth[0] != 0)
            {
                buttonObjects[0].transform.localScale = new Vector3(1.25f, 1.25f, 1.3f);
                PlaySound(pressSFX, 0.35f, 0, transform.position, 0.7f);
            }
        }
        if (Input.GetButtonDown("B_Button"))
        {
            if (playerHealth[1] != 0)
            {
                buttonObjects[1].transform.localScale = new Vector3(1.25f, 1.25f, 1.3f);
                PlaySound(pressSFX, 0.35f, 0, transform.position, 0.9f);
            }
        }
        if (Input.GetButtonDown("X_Button"))
        {
            if (playerHealth[2] != 0)
            {
                buttonObjects[2].transform.localScale = new Vector3(1.25f, 1.25f, 1.3f);
                PlaySound(pressSFX, 0.35f, 0, transform.position, 1.1f);
            }
        }
        if (Input.GetButtonDown("Y_Button"))
        {
            if (playerHealth[3] != 0)
            {
                buttonObjects[3].transform.localScale = new Vector3(1.25f, 1.25f, 1.3f);
                PlaySound(pressSFX, 0.35f, 0, transform.position, 1.3f);
            }
        }
    }

    void Attack()
    {

        if (atackNameUI.activeSelf == false)
        {
            fadeIn.color = fadeColor;
        }
        atackNameUI.SetActive(true);

    }
    void Update()
    {
        if (playerHealth[0] + playerHealth[1] + playerHealth[2] + playerHealth[3] == 0)
        {
            SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
        }
        if (curState != State.EnemyAttack)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].transform.GetChild(0).GetComponent<Animator>().GetFloat("hit") == 0.5f)
                {
                    if (players[i].transform.GetChild(0).GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
                    {
                        players[i].transform.GetChild(0).GetComponent<Animator>().SetFloat("hit", 0);
                    }
                }
            }
        }

        startFade.color = Color.LerpUnclamped(startFade.color, Color.clear, Time.deltaTime * Mathf.Abs(5f - startFade.color.a));
        //Debug.Log(startFade.color.a);
        if (startFade.color.a < 0.2f)
        {
            //startFade.color = Color.clear;
        }
        if (curState == State.Normal)
        {
            Normal();
        }
        rhythmTimer += Time.unscaledDeltaTime;
        if (rhythmTimer > 0.5f)
        {
            rhythmTimer -= 0.5f;
            //Vibrate (0.1f,0.3f);
            rhythmTime = true;
        }
        else
        {
            rhythmTime = false;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            fadeIn.color = fadeColor;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            charge[0] = 200;
            charge[1] = 200;
            charge[2] = 200;
            charge[3] = 200;
        }

        for (int i = 0; i < charge.Length; i++)
        {
            if (charge[i] > 200)
            {
                charge[i] = 200;
            }
            else if (charge[i] < 0)
            {
                charge[i] = 0;
            }
        }

        for (int i = 0; i < buttonObjects.Length; i++)
        {
            buttonObjects[i].transform.localScale = Vector3.MoveTowards(buttonObjects[i].transform.localScale, new Vector3(1, 1, buttonObjects[i].transform.localScale.z), Time.deltaTime * 10);
            buttonObjects[i].transform.localScale = Vector3.MoveTowards(buttonObjects[i].transform.localScale, new Vector3(buttonObjects[i].transform.localScale.x, buttonObjects[i].transform.localScale.y, 1), Time.deltaTime);
            abxyCooldown[i] -= Time.unscaledDeltaTime;
            if (abxyCooldown[i] < 0)
            {
                abxyCooldown[i] = 0;
            }
            if (buttonObjects[i].transform.localScale.z != 1)
            {
                abxyCooldown[i] = buttonObjects[i].transform.localScale.z - 1;
            }
        }

        if (fadeIn.color != Color.clear)
        {
            FadeIn();
        }
        bool canPress;
        canPress = true;
        if (atackNameUI.activeSelf == true)
        {
            canPress = false;
        }
        else if (coolDownTimer != 0)
        {
            canPress = false;
        }
        if (canPress == true)
        {
            if (turnAtack == false)
            {
                if (Vector2.SqrMagnitude(new Vector2(Mathf.Abs(Input.GetAxis("DPadLeftRight")), Mathf.Abs(Input.GetAxis("DPadUpDown")))) != 0)
                {
                    if (Input.GetAxis("DPadLeftRight") > 0)
                    {
                        atackingPlayer = 2;
                    }
                    else
                    {
                        atackingPlayer = 3;
                    }
                    if (Input.GetAxis("DPadUpDown") != 0)
                    {
                        if (Input.GetAxis("DPadUpDown") > 0)
                        {
                            atackingPlayer = 1;
                        }
                        else
                        {
                            atackingPlayer = 4;
                        }
                    }
                }
                if (curState == State.PlayerAttack)
                {
                    curState = State.Normal;
                }
            }
            else
            {
                curState = State.PlayerAttack;
            }

            if (turnAtack == true)
            {
                TurnAtack();
                camSlowMotionEffect.SetActive(true);
                selectAtackUI[atackingPlayer - 1].SetActive(true);
            }
            else
            {
                camSlowMotionEffect.SetActive(false);
                selectAtackUI[atackingPlayer - 1].SetActive(false);
                //}
                if (Input.GetAxis("DPadUpDown") != 0)
                {
                    bool canAtack = false;
                    if (charge[atackingPlayer - 1] >= 100)
                    {
                        canAtack = true;
                        Vibrate(0.1f, 1);
                    }
                    if (canAtack == true)
                    {
                        turnAtack = true;
                        Vibrate(0.1f, 1);
                    }
                }
                if (Input.GetAxis("DPadLeftRight") != 0)
                {
                    bool canAtack = false;
                    if (charge[atackingPlayer - 1] >= 100)
                    {
                        canAtack = true;
                        Vibrate(0.1f, 1);
                    }
                    if (canAtack == true)
                    {
                        turnAtack = true;
                        Vibrate(0.1f, 1);
                    }
                }
            }
        }
        if (curState == State.EnemyAttack)
        {//enemyAttack
            Attack();
        }

        if (curState == State.Victory)
        {//Victory
            Victory();
        }

        if (curState == State.Cooldown)
        {
            Cooldown();
        }

        Time.timeScale = timeScale;
    }

    void Cooldown()
    {
        coolDownTimer = Mathf.MoveTowards(coolDownTimer, 0, Time.deltaTime);
        if (coolDownTimer == 0)
        {
            curState = State.Normal;//normal
        }
    }
    void Victory()
    {
        if (victory.activeSelf == false)
        {
            if (victory != null)
            {
                victory.SetActive(true);
            }
            for (int i = 0; i < victoryOut.Length; i++)
            {
                //victoryOut[i].SetColor(Color.clear);
                // if(victoryOut[i].GetComponent<Text>() != null){
                if (victoryOut[i] != null)
                {
                    victoryOut[i].SetActive(false);
                }
                //}
            }
            Time.timeScale = 0;
            AudioSource musicHelp = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
            //StartCoroutine(GameObject.FindGameObjectWithTag("Music").GetComponent<NoDestroyLoad>().DestroyTime(6));
            musicHelp.clip = victoryMusic;
            musicHelp.Stop();
            musicHelp.Play();
            StartCoroutine(Load());
            //curState = State.End;//fuck
        }
    }

    IEnumerator Load()
    {
        yield return new WaitForSecondsRealtime(2);
        SceneManager.LoadScene("Overworld");
    }

    public void BackToNormal(bool coolDown)
    {
        fadeIn.color = fadeColor;
        if (coolDown == false)
        {
            curState = State.Normal;
        }
        else
        {
            curState = State.Cooldown;
            coolDownTimer = 0.5f;
        }
        //charge [atackingPlayer - 1] = 0;
        turnAtack = false;
        atackNameUI.SetActive(false);
        selectAtackUI[atackingPlayer - 1].SetActive(false);
    }

    void TurnAtack()
    {
        if (Time.timeScale != 0)
        {
            PlaySound(chargeSFX, 0.5f, 0, transform.position, 1);
            fadeIn.color = fadeColor;
        }
        timeScale = 0;
    }

    void FadeIn()
    {
        fadeIn.color = Color.Lerp(fadeIn.color, Color.clear, Time.unscaledDeltaTime * 3);
        if (fadeIn.color.a < 0.5f)
        {
            //fadeIn.color = Color.clear;
        }
    }
}
