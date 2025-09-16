using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Esta clase se encarga de instanciar una comida en un punto random del mapa.
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
        //No hay comida en el mapa? instanciamos una
        if (currentFood == null)
        {
            currentFood= DeployFood();
            Food foodComp = currentFood.GetComponent<Food>();
            //le damos la referencia de la comida al game manager para que este pueda darle su ubicación a los boids
            GameManager.instance.availableFood = foodComp;
        }
    }
    //Este metodo es similar al de adjustBounds, pero se encarga de calcular el rectangulo del mapa para no instanciar comida fuera     
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
