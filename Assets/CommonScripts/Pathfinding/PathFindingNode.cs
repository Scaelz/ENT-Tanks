using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFindingNode
{
    public int xGrid { get; private set; }
    public int yGrid { get; private set; }
    public Vector3 Position { get; private set; }
    public bool isObsticle { get; private set; }
    public PathFindingNode PreviousNode { get; private set; }
    public int H_cost { get; private set; }
    public int G_cost { get; private set; }
    public int F_cost { get => H_cost + G_cost; }

    public void SetPrevious(PathFindingNode node)
    {
        PreviousNode = node;
    }

    public void SetG(int value)
    {
        G_cost = value;
    }

    public void SetH(int value)
    {
        H_cost = value;
    }

    public PathFindingNode(Vector3 aPosition, int x, int y, bool isObsticle)
    {
        Position = aPosition;
        this.isObsticle = isObsticle;
        xGrid = x;
        yGrid = y;
    }
}
