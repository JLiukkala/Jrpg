using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum MenuState
{
    Ability, Target, FlavorText,Empty
}
//work in pixel space x
//gameObject.GetComponent<RectTransform>().rect.width +-pixel, or / by divisions 
//work in pixel space y
//gameObject.GetComponent<RectTransform>().rect.height +-pixel, or / by divisions 

//work in world space x starting in center
//((Camera.main.orthographicSize * Camera.main.aspect +- worldspacemodifier) * gameObject.GetComponent<RectTransform>().rect.height) / (Camera.main.orthographicSize * 2)
//work in world space y starting in center
//((Camera.main.orthographicSize +- worldspacemodifier) * gameObject.GetComponent<RectTransform>().rect.height) / (Camera.main.orthographicSize * 2)
public class UIHandler : MonoBehaviour {
    public AudioSource select;
    public BattleHandler _battleHandler;
    private PlayerEntity[] partyMembers;
    private EnemyEntity[] enemyMembers;
    //Splash Prefabs
    public TextEffect _splashText;

    private float screenRatio;
    private float scale;

    private int target;
    private string action;

    public GameObject _instructions;
    private GameObject instructions;
    private int currentSelection = 0;
    private MenuState currentState = MenuState.Empty;
    //Ability Screen
    public UISkillObject _uiSkill;
    private UISkillObject[] skills = new UISkillObject[4];
    public UINamePlate _namePlate;
    private UINamePlate namePlate;
    public UIStatusPlate _statusPlate;
    private UIStatusPlate statusPlate;

    private int currentMember;
    //Flavortext Screen

    //Selection Screen
    public UISKillNamePlate _uiSkillNamePlate;
    private UISKillNamePlate uiSkillNamePlate;
    //ui selection
    private int selectionsLength = 0;
    public UISelectionObject _uiSelection;
    private UISelectionObject[] Selections = new UISelectionObject[7];

    public int CurrentMember
    {
        get {
            return currentMember;
        }

        set {
            currentMember = value;
        }
    }

    public MenuState CurrentState
    {
        get {
            return currentState;
        }

        set {
            currentState = value;
        }
    }

    private void Start()
    {

        scale = gameObject.GetComponent<RectTransform>().lossyScale.x;
        screenRatio = gameObject.GetComponent<RectTransform>().rect.height / (Camera.main.orthographicSize * 2);
        InitializeNamePlate();
        InitializeSkillNamePlate();
        InitializeStatusPlate();
        InitializeSkills();
        InitializeSelections();
        InitializeInstructions();
    }

    private void InitializeNamePlate()
    {
        namePlate = Instantiate(_namePlate);
        namePlate.transform.parent = transform;
        namePlate.transform.localScale = new Vector3(1, 1, 1);
        namePlate.transform.localPosition = new Vector3(0, 0, 0);
        namePlate.gameObject.SetActive(false);
    }
    private void InitializeSkillNamePlate()
    {
        uiSkillNamePlate = Instantiate(_uiSkillNamePlate);
        uiSkillNamePlate.transform.parent = transform;
        uiSkillNamePlate.transform.localScale = new Vector3(1, 1, 1);
        uiSkillNamePlate.transform.localPosition = new Vector3(0, 0, 0);
        uiSkillNamePlate.gameObject.SetActive(false);
    }
    private void InitializeStatusPlate() {
        statusPlate = Instantiate(_statusPlate);
        statusPlate.transform.parent = transform;
        statusPlate.transform.localScale = new Vector3(1, 1, 1);
        statusPlate.transform.localPosition = new Vector3(0, 0, 0);
        statusPlate.gameObject.SetActive(false);
    }
    private void InitializeSkills() {
        for(int i = 0; i < skills.Length; i++)
        {
            skills[i] = Instantiate(_uiSkill);
            skills[i].transform.parent = transform;
            skills[i].transform.localScale = new Vector3(1, 1, 1);
            float x = (Camera.main.orthographicSize * Camera.main.aspect + 2f//gets center of game space then we add 1 unit
                + 3.5f * -Mathf.Sin(Mathf.PI * i + .5f * Mathf.PI))// depending on the sin graph wich will return 1 or -1 we either subtract or add our x from the center of the group
                * screenRatio;//the we multiply by our game to screen ratio
            float y = (Camera.main.orthographicSize - 3f
                + .80f * (1 / Mathf.Sin(Mathf.PI / 4)) * Mathf.Sin((Mathf.PI / 2) * i + (Mathf.PI / 4)))
                * screenRatio;
            skills[i].transform.position = new Vector2((x * scale), (y * scale));
            skills[i].gameObject.SetActive(false);
        }
    }
    private void InitializeSelections()
    {
        for(int i = 0; i < 7; i++)
        {
            Selections[i] = Instantiate(_uiSelection);
            Selections[i].transform.parent = transform;
            Selections[i].transform.localScale = new Vector3(1, 1, 1);
            float x = (Camera.main.orthographicSize * Camera.main.aspect + 2f
                + 3.5f * -Mathf.Sin(Mathf.PI * i + .5f * Mathf.PI))
                * screenRatio;//the we multiply by our game to screen ratio
            float y = (Camera.main.orthographicSize - 2.15f
                - .70f * Mathf.RoundToInt(.5f*i -.25f))
                * screenRatio;
            Selections[i].transform.position = new Vector2((x * scale), (y * scale));
            Selections[i].gameObject.SetActive(false);
        }
    }
    private void InitializeInstructions()
    {
        instructions = Instantiate(_instructions);
        instructions.transform.parent = transform;
        instructions.transform.localScale = new Vector3(1, 1, 1);
        instructions.transform.localPosition = new Vector3(0, 0, 0);
        instructions.gameObject.SetActive(false);
    }


