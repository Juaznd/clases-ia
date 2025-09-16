using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntState : State
{
    public override void OnEnter()
    {
        //Debug.Log("Entro al Hunt");
    }

    public override void OnExit()
    {
        //Debug.Log("Salgo del Hunt");
    }

    public override void OnUpdate()
    {
        getPrey();
        //Debug.Log("Estoy en el Hunt");
    }

    public void getPrey()
    {
        if (fsm.hunter._currentPrey != null)
        {
            //if(Vector3.Distance(fsm.hunter.transform.position, fsm.hunter._currentPrey.transform.position)< fsm.hunter.separationRadius)
            //{
            //    fsm.hunter.AddForce(fsm.hunter.Arrive(fsm.hunter._currentPrey.transform.position));
            //}
            //else
            //{

            //}
            fsm.hunter.AddForce(fsm.hunter.Persuit(fsm.hunter._currentPrey));

        }
    }

}
