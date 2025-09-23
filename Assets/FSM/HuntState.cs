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
        EnergyCheck();
        getPrey();
    }

    //Metodo para perseguir y comer a la presa
    public void getPrey()
    {
        //Mientras tengamos una presa guardada en memoria...
        if (fsm.hunter._currentPrey != null)
        {

            //Acá revisamos si el cazador está tocando a la presa, si es así se come a la presa
            if (Vector3.Distance(fsm.hunter.transform.position, fsm.hunter._currentPrey.transform.position) <= 1f)
            {
                //animación simple para feedback visual
                fsm.hunter.anim.SetTrigger("eatHunter");
                //El boid ejecuta eaten() que se encarga de borrar registro del mismo y avisar que murió
                fsm.hunter._currentPrey.GetComponent<Boid>().eaten();
                //Volvemos a patrullar, clave para que el estado no quede clavado
                backToPatrol();
                return;
            }

            //Revisamos si se encuentra en el rango del separationRadius, este radio lo usamos para saber si esta muy cerca. Si es así, aplicamos arrive para que el hunter no pase de largo
            if (Vector3.Distance(fsm.hunter.transform.position, fsm.hunter._currentPrey.transform.position) < fsm.hunter.separationRadius)
            {
                fsm.hunter.AddForce(fsm.hunter.Arrive(fsm.hunter._currentPrey.transform.position));
                return;
            }

            //Si la presa está en rango de visión nos acercamos con persuit
            if (Vector3.Distance(fsm.hunter.transform.position, fsm.hunter._currentPrey.transform.position) < fsm.hunter.visionRange)
            {
                fsm.hunter.AddForce(fsm.hunter.Persuit(fsm.hunter._currentPrey));
                return;

            }
            //Si la presa se fué del rango de visión, volvemos a patrullar
            if (Vector3.Distance(fsm.hunter.transform.position, fsm.hunter._currentPrey.transform.position) > fsm.hunter.visionRange)
            {
                fsm.hunter._currentPrey = null;
                backToPatrol();
            }

        }
    }

    //Este metodo lo agregué porque luego de comer un boid, el cazador quedaba en estado de hunt pero sin presa por un rato
    public void backToPatrol()
    {
        fsm.ChangeState(PlayerState.Patrol);
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
