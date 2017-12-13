using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionAnimation : MonoBehaviour {
    [SerializeField]
    private Animator _anim;

    public void SelfDestruct() {
        Debug.Log("Animating");
        Destroy(gameObject);
    }
}
