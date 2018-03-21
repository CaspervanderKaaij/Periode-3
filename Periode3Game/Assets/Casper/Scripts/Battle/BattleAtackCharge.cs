using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleAtackCharge : MonoBehaviour
{

    public int player = 0;
    public float percentage = 0;
    private float startX;
    private BattleManager manager;
    public float minPercantage = 0;
    public Image dPad;
    private Color startColor;
    private SpriteRenderer img;
    public AudioClip chargeSFX;
    public GameObject fireSprite;

    void Start()
    {
        startX = transform.parent.localScale.x;
        transform.parent.localScale = new Vector3(0, transform.parent.localScale.y, transform.parent.localScale.z);
        manager = GameObject.FindObjectOfType<BattleManager>();
        img = transform.GetComponent<SpriteRenderer>();
        startColor = img.color;
        if (fireSprite != null)
        {
            fireSprite.SetActive(false);
        }
    }

    void Update()
    {
        if (percentage >= minPercantage)
        {
            float realPercentage = percentage - minPercantage;
            transform.parent.localScale = Vector3.MoveTowards(transform.parent.localScale, new Vector3((realPercentage / 100) * startX, transform.parent.localScale.y, transform.parent.localScale.z), Time.deltaTime * 350);
            if (transform.parent.localScale != new Vector3((realPercentage / 100) * startX, transform.parent.localScale.y, transform.parent.localScale.z))
            {
                //img.color = Color.white;
                img.color = (startColor + Color.white) / 2;
            }
            else
            {
                img.color = Color.Lerp(img.color, startColor, Time.unscaledDeltaTime * 4);
            }
        }
        else
        {
            transform.parent.localScale = new Vector3(0, transform.parent.localScale.y, transform.parent.localScale.z);
        }
        percentage = manager.charge[player];
        if (percentage > 100)
        {
            if (dPad != null)
            {
                if (dPad.color != Color.white)
                {
                    manager.PlaySound(chargeSFX, 0.3f, 0, transform.position, 1.5f);
                    img.color = Color.red;
                    if (fireSprite != null)
                    {
                        fireSprite.SetActive(true);
                    }
                }
                dPad.color = Color.white;
                img.color = Color.Lerp(img.color, startColor, Time.unscaledDeltaTime / 10);
            }
        }
        else if (dPad != null)
        {
            dPad.color = new Color(1, 1, 1, 0.397f);
            if (fireSprite != null)
            {
                fireSprite.SetActive(false);
            }
        }
    }
}
