using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{
    private GridPosition gridPos;
    private Pawn pawn;
   // private List<Pawn> pawns = new List<Pawn>();
    
    public GridObject(GridPosition gridPos)
    {
        this.gridPos = gridPos;
    }

    public void AddPawn(Pawn p)
    {
        pawn = p;
    }

    public Pawn GetPawn()
    {
        return pawn;
    }

    public void RemovePawn()
    {
        pawn = null;
    }
}
