using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Esta interfaz la usan la comida y los boids, ya que ambos pueden ser comidos pero con distintos resultados
public interface IEdible 
{
    public void eaten();

}
