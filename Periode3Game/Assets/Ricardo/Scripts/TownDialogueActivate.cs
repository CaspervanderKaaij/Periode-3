using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownDialogueActivate : MonoBehaviour
{

    private TownManager manager;
    [HideInInspector]
    public bool on = true;
    public string[] txt;
    public string[] talker;
    public bool yesNo = false;
    public TownDialogue yesDialogue;
    public TownDialogue noDialogue;
    private TownYesNo selecter;
    void Start()
    {
        manager = GameObject.FindObjectOfType<TownManager>();
        selecter = manager.yesNo.transform.GetChild(1).GetComponent<TownYesNo>();
    }


    void OnTriggerStay(Collider other)
    {
        if (on == true)
        {
            if (other.tag == "Player")
            {
                if (Input.GetButtonDown("Confirm"))
                {
                    manager.dialogue.SetActive(true);
                    TownDialogue twndia = manager.dialogue.transform.GetChild(3).GetComponent<TownDialogue>();
                    twndia.text.Clear();
                    twndia.text.AddRange(txt);
                    twndia.talker.Clear();
                    twndia.talker.AddRange(talker);
                    twndia.yesNo = yesNo;
                    selecter.yesDialogue = yesDialogue;
                    selecter.noDialogue = noDialogue;
                    twndia.first = true;
                    on = false;
                }
            }
        }
    }

    void OnTriggerExit()
    {
        on = true;
    }
}
