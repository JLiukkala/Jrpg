using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassiveUI : MonoBehaviour {

    [SerializeField, Tooltip("Description")]
    private Text _description;
    private string[] text;
    private int position = 0;

    public string[] Text
    {
        private get {
            return text;
        }

        set {
            text = value;
            Initiate();
        }
    }

    public void HandleInput(string input) {

    }
    private void Initiate() {

    }
    private void Scroll() {

    }


}
