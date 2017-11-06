using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BattleHandler : MonoBehaviour {

    
    [SerializeField, Tooltip("Input Handler for this battle class")]
    private InputHandler _InputHandler;
    [SerializeField, Tooltip("UI Handler for this battle class")]
    private UIHandler _uIHandler;
    [SerializeField, Tooltip("Action Handler for this battle class")]
    private ActionHandler _actionHandler;
    //[SerializeField, Tooltip("Animation Handler for this battle class")]
    //private AnimationHandler _actionHandler

    int phase;
    int activeMember = -1;
    private StateHandler state;
    private EnemyEntity[] enemies;
    private PlayerEntity[] partyMembers;


    // Use this for initialization
    void Start () {
        phase = 0;
        //Get the local instance of State Handler
        state = (StateHandler)GameObject.FindWithTag("State Machine").GetComponent(typeof(StateHandler));
        //Initialize
        //  Partymember * n
        partyMembers = new PlayerEntity[state.PartySize];
        for(int i = 0; i < state.PartySize; i++)//Loops through and instantiates all party members
        {
            if(state.PartyMember(i) != null)//Checks if the member is null to avoid error
            {
                /**************This needs to be fixed later to appropriatly space players*******************/
                //  formula will be ((i + 1)*(screen.width/players.length))-(screen.width/2) 
                //  not sure how to get screen size in the unity units to properly translate though so for now its 16 units because thats what my unity displays
                partyMembers[i] = Instantiate(state.PartyMember(i), new Vector2(((i + 1) * (16 / (state.PartySize + 1))) - (16 / 2), -1.3f), Quaternion.identity);
                if(state.PartyMember(i).Stats.IsAlive == true && activeMember == -1)//If its the first alive member set to active
                {
                    activeMember = i;
                    partyMembers[activeMember].transform.position = partyMembers[activeMember].transform.position + Vector3.up * .5f;
                }
            } else
            {
                Debug.LogWarning("Tried to instantiate null party member at index: "+i);
            }
        }
        //  Enemy * n
        enemies = new EnemyEntity[state.EnemySize];
        for(int i = 0; i < state.EnemySize; i++)
        {
            if(state.EnemyMember(i) != null)
            {
                /**************This needs to be fixed later to appropriatly space enemies*******************/
                // same formula as above
                enemies[i] = Instantiate(state.EnemyMember(i), new Vector2(((i + 1) * (16 / (state.EnemySize + 1))) - (16 / 2), 3.3f), Quaternion.identity);
            } else
            {
                Debug.LogWarning("Tried to instantiate null enemy member at index: " + i);
            }
        }
        phase = 1;
    }


    // Update is called once per frame
    void Update () {
        PhaseSelector();
    }
    public PlayerEntity GetPlayer(int position)
    {
        return partyMembers[position];
    }
    

    private void PhaseSelector()
    {
        //Initialization Phase
        if(phase == 0)
        {
            Debug.LogWarning("Phase is still initialization");//Shouldnt be in init phase after start
        }
        //Entry Phase
        else if(phase == 1)
        {
            //  play intro animation
            //  play player and enemie animations
            //  Ui style toPassive(intro text)
            //  istextscrolled?
            //      currentuidestroy
            //      go to player phase
            phase = 2;
        }
        //Player Phase
        else if(phase == 2)
        {//Allow player to pick their interaction
            if(!_uIHandler.UIHasObject)
            {
                
                _uIHandler.CreateUI(partyMembers[activeMember]);
            }
        }
        //Enemy Phase
        else if(phase == 3)
        {//Determine enemy action
            for(int i = 0; i < enemies.Length; i++)
            {
                _actionHandler.Push(enemies[i], enemies[i].Think(), partyMembers[enemies[i].Target(partyMembers.Length)]);
            }
            _actionHandler.Print();
            phase = 4;
         //  call enemy action
         //      calls that battle stack function
        }
        //Action Process Phase
        else if(phase == 4)
        {//Sort and process actions
            _actionHandler.Fight();
            for(int i = 0; i < enemies.Length; i++)
            {
                enemies[i].Action();
                
            }
            for(int i = 0; i < partyMembers.Length; i++)
            {
                partyMembers[i].Action();
                
            }
            Debug.Log(enemies[0].Stats.Health);
            phase = 5;
        }
        //End Phase
        else if(phase == 5)
        {//Determines next game state. Win and lose condition, or restart.
            if(FirstMember())
            {
                

                phase = 2;

            } else
            {
                SceneManager.LoadScene("endscene");
            }
            
            
        }
        else
        {
            Debug.LogWarning("Unrecognized phase");
        }
    }

    public void PassInput(string input) {
        switch(phase)
        {
            case 1:
            case 2:
                _uIHandler.PassInput(input);
                break;
        }
    }



    public void Select(int target, string action) {
        //stack action
        if(target < enemies.Length)
        {
            _actionHandler.Push(partyMembers[activeMember], action, enemies[target]);
        } else
        {
            _actionHandler.Push(partyMembers[activeMember], action, partyMembers[target-enemies.Length]);
        }
        if(!NextMember())
        {
            phase = 3;
        }
    }



    public void PreviousMember()
    {
        _actionHandler.Pop();
        partyMembers[activeMember].transform.position = partyMembers[activeMember].transform.position + Vector3.up * -.5f;
        for(int i = activeMember-1; i >= 0; i--)
        {
            if(partyMembers[i] != null)
            {
                if(partyMembers[i].Stats.IsAlive == true)
                {
                    activeMember = i;
                    
                    break;
                }
            }
        }
        partyMembers[activeMember].transform.position = partyMembers[activeMember].transform.position + Vector3.up * .5f;
    }
    public bool NextMember()
    {
        partyMembers[activeMember].transform.position = partyMembers[activeMember].transform.position + Vector3.up * -.5f;
        for(int i = 0; i < partyMembers.Length; i++)
        {
            if(partyMembers[i] != null)
            {
                if(partyMembers[i].Stats.IsAlive == true && activeMember < i)
                {
                    activeMember = i;
                    partyMembers[activeMember].transform.position = partyMembers[activeMember].transform.position + Vector3.up * .5f;
                    return true;
                }
            }
        }
        partyMembers[activeMember].transform.position = partyMembers[activeMember].transform.position + Vector3.up * .5f;
        return false;
    }
    public bool FirstMember()
    {
        partyMembers[activeMember].transform.position = partyMembers[activeMember].transform.position + Vector3.up * -.5f;
        for(int i = 0; i <partyMembers.Length; i++)
        {

            if(partyMembers[i] != null)
            {
                if(partyMembers[i].Stats.IsAlive == true)
                {
                    activeMember = i;
                    partyMembers[activeMember].transform.position = partyMembers[activeMember].transform.position + Vector3.up * .5f;
                    return true;
                }
            }
        }
        return false;
    }
}