    public void TransformState(MenuState newState)
    {
        //Disable old state
        switch(CurrentState)
        {
            case MenuState.Ability:
                namePlate.gameObject.SetActive(false);
                statusPlate.gameObject.SetActive(false);
                for(int i = 0; i < skills.Length; i++)
                {
                    skills[i].gameObject.SetActive(false);
                }
                break;
            case MenuState.Target:
                namePlate.gameObject.SetActive(false);
                statusPlate.gameObject.SetActive(false);
                uiSkillNamePlate.gameObject.SetActive(false);
                for(int i = 0; i < 7; i++)
                {
                    Selections[i].gameObject.SetActive(false);
                }
                break;
            case MenuState.FlavorText:
                //flavor(false);
                break;
            case MenuState.Empty:
                break;
            default:
                break;
        }
        //enable new state
        switch(newState)
        {
            case MenuState.Target:
                CurrentState = MenuState.Target;
                namePlate.gameObject.SetActive(true);
                statusPlate.gameObject.SetActive(true);
                uiSkillNamePlate.gameObject.SetActive(true);
                uiSkillNamePlate._name.text = action;
                for(int i = 0; i < 7; i++)
                {
                    Selections[i].gameObject.SetActive(true);
                    if(i < enemyMembers.Length)
                    {
                        Selections[i]._name.text = enemyMembers[i].Name;
                        Selections[i]._health.text = "HP: " + enemyMembers[i].Stats.Health;
                        
                    } else if(i < 4 + enemyMembers.Length)
                    {
                        Selections[i]._name.text = partyMembers[i- enemyMembers.Length].Name;
                        Selections[i]._health.text = "HP: " + partyMembers[i- enemyMembers.Length].Stats.Health;
                    } else
                    {
                        Selections[i]._name.text = "";
                        Selections[i]._health.text = "";
                    }
                    selectionsLength = 4 + enemyMembers.Length;
                    Normalize(i);
                }
                currentSelection = 0;
                Highlight(currentSelection);
                break;
            case MenuState.Ability:
                CurrentState = MenuState.Ability;
                namePlate.gameObject.SetActive(true);
                namePlate._name.text = partyMembers[CurrentMember].Name;
                statusPlate.gameObject.SetActive(true);
                statusPlate._health.text = "HP: " + partyMembers[CurrentMember].Stats.Health;
                statusPlate._mana.text = "MP: " + partyMembers[CurrentMember].Stats.Mana;
                for(int i = 0; i < skills.Length; i++)
                {
                    skills[i].gameObject.SetActive(true);
                    skills[i]._name.text = partyMembers[CurrentMember].Ability(i).Name;
                    skills[i]._cost.text = "MP: " + partyMembers[CurrentMember].Ability(i).Cost;
                    if (partyMembers[CurrentMember].Ability(i).Cost> partyMembers[CurrentMember].Stats.Mana)
                    {
                        skills[i]._cost.color = Color.red;
                    }
                    else
                    {
                        skills[i]._cost.color = Color.white;
                    }

                    skills[i]._description.text = partyMembers[CurrentMember].Ability(i).Description;
                    Normalize(i);

                }
                currentSelection = 0;
                Highlight(currentSelection);
                break;
            case MenuState.FlavorText:
                break;
            case MenuState.Empty:
                CurrentState = MenuState.Empty;
                break;
            default:
                break;
        }
    }

    //Does Splash text on the screen
    public void Splash(string text)
    {
        TextEffect current = Instantiate(_splashText);
        current.SetText(text);
        current.transform.parent = transform;
        float x = ((Camera.main.orthographicSize * Camera.main.aspect) * gameObject.GetComponent<RectTransform>().rect.height) / (Camera.main.orthographicSize * 2);
        float y = ((Camera.main.orthographicSize) * gameObject.GetComponent<RectTransform>().rect.height) / (Camera.main.orthographicSize * 2);
        current.transform.position = new Vector2(x*scale, y*scale);
    }

