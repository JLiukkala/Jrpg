using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                //  not sure how to get screen size in the unity units to properly translate though
                partyMembers[i] = Instantiate(state.PartyMember(i), new Vector2(-5 + 3.334f * i, -1.3f), Quaternion.identity);
                if(state.PartyMember(i).Stats.IsAlive == true && activeMember == -1)//If its the first alive member set to active
                {
                    activeMember = i;
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
                enemies[i] = Instantiate(state.EnemyMember(i), new Vector2(-5 + 3.334f * i, -1.3f), Quaternion.identity);
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

    private void PhaseSelector()
    {
        if(phase == 0)//Initialization Phase
        {
            Debug.LogWarning("Phase is still initialization");//Shouldnt be in init phase after start
        } else if(phase == 1)//Entry Phase
        {
        //  play intro animation
        //  play player and enemie animations
        //  Ui style toPassive(intro text)
        //  istextscrolled?
        //      make ui to selector style
        //      go to player phase
        } else if(phase == 2)//Player Phase
        {//Allow player to pick their interaction
        //  send inputs to ui
        //  listen for action return
        //  if action(returned 1)
        //      ui is gonna call a public stack function in battle to stack action
        //      next member
        //      ui update selected
        //  if back(-1)
        //      pop last action
        //      go to last member
        //      ui update selected
        } else if(phase == 3)//Enemy Phase
        {//Determine enemy action
        //  call enemy action
        //      calls that battle stack function
        } else if(phase == 4)//Action Process Phase
        {//Sort and process actions

        } else if(phase == 5)//End Phase
        {//Determines next game state. Win and lose condition, or restart.

        } else
        {
            Debug.LogWarning("Unrecognized phase");
        }
    }
}
