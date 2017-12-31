using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/********************************************
 * UI Skill Object class
 * 
 * this is just a module to put on an object to bind the text elements for name cost and description to ease access
 */
public class UISkillObject : MonoBehaviour {

    public Text _name;
    public Text _cost;
    public Text _description;
    public int _selectionSize = 4;
    public int normalSize = 18;
    public void Highlight()
    {
        _name.fontSize = normalSize + _selectionSize;
        _cost.fontSize = normalSize + _selectionSize;
    }
    public void Normalize()
    {
        _name.fontSize = normalSize;
        _cost.fontSize = normalSize;
    }
}
