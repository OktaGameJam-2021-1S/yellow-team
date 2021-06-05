using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBCK : BaseChar, IControllerChar
{
    [Header("Set on editor")]
    [SerializeField] EnemyMovementSO enemyMovementSO;
    [SerializeField] private NavMeshAgent _agent;

    [Header("Others")]

    private bool aiEnabled;
    [SerializeField] private Transform _target;
    private MovementBehaviour _behaviour;

    private void Awake()
    {
        SetData(MovementBehaviour.Range, true, _target);
    }

    public void SetData(MovementBehaviour pMoveBehaviour, bool pIsAi, Transform pTarget)
    {
        _behaviour = pMoveBehaviour;
        aiEnabled = pIsAi;
        _target = pTarget;
        _agent.destination = _target.position;
        _agent.enabled = pIsAi;

        switch (_behaviour)
        {
            case MovementBehaviour.Melee:
                _agent.stoppingDistance = enemyMovementSO.meleeDistance;
                break;
            case MovementBehaviour.Range:
                // TODO: Enemy get back a litte
                _agent.stoppingDistance = enemyMovementSO.rangeDistance;
                break;
        }
    }

    public void ChangeAIToPlayer()
    {
        aiEnabled = false;
        _agent.enabled = aiEnabled;
    }
        
    private void FixedUpdate()
    {
        if (aiEnabled)
        {
            //AIMovement();
        }
        else
        {
            HandleInput();
        }
    }

   
    private void AIMovement()
    {
        switch (_behaviour)
        {
            case MovementBehaviour.Melee:
                    _agent.stoppingDistance = enemyMovementSO.meleeDistance;
                break;
            case MovementBehaviour.Range:
                break;

        }
    }

    public void HandleInput()
    {
        throw new NotImplementedException();
    }

    //EnemyType Type[Runner, Tank, SimpleRange, Poisoner]
    public enum MovementBehaviour
    {
        Melee,
        Range,
        //Runner,
        //Tank,
        //SimpleRange,
        //Poisoner
    }


}
