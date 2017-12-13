using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/********************************************
 * Buff class
 * 
 * This goes on every buff prefab and holds the associated severity and buff type
 */
public class Buff : MonoBehaviour {

    public StatusOptions BuffType;
    public Severity BuffSeverity;

    // These just allow us to set it visible and invisibles
    public void Activate() {
        gameObject.SetActive(true);
    }
    public void Deactivate() {
        gameObject.SetActive(false);
    }
}
