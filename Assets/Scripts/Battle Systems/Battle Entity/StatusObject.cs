using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/********************************************
 * Status Object class
 * 
 * Holds info for a single status effect
 * 
 * Also has functions to determine the values and duration
 */
public class StatusObject : MonoBehaviour {

    [SerializeField, Tooltip("Which effect will be used")]
    private StatusOptions _effect;
    [SerializeField, Tooltip("Number of turns effect will be active")]
    private int _duration = 1;
    [SerializeField, Tooltip("Margin around duration")]
    private int _durationMargin = 1;
    [SerializeField, Tooltip("Value specific to effect")]
    private float _value = 1;
    [SerializeField, Tooltip("Determines if it is percent or actual value margin")]
    private bool _isPercent = false;
    [SerializeField, Tooltip("Amount above and below value that it could possibly be")]
    private float _valueMargin = 1;
    [SerializeField, Tooltip("Amount above and below value that it could possibly be")]
    private StatsObject _masterStats;
    [SerializeField, Tooltip("Amount above and below value that it could possibly be")]
    private bool _instantCalculate = false;
    
    private int attack;
    private int intelligence;
    private int ThisDuration;

    //Checks for no null objects
    private void Start()
    {
        if(_masterStats == null)
        {
            Debug.LogWarning("Status Object: Missing MasterStats");
        }
    }
    
    //gets the attack and int of the stats that way the effect can know its power right now buffs and debuffs included
    public void Setup()
    {
        Attack = _masterStats.CurrentAttack;        
        Intelligence = _masterStats.CurrentIntelligence;
    }

    //rolls the duration
    public void RollDuration() {
        ThisDuration = _duration + Random.Range(-_durationMargin, _durationMargin);
    }

    //Public getters private setters
    public StatusOptions Effect
    {
        get {
            return _effect;
        }
    }
    public int Duration
    {
        get {
            return ThisDuration;
        }
        set {
            ThisDuration = value;
        }
    }
    public bool InstantCalculate
    {
        get {
            return _instantCalculate;
        }
    }
    public int Attack
    {
        get {
            return attack;
        }

        private set {
            attack = value;
        }
    }
    public int Intelligence
    {
        get {
            return intelligence;
        }

        private set {
            intelligence = value;
        }
    }
    public float ActualValue
    {
        get {
            return _value;
        }
    }
    public float CalculatedValue
    {
        //Checks if the margin is set to percent or absolute, then adds it and returns
        get {
            if(_isPercent)
            {
                return _value + _value * Random.Range(-_valueMargin, _valueMargin);
            } else
            {
                return _value + Random.Range(-_valueMargin, _valueMargin);
            }

        }
    }
}
