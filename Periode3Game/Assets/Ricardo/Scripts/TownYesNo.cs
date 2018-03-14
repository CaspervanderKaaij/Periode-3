using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownYesNo : MonoBehaviour
{

    public bool yes = true;
    public Vector3 posYes;
    public Vector3 posNo;
    private bool yesChange = true;
    public TownDialogue yesDialogue;
    public TownDialogue noDialogue;
    public TownDialogue toSet;
    void Start()
    {

    }

    void Update()
    {
        SetLoc();
        if (Input.GetButtonDown("A_Button"))
        {
            SelectYesNo();
        }
    }

    void SetLoc()
    {

        if (Input.GetAxis("Vertical") != 0)
        {
            if (yesChange == true)
            {
                yes = !yes;
                yesChange = false;
            }
        }
        else
        {
            yesChange = true;
        }

        if (yes == true)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, posYes, Time.deltaTime * 20);
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, posNo, Time.deltaTime * 20);
        }
    }

    void SelectYesNo()
    {
        if (yes == false)
        {
            SetTownDialogue(yesDialogue);
        }
        else
        {
            SetTownDialogue(noDialogue);
        }
    }

    void SetTownDialogue(TownDialogue twndia)
    {
        transform.parent.gameObject.SetActive(false);
        toSet.text.AddRange(twndia.text);
        toSet.talker.AddRange(twndia.talker);
        toSet.yesNo = twndia.yesNo;
    }
}
