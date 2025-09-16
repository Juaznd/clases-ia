using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public override void OnEnter()
    {
        //Debug.Log("Entro al idle");
        fsm.hunter.resting = true;
    }

    public override void OnExit()
    {
        //Debug.Log("Salgo del idle");
    }
    //el cazador para de moverse y recarga energias hasta que esté full
    public override void OnUpdate()
    {
        fsm.hunter.Seek(Vector3.zero,0);
        fsm.hunter.regenEnergy();
        //Debug.Log("Estoy en el idle");
    }

}
