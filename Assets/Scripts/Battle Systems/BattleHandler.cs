using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/********************************************
 * Battle Handler class
 * 
 * Main loop for the battle scene
 * 
 * links all the modules together
 * 
 */
public class BattleHandler : MonoBehaviour {

    public AudioSource battleMusic;
    [SerializeField, Tooltip("Input Handler for this battle class")]
    private InputHandler _InputHandler;
    [SerializeField, Tooltip("UI Handler for this battle class")]
    private UIHandler _uiHandler;
    [SerializeField, Tooltip("Action Handler for this battle class")]
    private ActionHandler _actionHandler;

    int phase;
    int activeMember = -1;
    private StateHandler state;
    private EnemyEntity[] enemies;
    private PlayerEntity[] partyMembers;

    //Initializes position of party members and enemies
    void Start () {
        phase = 0;
        //Get the local instance of State Handler
        state = (StateHandler)GameObject.FindWithTag("State Machine").GetComponent(typeof(StateHandler));
        //Initialize
        //  Partymember * n
        partyMembers = new PlayerEntity[state.PartyCount];
        for(int i = 0; i < state.PartyCount; i++)//Loops through and instantiates all party members
        {
            if(state.PartyMember(i) != null)//Checks if the member is null to avoid error
            {
                //  formula is ((i + 1)*(screen.width/players.length))-(screen.width/2) 
                partyMembers[i] = Instantiate(state.PartyMember(i), new Vector2(((i + 1) * (16 / (state.PartyCount + 1))) - (16 / 2), -.3f), Quaternion.identity);
                //Makes the first live player active
                partyMembers[i].Stats.AbsoluteSetHealth(state.GetPartyMemberHealth(i)); 
                partyMembers[i].Stats.AbsoluteSetMana(state.GetPartyMemberMana(i));
                if (state.PartyMember(i).Stats.IsAlive == true && activeMember == -1)
                {
                    activeMember = i;
                }
                if (partyMembers[i].Stats.Health ==0)
                {
                    partyMembers[i].gameObject.SetActive(false);
                }
            } else
            {
                Debug.LogWarning("Tried to instantiate null party member at index: "+i);
            }
            
        }
        //  Enemy * n
        enemies = new EnemyEntity[state.EnemyCount];
        for(int i = 0; i < state.EnemyCount; i++)
        {
            if(state.Enemy(i) != null)
            {
                enemies[i] = Instantiate(state.Enemy(i), new Vector2(((i + 1) * (16 / (state.EnemyCount + 1))) - (16 / 2), 3.3f), Quaternion.identity);
            } else
            {
                Debug.LogWarning("Tried to instantiate null enemy member at index: " + i);
            }
        }
        battleMusic.clip = enemies[0].battleMusic.clip;
        battleMusic.volume = enemies[0].volume;
        battleMusic.Play();
        phase = 1;
    }


    //Based on phase preforms actions
    void Update () {
        //Entry Phase
        if(phase == 1)
        {
            
            if (state.battledOnce)
            {
                partyMembers[activeMember].Select();
                //  play player and enemies battle start animations if we ever get them
                _uiHandler.SetMembers(partyMembers, enemies);
                phase = 2;
            }
            else {
                _uiHandler.Instrctions(true);
            }
            
        }
        //Player Phase
        else if(phase == 2)
        {
            //instantiate player phase text
            if (_uiHandler.CurrentState == MenuState.Empty) {
                
                _uiHandler.CurrentMember=(activeMember);
                _uiHandler.TransformState(MenuState.Ability);
            }
        }
        //Enemy Phase
        else if(phase == 3)
        {//Determine enemy action
            for(int i = 0; i < enemies.Length; i++)
            {
                _actionHandler.Push(enemies[i], enemies[i].Think(), partyMembers[enemies[i].Target(partyMembers.Length)]);
            }
            _uiHandler.Splash("Battle Phase!", new Vector3(0, 1.5f, 0));
            _actionHandler.Sort();
            //sort actions
            phase = 4;
            //  call enemy action
            //      calls that battle stack function
        }
        //Action Process Phase
        else if(phase == 4)
        {//process actions
            //if all friendlies dead end game
            _actionHandler.Fight();
            if (PlayerAlive())
            {
                if (!EnemiesAlive())
                {
                    Debug.Log("enemies still alve");
                    for (int i = 0; i < partyMembers.Length; i++)
                    {
                        state.SetPartyMemberHealth(i, partyMembers[i].Stats.Health);
                        state.SetPartyMemberMana(i, partyMembers[i].Stats.Mana);
                    }
                    if (enemies[0]._nextEnemy != null)
                    {
                        state.ClearEnemies();
                        state.AddEnemy(enemies[0]._nextEnemy);
                        state.ChangeState(GameState.Battle);
                    }
                    else if (enemies[0]._isFinalBoss)
                    {
                        state.ChangeState(GameState.Victory);
                    }
                    else {
                        state.ChangeState(GameState.Dungeon);
                    }
                    
                    
                }
            }
            else
            {
                SceneManager.LoadScene("endscene");
            }
            if (_actionHandler.ActionCount < 1)
            {
                _uiHandler.Splash("Player Phase!", new Vector3(0,1.5f,0));
                FirstMember();
                phase = 2;
            }

        }
        else
        {
            Debug.LogWarning("Unrecognized phase");
        }
    }


