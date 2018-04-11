using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Mario : Move
{
    public float accSpeed = 2;
    private Animator anim;
    private SpriteRenderer sprite;
    private float turnSpeed = 0;
    [HideInInspector]
    public int hit = 0;
    public bool starman = false;
    public bool big = false;
    public AudioClip jumpSFX;
    public GameObject blankAudioObject;
    public AudioClip music;
    public GameObject smallMario;
    private float invincibleTime = 0;

    public enum State
    {
        Normal,
        Dead,
        Warp,
        Finish
    }
    public State curState = State.Normal;
    void Start()
    {
        cc = transform.GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        sprite = anim.GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (starman == true)
        {
            Starman();
        }
        else
        {
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetComponentInChildren<SpriteRenderer>().color = Color.white;
        }
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
    void Update()
    {
        invincibleTime = Mathf.MoveTowards(invincibleTime, 0, Time.deltaTime);
        if (curState == State.Normal)
        {
            Activate();
        }
        else if (curState == State.Finish)
        {
            SceneManager.LoadScene("MainMenuScene");
            if (cc.isGrounded == false)
            {
                LeftRight(Vector3.zero);
                Animate();
                moveVel = new Vector3(moveVel.x, gravitySTR * 10, 0);
            }
            else
            {
                LeftRight(Vector3.right / 2);
                Animate();
                moveVel = new Vector3(moveVel.x, gravitySTR * 20, 0);
            }
            cc.Move(moveVel * Time.deltaTime);
        }
    }

    void Starman()
    {

        transform.GetChild(1).gameObject.SetActive(true);
        SpriteRenderer rend = transform.GetComponentInChildren<SpriteRenderer>();
        if (rend.color == Color.yellow)
        {
            rend.color = Color.green;
        }
        else if (rend.color == Color.green)
        {
            rend.color = Color.magenta;
        }
        else
        {
            rend.color = Color.yellow;
        }
    }

    public IEnumerator StarmanStop()
    {
        yield return new WaitForSeconds(10);
        Camera.main.GetComponent<AudioSource>().Stop();
        Camera.main.GetComponent<AudioSource>().clip = music;
        Camera.main.GetComponent<AudioSource>().time = 0;
        Camera.main.GetComponent<AudioSource>().Play();
        starman = false;
    }

    public override void Activate()
    {
        if (gravity != 0)
        {
            hit = 0;
        }
        Gravity();
        horVertInput = new Vector2(Mathf.MoveTowards(horVertInput.x, Input.GetAxis("Horizontal") * (Input.GetAxis("Sprint") / 2 + 1), Time.deltaTime * (accSpeed + turnSpeed)), Input.GetAxis("Vertical"));
        if (Input.GetButtonDown("SMBJump"))
        {
           // Debug.Log("!");
            if (cc.isGrounded)
            {
                Jump();
                PlaySound(jumpSFX, 1, 0, transform.position, 1);
            }
        }
        LeftRight(new Vector3(horVertInput.x, moveVel.y, horVertInput.y));
        Animate();
        cc.Move(moveVel * Time.deltaTime);
        if (cc.isGrounded == false)
        {
            turnSpeed = 0;
            if (cc.velocity.y == 0)
            {
                //gravity = 0;
               // hit = 1;
            }
            if (cc.velocity.x == 0)
            {
                horVertInput.x /= 10;
                moveVel.x /= 10;
            }
        }
        else
        {
            if (Vector2.Distance(new Vector2(0, horVertInput.x), new Vector2(0, Input.GetAxis("Horizontal"))) > 1.25f)
            {
                turnSpeed = 9f;
            }
            else if (Mathf.Abs(Input.GetAxis("Horizontal")) != 0)
            {
                turnSpeed = 0;
            }
            else
            {
                turnSpeed = 3;
            }
            if (cc.velocity.x == 0)
            {
                horVertInput.x = 0;
                moveVel.x = 0;
            }
        }
    }

    public void Dead()
    {
        if (big == false)
        {
            if (invincibleTime == 0)
            {
                SceneManager.LoadScene("MainMenuScene");
            }
        }
        else
        {
            Destroy(gameObject);
            GameObject newMario = Instantiate(smallMario, transform.position, transform.rotation);
            newMario.GetComponent<Mario>().invincibleTime = 3;
        }
    }

    void Animate()
    {
        if (cc.isGrounded)
        {
            anim.SetBool("Grounded", true);
            if (moveVel.x == 0)
            {
                anim.SetBool("Walk", false);
                //anim.Play("Idle", 0, 0);
                anim.Play(0, 0, 0);

                if (horVertInput.y < 0)
                {
                    anim.Play(0, 0, 0.4f);
                }
            }
            else
            {
                anim.SetBool("Walk", true);
                if (moveVel.x < 0)
                {
                    sprite.flipX = true;
                }
                else
                {
                    sprite.flipX = false;
                }
            }
        }
        else
        {
            anim.SetBool("Grounded", false);
            //anim.Play("Idle", 0, 0.6f);
            anim.Play(0, 0, 0.9f);
        }
    }
}
