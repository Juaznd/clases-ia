using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class IdleState : State
{
    public override void OnEnter()
    {
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
        regenEnergy();
    }

    //Cuando entra a idle llama a este metodo para curarse
    public void regenEnergy()
    {
        //Tenemos menos energia que el maximo? Cargamos por tiempo
        if (fsm.hunter.energy < fsm.hunter.maxEnergy)
        {
            fsm.hunter.energy += fsm.hunter.regenPorSegundo * Time.deltaTime;
            fsm.hunter.updateEnergybar();
        }
        //Tengo igual o mas que el maximo? Cortamos y volvemos a patrullar
        else if(fsm.hunter.energy >= fsm.hunter.maxEnergy)
        {
            //Si se curó al maximo puede volver a patrullar
            fsm.hunter.energy = fsm.hunter.maxEnergy;
            fsm.ChangeState(PlayerState.Patrol);
            fsm.hunter.resting = false;
        }
    }

}
