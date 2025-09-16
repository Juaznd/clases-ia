using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine
{
    Dictionary<PlayerState, State> _allStates = new Dictionary<PlayerState, State>();
    public State _currentState;
    public SteeringAgent steeringAgent;
    public Hunter hunter;
    public PlayerState currentPS;
    public void Update()
    {
        _currentState?.OnUpdate();
    }
    public void AddState(PlayerState name, State state)
    {
        if (!_allStates.ContainsKey(name))
        {
            _allStates.Add(name, state);
            state.fsm = this;
        }
        else
        {
            _allStates[name] = state;
        }
    }

    public void ChangeState(PlayerState name)
    {
        if (currentPS == name) return;
        _currentState?.OnExit();
        if(_allStates.ContainsKey(name))_currentState = _allStates[name];
        _currentState.OnEnter();
        currentPS = name;
        hunter.stateText.text=currentPS.ToString();
    }
}

public enum PlayerState
{
    Idle, Walk, Patrol, Hunt
}