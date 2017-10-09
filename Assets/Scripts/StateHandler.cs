using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateHandler : MonoBehaviour {

    //add serialized inventory here later
    [SerializeField, Tooltip("Array of members")]
    private PlayerEntity[] _members;
    [SerializeField, Tooltip("Array of enemies")]
    private EnemyEntity[] _enemies;

    public int PartySize
    {
        get {
            return _members.Length;
        }
    }

    public PlayerEntity PartyMember(int index)
    {
        if(index < _members.Length)
        {
            return _members[index];
        } else
        {
            Debug.LogWarning("No state party member at this index");
            return null;
        }
    }
    public int EnemySize
    {
        get {
            return _enemies.Length;
        }
    }

    public EnemyEntity EnemyMember(int index)
    {
        if(index < _enemies.Length)
        {
            return _enemies[index];
        } else
        {
            Debug.LogWarning("No state enemy member at this index");
            return null;
        }
    }


}
