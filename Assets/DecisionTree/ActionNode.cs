using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionNode : Node
{
    public ActionType actionType;
    public override void Execute(Police npc)
    {
        switch (actionType) 
        { 
           case ActionType.Comer:
                Debug.Log("correr hacia el ladron");
                break;
           case ActionType.Evadir:
                Debug.Log("seguir patrullando");
                break;
           case ActionType.Flock:
                Debug.Log("arrestamos al ladron");
                break;
           case ActionType.Deambular:
                Debug.Log("pedir refuerzos");
                break;
           default:
                break;
        }

    }

}
public enum ActionType
{
    Comer,Evadir,Flock,Deambular
}