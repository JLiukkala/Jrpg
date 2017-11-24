using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionHandler : MonoBehaviour {

    private bool isStackSorted = false;
    private bool hasEnemyActed = false;
    private List<ActionObject> actionList = new List<ActionObject>();

    [SerializeField]
    private BattleHandler _battle;
    public bool EnemyActed
    {
        get { return hasEnemyActed; }
    }
    public int StackHeight
    {
        get { return actionList.Count; }
    }

    public void Sort()
    {
        //Find a way to sort the list
        //actionList.Sort((x, y) => _battle.GetPlayer(x.Origin).Stats.Speed + _battle.GetPlayer(x.Origin).Abilities.AbilityList[_battle.GetPlayer(x.Origin).Abilities.SkillIndex(x.Action)].Speed -
         //   _battle.GetPlayer(x.Origin).Stats.Speed + _battle.GetPlayer(x.Origin).Abilities.AbilityList[_battle.GetPlayer(x.Origin).Abilities.SkillIndex(x.Action)].Speed);
        //sorts the stack of current actions based on speed
    }
    public int ActionLength() {
        return actionList.Count;
    }
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
    public ActionObject Current()
    {
        if(actionList.Count > 0)
        {
            var temp = actionList[actionList.Count - 1];
            return temp;
        } else
        {
            return null;
        }

    }
    public void Push(BattleEntity origin, string type, BattleEntity target)
    {
        ActionObject obj = new ActionObject(origin, type, target);
        actionList.Add(obj);
    }

    bool newattack = true;
    float starttime;
    Vector3 pos;
    public void Fight()
    {
        ActionObject obj = Current();
        if(newattack)
        {
            starttime = Time.time;
            pos = obj.Origin.transform.position;
            obj.Origin.transform.position = obj.Target.transform.position;
            obj.Origin.Stats.Mana = obj.Origin.FindAbility(obj.Action).Cost;
            for(int j = 0; j < obj.Origin.FindAbility(obj.Action).Effects.Length; j++)
            {

                obj.Origin.FindAbility(obj.Action).Effects[j].Setup();
                obj.Target.Stats.Effects = obj.Origin.FindAbility(obj.Action).Effects[j];
                //run action animation on target
            }
            if(obj.Origin.FindAbility(obj.Action).Animation!=null)
            {
                
            }
            newattack = false;
        }



        if(Time.time - starttime> 1)
        {
            obj.Origin.transform.position = pos;
            
        }
        if(Time.time - starttime > 2)
        {
            obj.Origin.Action();
            Pop();
            newattack = true;
        }
    }
    public void Print()
    {
        for(int i = 0; i < actionList.Count; i++)
        {
            Debug.Log(actionList[i].Origin + " " +actionList[i].Action +" "+ actionList[i].Target.Name);
        }
    }

}
