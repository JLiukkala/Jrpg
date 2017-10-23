using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetUI : MonoBehaviour {

    private string action;
    private int players;
    private int enemies;
    private int side = 0;
    private int selection = 0;
    private float x, y;
    private ActiveUI ui;

    private void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player").Length;
        enemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        ui = (ActiveUI)GameObject.FindWithTag("Active UI").GetComponent(typeof(ActiveUI));
        UpdatePosition();
    }


    public string Action
    {
        get {
            return action;
        }

        set {
            action = value;
        }
    }
    private void UpdatePosition() {
        if(side==0)
        {
            x = ((selection + 1) * (16 / (enemies+1))) - (16 / 2);
            y =  4.6f;
        } else
        {
            x = ((selection +1) * (16 / (players + 1))) - (16 / 2);
            y = 0;
        }
        transform.position = new Vector3(x,y,0);
    }

    public void HandleInput(string input)
    {
        if(input == "Select")
        {
            Select();
        }
        if(input == "Back")
        {
            Destroy(gameObject);          
        }
        if(input == "Up")
        {
            if(side == 0)
            {

            } else
            {
                side = 0;
                selection = 0; //1 + ((enemies - 1) / (players - 1)) * (selection - 1);
            }
        }
        if(input == "Down")
        {
            if(side == 1)
            {

            } else
            {
                side = 1;
                selection = 0; //1 + ((enemies - 1) / (players - 1)) * (selection - 1);
            }
        }
        if(input == "Left")
        {
            if(side == 0)
            {
                if(selection == 0)
                {
                    selection = enemies - 1;
                } else
                {
                    selection -= 1;
                }
            } else
            {
                if(selection == 0)
                {
                    selection = players - 1;
                } else
                {
                    selection -= 1;
                }

            }
        }
        if(input == "Right")
        {
            if(side == 0)
            {
                if(selection == enemies - 1)
                {
                    selection = 0;
                } else
                {
                    selection += 1;
                }
            } else
            {
                if(selection == players - 1)
                {
                    selection = 0;
                } else
                {
                    selection += 1;
                }

            }
        }
        UpdatePosition();
    }

    private void Select() {
        if(side == 0)
        {
            ui.Select(selection);
        } else
        {
            ui.Select(selection+enemies);
        }
        Destroy(gameObject);
    }
    
}
