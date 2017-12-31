using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/********************************************
 * Action Handler class
 * 
 * Keeps track of a list of actions
 * 
 * has the functions to chenge the list and process the actions
 */
public class ActionHandler : MonoBehaviour {

    [SerializeField]
    private BattleHandler _battle;

    private bool isStackSorted = false;
    private bool hasEnemyActed = false;
    private List<ActionObject> actionList = new List<ActionObject>();
    
    //these are all for Fight()
    bool newattack = true;
    float starttime;
    Vector3 pos;
    

    
    //This function sorts the current stack of actions by speed
    //Not yet implemented                                                                                                          ////////////Tagged////////////
    //*****************************************************************************************************************************//////////////To//////////////
    //                                                                                                                             ////////////Change////////////
    public void Sort()
    {
        //Find a way to sort the list
        //actionList.Sort((x, y) => _battle.GetPlayer(x.Origin).Stats.Speed + _battle.GetPlayer(x.Origin).Abilities.AbilityList[_battle.GetPlayer(x.Origin).Abilities.SkillIndex(x.Action)].Speed -
         //   _battle.GetPlayer(x.Origin).Stats.Speed + _battle.GetPlayer(x.Origin).Abilities.AbilityList[_battle.GetPlayer(x.Origin).Abilities.SkillIndex(x.Action)].Speed);
        //sorts the stack of current actions based on speed
    }

    //returns the last item in the action stack and removes it
    //basicly we are giving our list the functionality of stack with the ease of use of a list
    public ActionObject Pop()
    {
        if(actionList.Count>0)
        {
            var temp = actionList[actionList.Count - 1];
            actionList.RemoveAt(actionList.Count - 1);
            return temp;
        } else
        {
            return null;
        }
        
    }
    
    //Returns the last item in the action list without erasing it
    public ActionObject Current()
    {
        if(actionList.Count > 0)
        {
            return actionList[actionList.Count - 1];
        } else
        {
            return null;
        }

    }

    //adds a new actionObject onto the stack based on the parameters
    public void Push(BattleEntity origin, string type, BattleEntity target)
    {
        actionList.Add(new ActionObject(origin, type, target));
    }

    //responsible for moving, animating, and processing the actions of the list
    public void Fight()
    {
        ActionObject obj = Current();
        if (obj.Target.Stats.Health == 0)
        {
            _battle.Reselect(Pop());
            return;
        }
        if (obj.Origin.Stats.IsAlive)
        {
            //We get the current action

            //If this is our first loop through fight with this action run this
            if (newattack)
            {
                //set the start time
                starttime = Time.time;
                //record starting pos
                pos = obj.Origin.transform.position;
                //transform positions
                if (obj.Origin.Name != obj.Target.Name)
                {
                    obj.Origin.transform.position = obj.Target.transform.position - new Vector3(1.2f, .5f, 0);
                }
                //subtract the mana cost
                Debug.Log(obj.Origin.FindAbility(obj.Action));
                Debug.Log(obj.Action);
                obj.Origin.Stats.Mana = obj.Origin.FindAbility(obj.Action).Cost;
                //for all the effects of our ability 
                for (int j = 0; j < obj.Origin.FindAbility(obj.Action).Effects.Length; j++)
                {
                    //setup the effect
                    obj.Origin.FindAbility(obj.Action).Effects[j].Setup();
                    //stack the effect into the targets effect list
                    obj.Target.Stats.Effects(obj.Origin.FindAbility(obj.Action).Effects[j]);

                }
                //check if there is an animation for the current ability
                if (obj.Origin.FindAbility(obj.Action).GetAnimation != null)
                {
                    GameObject tempanim = Instantiate(obj.Origin.FindAbility(obj.Action).GetAnimation);
                    tempanim.transform.position = obj.Target.transform.position;
                }
                //play action sound

                //lets us know that the first loop of this action has run
                newattack = false;
            }


            //after 1 second has elapsed change position back to the old position
            if (Time.time - starttime > 1)
            {
                obj.Origin.transform.position = pos;
            }
            //after 2 seconds call the origin entities action, as we want buffs and poisons and suck to tick down after their action
            if (Time.time - starttime > 2)
            {
                obj.Origin.Action();
                //pop the current action so we can process the next
                Pop();
                //turn new attack false
                newattack = true;
            }
        }
        else {
            Pop();
            //turn new attack false
            newattack = true;
        }
        
    }

    //Public class for debugging, it prints out all of the current actions in order
    public void Print()
    {
        for(int i = 0; i < actionList.Count; i++)
        {
            Debug.Log(actionList[i].Origin + " " +actionList[i].Action +" "+ actionList[i].Target.Name);
        }
    }


    //getters for state of enemy and actionlist length
    public bool EnemyActed
    {
        get { return hasEnemyActed; }
    }
    public int ActionCount
    {
        get { return actionList.Count; }
    }
}
