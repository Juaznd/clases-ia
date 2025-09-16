using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : SteeringAgent
{
    [SerializeField] float _alignmentWeight;
    [SerializeField] float _separationWeight;
    [SerializeField] float _cohesionWeight;
    // Start is called before the first frame update
    void Start()
    {
        float x = Random.Range(-1f,1f);
        float y = Random.Range(-1f, 1f);

        Vector3 dir = new Vector3(x,y);

        _velocity = dir.normalized*_maxForce;

        GameManager.instance.allagents.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        Flocking();
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
}
