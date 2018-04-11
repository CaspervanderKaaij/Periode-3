using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxMario : MonoBehaviour
{

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject != transform.parent.gameObject)
        {
            if (other.GetComponent<LinearMove>() != null)
            {
                other.GetComponent<LinearMove>().Death();
            }
        }
    }
}
