using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsObject : MonoBehaviour {

    //Pretty straightforward stat holder
    [SerializeField, Tooltip("Name of ability")]
    private int _level = 0;

    [SerializeField, Tooltip("Health and its per level growth")]
    private int _health = 100, _healthMultiplier = 1;
    private int _healthModifier;
    [SerializeField, Tooltip("Spell Power and its per level growth")]
    private int _special = 100, _specialMultiplier = 1;
    private int _specialModifier;
    [SerializeField, Tooltip("Attack and its per level growth")]
    private int _attack = 20, _attackMultiplier = 1;
    private int _attackModifier;
    [SerializeField, Tooltip("Defense and its per level growth")]
    private int _defense = 20, _defenseMultiplier = 1;
    private int _defenseModifier;
    [SerializeField, Tooltip("Speed and its per level growth")]
    private int _speed = 20, _speedMultiplier = 1;
    private int _speedModifier;

    List<StatusObject> effects;



    public void Process() {
        for(int i = 0; i < effects.Count;)
        {
            switch(effects[i].Effect)
            {
                case "Damage":
                    Health = effects[i].Value;
                    break;
                case "Heal":
                    Health = -effects[i].Value;
                    break;
                default:
                    break;
            }
            effects[i].Length -= 1;
            if(effects[i].Length == 0)
            {
                effects.RemoveAt(i);
            } else
            {
                i++;
            }
            
        }
    }



    public int Level
    {
        get {
            return _level;
        }
    }

    public bool IsAlive
    {
        get {
            if(Health>0)
            {
                return true;
            } else
            {
                return false;
            }
        }
    }

    public StatusObject Effects
    {
        set {
            value.RollDuration();
            effects.Add(value);
        }
    }

    public int Health
    {
        get {
            return _health * _healthMultiplier - _healthModifier;
        }

        private set {
            Mathf.Clamp(_healthModifier += value, 0, MaxHealth);
        }
    }
    public int MaxHealth {
        get {
            return _health * _healthMultiplier;
        }
    }

    public int Special
    {
        get {
            return _special * _specialMultiplier - _specialModifier;
        }

        private set {
            Mathf.Clamp(_specialModifier += value, 0, MaxSpecial);
        }
    }
    public int MaxSpecial
    {
        get {
            return _special * _specialMultiplier;
        }
    }
}
