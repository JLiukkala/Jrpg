using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/********************************************
 * Input Handler class
 * 
 * Waits for input and passes the keyword to the relevant class
 */
public class InputHandler : MonoBehaviour {

    //object for the relevant handler for each scene
    [SerializeField, Tooltip("State Handler for the input to pass to")]
    private StateHandler _stateHandler;
    //add the main handler for dungeon scene here


    //Key for the keyword to actual key
    static string[,] KEYS = new string[,] { {"Select", "x"}, { "Back", "z" }, { "Up", "up" }, { "Down", "down" }, { "Left", "left" }, { "Right", "right" }, {"Esc", "escape" } };
	

	void Update () {
        //cycle through each key and look for a press
        for(int i = 0; i < KEYS.GetLength(0); i++)
        {
            if(Input.GetKeyDown(KEYS[i,1]))
            {
                if(_stateHandler != null)
                {
                    _stateHandler.PassInput(KEYS[i, 0]);
                }
            }
        }
    }
}
