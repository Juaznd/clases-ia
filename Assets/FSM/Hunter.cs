using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

//Tanto Hunter como Boid heredan de SteeringAgent, lo que les permite poder moverse con seek, persuit, evade, etc.
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
    public Animator anim;
        
    // Start is called before the first frame update

    public float visionRange
    {
        get { return _radius; }
    }

    void Start()
    {
        //SeparationRadius se usa para ver si el Boid está al alcance de la mano
        separationRadius=_separationRadius;
        //Inicializamos state machine
        fsm = new FiniteStateMachine(this);
        fsm.AddState(PlayerState.Idle,new IdleState());
        fsm.AddState(PlayerState.Patrol,new Patrolstate());
        fsm.AddState(PlayerState.Hunt,new HuntState());
        //Arranca el idle, y este mismo delegará luego a patrol
        fsm.ChangeState(PlayerState.Idle);
        lastPosition=transform.position;
        energy = maxEnergy;
    }

    // Update is called once per frame
    void Update()
    {        
        fsm.Update();

        //Consume energia si detecta que se movió
        consumeEnergyByMoving();

        //si estoy en idle no me puedo mover
        if (fsm.currentPS != PlayerState.Idle)
        {
            Move();
        }

        AdjustBounds();
    }

    //Detecta si los boids están en rango de visión, si encuentra alguno lo devuelve para tenerlo como target
    public SteeringAgent senseBoids(List<SteeringAgent> boids)
    {
        SteeringAgent _BoidFound = null; 

        foreach (SteeringAgent boid in boids)
        {
            if(boid==null) continue;
            if (Vector3.Distance(transform.position, boid.transform.position) < visionRange)_BoidFound = boid;            
        } 
        return _BoidFound; 
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
        //guardamos la ultima posición, si no no sabemos si se movió
        lastPosition = transform.position;
    }

    //Actualiza la barrita de stamina
    public void updateEnergybar()
    {
        if (energy >= 0)
        {
            energyBar.fillAmount = energy / maxEnergy;

        }
    }

    //Lo mismo que los boids, para que no se vaya fuera del rectangulo de la camara
    public void AdjustBounds()
    {
        transform.position = GameManager.instance.AdjustToBounds(transform.position);

    }
}
