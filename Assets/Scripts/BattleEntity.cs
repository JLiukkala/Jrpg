using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEntity : MonoBehaviour {

    [SerializeField, Tooltip("Name of this BattleEntity")]
    private string _name;
    [SerializeField, Tooltip("Stats attached to this BattleEntity")]
    private StatsObject _stats;
    [SerializeField, Tooltip("Array of all abilities for this BattleEntity")]
    private AbilityObject[] _abilities;
    [SerializeField, Tooltip("Array of all abilities for this BattleEntity")]
    private AbilityObject _attack;
    [SerializeField, Tooltip("Array of all abilities for this BattleEntity")]
    private AbilityObject _bolster;

    public string Name
    {
        get {
            return _name;
        }
    }

    public StatsObject Stats
    {
        get {
            return _stats;
        }
    }

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
        switch(abil)
        {
            case "Attack":
                return _attack;
            case "Bolster":
                return _bolster;
        }
        for(int i = 0; i < _abilities.Length; i++)
        {
            if(_abilities[i].Name == abil)
            {
                return _abilities[i];
            }
        }
        return null;
    }

    public void Action()
    {
        _stats.Process();
    }
}
