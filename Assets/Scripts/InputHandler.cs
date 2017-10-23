using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour {

    [SerializeField, Tooltip("Battle Handler for the input to pass to")]
    private BattleHandler _battleHandler;

    static string[,] KEYS = new string[,] { {"Select", "x"}, { "Back", "z" }, { "Up", "up" }, { "Down", "down" }, { "Left", "left" }, { "Right", "right" } };
	
	// Update is called once per frame
	void Update () {
        //send key to battle handler
        for(int i = 0; i < KEYS.GetLength(0); i++)
        {
            if(Input.GetKeyDown(KEYS[i,1]))
            {
                _battleHandler.PassInput(KEYS[i,0]);
            }
        }
    }
}
