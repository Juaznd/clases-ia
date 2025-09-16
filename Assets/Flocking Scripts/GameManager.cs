using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public float _boundWidth,_boundHeight;
    [SerializeField] public Hunter _hunter;
    public List<Transform> patrolPoints;
    public static GameManager instance;
    public List<SteeringAgent> allagents = new List<SteeringAgent>();
    public Food availableFood;
    public Node decisionTree;
    public GameObject boidPrefab;
    private void Awake()
    {
        
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public Vector3 AdjustToBounds(Vector3 pos)
    {
        float height = _boundHeight/2;
        float width = _boundWidth/2;

        if(pos.x < -width)pos.x = width;
        if(pos.x > width)pos.x = -width;
        if(pos.y < -height) pos.y = height;
        if (pos.y > height) pos.y = -height;
        return pos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(transform.position,new Vector3(_boundWidth, _boundHeight));
    }
    //Cuando un boid es comido, limpiamos la referencia y creamos uno nuevo
    public void consumeBoid(SteeringAgent boid)
    {
        if(boid==null)return;
        allagents.Remove(boid);
        instantiateBoid();
    }
    //el boid nuevo se instancia en el centro de la escena agregandolo a la lista de boids
    public void instantiateBoid()
    {
        SteeringAgent boid = null;

        GameObject boidGO = Instantiate(boidPrefab,new Vector3(0,0,0),Quaternion.identity);

        boid= boidGO.GetComponent<SteeringAgent>();

        allagents.Add(boid);
    }
}
