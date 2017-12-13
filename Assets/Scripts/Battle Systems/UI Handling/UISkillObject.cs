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

    public void Highlight()
    {
        _name.fontStyle = FontStyle.Bold;
        _cost.fontStyle = FontStyle.Bold;
    }
    public void Normalize()
    {
        _name.fontStyle = FontStyle.Normal;
        _cost.fontStyle = FontStyle.Normal;
    }
}
