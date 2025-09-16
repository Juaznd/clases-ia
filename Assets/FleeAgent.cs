using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeAgent : SteeringAgent
{
    [SerializeField] Transform targetAgent;

    void Update()
    {
        //AddForce(Flee(targetAgent.position));

        Move();
    }
}
