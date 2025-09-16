using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    FiniteStateMachine fsm;
    

    // Start is called before the first frame update
    void Start()
    {
        fsm = new FiniteStateMachine();
        fsm.AddState(PlayerState.Idle,new IdleState());
        fsm.AddState(PlayerState.Walk,new WalkState());
        fsm.ChangeState(PlayerState.Idle);
    }

    // Update is called once per frame
    void Update()
    {
        fsm.Update();
        if (Input.GetMouseButtonDown(0)) { fsm.ChangeState(PlayerState.Walk); }

    }
}
