using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum StatusOptions
{
    Attack, MagicAttack,//damage based of character plus a percent or value margin
    Damage,//spicific damage rang
    Heal,
    //buffs and debuffs
    ModifyAttack, ModifyDefense,
    ModifySpeed, ModifyIntelligence, ModifyMagicResist,
    //status effects
    Sleep
}

public class StatsObject : MonoBehaviour {

    //Pretty straightforward stat holder
    [SerializeField, Tooltip("Name of ability")]
    private int _level = 0;
    public bool showMana = true;
    [SerializeField, Tooltip("Health and its per level growth")]
    private int _health = 100, _healthGrowth = 1;
    private int _currentDamage;
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
    [SerializeField, Tooltip("GameObject to hold all the Status effects")]
    private StatusHolder _statusHolder;
    [SerializeField, Tooltip("GameObject to hold all the buff images")]
    private BuffHolder _buffHolder;
    public bool log = false;

    [SerializeField, Tooltip("Health text")]
    private Text _healthText;
    [SerializeField, Tooltip("mana text")]
    private Text _manaText;

    List<StatusObject> effects = new List<StatusObject>();

    private void Start()
    {
        _healthText.text = Health.ToString();
        _manaText.text = Mana.ToString();
        float x = ((transform.parent.transform.position.x + Camera.main.orthographicSize * Camera.main.aspect-.5f) * _healthText.transform.parent.gameObject.GetComponent<RectTransform>().rect.height) / (Camera.main.orthographicSize* 2);
        float y = ((transform.parent.transform.position.y + Camera.main.orthographicSize + 1.5f) * _healthText.transform.parent.gameObject.GetComponent<RectTransform>().rect.height) / (Camera.main.orthographicSize * 2);
        _healthText.transform.position = new Vector3(x,y,0);
        x = ((transform.parent.transform.position.x + Camera.main.orthographicSize * Camera.main.aspect + .5f) * _manaText.transform.parent.gameObject.GetComponent<RectTransform>().rect.height) / (Camera.main.orthographicSize * 2);
        y = ((transform.parent.transform.position.y + Camera.main.orthographicSize + 1.5f) * _manaText.transform.parent.gameObject.GetComponent<RectTransform>().rect.height) / (Camera.main.orthographicSize * 2);
        _manaText.transform.position = new Vector3(x, y, 0);
        if(!showMana)
        {
            _manaText.gameObject.SetActive(false);
        }
    }

    public void Process() {        
        ResetCurrents();
        for(int i = 0; i < effects.Count;)
        {
            
            if(effects[i].Length == 0)
            {
                effects.RemoveAt(i);
            } else
            {
                Process(effects[i]);
                effects[i].Length -= 1;
                i++;
            }
        }
        _buffHolder.Clear();
        _manaText.text = Mana.ToString();
    }
    public void Process(StatusObject effect)
    {
        
        switch(effect.Effect)
        {
            case StatusOptions.Attack:
                TakeDamage((int)(effect.Attack * effect.Value));
                break;
            case StatusOptions.MagicAttack:
                TakeMagicDamage((int)(effect.Intelligence * effect.Value)) ;
                break;
            
            case StatusOptions.Damage:
                Health = (int)(effect.Value);
                break;

            case StatusOptions.Heal:
                Health = -(int)(effect.Intelligence * effect.Value);
                break;
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //******************************************************Status Effects***********************************************//
            case StatusOptions.ModifyAttack:
                _currentAttack += (int)(effect.Value*_attack- _attack);
                _buffHolder.Add(effect.Effect, DetermineSeverity(effect) );
                break;
            case StatusOptions.ModifyDefense:
                _currentDefense += (int)(effect.Value * _defense-_defense);
                _buffHolder.Add(effect.Effect, DetermineSeverity(effect));
                break;
            case StatusOptions.ModifySpeed:
                _currentSpeed += (int)(effect.Value * _speed-_speed);
                _buffHolder.Add(effect.Effect, DetermineSeverity(effect));
                break;
            case StatusOptions.ModifyIntelligence:
                _currentIntelligence += (int)(effect.Value * _intelligence - _intelligence);
                _buffHolder.Add(effect.Effect, DetermineSeverity(effect));
                break;
            case StatusOptions.ModifyMagicResist:
                _currentMagicResist += (int)(effect.Value * _magicResist - _magicResist);
                _buffHolder.Add(effect.Effect, DetermineSeverity(effect));
                break;
            default:
                break;
        }
        _healthText.text = Health.ToString();
        

    }
    public Severity DetermineSeverity(StatusObject effect) {
        if(effect.ActualValue > 1.4 &&effect.Value > 0)
        {
            return Severity.DoubleUp;
        } else if(effect.ActualValue <= 1.4 && effect.Value > 0)
        {
            return Severity.Up;
        } else if(effect.ActualValue > 1.4 && effect.Value < 0)
        {
            return Severity.DoubleDown;
        } else
        {
            return Severity.Down;
        }
    }
    private void ResetCurrents()
    {
        _currentAttack = 0;
        _currentDefense = 0;
        _currentSpeed = 0;
        _currentIntelligence = 0;
        _currentMagicResist = 0;
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
            }
            value.RollDuration();
            effects.Add(value);
        }
    }


    public int Level
    {
        get {
            return _level;
        }
    }
    public int Health
    {
        get {
            return _health + Level * _healthGrowth - _currentDamage;
        }

        private set {
            _currentDamage = Mathf.Clamp(_currentDamage + value, 0, MaxHealth);
            if(Health == 0)
            {

            }
        }
    }
    public int Mana
    {
        get {
            return MaxMana - _currentSpecial;
        }

        set {
            _currentSpecial = Mathf.Clamp(_currentSpecial + value, 0, MaxMana);
        }
    }


    public void TakeDamage(int damage) {
        int wut = damage * damage / (damage + CurrentDefense);
        _currentDamage += wut;
        _currentDamage = Mathf.Clamp(_currentDamage, 0, MaxHealth);
    }
    public void TakeMagicDamage(int Damage)
    {
        int wut = Damage * (Damage / (Damage + CurrentMagicDefense));


        Debug.Log(wut);
        Debug.Log("Damage: " + Damage + "  Defense:" + CurrentMagicDefense);
        _currentDamage += wut;
        _currentDamage = Mathf.Clamp(_currentDamage, 0, MaxHealth);
    }



    public int MaxHealth {
        get {
            return _health + Level * _healthGrowth;
        }
    }
    public int MaxMana
    {
        get {
            return _special + Level * _specialGrowth;
        }
    }
    

    public int CurrentAttack
    {
        get {
            return _attack + Level * _defenseGrowth + _currentDefense;
        }
    }
    public int CurrentIntelligence
    {
        get {
            return _intelligence + Level * _defenseGrowth + _currentDefense;
        }
    }
    public int CurrentDefense
    {
        get {
            return _defense + Level * _defenseGrowth + _currentDefense;
        }
    }
    public int CurrentMagicDefense
    {
        get {
            return _magicResist + Level * _magicResistGrowth + _currentMagicResist;
        }
    }
    public int CurrentSpeed
    {
        get {
            return _speed + (int)(_speedGrowth * _level);
        }
    }

}
