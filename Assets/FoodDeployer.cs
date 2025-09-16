using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodDeployer : MonoBehaviour
{
    public GameObject foodPrefab;
    public GameObject currentFood;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentFood == null)
        {
            currentFood= DeployFood();
            Food foodComp = currentFood.GetComponent<Food>();
            GameManager.instance.availableFood = foodComp;
        }
        //else
        //{
        //    GameManager.instance.availableFood = null;
        //}
    }

    public GameObject DeployFood()
    {        
        float xPos = GameManager.instance._boundWidth*0.5f;
        float yPos = GameManager.instance._boundHeight * 0.5f;

        xPos = Random.Range(-xPos, xPos);
        yPos = Random.Range(-yPos, yPos);

        Vector3 center = new Vector3(0, 0, 0);
        Vector3 foodPosition = center+new Vector3(xPos, yPos, 0f);

        GameObject foodToDeploy = Instantiate(foodPrefab, foodPosition,Quaternion.identity);
        Debug.Log("Deploy foooood");
        return foodToDeploy;
    }
}
