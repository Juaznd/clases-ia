using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hunter : SteeringAgent
{
    FiniteStateMachine fsm;
    public SteeringAgent _currentPrey;
    public float energy;
    public float maxEnergy=100;
    Vector3 lastPosition;
    public Image energyBar;
    public int currentPatrolPoint = 0;
    public bool resting=false;
    public float regenPorSegundo = 0.6f;
    public TextMeshProUGUI stateText;
    public float separationRadius;
    // Start is called before the first frame update

    public float visionRange
    {
        get { return _radius; }
    }

    void Start()
    {
        separationRadius=_separationRadius;
        fsm = new FiniteStateMachine();
        fsm.hunter = this;
        fsm.AddState(PlayerState.Idle,new IdleState());
        fsm.AddState(PlayerState.Patrol,new Patrolstate());
        fsm.AddState(PlayerState.Hunt,new HuntState());
        fsm.ChangeState(PlayerState.Idle);
        lastPosition=transform.position;
        energy = maxEnergy;
    }

    // Update is called once per frame
    void Update()
    {        
        fsm.Update();
        //Consume energia si detectata que se movió
        consumeEnergyByMoving();
        //Chequear si hay algún boid en el rango y tambien si ese boid se fue
        if (_currentPrey == null) _currentPrey = senseBoids(GameManager.instance.allagents);
        else
        {
            if (Vector3.Distance(transform.position, _currentPrey.transform.position) > visionRange)
            {
                //Debug.Log(_currentAgent.name+" escapó");
                _currentPrey = null;
                fsm.ChangeState(PlayerState.Patrol);
            }
            else
            {
                fsm.ChangeState(PlayerState.Hunt);
            }
        }
        if (energy == maxEnergy)
        {
            fsm.ChangeState(PlayerState.Patrol);
        }
        else if (energy == 0 || resting)
        {
            fsm.ChangeState(PlayerState.Idle);
        }
        if (fsm.currentPS != PlayerState.Idle)
        {
            Move();
        }
        AdjustBounds();
    }
    //Detecta si los boids están en rango de visión, si encuentra alguno lo devuelve para tenerlo como target 
    public SteeringAgent senseBoids(List<SteeringAgent> boids)
    {
        SteeringAgent best = null;
        float bestSqr = float.PositiveInfinity;

        foreach (var b in boids)
        {
            if (!b || b == this) continue;
            float sqr = (b.transform.position - transform.position).sqrMagnitude;
            if (sqr <= visionRange * visionRange && sqr < bestSqr)
            {
                bestSqr = sqr;
                best = b;
            }
        }
        return best;
    }

    //Detecta si el hunter se mueve, y si es así va consumiendo energia
    public void consumeEnergyByMoving()
    {
        if (Vector3.Distance(transform.position, lastPosition) > 0.001f)
        {
            
            energy -= 0.1f;
            if(energy < 0) energy = 0; 
            updateEnergybar();
        }
        lastPosition = transform.position;

    }
    //Actualiza la barrita de la interfaz
    public void updateEnergybar()
    {
        if (energy >= 0)
        {
            energyBar.fillAmount = energy / maxEnergy;

        }
    }

    public void regenEnergy()
    {
        Debug.Log("Entro a regen " + (energy < maxEnergy));
        if (energy < maxEnergy) 
        {
            energy += regenPorSegundo * Time.deltaTime;
            updateEnergybar();
        }
        else
        {
            fsm.ChangeState(PlayerState.Patrol);
            resting = false;
        }

    }
    public void AdjustBounds()
    {
        transform.position = GameManager.instance.AdjustToBounds(transform.position);

    }
}
