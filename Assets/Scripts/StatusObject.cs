using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusObject : MonoBehaviour {

    static string[] EffectsList = new string[] {"Damage", "Heal" };

    [SerializeField, Tooltip("Which effect will be used")]
    private string _effect;
    [SerializeField, Tooltip("Number of turns effect will be active")]
    private int _duration = 1;
    [SerializeField, Tooltip("Margin around duration")]
    private int _durationMargin = 1;
    [SerializeField, Tooltip("Value specific to effect")]
    private float _value = 1;
    [SerializeField, Tooltip("Amount above and below value that it could possibly be")]
    private float _valueMargin = 1;
    [SerializeField, Tooltip("Amount above and below value that it could possibly be")]
    private StatsObject _masterStats;
    [SerializeField, Tooltip("Amount above and below value that it could possibly be")]
    private bool _instantCalculate = false;
    

    private int attack;
    private int intelligence;

    private int ThisDuration;

    public void Setup()
    {
        Attack = _masterStats.CurrentAttack;
        Debug.Log("Attack set to: "+ _masterStats.CurrentAttack);
        Intelligence = _masterStats.CurrentIntelligence;
    }

    public void RollDuration() {
        ThisDuration = _duration + Random.Range(-_durationMargin, _durationMargin);
    }
    public string Effect
    {
        get {
            return _effect;
        }
    }

    public int Length
    {
        get {
            return ThisDuration;
        }
        set {
            ThisDuration = value;
        }
    }

    public float Value
    {
        get {
            return _value + Random.Range(-_valueMargin, _valueMargin);
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
}
