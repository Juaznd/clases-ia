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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("available food name "+availableFood.name);
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

    public void consumeBoid(SteeringAgent boid)
    {
        if(boid==null)return;
        allagents.Remove(boid);
        instantiateBoid();
    }
    public void instantiateBoid()
    {
        SteeringAgent boid = null;

        GameObject boidGO = Instantiate(boidPrefab,new Vector3(0,0,0),Quaternion.identity);

        boid= boidGO.GetComponent<SteeringAgent>();

        allagents.Add(boid);
    }
}
