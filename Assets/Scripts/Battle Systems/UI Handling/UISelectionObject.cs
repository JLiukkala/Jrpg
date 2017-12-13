using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISelectionObject : MonoBehaviour {


    public Text _name;
    public Text _health;

    public void Highlight()
    {
        _name.fontStyle = FontStyle.Bold;
        _health.fontStyle = FontStyle.Bold;
    }
    public void Normalize()
    {
        _name.fontStyle = FontStyle.Normal;
        _health.fontStyle = FontStyle.Normal;
    }
}
