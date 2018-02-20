using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class TestVibrateEffect : MonoBehaviour {

    public float s;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;
    bool playerIndexSet = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < 4; ++i)
        {
            PlayerIndex testPlayerIndex = (PlayerIndex)i;
            GamePadState testState = GamePad.GetState(testPlayerIndex);
            if (testState.IsConnected)
            {
                Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
                playerIndex = testPlayerIndex;
                playerIndexSet = true;
            }
        }
        GamePad.SetVibration(playerIndex, s, s);
        if (Input.GetButtonDown("DPadUpDown"))
        {

        }
	}
}
