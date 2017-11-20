using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour {

    public StatusOptions BuffType;
    public Severity BuffSeverity;
    public int Timer = 0;
    private bool active = false;
    public bool Active
    {
        get {
            return active;
        }

        private set {
            active = value;
        }
    }



    // Use this for initialization
    public void Activate() {
        gameObject.SetActive(true);
        Active = true;
    }
    public void Deactivate() {
        gameObject.SetActive(false);
        Active = false;
    }
}
