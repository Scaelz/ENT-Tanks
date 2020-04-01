using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    Grid grid;
    [SerializeField] Transform start;
    [SerializeField] Transform end;
    List<PathFindingNode> finalPath;

    private void Start()
    {
        grid = GetComponent<Grid>();
    }

    private void Update()
    {
        FindPath(start.position, end.position);
        
    }

    public void FindPath(Vector3 a_start, Vector3 a_end)
    {
        PathFindingNode startNode = grid.WorldPositionToGridCoordinates(a_start);
        PathFindingNode endNode = grid.WorldPositionToGridCoordinates(a_end);

        List<PathFindingNode> openList = new List<PathFindingNode>();
        HashSet<PathFindingNode> closedSet = new HashSet<PathFindingNode>();

        openList.Add(startNode);

        while(openList.Count > 0)
        {
            PathFindingNode currentNode = openList[0];
            for (int i = 1; i < openList.Count; i++)
            {
                if(openList[i].F_cost < currentNode.F_cost || 
                    openList[i].F_cost == currentNode.F_cost &&  openList[i].G_cost < currentNode.G_cost)
                {
                    currentNode = openList[i];
                }
            }
            openList.Remove(currentNode);
            closedSet.Add(currentNode);

            if(currentNode == endNode)
            {
                GetFinalPath(startNode, currentNode);
            }

            foreach (PathFindingNode node in grid.GetNeighbours(currentNode))
            {
                if (!node.isObsticle || closedSet.Contains(node)) { continue; }
                int moveCost = currentNode.G_cost + GetManhattenCost(currentNode, node);
                if (moveCost < node.G_cost || !openList.Contains(node))
                {
                    node.SetG(moveCost);
                    node.SetH(GetManhattenCost(node, endNode));
                    node.SetPrevious(currentNode);
                }
                if (!openList.Contains(node))
                {
                    openList.Add(node);
                }
            }
        }
    }

    int GetManhattenCost(PathFindingNode nodeA, PathFindingNode nodeB)
    {
        int x = Mathf.Abs(nodeA.xGrid - nodeB.xGrid);
        int y = Mathf.Abs(nodeA.yGrid - nodeB.yGrid);
        return x + y;
    }

    List<PathFindingNode> GetFinalPath(PathFindingNode startNode, PathFindingNode endNode)
    {
        finalPath = new List<PathFindingNode>();
        PathFindingNode current = endNode;
        while(current != startNode)
        {
            finalPath.Add(current);
            current = current.PreviousNode;
        }
        finalPath.Reverse();
        grid.fullPath = finalPath;
        return finalPath;
    }
}
