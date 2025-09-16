using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Food : MonoBehaviour,IEdible
{
    public void eaten()
    {
        if (GameManager.instance.availableFood == this)
        {
            GameManager.instance.availableFood = null;
            Debug.Log(gameObject.name+" ejecuto eaten");
        }
        Destroy(gameObject);
    }
}
