using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionNode : Node
{
    public QuestionType QuestionType;
    public Node trueNode, falseNode;
    //Acá están las preguntas que se hace el decision tree
    public override void Execute(Boid _boid)
    {
        switch (QuestionType)
        {
            //Hay comida cerca del boid?
            case QuestionType.Comida:

                Food food = GameManager.instance.availableFood;

                if (food == null)
                {                    
                    falseNode?.Execute(_boid);
                    return;
                }
                if (Vector3.Distance(_boid.transform.position, GameManager.instance.availableFood.transform.position)< _boid.visionRange)
                {
                    trueNode.Execute(_boid);
                }
                else
                {
                    falseNode.Execute(_boid);
                }
                break;
            //El cazador está cerca?
            case QuestionType.Cazador:
                if (Vector3.Distance(_boid.transform.position, GameManager.instance._hunter.transform.position) < _boid.visionRange)
                {
                    trueNode.Execute(_boid);
                }
                else
                {
                    falseNode.Execute(_boid);
                }
                break;
            //Hay otros boids cerca?
            case QuestionType.Boids:
                int boidCount = 0;
                foreach (SteeringAgent listedBoid in GameManager.instance.allagents)
                {
                    if (listedBoid == this||listedBoid==null) continue;

                    if (Vector3.Distance(_boid.transform.position, listedBoid.transform.position) > _boid.visionRange) continue;

                    boidCount++;
                }
                if (boidCount!=0)
                {
                    trueNode.Execute(_boid);
                }
                else
                {
                    falseNode.Execute(_boid);
                }
                break;
        }

    }
}
public enum QuestionType
{
    Comida, Cazador, Boids
}