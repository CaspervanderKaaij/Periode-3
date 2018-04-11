using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{

    public int coins = 0;
    public Text coinText;
    public float timeLeft = 500;
    public Text timeText;
    public int score = 0;
    public Text scoreText;

    void Update()
    {
        coinText.text = "X " + coins;
        timeLeft -= Time.deltaTime * 3;
		if(timeLeft <= 0){
			FindObjectOfType<Mario>().Dead();
		}
        timeText.text = timeLeft.ToString("000");

        if (score < 1000000)
        {
            scoreText.text = score.ToString("000000");
        }
        else
        {
            scoreText.text = "Infinity";
        }
    }
}
