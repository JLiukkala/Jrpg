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
    private int _value = 1;
    [SerializeField, Tooltip("Amount above and below value that it could possibly be")]
    private int _valueMargin = 1;

    private int ThisDuration;

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

    public int Value
    {
        get {
            return _value + Random.Range(-_valueMargin, _valueMargin + 1);
        }
    }

}
