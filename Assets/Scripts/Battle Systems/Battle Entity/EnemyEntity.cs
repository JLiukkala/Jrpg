using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/********************************************
 * Enemy Entity class
 * 
 * A child of battle entity.
 * 
 * Has functions to generate an action and target for its turn.
 * 
 * Also holds data for entry text on the start of battle.
 */
public class EnemyEntity : BattleEntity {
    public AudioSource battleMusic;
    [SerializeField, Tooltip("Name of ability")]
    private string _entryText;
    public EnemyEntity _nextEnemy;
    public bool _isFinalBoss = false;
    public string EntryText
    {
        get {
            return _entryText;
        }
    }

    //Randomly returns one of the enemies abilitiesd
    public string Think() {
        return Ability(Random.Range(0, AbilitiesLength)).Name;
    }
    //Randomly returns a target between 0 and enemy count
    public int Target(int enemyCount)
    {
        return Random.Range(0, enemyCount);
    }

}