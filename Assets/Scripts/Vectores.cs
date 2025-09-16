using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vectores : MonoBehaviour
{
    Vector2 vec1 = new Vector2(2,1);
    //pitagoras
    // hipotenusa al cuadrado = cateto 1 al cuadrado + cateto 2 al cuadrado
    // Start is called before the first frame update
    Vector2 vec2 = new Vector2(5,4);

    int radio = 5;
    void Start()
    {
        //con esto podemos calcular la longitud de un vector
        float m1 = Mathf.Sqrt((vec1.x * vec1.x) + (vec1.y * vec1.y));
        Debug.Log(m1);
        //con esto tambien, es lo mismo
        float m2 =vec1.magnitude;
        Debug.Log(m2);
        //Devuelve la hipotenusa al cuadrado
        float m3 = vec1.sqrMagnitude;
        Debug.Log(m3);

        if (Vector2.Distance(vec1, vec2) < radio)
        {

        }

        //Estos dos hacen lo mismo, los dos calculan distancia
        //pero el de abajo está más optimizado

        var diferencia = vec1 - vec2;
        if (diferencia.sqrMagnitude < radio * radio)
        { 
            
        }
        Debug.Log(vec1.normalized);
        vec1.Normalize();
    }

}
