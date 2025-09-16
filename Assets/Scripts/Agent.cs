using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] float _radius,_speed;

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, _target.position) < _radius)
        {
            //posicion final - posicion inicial
            Vector3 dir = _target.position - transform.position;
            transform.right=dir;

            transform.position += dir.normalized * Time.deltaTime * _speed;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

}