    public void PassInput(string input) {
        switch (phase)
        {
            case 1:
                if (input == "Select")
                {
                    state.battledOnce = true;
                    _uiHandler.Instrctions(false);
                }
                break;
            case 2:
                _uiHandler.PassInput(input);
                break;
            default:
                break;
        }
        
    }

    //Returns active player object
    public PlayerEntity GetPlayer()
    {
        return partyMembers[activeMember];
    }
    //Returns player at given index
    public PlayerEntity GetPlayer(int position)
    {
        return partyMembers[position];
    }

   
    //Passes actions to Action Handler also goes to next member
    public void Select(int target, string action, bool isTargetEnemy) {
        if(isTargetEnemy)
        {
            _actionHandler.Push(partyMembers[activeMember], action, enemies[target]);

        } else
        {
            _actionHandler.Push(partyMembers[activeMember], action, partyMembers[target]);
        }
        if(!NextMember())
        {
            phase = 3;
        }
    }

    public void Reselect(ActionObject obj) {
        BattleEntity origin = obj.Origin;
        string action = obj.Action;
        bool goodselect = false;
        bool istargetenemy = origin.FindAbility(action).TargetEnemy;
        int roll;
        while (!goodselect)
        {
            if (istargetenemy)
            {
                roll = Random.Range(0, enemies.Length);
                if (enemies[roll].Stats.Health >0)
                {
                    goodselect = true;
                    _actionHandler.Push(origin, action, enemies[roll]);
                }
            }
            else
            {
                roll = Random.Range(0, partyMembers.Length);
                if (partyMembers[roll].Stats.Health > 0)
                {
                    goodselect = true;
                    _actionHandler.Push(origin, action, partyMembers[roll]);
                }
            }
            
        }
    }

    public void Splash(string text,Vector3 pos,Color c) {
        _uiHandler.Splash(text, pos, c);
    }
    public void Splash(string text, Vector3 pos)
    {
        _uiHandler.Splash(text, pos);
    }


    //Functions to change selected member
    public void PreviousMember()
    {
        _actionHandler.Pop();
        partyMembers[activeMember].Deselect();
        for(int i = activeMember-1; i >= 0; i--)
        {
            if(partyMembers[i].Stats.IsAlive == true)
            {
                activeMember = i;
                break;
            }
        }
        partyMembers[activeMember].Select();
        _uiHandler.CurrentMember = activeMember;
    }
    public bool NextMember()
    {
        partyMembers[activeMember].Deselect();
        for(int i = 0; i < partyMembers.Length; i++)
        {
            if(partyMembers[i] != null)
            {
                if(partyMembers[i].Stats.IsAlive == true && activeMember < i)
                {
                    activeMember = i;
                    partyMembers[activeMember].Select();
                    return true;
                }
            }
        }
        return false;
    }
    public bool FirstMember()
    {
        for(int i = 0; i <partyMembers.Length; i++)
        {

            if(partyMembers[i] != null)
            {
                if(partyMembers[i].Stats.IsAlive == true)
                {
                    activeMember = i;
                    partyMembers[activeMember].Select();
                    return true;
                }
            }
        }
        return false;
    }
    public bool PlayerAlive() {
        for (int i = 0; i < partyMembers.Length; i++)
        {

            if (partyMembers[i] != null)
            {
                if (partyMembers[i].Stats.IsAlive == true)
                {
                    return true;
                }
            }
        }
        return false;
    }
    public bool EnemiesAlive()
    {
        for (int i = 0; i < enemies.Length; i++)
        {   
            if (enemies[i].Stats.IsAlive == true)
            {
                return true;
            }
            
        }
        return false;
    }


    //Getters for battle entity count
    public int EnemyCount()
    {
        return enemies.Length;
    }
    public int PlayerCount()
    {
        return partyMembers.Length;
    }

}
