using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : SteeringAgent,IEdible
{
    [SerializeField] float _alignmentWeight;
    [SerializeField] float _separationWeight;
    [SerializeField] float _cohesionWeight;
    [SerializeField] Node decisionTree;
    public Animator anim;
    public float visionRange
    {
        get { return _radius; }
    }
    // Start is called before the first frame update
    void Start()
    {
        anim.SetTrigger("summon");
        wander();
        decisionTree=GameManager.instance.decisionTree;
        GameManager.instance.allagents.Add(this);
    }

    //En update el boid consulta cada frame al decision tree, utiliza Move() para poder moverse y consulta a AdjustBounds para evitar irse del mapa
    void Update()
    {
        decisionTree.Execute(this);
        //Flocking();
        Move();
        AdjustBounds();
    }
    public void Flocking()
    {
        List<SteeringAgent> agentlist = GameManager.instance.allagents;
        AddForce(Alignment(agentlist)*_alignmentWeight);
        AddForce(Separation(agentlist)*_separationWeight);
        AddForce(Cohesion(agentlist)*_cohesionWeight);
    }
    public void AdjustBounds()
    {
        transform.position = GameManager.instance.AdjustToBounds(transform.position);

    }
    //Cuando hay comida en rango, el boid usa este metodo para alcanzarlo y comerlo
    public void getFood(Food targetFood)
    {
        if (targetFood == null) return;
        Flocking();
        AddForce(Arrive(targetFood.transform.position));
        if (Vector3.Distance(transform.position, targetFood.transform.position) < 1.2f)
        {
            targetFood.GetComponent<Food>().eaten();
            anim.SetTrigger("eat");
            Debug.Log(gameObject.name+ "ejecutó eaten");
        }
    }
    //Cuando hay un cazador en rango el boid intenta evadirlo con este metodo
    public void getAway(SteeringAgent hunter)
    {
        if (hunter == null) return;
        AddForce(Evade(hunter));
    }
    //Wander significa merodear, lo utiliza el boid para moverse sin rumbo en caso de que no haya comida, ni cazador ni otros boids para hacer flocking
    public void wander()
    {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);

        Vector3 dir = new Vector3(x, y);

        _velocity = dir.normalized * _maxForce;
    }
    //este metodo maneja lo que sucede cuando el cazador se come al boid. Spoiler, le avisa al game manager para que borre la referencia y cree uno nuevo, y elimina el objeto de la escena
    public void eaten()
    {
        if (this == null) return;
        GameManager.instance.consumeBoid(this);
        Destroy(gameObject);
    }
}
