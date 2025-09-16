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
    //Metodo para perseguir y comer a la presa
    public void getPrey()
    {
        if (fsm.hunter._currentPrey != null)
        {
            //Revisamos si se encuentra en el rango del separationRadius, este radio lo usamos para saber si esta cerca. Si es as�, aplicamos arrive para que no pase de largo
            if(Vector3.Distance(fsm.hunter.transform.position, fsm.hunter._currentPrey.transform.position)< fsm.hunter.separationRadius)
            {
                fsm.hunter.AddForce(fsm.hunter.Arrive(fsm.hunter._currentPrey.transform.position));
            }
            //Ac� revisamos si el cazador est� tocando a la presa, si es as� se come a la presa
            if (Vector3.Distance(fsm.hunter.transform.position, fsm.hunter._currentPrey.transform.position) <= 1f)
            {
                fsm.hunter.anim.SetTrigger("eatHunter");
                fsm.hunter._currentPrey.GetComponent<Boid>().eaten();
                fsm.hunter.backToPatrol();
            }
            //Si la presa est� en rango nos acercamos con persuit
            else
            {
                fsm.hunter.AddForce(fsm.hunter.Persuit(fsm.hunter._currentPrey));

            }

        }
    }

}
