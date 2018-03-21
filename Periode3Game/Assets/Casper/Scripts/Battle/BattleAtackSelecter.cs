using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleAtackSelecter : MonoBehaviour
{

    public GameObject[] positions;
    public int curPos = 0;
    private bool canPress = false;
    BattleManager manager;
    public string[] atackName;
    private Vector3 startScale;
    public int playerNumber = 0;

    void Start()
    {
        canPress = false;
        manager = GameObject.FindObjectOfType<BattleManager>();
        canPress = false;
        transform.position = positions[curPos].transform.position;
        startScale = transform.localScale;
    }

    void Update()
    {

        transform.localScale = Vector3.MoveTowards(transform.localScale, startScale, Time.unscaledDeltaTime * 3);

        if (manager.atackNameUI.activeSelf == false)
        {
            transform.position = Vector3.Lerp(transform.position, positions[curPos].transform.position, Time.unscaledDeltaTime * 30);
            if (manager.fadeIn.color.a <= 0.5f)
            {
                if (Input.GetAxisRaw("DPadLeftRight") != 0)
                {
                    if (canPress == true)
                    {
                        if (curPos + Mathf.RoundToInt(Input.GetAxisRaw("DPadLeftRight")) > -1)
                        {
                            if (curPos + Mathf.RoundToInt(Input.GetAxisRaw("DPadLeftRight")) < positions.Length)
                            {
                                curPos += Mathf.RoundToInt(Input.GetAxisRaw("DPadLeftRight"));
                                transform.localScale = new Vector3(startScale.x + 0.1f, startScale.y + 0.4f, 1);

                            }
                        }
                    }
                    canPress = false;
                }
                else if (Input.GetAxisRaw("DPadUpDown") == 0)
                {
                    canPress = true;
                }
            }
            else
            {
                canPress = false;
            }
        }
        if (manager.fadeIn.color.a <= 0.5f)
        {
            if (Input.GetButtonDown("Confirm"))
            {
                manager.Vibrate(0.2f, 1);
				GameObject.FindObjectOfType<BattleCamera>().StartShake(Time.deltaTime, 0.05f);
                bool canSelect = false;
                if (curPos < manager.players[manager.atackingPlayer - 1].GetComponent<BattleCharacter>().highLevelNumber)
                {
                    canSelect = true;
                }
                else if (manager.charge[manager.atackingPlayer - 1] >= 200)
                {
                    canSelect = true;
                }
                if (canSelect == true)
                {
                    canPress = false;
                    manager.curState = BattleManager.State.Attack;
                    manager.fadeIn.color = manager.fadeColor;
                    GameObject atackNameUI = manager.atackNameUI;
                    atackNameUI.SetActive(true);
                    atackNameUI.transform.GetChild(1).GetComponent<Text>().text = atackName[curPos];
                    manager.players[playerNumber].GetComponent<BattleCharacter>().attackNumber = curPos;
                    manager.players[manager.atackingPlayer - 1].GetComponent<BattleCharacter>().atackNumber = curPos;
                    manager.turnAtack = false;
                    manager.timeScale = 1;
                    manager.charge[manager.atackingPlayer - 1] = 0;
                }
            }
        }
    }
}
