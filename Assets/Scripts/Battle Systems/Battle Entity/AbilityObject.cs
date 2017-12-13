using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/********************************************
 * AbilityObject class
 * 
 * Holds the relevant information on each ability
 * 
 * Has getters for all the information
 */
public class AbilityObject : MonoBehaviour {

    [SerializeField, Tooltip("Description of ability")]
    private string _description;
    [SerializeField, Tooltip("Array of effects ability applies")]
    private StatusObject[] _effects;
    [SerializeField, Tooltip("Sp cost of ability")]
    private int _cost = 0;
    [SerializeField, Tooltip("Speed of ability")]
    private int _speed = 0;
    [SerializeField, Tooltip("Accuracy of ability"), Range(0, 100)]
    private int _accuracy = 100;
    [SerializeField, Tooltip("Wether or not to attack all enemies")]
    private bool _areaOfEffect = false;
    [SerializeField, Tooltip("Randomly decide target")]
    private bool _isRandom = false;
    [SerializeField, Tooltip("For random targets how many")]
    private int _targets = 1;
    [SerializeField, Tooltip("For aoe wich side to target")]
    private bool _targetEnemy = true;
    [SerializeField]
    private GameObject animation1;



    public string Name
    {
        get {
            return gameObject.name;
        }
    }
    public string Description
    {
        get {
            return _description;
        }
    }

    public StatusObject[] Effects
    {
        get {
            return _effects;
        }
    }

    public int Cost
    {
        get {
            return _cost;
        }
    }

    public int Speed
    {
        get {
            return _speed;
        }
    }

    public int Accuracy
    {
        get {
            return _accuracy;
        }
    }

    public bool AreaOfEffect
    {
        get {
            return _areaOfEffect;
        }
    }

    public bool IsRandom
    {
        get {
            return _isRandom;
        }
    }

    public int Targets
    {
        get {
            return _targets;
        }
    }

    public bool TargetEnemy
    {
        get {
            return _targetEnemy;
        }
    }

    public GameObject GetAnimation
    {
        get
        {
            return animation1;
        }

        set
        {
            animation1 = value;
        }
    }
}
