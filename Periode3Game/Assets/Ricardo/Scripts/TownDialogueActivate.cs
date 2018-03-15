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
                    manager.dialogue.transform.GetChild(3).GetComponent<TownDialogue>().text.Clear();
                    manager.dialogue.transform.GetChild(3).GetComponent<TownDialogue>().text.AddRange(txt);
                    manager.dialogue.transform.GetChild(3).GetComponent<TownDialogue>().talker.Clear();
                    manager.dialogue.transform.GetChild(3).GetComponent<TownDialogue>().talker.AddRange(talker);
                    manager.dialogue.transform.GetChild(3).GetComponent<TownDialogue>().yesNo = yesNo;
                    selecter.yesDialogue = yesDialogue;
                    selecter.noDialogue = noDialogue;
                    manager.dialogue.transform.GetChild(3).GetComponent<TownDialogue>().first = true;
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
