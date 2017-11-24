using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveUI : MonoBehaviour {

    [SerializeField, Tooltip("Description")]
    private Text _description;
    [SerializeField, Tooltip("Selection Texts")]
    private Text[] _selections;
    [SerializeField, Tooltip("Selection Texts")]
    private Text _mpcost;
    [SerializeField, Tooltip("Prefab for target")]
    private TargetUI _targetPrefab;
    [SerializeField, Tooltip("Prefab for target")]
    private GameObject arrowup, arrowdown;
    public int _blinkSpeed = 1;
    private BattleHandler battleHandler;
    private int textoption = 0;
    private int position = 0;
    private bool isInTarget = false;
    private bool isAreaofEffect = false;
    private UIObject ui;
    private bool sync = false;
    private UIHandler uiHandler;
    private TargetUI target;
    //private string[,] currentUI;
    private int goingUp = -1, goingDown = 1, goingFirst = -10, goingLast = 10;
    private Color gray = new Color(.4f, .4f, .4f);
    private Color black = new Color(0, 0, 0);
    private void Awake()
    {
        battleHandler = (BattleHandler)GameObject.FindWithTag("Battle Handler").GetComponent(typeof(BattleHandler));
    }
    public bool HasObject() {
        if(ui != null)
        {
            return true;
        } else
        {
            return false;
        }
    }
    private int frames = 1;
    public void Populate(PlayerEntity player, UIHandler UI)
    {
        uiHandler = UI;
        if(!HasObject())
        {
            ui = new UIObject(player);
        }
        SetScreen(0);
    }

    //0 is main, 1 is skill, 2 is inventory
    private void SetScreen(int screen)
    {
        ui.SelectMenu(screen);
        UpdateOptions();
        UpdateDescription();

        Highlight(ui.CurrentSelection);
    }
    private void Update()
    {
        if(ui!=null)
        {

        }
        sync = !sync;
        Debug.Log(ui);
        bool up = ui.CurrentSelection ==0;
        bool down = (ui.MenuLength() - 1 == ui.CurrentSelection);
        if( frames % _blinkSpeed== 0)
        {
            
            if(!up)
            {
                arrowup.SetActive(sync);  
            }
            if(!down)
            {
                arrowdown.SetActive(sync);
            }
            frames = 1;
        }
        if(up)
        {
            arrowup.SetActive(false);
        }
        if(down)
        {
            arrowdown.SetActive(false);
        }
        frames++;
    }

    private void UpdateDescription()
    {
        //Displays discription of menu item given
        _description.text = ui.Description();
        if(ui.CurrentScreen == 1)
        {
            _mpcost.text = "Cost: "+battleHandler.GetPlayer().FindAbility(ui.Action).Cost.ToString();
            _mpcost.gameObject.SetActive(true);
        } else
        {
            _mpcost.gameObject.SetActive(false);
        }
    }
    private void UpdateOptions()
    {
        //Displays menu items
        _selections[0].text = ui.Option(0);
        
        _selections[1].text = ui.Option(1);
        _selections[2].text = ui.Option(2);
        if(ui.CurrentScreen == 1)
        {
            if(!(battleHandler.GetPlayer().FindAbility(ui.Option(0)).Cost<= battleHandler.GetPlayer().Stats.Mana))
            {
                _selections[0].color = gray;
            } else
            {
                _selections[0].color = black;
            }
            if(!(battleHandler.GetPlayer().FindAbility(ui.Option(1)).Cost <= battleHandler.GetPlayer().Stats.Mana))
            {
                _selections[1].color = gray;
            } else
            {
                _selections[1].color = black;
            }
            if(!(battleHandler.GetPlayer().FindAbility(ui.Option(2)).Cost <= battleHandler.GetPlayer().Stats.Mana))
            {
                _selections[2].color = gray;
            } else
            {
                _selections[2].color = black;
            }
            
        } else
        {
            _selections[0].color = black;
            _selections[1].color = black;
            _selections[2].color = black;
        }
    }


    public void HandleInput(string input)
    {
        if(target == null)
        {
            Restore(ui.CurrentTextItem);//restore every loop to prepare for change
            if(input == "Select")
            {
                if(ui.Action == "Skills")
                {
                    SetScreen(1);
                } else if(ui.Action == "Inventory")
                {
                    Debug.LogWarning("No Inventory yet");
                    //SetScreen(2);
                } else
                {
                    if((battleHandler.GetPlayer().FindAbility(ui.Action).Cost <= battleHandler.GetPlayer().Stats.Mana))
                    {


                        if(battleHandler.GetPlayer().FindAbility(ui.Action).AreaOfEffect || battleHandler.GetPlayer().FindAbility(ui.Action).IsRandom)
                        {
                            Select();
                        } else
                        {
                            target = Instantiate(_targetPrefab, new Vector2(0, 0), Quaternion.identity);
                            target.Action = ui.Action;
                            isAreaofEffect = false;
                        }

                        isInTarget = true;
                    }
                }
            }
            if(input == "Back")
            {
                if(ui.CurrentScreen != 0)
                {
                    SetScreen(0);
                } else
                {
                    uiHandler.Destroy();
                    battleHandler.PreviousMember();
                    Destroy(gameObject);
                }          
            }
            if(input == "Up")
            {
                ui.UpSelect();
            }
            if(input == "Down")
            {
                ui.DownSelect();
            }
            if(input == "Left")
            {
                ui.FirstSelect();
            }
            if(input == "Right")
            {
                ui.LastSelect();
            }
            UpdateOptions();
            UpdateDescription();
            Highlight(ui.CurrentTextItem);//highlight at the end of every update to assure a highlighted option
        } else
        {
            target.HandleInput(input);
        }
    }
   
    private void Highlight(int pos)
    {
        //highlights an item at position
        _selections[pos].fontSize = 16;
        _selections[pos].fontStyle = FontStyle.Bold;
    }
    private void Restore(int pos)
    {
        //unhighlights an item at position
        _selections[pos].fontSize = 14;
        _selections[pos].fontStyle = FontStyle.Normal;
    }

    public void Select(int target) {
        
        battleHandler.Select(target, ui.Action);
        uiHandler.Destroy();
        Destroy(gameObject);
    }

    public void Select()
    {
        AbilityObject ability = battleHandler.GetPlayer().FindAbility(ui.Action);
        if(ability.IsRandom)
        {
            for(int i = 0; i < ability.Targets; i++)
            {
                if(ability.TargetEnemy)
                {
                    battleHandler.Select(Random.Range(0, battleHandler.EnemyCount()), ui.Action);
                } else
                {
                    battleHandler.Select(Random.Range(battleHandler.EnemyCount(), battleHandler.EnemyCount() + battleHandler.PlayerCount()), ui.Action);
                }
            }
        }else if(ability.TargetEnemy)
        {
            for(int i = 0; i < battleHandler.EnemyCount(); i++)
            {
                battleHandler.Select(i, ui.Action);
            }
        } else
        {
            for(int i = 0; i < battleHandler.PlayerCount(); i++)
            {
                battleHandler.Select(i+battleHandler.EnemyCount(), ui.Action);
            }
        }
        uiHandler.Destroy();
        Destroy(gameObject);
    }
}
