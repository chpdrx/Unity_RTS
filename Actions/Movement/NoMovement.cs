using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NoMovement : IMovement
{
    public void StateChoise()
    {

    }

    public void SetDestination(NavMeshAgent navMeshAgent, Vector3 position)
    {
        
    }

    public void StartAI(NavMeshAgent navMeshAgent, LayerMask enemyLayer, GameObject unit, Vector3 position)
    {
        
    }

    public bool IsAttacking()
    {
        return false;
    }
}
