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
}
