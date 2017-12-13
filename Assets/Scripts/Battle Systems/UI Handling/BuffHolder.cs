using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//enum for the severity of a buff
public enum Severity
{
    Up,Down,DoubleDown, DoubleUp
}
/********************************************
 * Buffholder class
 * 
 * This is all about handeling wich buffs to turn on and off and when
 * 
 * it also ties all the buffs neatly together for the battle entity to easily access
 */
public class BuffHolder : MonoBehaviour {

    [SerializeField]
    private GameObject[] prefabs;
    private Buff[] allBuffs;

    // binds all the buffs
    void Start () {
        //Gives the length to allBuffs for the current lengths of buffs we have
        allBuffs = new Buff[prefabs.Length];
        //For all of the serialized prefabs cycle through them
        for(int i = 0; i < prefabs.Length; i++)
        {
            //instantiate the buff and put it in all buffs
            allBuffs[i] = Instantiate(prefabs[i]).GetComponent<Buff>();
            //make it a child of buffholder
            allBuffs[i].transform.parent = transform;
            //Depending on type and severity give it a position this can be done in a mathmatical way later but for now this was the fastest way to put it in
            switch(allBuffs[i].BuffType)
            {
                case StatusOptions.ModifyAttack:
                    if(allBuffs[i].BuffSeverity == Severity.Up || allBuffs[i].BuffSeverity == Severity.DoubleUp)
                    {
                        allBuffs[i].transform.localPosition = new Vector3(.2f, -.0f, 0);
                    } else
                    {
                        allBuffs[i].transform.localPosition = new Vector3(.4f, -.0f, 0);
                    }
                    break;
                case StatusOptions.ModifyDefense:
                    if(allBuffs[i].BuffSeverity == Severity.Up || allBuffs[i].BuffSeverity == Severity.DoubleUp)
                    {
                        allBuffs[i].transform.localPosition = new Vector3(.2f, -.2f, 0);
                    } else
                    {
                        allBuffs[i].transform.localPosition = new Vector3(.4f, -.2f, 0);
                    }
                    break;
                case StatusOptions.ModifyIntelligence:
                    if(allBuffs[i].BuffSeverity == Severity.Up || allBuffs[i].BuffSeverity == Severity.DoubleUp)
                    {
                        allBuffs[i].transform.localPosition = new Vector3(.2f, -.4f, 0);
                    } else
                    {
                        allBuffs[i].transform.localPosition = new Vector3(.4f, -.4f, 0);
                    }
                    break;
                case StatusOptions.ModifyMagicResist:
                    if(allBuffs[i].BuffSeverity == Severity.Up || allBuffs[i].BuffSeverity == Severity.DoubleUp)
                    {
                        allBuffs[i].transform.localPosition = new Vector3(.2f, -.6f, 0);
                    } else
                    {
                        allBuffs[i].transform.localPosition = new Vector3(.4f, -.6f, 0);
                    }
                    break;
                case StatusOptions.ModifySpeed:
                    if(allBuffs[i].BuffSeverity == Severity.Up || allBuffs[i].BuffSeverity == Severity.DoubleUp)
                    {
                        allBuffs[i].transform.localPosition = new Vector3(.2f, -.8f, 0);
                    } else
                    {
                        allBuffs[i].transform.localPosition = new Vector3(.4f, -.8f, 0);
                    }
                    break;
                default:
                    Debug.LogWarning("BuffHolder: Buff type of buff in slot "+i+" not recognized");
                    break;
            }
            
        }

    }

    //Pass in a type of buff and severity and then activate it
    public void AddBuff(StatusOptions effect, Severity severity)
    {
        for(int i = 0; i < allBuffs.Length; i++)
        {
            if(allBuffs[i].BuffType == effect && allBuffs[i].BuffSeverity == severity)
            {
                allBuffs[i].Activate();
            }
        }
    }
    //Pass in a type of buff and severity and then deactivate it
    public void RemoveBuff(StatusOptions effect, Severity severity ){
        for(int i = 0; i < allBuffs.Length; i++)
        {
            if(allBuffs[i].BuffType == effect && allBuffs[i].BuffSeverity == severity)
            {
                allBuffs[i].Deactivate();
            }
        }
    }
    
}
