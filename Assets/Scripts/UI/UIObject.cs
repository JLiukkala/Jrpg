using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIObject {
    List<string[]>[] UI = new List<string[]>[3];
    private int currentSelection = 0;
    private int currentTextItem = 0;
    private int currentScreen = 0;

    //Initializer creates menu structure
    public UIObject(PlayerEntity player)
    {
        UI[0] = new List<string[]>();
        UI[1] = new List<string[]>();
        UI[2] = new List<string[]>();
        UI[0].Add(new string[] { "Attack", "Attack a target" });
        UI[0].Add(new string[] { "Skills", "View your skills" });
        UI[0].Add(new string[] { "Bolster", "Raise defence for a turn" });
        UI[0].Add(new string[] { "Inventory", "Look at your inventory" });
        UI[0].Add(new string[] { "Flee", "Run away from your foes" });
        for(int i = 0; i < player.AbilitiesLength; i++)
        {
            UI[1].Add(new string[] { player.Ability(i).Name, player.Ability(i).Description });
        }
        //for inventory add objects
        //create heirarchy

    }
    public int MenuLength() {
        return UI[currentScreen].Count;
    }
    //Getters and Setters
    public int CurrentSelection
    {
        get {
            return currentSelection;
        }

        private set {
            currentSelection = value;
        }
    }
    public int CurrentTextItem
    {
        get {
            return currentTextItem;
        }

        private set {
            currentTextItem = value;
        }
    }
    public int CurrentScreen
    {
        get {
            return currentScreen;
        }

        private set {
            currentScreen = value;
        }
    }
    public string Action
    {
        get {
            return UI[CurrentScreen][CurrentSelection][0];
        }
    }


    public void SelectMenu(int menu)
    {
        CurrentScreen = menu;
        currentTextItem = 0;
        currentSelection = 0;
    }

    //For returning options text and description
    public string Option(int option) {
        if(CurrentSelection + option - CurrentTextItem < UI[CurrentScreen].Count)
        {
            return UI[CurrentScreen][CurrentSelection + option - CurrentTextItem][0];
        }
        return "";
        
    }
    public string Description()
    {
        return UI[CurrentScreen][CurrentSelection][1];
    }

    //Up down top bottom navigation for menu
    public void UpSelect() {
        if(currentSelection == 0)
        {
            LastSelect();
        } else //otherwise go up one
        {
            currentSelection -= 1;
            if(CurrentTextItem !=0)
            {
                CurrentTextItem -= 1;
            }
        }
    }
    public void DownSelect()
    {
        if(currentSelection == UI[CurrentScreen].Count-1)
        {
            FirstSelect();
        } else //otherwise go up one
        {
            currentSelection += 1;
            if(CurrentTextItem != 2)
            {
                CurrentTextItem += 1;
            }
        }
    }
    public void FirstSelect() {
        currentSelection = 0;
        CurrentTextItem = 0;
    }
    public void LastSelect()
    {
        currentSelection = UI[CurrentScreen].Count - 1;
        if(UI[CurrentScreen].Count < 3)
        {
            CurrentTextItem = UI[CurrentScreen].Count - 1;
        } else
        {
            CurrentTextItem = 2;
        }
        
    }
}
