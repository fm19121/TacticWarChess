using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class GridLevel : MonoBehaviour
{
    public static GridLevel Instance { get; private set; }

    private GridSystem gridSystem;

    [SerializeField] private int gridWidth;
    [SerializeField] private int gridHeight;
    [SerializeField] private int unitSize;

    private void Awake()
    {
        //gridSystem = new GridSystem(30, 26, 2);
        gridSystem = new GridSystem(gridWidth, gridHeight, unitSize);
        if (Instance != null)
        {
            Debug.LogError("More than one GridLevel");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    void Start()
    {

    }

    void Update()
    {
        
    }

    public void AddPawnAtGrid(GridPosition gridPos, Pawn pawn)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPos);
        gridObject.AddPawn(pawn);
    }

    public Pawn GetPawnAtGrid(GridPosition gridPos)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPos);
        return gridObject.GetPawn();
    }

    public void RemovePawnAtGrid(GridPosition gridPos)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPos);
        gridObject.RemovePawn();
    }

    public GridPosition GetGridPosition(Vector3 worldPos)
    {
        return gridSystem.GetGridPosition(worldPos);
    }

    public Vector3 GetWorldPosition(GridPosition gridPos)
    {
        return gridSystem.GetWorldPos(gridPos);
    }

    public int GetGridHeight()
    {
        return gridSystem.GetHeight();
    }

    public int GetGridWidth()
    {
        return gridSystem.GetWidth();
    }

    public bool IsValidRegion(GridPosition gridPos)
    {
        return gridSystem.IsValidRegion(gridPos);
    }

    public bool IsPawnInRegion(GridPosition gridPos)
    {
        return gridSystem.IsPawnInRegion(gridPos);
    }

    public bool IsEnemyInRegion(GridPosition gridPos, Pawn selectedPawn)
    {
        return gridSystem.IsEnemyInRegion(gridPos, selectedPawn);
    }

    public bool IsAllyInRegion(GridPosition gridPos, Pawn selectedPawn)
    {
        return gridSystem.IsAllyInRegion(gridPos, selectedPawn);
    }

    public PathNode GetPathNode(GridPosition gridPos)
    {
        return gridSystem.GetPathNode(gridPos);
    }

    public PathNode GetPathNode(int x, int z)
    {
        return gridSystem.GetPathNode(x, z);
    }
}
