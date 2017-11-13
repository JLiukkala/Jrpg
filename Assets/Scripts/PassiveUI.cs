using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassiveUI : MonoBehaviour {

    [SerializeField, Tooltip("Description")]
    private Text _description;
    private string[] text;
    private int position = 0;
    private bool isAtEnd;

    // Makes a getter and a setter for the string array variable text.
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

<<<<<<< HEAD
    // Makes a getter and a setter for the boolean variable isAtEnd.
    public bool IsAtEnd
    {
        private get {
            return isAtEnd;
        }

        set {
            isAtEnd = value;
        }
    }

    // If an input key is pressed, the Scroll method is called.
=======
>>>>>>> 93e0a6c7e73a362fb24949103112ddec8b757565
    public void HandleInput(string input) {
        if (input == ("Select"))
        {
            Scroll();
        } 
    }

<<<<<<< HEAD
    // Initiates the description text and sets it in the first position.
=======
    }
>>>>>>> 93e0a6c7e73a362fb24949103112ddec8b757565
    private void Initiate() {
        _description.text = text[position];
    }

<<<<<<< HEAD
    // Scrolls to the next position.
=======
    }
>>>>>>> 93e0a6c7e73a362fb24949103112ddec8b757565
    private void Scroll() {
        if (text.Length == position + 1)
        {

        } else
        {
            position = position + 1;
            _description.text = text[position];
        }
    }


<<<<<<< HEAD
=======
    }


>>>>>>> 93e0a6c7e73a362fb24949103112ddec8b757565
}
