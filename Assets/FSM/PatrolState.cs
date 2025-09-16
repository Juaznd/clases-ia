using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrolstate : State
{
    public override void OnEnter()
    {
        Debug.Log("Entro al patrol");
    }

    public override void OnExit()
    {
        Debug.Log("Salgo del Patrol");
    }

    public override void OnUpdate()
    {
        patrol();
        Debug.Log("Estoy en el Patrol");
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
            }
            else
            {
                fsm.hunter.currentPatrolPoint++;
            }
        }
    }
}
