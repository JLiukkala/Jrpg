using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIObject : MonoBehaviour {
    string[,,] UI;
    private bool isPassive;
    private int currentSelection = 0;
    private int currentScreen = 0;

	// Use this for initialization
	void Start () {
		//create heirarchy
	}

    public string[,] Select
    {
        get {
            return new string[,] { {""} ,{""} };
        }
    }

    public void ToPassive() {

    }

    public void ToInteractive() {

    }
	
	
}
