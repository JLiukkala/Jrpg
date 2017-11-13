using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : BattleEntity {

    public string _entryText;
    public string Think() {
        if(Random.Range(0, 2) == 0)
        {
            return "Attack";
        } else
        {
            return Ability(Random.Range(0, AbilitiesLength)).Name;
        }
        
    }
    //update to take an array of players so it can check their status and make better decision
    public int Target(int enemiecount)
    {
        return Random.Range(0, enemiecount);
    }

    
}
