using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/********************************************
 * Battle Entity class
 * 
 * Holds the Entity structure together linking to stats and abilities
 * 
 * Also has various getters and functions that call into the structure
 */
public class BattleEntity : MonoBehaviour {
    [SerializeField, Tooltip("Entity Name")]
    private string _name;
    [SerializeField, Tooltip("Stats attached to this BattleEntity")]
    private StatsObject _stats;
    [SerializeField, Tooltip("Array of all abilities for this BattleEntity")]
    private AbilityObject[] _abilities;
    private BattleHandler battle;
    private void Start()
    {
        battle = (BattleHandler)GameObject.FindWithTag("Battle Handler").GetComponent(typeof(BattleHandler));
    }
    //name getter obviously
    public string Name
    {
        get {
            return _name;
        }
    }

    //Getter for stats
    public StatsObject Stats
    {
        get {
            return _stats;
        }
    }

    //How many abilities, and Getter for an ability by index or searching
    public int AbilitiesLength
    {
        get {
            return _abilities.Length;
        }
    }
    public AbilityObject Ability(int index)
    {
        return _abilities[index];
    }
    public AbilityObject FindAbility(string abil) {
        for(int i = 0; i < _abilities.Length; i++)
        {
            if(_abilities[i].Name == abil)
            {
                return _abilities[i];
            }
        }
        return null;
    }


    //Tells the stats to process Actions
    public void Action()
    {
        _stats.Process();
    }

    public void Splash(string text)
    {
        battle.Splash(text, transform.position+new Vector3(1,1,0));
    }
    public void Splash(string text, Color c)
    {
        battle.Splash(text, transform.position, c);
    }
}
