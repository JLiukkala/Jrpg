using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Enum that holds the different options for effects
public enum StatusOptions
{
    Attack, MagicAttack,
    ManaRestore,
    Damage, Heal,
    ModifyAttack, ModifyDefense,
    ModifySpeed, ModifyIntelligence, ModifyMagicResist
}


/********************************************
 * Stats Object class
 * 
 * Holds the level, each stat along with growth per level and the current tracker of any modifiers.
 * 
 * Also processes any Status Object that would effect it.
 */
public class StatsObject : MonoBehaviour {

    //Pretty straightforward stat holder
    [SerializeField, Tooltip("Name of ability")]
    private int _level = 0;
    [SerializeField, Tooltip("Health and its per level growth")]
    private int _health = 100, _healthGrowth = 1;
    private int _currentDamage;
    [SerializeField, Tooltip("Spell Points and its per level growth")]
    private int _mana = 100, _manaGrowth = 1;
    private int _currentMana;
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
    [SerializeField, Tooltip("GameObject to hold all the buff images")]
    private BuffHolder _buffHolder;
    [SerializeField, Tooltip("GameObject to hold all the buff images")]
    private BattleEntity _battleEntity;
    private const float DOUBLEVALUE = 1.5f;
    public bool log = false;

    List<StatusObject> effects = new List<StatusObject>();

    //Double checks everything is properly present
    private void Start()
    {
        if(_buffHolder == null)
        {
            Debug.LogWarning("StatsObject: Missing BuffHolder");
        }
        if(_battleEntity == null)
        {
            Debug.LogWarning("StatsObject: Missing BattleEntity");
        }
    }

    //If process is called with no argumants it runs the default process.
    //Cycles through all current effects and sends them to be process(effect)ed
    public void Process() {
        //Reset buffs so instead of having to undo a buff when its gone we can just have them re-apply each turn
        ResetCurrents();

        //Cycles through all current applied effects
        for(int i = 0; i < effects.Count;)
        {
            //Process current effect and take one away from the duration
            Process(effects[i]);
            effects[i].Duration -= 1;
            //if the effect has no duration left remove it
            if(effects[i].Duration == 0)
            {
                //Also try to remove it from buffs, even if it isnt a buff
                //add an if later if effects[i].Effect.value >= whatever the value of first buff is in enum                                    ////////////Tagged////////////
                //*****************************************************************************************************************************//////////////To//////////////
                //dont wanna do it now incase i add more                                                                                       ////////////Change////////////
                _buffHolder.RemoveBuff(effects[i].Effect, DetermineSeverity(effects[i]));
                effects.RemoveAt(i);
            } else
            {
                //Only increment if there isn't a list item removed. 
                //This is because when we remove one all the following enteries get their index reduced by one.
                //If we always did this every time we remove an element it would skip the following
                i++;
            }
        }
        if (!IsAlive)
        {
            //show dead
            gameObject.SetActive(false);
        }

    }
    
    //Takes an effect and preforms the action
    public void Process(StatusObject effect)
    {
        //Switches based on the effect enum on current effect
        //each effect has its own calculation, they will also reference any movement or numbers to apear on character
        switch(effect.Effect)
        {
            case StatusOptions.Attack:
                TakeDamage((int)(effect.Attack * effect.CalculatedValue));
                _battleEntity.Splash(((int)(effect.Attack * effect.CalculatedValue)).ToString(),Color.red);
                //instantiate a number based on value over head                                                                                ////////////Tagged////////////
                //call battleentity shake for a few seconds************************************************************************************//////////////To//////////////
                //call battleentity color change for a second                                                                                  ////////////Change////////////
                break;
            case StatusOptions.ManaRestore:
                Mana = -(int)(effect.Intelligence * effect.CalculatedValue);
                _battleEntity.Splash(((int)(effect.Intelligence * effect.CalculatedValue)).ToString(), Color.blue);
                //instantiate a number based on value over head                                                                                ////////////Tagged////////////
                //call battleentity shake for a few seconds************************************************************************************//////////////To//////////////
                //call battleentity color change for a second                                                                                  ////////////Change////////////
                break;
            case StatusOptions.MagicAttack:
                TakeMagicDamage((int)(effect.Intelligence * effect.CalculatedValue)) ;
                _battleEntity.Splash(((int)(effect.Intelligence * effect.CalculatedValue)).ToString(),Color.red);
                //call battleentity shake for a few seconds
                //call battleentity color change for a second
                break;
            case StatusOptions.Damage:
                Health = -(int)(effect.CalculatedValue);
                _battleEntity.Splash(((int)(effect.CalculatedValue)).ToString(),Color.red);
                //call battleentity shake for a few seconds
                //call battleentity color change for a second
                break;
            case StatusOptions.Heal:
                Health = (int)(effect.Intelligence * effect.CalculatedValue);
                _battleEntity.Splash(((int)(effect.Intelligence * effect.CalculatedValue)).ToString(), Color.green);
                break;
            ///////////////////////////////////////////////////////////////////////////////////////////////
            //*****************************************Status Effects************************************//
            case StatusOptions.ModifyAttack:
                _currentAttack += (int)(effect.CalculatedValue*_attack- _attack);
                _buffHolder.AddBuff(effect.Effect, DetermineSeverity(effect) );
                break;
            case StatusOptions.ModifyDefense:
                _currentDefense += (int)(effect.CalculatedValue * _defense-_defense);
                _buffHolder.AddBuff(effect.Effect, DetermineSeverity(effect));
                break;
            case StatusOptions.ModifySpeed:
                _currentSpeed += (int)(effect.CalculatedValue * _speed-_speed);
                _buffHolder.AddBuff(effect.Effect, DetermineSeverity(effect));
                break;
            case StatusOptions.ModifyIntelligence:
                _currentIntelligence += (int)(effect.CalculatedValue * _intelligence - _intelligence);
                _buffHolder.AddBuff(effect.Effect, DetermineSeverity(effect));
                break;
            case StatusOptions.ModifyMagicResist:
                _currentMagicResist += (int)(effect.CalculatedValue * _magicResist - _magicResist);
                _buffHolder.AddBuff(effect.Effect, DetermineSeverity(effect));
                break;
            default:
                Debug.LogWarning("StatsObject: Proccess not set up for "+ effect.Effect);
                break;
        }

        if (!IsAlive)
        {
            //show dead
            gameObject.SetActive(false);
        }
    }

