using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringAgent : MonoBehaviour
{
    //[SerializeField] Transform _target;
    [SerializeField] protected float _maxSpeed, _radius,_separationRadius, _maxForce;
    protected Vector3 _velocity;
    [SerializeField] LayerMask _obstacles;
    // Update is called once per frame
    void Update()
    {

    }

    public void Move()
    {
        //AddForce(Arrive(_target.position));
        transform.position += _velocity * Time.deltaTime;
        transform.right = _velocity;
    }
    public Vector3 Persuit(SteeringAgent AgentPosition)
    {
        Vector3 futurePos = AgentPosition.transform.position + AgentPosition._velocity;
        Debug.DrawLine(transform.position, futurePos, Color.magenta);
        return Seek(futurePos);
    }
    public Vector3 Evade(SteeringAgent AgentPosition)
    {
        Vector3 evadeVector = Persuit(AgentPosition)*-1;

        return evadeVector;
    }
    public Vector3 ObstacleAvoidance()
    {
        Debug.DrawLine(transform.position-transform.up*0.5f,transform.position-transform.up*0.5f+transform.right*_radius);
        Debug.DrawLine(transform.position + transform.up * 0.5f, transform.position + transform.up * 0.5f + transform.right * _radius);

        if (Physics.Raycast(transform.position-transform.up*0.5f, transform.right, _radius, _obstacles))
        {
            Debug.DrawLine(transform.position, transform.position + transform.up);
            return Seek(transform.position+transform.up);
        }
        else if(Physics.Raycast(transform.position + transform.up * 0.5f, transform.right, _radius, _obstacles))
        {
            Debug.DrawLine(transform.position, transform.position - transform.up);

            return Seek(transform.position - transform.up);
        }

        return Vector3.zero;
    }

    public bool HastToUseObstacleAvoidance()
    {
        Vector3 avoidance = ObstacleAvoidance();
        AddForce(avoidance);

        return avoidance != Vector3.zero;
    }

    public Vector3 Alignment(List<SteeringAgent> boids)
    {
        Vector3 desired = Vector3.zero;
        int boidCount = 0;

        foreach (SteeringAgent bo in boids) 
        {
            if (bo == null) continue;
            if(Vector3.Distance(transform.position, bo.transform.position) > _radius)continue;
            desired += bo._velocity;
            boidCount++;
        }
        //desired = desired.normalized;
        desired = desired / boidCount;
        return CalculateSteering(desired.normalized * _maxSpeed);
    }

    public Vector3 Separation(List<SteeringAgent> boids)
    {
        Vector3 desired = Vector3.zero;

        foreach (SteeringAgent bo in boids)
        {
            if(bo==this||bo==null) continue;

            Vector3 dist = bo.transform.position - transform.position;

            if (dist.sqrMagnitude > _separationRadius*_separationRadius)continue;

            desired += dist;
        }

        if(desired==Vector3.zero) return Vector3.zero;

        desired *= -1;
        return CalculateSteering(desired.normalized * _maxSpeed);

    }

    public Vector3 Cohesion(List<SteeringAgent> boids)
    {
        Vector3 desired = Vector3.zero;
        int boidCount = 0;

        foreach (SteeringAgent boid in boids)
        {
            if (boid == this||boid==null) continue;

            if(Vector3.Distance(transform.position,boid.transform.position)>_radius) continue;

            desired += boid.transform.position;
            boidCount++;
        }

        if(desired==Vector3.zero) return Vector3.zero;

        desired /=boidCount;
        return Seek(desired);

    }

    public Vector3 CalculateSteering(Vector3 desired)
    {
        return Vector3.ClampMagnitude(desired-_velocity,_maxForce*Time.deltaTime);
    }

    public Vector3 Seek(Vector3 targetPosition)
    {
        return Seek(targetPosition,_maxSpeed);
    }
    public Vector3 Seek(Vector3 targetPosition, float speed)
    {
        Vector3 desired = targetPosition - transform.position;
        desired.Normalize();
        desired *= speed;

        Vector3 steering = desired - _velocity;
        steering = Vector3.ClampMagnitude(steering, _maxForce * Time.deltaTime);

        //AddForce(steering);

        return steering;
    }
    public Vector3 Arrive(Vector3 targetPosition)
    {
        float dist = Vector3.Distance(transform.position, targetPosition);

        if (dist< _radius) return Seek(targetPosition, (dist / _radius) * _maxSpeed);
        return Seek(targetPosition);
    }
    public Vector3 Flee(Vector3 targetPosition)
    {
        return Seek(targetPosition)*-1;
    }
    public void AddForce(Vector3 force)
    {
        _velocity = Vector3.ClampMagnitude(_velocity + force,_maxSpeed);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _radius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _separationRadius);
    }
}
