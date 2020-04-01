using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public List<PathFindingNode> fullPath;
    [SerializeField] Vector2 worldSize;
    [SerializeField] LayerMask obsticleLayer;
    [SerializeField] float radius;
    PathFindingNode[,] grid;
    [SerializeField] float Diameter { get => radius * 2; }
    [SerializeField] float distance;
    int xGridSize;
    int yGridSize;

    private void Awake()
    {
        xGridSize = Mathf.RoundToInt(worldSize.x / Diameter);
        yGridSize = Mathf.RoundToInt(worldSize.y / Diameter);
        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new PathFindingNode[xGridSize, yGridSize];
        Vector3 bottomnLeft = transform.position - transform.right * worldSize.x / 2 - transform.forward * worldSize.y / 2;
        for (int x = 0; x < xGridSize; x++)
        {
            for (int y = 0; y < yGridSize; y++)
            {
                Vector3 x_pos = transform.right * (x * Diameter + radius);
                Vector3 y_pos = transform.forward * (y * Diameter + radius);
                Vector3 worldPosition = bottomnLeft + x_pos + y_pos;
                bool isObsticle = true;
                if (Physics.CheckSphere(worldPosition, radius, obsticleLayer))
                {
                    isObsticle = !isObsticle;
                }
                grid[x, y] = new PathFindingNode(worldPosition, x, y, isObsticle);
            }
        }
    }

    public PathFindingNode WorldPositionToGridCoordinates(Vector3 position)
    {
        float point_x = (position.x + worldSize.x / 2) / worldSize.x;
        float point_y = (position.z + worldSize.y / 2) / worldSize.y;

        point_x = Mathf.Clamp01(point_x);
        point_y = Mathf.Clamp01(point_y);

        int x = Mathf.RoundToInt((xGridSize - 1) * point_x);
        int y = Mathf.RoundToInt((yGridSize - 1) * point_y);
        return grid[x, y];
    }

    public List<PathFindingNode> GetNeighbours(PathFindingNode node)
    {
        List<PathFindingNode> neigbours = new List<PathFindingNode>();
        int[,] coords =
        {
            { node.xGrid - 1, node.yGrid},
            { node.xGrid - 1, node.yGrid - 1},
            { node.xGrid - 1, node.yGrid + 1},
            { node.xGrid + 1, node.yGrid - 1},
            { node.xGrid + 1, node.yGrid + 1},
            { node.xGrid + 1, node.yGrid},
            { node.xGrid , node.yGrid + 1},
            { node.xGrid , node.yGrid - 1},
        };
        for (int i = 0; i < coords.GetLength(0); i++)
        {
            if (coords[i, 0] >= 0 && coords[i, 0] < xGridSize &&
                coords[i, 1] >= 0 && coords[i, 1] < yGridSize)
            {
                neigbours.Add(grid[coords[i, 0], coords[i, 1]]);
            }
        }
        return neigbours;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(worldSize.x , 1, worldSize.y));
        if(grid != null)
        {
            foreach (PathFindingNode node in grid)
            {
                if (node.isObsticle)
                {
                    Gizmos.color = Color.white;
                }
                else
                {
                    Gizmos.color = Color.red;
                }
                if (fullPath != null)
                {
                    if (fullPath.Contains(node))
                    {
                        Gizmos.color = Color.green;
                    }
                }
                Gizmos.DrawCube(node.Position, Vector3.one * (Diameter - distance));
            }
        }
    }
}
