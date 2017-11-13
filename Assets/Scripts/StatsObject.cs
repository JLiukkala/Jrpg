using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum StatusOptions
{
    Attack, Damage, Heal, ModifyAttack, ModifyDefense,
    ModifySpeed, ModifyIntelligence, ModifyMagicResist,
}

public class StatsObject : MonoBehaviour {

    //Pretty straightforward stat holder
    [SerializeField, Tooltip("Name of ability")]
    private int _level = 0;

    [SerializeField, Tooltip("Health and its per level growth")]
    private int _health = 100, _healthGrowth = 1;
    private int _currentHealth;
    [SerializeField, Tooltip("Spell Points and its per level growth")]
    private int _special = 100, _specialGrowth = 1;
    private int _currentSpecial;
    [SerializeField, Tooltip("Attack and its per level growth")]
    private int _attack = 20, _attackGrowth = 1;
    private int _currentAttack;
    [SerializeField, Tooltip("Defense and its per level growth")]
    private int _defense = 20, _defenseGrowth = 1;
    private int _currentDefense;
    [SerializeField, Tooltip("Speed and its per level growth")]
    private int _speed = 20, _speedGrowth = 1;
    private int _currentSpeed;
    [SerializeField, Tooltip("magic effectiveness and its per level growth")]
    private int _intelligence = 20, _intelligenceGrowth = 1;
    private int _currentIntelligence;
    [SerializeField, Tooltip("magic resist and its per level growth")]
    private int _magicResist = 20, _magicResistGrowth = 1;
    private int _currentMagicResist;

    [SerializeField, Tooltip("Health text")]
    private Text _healthText;
    [SerializeField, Tooltip("mana text")]
    private Text _manaText;

    List<StatusObject> effects = new List<StatusObject>();

    private void Start()
    {
        _healthText.text = Health.ToString();
        _manaText.text = Special.ToString();
        float x = ((transform.parent.transform.position.x + Camera.main.orthographicSize * Camera.main.aspect-.5f) * _healthText.transform.parent.gameObject.GetComponent<RectTransform>().rect.height) / (Camera.main.orthographicSize* 2);
        float y = ((transform.parent.transform.position.y + Camera.main.orthographicSize + 1f) * _healthText.transform.parent.gameObject.GetComponent<RectTransform>().rect.height) / (Camera.main.orthographicSize * 2);
        _healthText.transform.position = new Vector3(x,y,0);
        x = ((transform.parent.transform.position.x + Camera.main.orthographicSize * Camera.main.aspect + .5f) * _manaText.transform.parent.gameObject.GetComponent<RectTransform>().rect.height) / (Camera.main.orthographicSize * 2);
        y = ((transform.parent.transform.position.y + Camera.main.orthographicSize + 1f) * _manaText.transform.parent.gameObject.GetComponent<RectTransform>().rect.height) / (Camera.main.orthographicSize * 2);
        _manaText.transform.position = new Vector3(x, y, 0);
    }

    public void Process() {
        ResetCurrents();
        for(int i = 0; i < effects.Count;)
        {
            Process(effects[i]);
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
    public void Process(StatusObject effect)
    {
        switch(effect.Effect)
        {
            case StatusOptions.Attack:
                Health = (int)(effect.Attack * effect.Value);
                break;
            case StatusOptions.Damage:
                Health = (int)(effect.Value);
                break;
            case StatusOptions.Heal:
                Health = -(int)(effect.Value);
                break;
            case StatusOptions.ModifyAttack:
                _currentAttack += (int)(effect.Value*_attack);
                break;
            case StatusOptions.ModifyDefense:
                _currentDefense += (int)(effect.Value * _defense);
                break;
            case StatusOptions.ModifySpeed:
                _currentSpeed += (int)(effect.Value * _speed);
                break;
            case StatusOptions.ModifyIntelligence:
                _currentIntelligence += (int)(effect.Value * _intelligence);
                break;
            case StatusOptions.ModifyMagicResist:
                _currentMagicResist += (int)(effect.Value * _magicResist);
                break;
            default:
                break;
        }
        _healthText.text = Health.ToString();
        _manaText.text = Special.ToString();

    }

    private void ResetCurrents()
    {
        _currentAttack = 0;
        _currentDefense = 0;
        _currentSpeed = 0;
        _currentIntelligence = 0;
        _currentMagicResist = 0;

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
    public bool IsActive
    {
        get {
            if(Health > 0)
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
            if(value.InstantCalculate)
            {
                Process(value);
            } else
            {
                value.RollDuration();
                effects.Add(value);
            }
        }
    }

    public int Health
    {
        get {
            return _health * _healthGrowth - _currentHealth;
        }

        private set {
            _currentHealth = Mathf.Clamp(_currentHealth + value, 0, MaxHealth);
            if(Health == 0)
            {

            }
        }
    }
    public int MaxHealth {
        get {
            return _health + Level * _healthGrowth;
        }
    }

    public int Special
    {
        get {
            return _special * _specialGrowth - _currentSpecial;
        }

        set {
            _currentSpecial = Mathf.Clamp(_currentSpecial + value, 0, MaxSpecial);
        }
    }
    public int MaxSpecial
    {
        get {
            return _special + Level * _specialGrowth;
        }
    }

    public int Speed
    {
        get {
            return _speed + (int)(_speedGrowth * _level);
        }
    }

    public int CurrentAttack
    {
        get {
            return _attack + _currentAttack;
        }
    }

    public int CurrentIntelligence
    {
        get {
            return _intelligence + _currentIntelligence;
        }
    }

}
