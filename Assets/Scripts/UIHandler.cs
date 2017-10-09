using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour {

	public void ToActive()
    {
        //destroy passive
        //activate active
        //pass in uiobject to active
    }

    public void ToPassive(string[] text)
    {
        //destroy active
        //activate passive
        //pass description into passive
    }

    public void PassInput(string input)
    {
        //pass inout to active menu
    }
    public bool IsTextScrolled()
    {
        //if passive is text scrolled
        return true;
        //else
        //false
    }

}
