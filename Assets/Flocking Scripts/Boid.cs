using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : SteeringAgent
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
        wander();
        decisionTree=GameManager.instance.decisionTree;
        GameManager.instance.allagents.Add(this);
    }

    // Update is called once per frame
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
    public void getFood(Food targetFood)
    {
        if (targetFood == null) return;
        AddForce(Arrive(targetFood.transform.position));
        if (Vector3.Distance(transform.position, targetFood.transform.position) < 1.2f)
        {
            targetFood.GetComponent<Food>().eaten();
            anim.SetTrigger("eat");
            Debug.Log(gameObject.name+ "ejecutó eaten");
        }
    }

    public void getAway(SteeringAgent hunter)
    {
        if (hunter == null) return;
        AddForce(Evade(hunter));
    }

    public void wander()
    {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);

        Vector3 dir = new Vector3(x, y);

        _velocity = dir.normalized * _maxForce;
    }
}