    //Returns severity of an effect based on DOUBLEVALUE, this can be misleading as it determines of the value not including margin calculation
    //If passed zero this will also return down as enums cant have null returns and im to lazy to put a safegaurd in
    public Severity DetermineSeverity(StatusObject effect) {
        if(effect.ActualValue >= DOUBLEVALUE)
        {
            return Severity.DoubleUp;
        } else if(effect.ActualValue >0)
        {
            return Severity.Up;
        } else if(effect.ActualValue <= -DOUBLEVALUE)
        {
            return Severity.DoubleDown;
        } else
        {
            return Severity.Down;
        }
    }

    //Sets all buffs and debufs to zero
    private void ResetCurrents()
    {
        _currentAttack = 0;
        _currentDefense = 0;
        _currentSpeed = 0;
        _currentIntelligence = 0;
        _currentMagicResist = 0;
    }     

    //Processes or adds effect accordingle
    public void Effects(StatusObject value)
    {
        //if its set to be instant do an instant process
        if(value.InstantCalculate)
        {
            Process(value);
        }
        //Roll the effects duration and add it if over 0
        value.RollDuration();
        if(value.Duration>0)
        {
            effects.Add(value);
        }
        
    }

    //Nearly the same for both so I'll explain one
    //Takes raw damage int
    public void TakeDamage(int rawDamage) {
        //calculates the damage based on jaakos damage to defense calculation
        //put into a temporary variable because when i was directly putting it into _current damage it was always zero
        //don't know why couldnt fix it so dont make the code into shorthand
        int calculatedDamage = rawDamage * rawDamage / (rawDamage + CurrentDefense);
        _currentDamage += calculatedDamage;
        _currentDamage = Mathf.Clamp(_currentDamage, 0, MaxHealth);
    }
    public void TakeMagicDamage(int rawDamage)
    {
        int calculatedDamage = rawDamage * (rawDamage / (rawDamage + CurrentMagicDefense));
        _currentDamage += calculatedDamage;
        _currentDamage = Mathf.Clamp(_currentDamage, 0, MaxHealth);
    }



    //Max mana and special
    public int MaxHealth {
        get {
            return _health + Level * _healthGrowth;
        }
    }
    public int MaxMana
    {
        get {
            return _mana + Level * _manaGrowth;
        }
    }
    //Curent health and mana
    public int Health
    {
        get {
            return _health + Level * _healthGrowth - _currentDamage;
        }

        set {
            //When a value is directly sent to health invert it and add it to current damage
            // health = -5 becomes _currentDamage + 5
            _currentDamage = Mathf.Clamp(_currentDamage - value, 0, MaxHealth);
        }
    }
    public void AbsoluteSetHealth(int value) {
        _currentDamage = Mathf.Clamp(Health - value, 0, MaxHealth);
    }
    public void AbsoluteSetMana(int value)
    {
        _currentMana = Mathf.Clamp(Mana - value, 0, MaxMana);
    }
    public int Mana
    {
        get {
            return MaxMana - _currentMana;
        }

        set {
            _currentMana = Mathf.Clamp(_currentMana + value, 0, MaxMana);
        }
    }
    //Getters for all the stats
    public int Level
    {
        get {
            return _level;
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
            return _speed + Level * _speedGrowth + _currentSpeed;
        }
    }
    //Returns a bool based on the level of health
    public bool IsAlive
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
}
