using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Patrolstate : State
{
    public override void OnEnter()
    {
        //Debug.Log("Entro al patrol");
    }

    public override void OnExit()
    {
        //Debug.Log("Salgo del Patrol");
    }

    public override void OnUpdate()
    {
        EnergyCheck();
        Hunt();
        patrol();
        //Debug.Log("Estoy en el Patrol");
    }

    public void patrol()
    {
        //Mientras patrulla usamos Arrive, para que alcance al objetivo de manera un poco más suave y no se pase mucho de largo
        fsm.hunter.AddForce(fsm.hunter.Arrive(GameManager.instance.patrolPoints[fsm.hunter.currentPatrolPoint].transform.position));
        //Acá revisamos a que distancia estamos del siguiente waypoint, si estamos cerca o encima...
        if(Vector3.Distance(fsm.hunter.transform.position, GameManager.instance.patrolPoints[fsm.hunter.currentPatrolPoint].transform.position)<5f)
        {
            //Le decimos que ahora vaya al siguiente waypoint de la lista
            if(fsm.hunter.currentPatrolPoint== GameManager.instance.patrolPoints.Count-1)
            {
                //cuando llegamos al ultimo waypoint volvemos al principio de la lista y que empiece de nuevo
                fsm.hunter.currentPatrolPoint = 0;
                return;
            }
            else
            {
                fsm.hunter.currentPatrolPoint++;
                return;
            }
        }
    }

    void Hunt()
    {
        //Si no tenemos una presa a la vista guardada en _currentPrey, consultamos al game manager para que nos diga si tenemos una en rango
        if (fsm.hunter._currentPrey == null) fsm.hunter._currentPrey = fsm.hunter.senseBoids(GameManager.instance.allagents);
        else
        {        
            //Una vez que tenemos una presa, revisamos que esté en rango de visión, si es así pasamos al state de hunt

            if (Vector3.Distance(fsm.hunter.transform.position, fsm.hunter._currentPrey.transform.position) < fsm.hunter.visionRange)
            {  
                fsm.ChangeState(PlayerState.Hunt);
            }

        }
    }

    //Si me quedo sin energia, vamos a idle para recargar
    void EnergyCheck()
    {
        if (fsm.hunter.energy == 0 || fsm.hunter.resting)
        {
            fsm.ChangeState(PlayerState.Idle);
        }
    }
}
