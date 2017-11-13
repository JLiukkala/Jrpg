using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour {

    [SerializeField, Tooltip("Passive ui prefab")]
    private PassiveUI _passiveUIPrefab;
    [SerializeField, Tooltip("active ui prefab")]
    private ActiveUI _activeUIPrefab;
    [SerializeField, Tooltip("Battle Handler Instance")]
    private BattleHandler _battleHandler;
    private int currentMember;
    private PassiveUI passiveUI;
    private ActiveUI activeUI;
    private bool uIHasObject = false;

    public bool UIHasObject {
        get {
            return passiveUI !=null || activeUI != null;
        }
        private set {
            uIHasObject = value;
        }
        
    }

    public void CreateUI(PlayerEntity player)
    {
        
        //activate active
        activeUI = Instantiate(_activeUIPrefab, new Vector2( 0, 0), Quaternion.identity);
        activeUI.Populate(player, this);
        //pass in uiobject to active
        UIHasObject = true;
    }
    public void CreateUI(string[] text)
    {
        //destroy active
        //activate passive
        //pass description into passive
        UIHasObject = true;
    }
    public void Destroy() {
        UIHasObject = false;
    }
    public void PassInput(string input)
    {
        //pass inout to active menu
        if(passiveUI != null)
        {
            //pass to passive ui
            passiveUI.HandleInput(input);
        } else if(activeUI != null)
        {
            activeUI.HandleInput(input);
        }
    }
    public bool IsTextScrolled()
    {
        //if passive is text scrolled
        return true;
        //else
        //false
    }
   
}
