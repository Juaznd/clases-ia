using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionNode : Node
{
    public QuestionType QuestionType;
    public Node trueNode, falseNode;
    public override void Execute(Police npc)
    {
        switch (QuestionType)
        {
            case QuestionType.Comida:
                if (Vector3.Distance(npc.transform.position, npc.Thief.transform.position)<npc.distance)
                {
                    trueNode.Execute(npc);
                }
                else
                {
                    falseNode.Execute(npc);
                }
                    break;
            case QuestionType.Cazador:
                if (npc.Thief.bArmed)
                {
                    trueNode.Execute(npc);
                }
                else
                {
                    falseNode.Execute(npc);
                }
                break;
            case QuestionType.Boids:
                if (npc.Thief.bRobbed)
                {
                    trueNode.Execute(npc);
                }
                else
                {
                    falseNode.Execute(npc);
                }
                break;
        }

    }
}
public enum QuestionType
{
    Comida, Cazador, Boids
}