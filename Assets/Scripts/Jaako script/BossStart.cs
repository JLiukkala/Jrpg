using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStart : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BossCollider"))
        {
            // Call function in StateHandler that sets the enemy (boss) and starts the battle.
        }
    }
}
