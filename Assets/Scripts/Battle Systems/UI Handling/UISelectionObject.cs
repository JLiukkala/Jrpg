using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISelectionObject : MonoBehaviour {


    public Text _name;
    public Text _health;
    public int _selectionSize = 2;
    public int normalSize = 16;
    public void Highlight()
    {
        _name.fontSize = normalSize + _selectionSize;
        _health.fontSize = normalSize + _selectionSize;
    }
    public void Normalize()
    {
        _name.fontSize = normalSize;
        _health.fontSize = normalSize;
    }
}
