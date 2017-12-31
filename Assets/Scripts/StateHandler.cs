using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    MainMenu = 0, Dungeon, Battle, GameOver, Victory
}
/********************************************
 * State Handler class
 * 
 * Holds data used when switching from one state to another.
 * 
 * Has getters for all the information and setters for the
 * health and enemies.
 */
public class StateHandler : MonoBehaviour {

    //Global Trackers
    private GameState currentState = GameState.MainMenu;
    public bool battledOnce;


    //Dungeon scene Variables
    [SerializeField, Tooltip("Dungeon position of party")]
    private Vector3 _partyPosition;
    private GameObject player;
    private float walkTimer = 0;
    private int nextEncounter = 0;
    public int mintime = 0;
    public int maxtime = 0;
    //Battle Scene Variables
    //random encounter enemies
    [SerializeField, Tooltip("Array of enemies")]
    private EnemyEntity[] _dungeonEnemies;
    [SerializeField, Tooltip("Array of enemies")]
    private List<EnemyEntity> _enemies;
    [SerializeField, Tooltip("Array of members")]
    private PlayerEntity[] _partyMembers;
    [SerializeField, Tooltip("Health modifier of each party member")]
    private int[] _membersHealth;
    [SerializeField, Tooltip("Mana modifier of each party member")]
    private int[] _membersMana;
    private BattleHandler battleHandler;




    //Checks the data is all properly filled in at start.
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    private void Start()
    {
        for (int i = 0; i < PartyCount; i++)
        {
            if (_partyMembers[i] == null)
            {
                Debug.LogWarning("StateHandler: Null Party Member at index " + i);
            }
        }
        for (int i = 0; i < PartyCount; i++)
        {
            if (_partyMembers[i] == null)
            {
                Debug.LogWarning("StateHandler: Null Party Member at index " + i);
            }
        }
        if (_partyMembers.Length != _membersHealth.Length)
        {
            Debug.LogWarning("StateHandler: Members Health length does not match Party Members length");
        }
        if (_partyMembers.Length != _membersMana.Length)
        {
            Debug.LogWarning("StateHandler: Members Mana length does not match Party Members length");
        }
        for (int i = 0; i < _partyMembers.Length; i++)
        {
            SetPartyMemberHealth(i, _partyMembers[i].Stats.Health);
            SetPartyMemberMana(i, _partyMembers[i].Stats.Mana);
        }
    }

    private void Update()
    {
        switch (currentState)
        {
            case GameState.MainMenu:
                break;
            case GameState.Dungeon:
                if (!(_partyPosition == player.transform.position))
                {
                    _partyPosition = player.transform.position;
                    walkTimer += Time.deltaTime;
                    if (nextEncounter <= walkTimer)
                    {
                        ClearEnemies();
                        RollEnemies();
                        ChangeState(GameState.Battle);
                    }
                }

                break;
            case GameState.Battle:
                break;
            case GameState.GameOver:
                break;
            case GameState.Victory:
                break;
            default:
                break;
        }
    }