    public void Splash(string text, Vector3 pos)
    {
        TextEffect current = Instantiate(_splashText);
        current.SetText(text);
        current.transform.parent = transform;
        float x = ((Camera.main.orthographicSize * Camera.main.aspect+pos.x) * gameObject.GetComponent<RectTransform>().rect.height) / (Camera.main.orthographicSize * 2);
        float y = ((Camera.main.orthographicSize + pos.y) * gameObject.GetComponent<RectTransform>().rect.height) / (Camera.main.orthographicSize * 2);
        current.transform.position = new Vector2(x*scale, y*scale);
    }
    public void Splash(string text, Vector3 pos, Color c)
    {
        TextEffect current = Instantiate(_splashText);
        current.SetText(text);
        current.SetColor(c);
        current.transform.parent = transform;
        float x = ((Camera.main.orthographicSize * Camera.main.aspect + pos.x) * gameObject.GetComponent<RectTransform>().rect.height) / (Camera.main.orthographicSize * 2);
        float y = ((Camera.main.orthographicSize + pos.y) * gameObject.GetComponent<RectTransform>().rect.height) / (Camera.main.orthographicSize * 2);
        current.transform.position = new Vector2(x * scale, y * scale);
    }

    public void Instrctions(bool show)
    {
        instructions.SetActive(show);
    }

    public void SetMembers(PlayerEntity[] newMembers, EnemyEntity[] newEnemies)
    {
        partyMembers = newMembers;
        enemyMembers = newEnemies;
    }


    public void PassInput(string input)
    {
        switch (input)
        {
            case "Select":
                select.Play();
                if (CurrentState == MenuState.Ability)
                {
                    if (partyMembers[CurrentMember].Ability(currentSelection).Cost > partyMembers[CurrentMember].Stats.Mana)
                    {
                        Splash("Not Enough Mana");
                    }
                    else
                    {
                        action = skills[currentSelection]._name.text;
                        TransformState(MenuState.Target);
                    }

                } else if (CurrentState == MenuState.Target)
                {
                    if (currentSelection<enemyMembers.Length)
                    {
                        if (enemyMembers[currentSelection].Stats.Health == 0)
                        {
                            Splash("Can't target the dead");
                        }
                        else
                        {
                            Select();
                        }
                    }
                    else
                    {
                        if (partyMembers[currentSelection-enemyMembers.Length].Stats.Health == 0)
                        {
                            Splash("Can't target the dead");
                        }
                        else
                        {
                            Select();
                        }
                    }
                    
                    
                }
                break;
            case "Back":
                if (CurrentState == MenuState.Ability)
                {
                    _battleHandler.PreviousMember();
                    TransformState(MenuState.Ability);
                }
                else if (CurrentState == MenuState.Target)
                {
                    TransformState(MenuState.Ability);
                }
                break;
            case "Up":
                select.Play();
                if (CurrentState == MenuState.Ability)
                {
                    TransformSelect(-2);
                } else if(CurrentState == MenuState.Target)
                {
                    TransformSelect(-2);
                }
                break;
            case "Down":
                select.Play();
                if (CurrentState == MenuState.Ability)
                {
                    TransformSelect(2);

                } else if(CurrentState == MenuState.Target)
                {
                    TransformSelect(2);
                }
                break;
            case "Left":
                select.Play();
                if (CurrentState == MenuState.Ability)
                {
                    TransformSelect(-1);
                } else if(CurrentState == MenuState.Target)
                {
                    TransformSelect(-1);
                }
                break;
            case "Right":
                select.Play();
                if (CurrentState == MenuState.Ability)
                {
                    TransformSelect(1);
                } else if(CurrentState == MenuState.Target)
                {
                    TransformSelect(1);
                }
                break;
            default:
                break;
        }
    }

    
    private void TransformSelect(int transform) {
        Normalize(currentSelection);
        if(CurrentState == MenuState.Ability)
        {
            if(currentSelection + transform >= 0 && currentSelection + transform < 4)
            {
                currentSelection += transform;
            }
        } else if(CurrentState == MenuState.Target)
        {
            if(currentSelection + transform >= 0 && currentSelection + transform < selectionsLength)
            {
                currentSelection += transform;
            }
        }
        Highlight(currentSelection);
        
        
    }

    private void Highlight(int index) {
        if(CurrentState == MenuState.Ability)
        {
            skills[index].Highlight();
        } else if(CurrentState == MenuState.Target)
        {
            Selections[index].Highlight();
        }
    }
    private void Normalize(int index) {
        if(CurrentState == MenuState.Ability)
        {
            skills[index].Normalize();
        } else if(CurrentState == MenuState.Target)
        {
            Selections[index].Normalize();
        }
    }

    private void Select()
    {
        if (currentSelection< enemyMembers.Length)
        {
            _battleHandler.Select(currentSelection, action,true);
        }
        else
        {
            _battleHandler.Select(currentSelection- enemyMembers.Length, action,false);
        }
        TransformState(MenuState.Empty);
    }
}

