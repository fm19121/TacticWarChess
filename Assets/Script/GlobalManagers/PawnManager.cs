using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnManager : MonoBehaviour
{
    public static PawnManager Instance { get; private set; }

    private List<Pawn> enemyPawns = new List<Pawn>();
    private List<Pawn> allyPawns = new List<Pawn>();

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    void Start()
    {
        Pawn.OnPawnDestroyed += RemovePawn;
        Pawn.OnPawnCreated += AddPawn;
    }

    private void RemovePawn(object sender, EventArgs e)
    {
        Pawn pawn = sender as Pawn;
        if (pawn.IsEnemy()) 
        {
            enemyPawns.Remove(pawn);
        }
        else
        {
            allyPawns.Remove(pawn);
        }
    }

    private void AddPawn(object sender, EventArgs e)
    {
        Pawn pawn = sender as Pawn;
        if (pawn.IsEnemy())
        {
            enemyPawns.Add(pawn);
        }
        else
        {
            allyPawns.Add(pawn);
        }
    }

    public List<Pawn> GetEnemyPawns()
    {
        return enemyPawns;
    }

    public List<Pawn> GetAllyPawns()
    {
        return allyPawns;
    }
}
