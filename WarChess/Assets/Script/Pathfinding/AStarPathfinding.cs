using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class AStarPathFinding : MonoBehaviour
{
    public List<GridPosition> FindPath(GridPosition startGridPos, GridPosition endGridPos)
    {
        List<PathNode> openList = new List<PathNode>();
        List<PathNode> closedList = new List<PathNode>();

        PathNode startNode = GridLevel.Instance.GetPathNode(startGridPos);
        PathNode endNode = GridLevel.Instance.GetPathNode(endGridPos);

        openList.Add(startNode);

        startNode.SetGCost(0);
        startNode.SetHCost(GetDistance(startGridPos, endGridPos));
        startNode.CalcuateFCost();

        while(openList.Count > 0)
        {
            PathNode currentNode = GetMinFCostNode(openList);

            if(currentNode == endNode)
            {
                return GetPath(currentNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach(PathNode neighbourNode in GetNeighbourNodes(currentNode))
            {
                if (closedList.Contains(neighbourNode))
                {
                    continue;
                }

                int cost = currentNode.GetGCost() + GetDistance(currentNode.GetGridPosition(), neighbourNode.GetGridPosition());
                if (cost < neighbourNode.GetGCost())
                {
                    neighbourNode.SetPreviousNode(neighbourNode);
                    neighbourNode.SetGCost(cost);
                    neighbourNode.SetHCost(GetDistance(neighbourNode.GetGridPosition(), endGridPos));
                    neighbourNode.CalcuateFCost();

                    if (!openList.Contains(neighbourNode))
                    {
                        openList.Add(neighbourNode);
                    }
                }
            }
        }
        return null;
    }
    
    private int GetDistance(GridPosition startGridPos, GridPosition endGridPos)
    {
        int x = startGridPos.x - endGridPos.x;
        int z = startGridPos.z - endGridPos.z;
        float dis = Mathf.Sqrt((float)(x * x + z * z));
        return (int)dis;
    }

    private PathNode GetMinFCostNode(List<PathNode> pathNodes)
    {
        PathNode minFCostNode = pathNodes[0];
        for(int i = 0; i < pathNodes.Count; i++)
        {
            if (pathNodes[i].GetFCost() < minFCostNode.GetFCost())
            {
                minFCostNode = pathNodes[i];
            }
        } 
        return minFCostNode;
    }

    private List<PathNode> GetNeighbourNodes(PathNode currentNode) 
    {
        List<PathNode> neighbourNodes = new List<PathNode>();

        GridPosition gridPos = currentNode.GetGridPosition();
        for(int i = -1; i < 2; i++)
        {
            for(int j = -1; j < 2; j++)
            {
                if(i == 0 && j == 0)
                {
                    continue;
                }
                GridPosition temPos = new GridPosition(gridPos.x + i, gridPos.z + j);
                if (GridLevel.Instance.IsValidRegion(temPos))
                {
                    neighbourNodes.Add(GridLevel.Instance.GetPathNode(temPos));
                }
            }
        }
        return neighbourNodes;
    }

    private List<GridPosition> GetPath(PathNode node)
    {
        List<GridPosition> path = new List<GridPosition>();
        path.Add(node.GetGridPosition());
        PathNode currentNode = node;

        while (node.GetPreviousNode() != null)
        {
            currentNode = currentNode.GetPreviousNode();
            path.Add(currentNode.GetGridPosition());
        }

        path.Reverse();

        return path;
    }
}
