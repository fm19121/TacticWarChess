using UnityEngine;

public class GridSystem
{
    private int width;
    private int height;
    private int unitSize;
    private GridObject[,] gridObjects;
    private PathNode[,] pathNodes;

    public GridSystem(int width, int height, int unitSize)
    {
        this.width = width;
        this.height = height;
        this.unitSize = unitSize;
        gridObjects = new GridObject[width, height];
        pathNodes = new PathNode[width, height];

        for(int x = 0; x < width; x++)
        {
            for(int z = 0; z < height; z++)
            {
                gridObjects[x, z] = new GridObject(new GridPosition (x, z));
                pathNodes[x, z] = new PathNode(new GridPosition (x, z));
            }
        }
    }

    public int GetWidth()
    {
        return width;
    }

    public int GetHeight()
    {
        return height;
    }
    public Vector3 GetWorldPos(int x, int z)
    {
        return new Vector3(x, 0, z) * unitSize;
    }

    public Vector3 GetWorldPos(GridPosition gridPos)
    {
        return new Vector3(gridPos.x, 0, gridPos.z) * unitSize;
    }

    public GridPosition GetGridPosition(Vector3 worldPos)
    {
        return new GridPosition(Mathf.RoundToInt(worldPos.x / unitSize), Mathf.RoundToInt(worldPos.z / unitSize));
    }

    public GridObject GetGridObject(GridPosition gridPos)
    {
        return gridObjects[gridPos.x, gridPos.z];
    }

    public PathNode GetPathNode(GridPosition gridPos)
    {
        return pathNodes[gridPos.x, gridPos.z];
    }

    public PathNode GetPathNode(int x, int z)
    {
        return pathNodes[x, z];
    }

    public void CreateDebugGrid(GameObject gridUI)
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GameObject.Instantiate(gridUI, GetWorldPos(x, z), Quaternion.identity);
            }
        }
    }

    private bool IsInsideGrid(GridPosition gridPos)
    {
        return gridPos.x >= 0 && gridPos.z >= 0 && gridPos.x < width && gridPos.z < height;
    }

    public bool IsPawnInRegion(GridPosition gridPos)
    {
        GridObject gridObject = gridObjects[gridPos.x, gridPos.z];
        if(gridObject.GetPawn() != null)
        {
            return true;
        }
        return false;
    }

    public bool IsEnemyInRegion(GridPosition gridPos, Pawn selectedPawn)
    {
        GridObject gridObject = gridObjects[gridPos.x, gridPos.z];
        if (IsPawnInRegion(gridPos))
        {
            return gridObject.GetPawn().IsEnemy() != selectedPawn.IsEnemy();
        }
        return false;
    }

    public bool IsAllyInRegion(GridPosition gridPos, Pawn selectedPawn)
    {
        GridObject gridObject = gridObjects[gridPos.x, gridPos.z];
        if (IsPawnInRegion(gridPos))
        {
            return gridObject.GetPawn().IsEnemy() == selectedPawn.IsEnemy();
        }
        return false;
    }

    public bool IsValidRegion(GridPosition gridPos)
    {
        // return IsInsideGrid(gridPos) && gridPos != selectedPawn.GetGridPosition();
        return IsInsideGrid(gridPos);
    }
}