    public void ChangeState(GameState state)
    {
        if (currentState == GameState.GameOver || currentState == GameState.Victory)
        {
            for (int i = 0; i < _membersHealth.Length; i++)
            {
                SetPartyMemberHealth(i, _partyMembers[i].Stats.Health);
                SetPartyMemberMana(i, _partyMembers[i].Stats.Mana);
            }
            _partyPosition = new Vector3(0, 0, 0);
            battledOnce = false;
        }
        switch(state)
        {
            case GameState.MainMenu:
                SceneManager.LoadScene("MainMenu");
                Destroy(gameObject);
                break;
            case GameState.Dungeon:
                walkTimer = 0;
                nextEncounter = Random.Range(mintime, maxtime);

                SceneManager.LoadScene("DungeonScene");
                break;
            case GameState.Battle:
                
                SceneManager.LoadScene("BattleScene");
                break;
            case GameState.GameOver:

                SceneManager.LoadScene("endscene");
                break;
            case GameState.Victory:

                SceneManager.LoadScene("win");
                break;
            default:
                break;
        }
    }
    private void OnLevelWasLoaded(int level)
    {
        switch(level)
        {
            case (int)GameState.MainMenu:
                break;
            case (int)GameState.Dungeon:
                player = GameObject.Find("DungeonPlayer");
                currentState = GameState.Dungeon;
                break;
            case (int)GameState.Battle:
                battleHandler = (BattleHandler)GameObject.Find("Battle Handler").GetComponent(typeof(BattleHandler));
                currentState = GameState.Battle;
                break;
            case (int)GameState.GameOver:
                currentState = GameState.GameOver;
                break;
            case (int)GameState.Victory:
                currentState = GameState.Victory;
                break;
            default:
                break;
        }
    }
    public void PassInput(string input)
    {
        switch(currentState)
        {
            case GameState.MainMenu:
                if(input == "Select")
                {
                    ChangeState(GameState.Dungeon);
                } else if(input == "Esc")
                {
                    Application.Quit();
                }
                break;
            case GameState.Dungeon:
                break;
            case GameState.Battle:
                battleHandler.PassInput(input);
                break;
            case GameState.GameOver:
                if (input == "Select")
                {
                    ChangeState(GameState.Dungeon);
                }
                else if (input == "Esc")
                {
                    Application.Quit();
                }
                break;
            case GameState.Victory:
                if (input == "Select")
                {
                    ChangeState(GameState.Dungeon);
                }
                else if (input == "Esc")
                {
                    Application.Quit();
                }
                break;
            default:
                break;
        }
    }

    public int EnemyCount
    {
        get {
            return _enemies.Count;
        }
    }
    public int PartyCount
    {
        get {
            return _partyMembers.Length;
        }
    }
    public Vector3 PartyPosition
    {
        get {
            return _partyPosition;
        }

        set {
            _partyPosition = value;
        }
    }
    
    public PlayerEntity PartyMember(int index)
    {
        if(index < PartyCount && index >= 0)
        {
            return _partyMembers[index];
        } else
        {
            Debug.LogWarning("StateHandler: No state party member at index " + index);
            return null;
        }
    }

    public int GetPartyMemberHealth(int index)
    {
        if(index < _membersHealth.Length && index >= 0)
        {
            return _membersHealth[index];
        } else
        {
            Debug.LogWarning("StateHandler: No state index of " + index);
            return 0;
        }
    }
    public void SetPartyMemberHealth(int index, int value)
    {
        if(index < _membersHealth.Length && index >= 0)
        {
            _membersHealth[index] = value;
        } else
        {
            Debug.LogWarning("StateHandler: No state index of " + index);
        }
    }

    public int GetPartyMemberMana(int index)
    {
        if(index < _membersMana.Length && index >= 0)
        {
            return _membersMana[index];
        } else
        {
            Debug.LogWarning("StateHandler: No state index of " + index);
            return 0;
        }
    }
    public void SetPartyMemberMana(int index, int value)
    {
        if(index < _membersMana.Length && index >= 0)
        {
            _membersMana[index] = value;
        } else
        {
            Debug.LogWarning("StateHandler: No state index of " + index);
        }
    }



    public EnemyEntity Enemy(int index)
    {
        if(index < EnemyCount && index >= 0)
        {
            return _enemies[index];
        } else
        {
            Debug.LogWarning("StateHandler: No state enemy member at index "+index);
            return null;
        }
    }
    public void ClearEnemies() {
        _enemies.Clear();
    }
    public void AddEnemy(EnemyEntity newEnemy) {
        _enemies.Add(newEnemy);
    }
    private void RollEnemies() {
        int enemies = Random.Range(1, 4);
        EnemyEntity Enemy = _dungeonEnemies[Random.Range(0, _dungeonEnemies.Length)];
        for(int i = 0; i < enemies; i++)
        {
            AddEnemy(Enemy);
        }
    }
   

   
}
