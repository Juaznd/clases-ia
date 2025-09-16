using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PersuitAgent : SteeringAgent
{
    [SerializeField] SteeringAgent targetAgent;
    // Update is called once per frame
    void Update()
    {
        if (!HastToUseObstacleAvoidance())
        {
            AddForce(Persuit(targetAgent));
        }

        Move();
    }
}
