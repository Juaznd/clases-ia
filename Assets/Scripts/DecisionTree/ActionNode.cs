using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionNode : Node
{
    public ActionType actionType;
    //Acá están las acciones que se realizan en caso de que el decision tree lo decida
    public override void Execute(Boid _boid)
    {
        switch (actionType) 
        { 
           case ActionType.Comer:
                _boid.nodeState = "comer";
                
                _boid.getFood(GameManager.instance.availableFood);
                break;
            case ActionType.Evadir:
                _boid.nodeState = "evadir";
                _boid.getAway(GameManager.instance._hunter);
                break;
            case ActionType.Flock:
                _boid.nodeState = "flock";
                _boid.Flocking();
                break;
            case ActionType.Deambular:
                _boid.nodeState = "wander";
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