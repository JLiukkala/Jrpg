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
    public ActionObject Pop()
    {
        var temp = actionList[actionList.Count - 1];
        actionList.RemoveAt(actionList.Count - 1);
        return temp;
    }
    public void Push(BattleEntity origin, string type, BattleEntity target)
    {
        ActionObject obj = new ActionObject(origin, type, target);
        actionList.Add(obj);
    }
    public void Fight()
    {
        int length = actionList.Count;
        
        for(int i = 0; i < length; i++)
        {
            
            ActionObject obj = Pop();
            for(int j = 0; j < obj.Origin.FindAbility(obj.Action).Effects.Length; j++)
            {
                obj.Target.Stats.Effects = obj.Origin.FindAbility(obj.Action).Effects[j];
            }
            
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
