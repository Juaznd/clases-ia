using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionNode : Node
{
    public ActionType actionType;
    public override void Execute(Boid _boid)
    {
        switch (actionType) 
        { 
           case ActionType.Comer:
                Debug.Log("deberia ir por la comida");
                _boid.getFood(GameManager.instance.availableFood);
                break;
            case ActionType.Evadir:
                Debug.Log("Escapando del cazador");
                _boid.getAway(GameManager.instance._hunter);
                break;
            case ActionType.Flock:
                Debug.Log("haciendo flocking");
                _boid.Flocking();
                break;
            case ActionType.Deambular:
                Debug.Log("boludeando");
                _boid.wander();
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