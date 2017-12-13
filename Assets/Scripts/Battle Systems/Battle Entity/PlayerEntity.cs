using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/********************************************
 * Player Entity class
 * 
 * Has nothing right now maybe useless
 */
public class PlayerEntity : BattleEntity {

    private const float MOVESELECTDIST = .5f;
    public void Deselect() {
        transform.position = transform.position + Vector3.up * -MOVESELECTDIST;
    }

    public void Select()
    {
        transform.position = transform.position + Vector3.up * MOVESELECTDIST;
    }
}
