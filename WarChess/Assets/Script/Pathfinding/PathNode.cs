using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    private GridPosition gridPos;
    private int gCost;
    private int hCost;
    private int fCost;

    private PathNode previousNode;

    public PathNode(GridPosition gridPos)
    {
        this.gridPos = gridPos;
        SetGCost(int.MaxValue);
        SetHCost(0);
        CalcuateFCost();
        InitPreviousNode();
    }

    public override string ToString()
    {
        return gridPos.ToString();
    }

    public GridPosition GetGridPosition()
    {
        return gridPos;
    }
    public void SetGCost(int cost)
    {
        this.gCost = cost;
    }

    public int GetGCost()
    {
        return this.gCost;
    }

    public void SetHCost(int cost) 
    {
        this.hCost = cost;
    }

    public int GetHCost() 
    { 
        return hCost; 
    }

    public void CalcuateFCost()
    {
        fCost = hCost + gCost;
    }

    public int GetFCost()
    {
        return fCost;
    }

    private void InitPreviousNode()
    {
        previousNode = null;
    }

    public void SetPreviousNode(PathNode node)
    {
        previousNode = node;
    }

    public PathNode GetPreviousNode()
    {
        return previousNode;
    }
}
