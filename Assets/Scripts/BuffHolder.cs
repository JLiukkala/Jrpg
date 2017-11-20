using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Severity
{
    Up,Down,DoubleDown, DoubleUp
}
public class BuffHolder : MonoBehaviour {

    [SerializeField]
    private GameObject[] prefabs;
    private Buff[] allBuffs;
    private List<Buff> activeBuffs = new List<Buff>();
    private int[,] hasProcessed;
    public bool log = false;
    // Use this for initialization
    void Start () {
        hasProcessed = new int[5, 3] { {(int)StatusOptions.ModifyAttack,0,0 },
            { (int)StatusOptions.ModifyDefense, 0, 0 },
            { (int)StatusOptions.ModifyIntelligence, 0, 0 },
            { (int)StatusOptions.ModifyMagicResist, 0, 0 },
            { (int)StatusOptions.ModifySpeed, 0, 0 } };
        allBuffs = new Buff[prefabs.Length];
        for(int i = 0; i < prefabs.Length; i++)
        {
            allBuffs[i] = Instantiate(prefabs[i]).GetComponent<Buff>();
            allBuffs[i].transform.parent = transform;
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
                    break;
            }
            
        }

    }
	

    public void Add(StatusOptions effect, Severity severity) {
        
        Debug.Log(severity);
        int currentprocess;
        for(int i = 0; i < hasProcessed.GetLength(0); i++)
        {
            if(hasProcessed[i, 0] == (int)effect)
            {

                currentprocess = i;
                switch(severity)
                {
                    case Severity.Up:
                        if(hasProcessed[i, 1]>0)
                        {
                            return;
                        } else
                        {
                            hasProcessed[i, 1] = 1;
                        }
                        break;
                    case Severity.Down:
                        if(hasProcessed[i, 2] > 0)
                        {
                            return;
                        } else
                        {
                            hasProcessed[i, 2] = 1;
                        }
                        break;
                    case Severity.DoubleDown:
                        if(hasProcessed[i, 2] > 1)
                        {
                            return;
                        } else
                        {
                            hasProcessed[i, 2] = 2;
                        }
                        break;
                    case Severity.DoubleUp:
                        if(hasProcessed[i, 1] > 1)
                        {
                            return;
                        } else
                        {
                            hasProcessed[i, 1] = 2;
                        }
                        break;
                    default:
                        break;
                }
                Debug.Log(hasProcessed[i, 1]);
            }
        }
        for(int i = 0; i < allBuffs.Length; i++)
        {
            if(allBuffs[i].BuffType == effect && allBuffs[i].BuffSeverity == severity)
            {
                allBuffs[i].Activate();
                allBuffs[i].Timer++;
            }
        }
    }
     
    public void Clear() {

        
        for(int i = 0; i < allBuffs.Length; i++)
        {
            
            if(allBuffs[i].Timer <= 0)
            {
                allBuffs[i].Deactivate();
            }
            if(allBuffs[i].Active)
            {
                allBuffs[i].Timer -= 1;
            }
        }
        for(int i = 0; i < hasProcessed.GetLength(0); i++)
        {
            hasProcessed[i, 1] = 0;
            hasProcessed[i, 2] = 0;
        }

    }

}
