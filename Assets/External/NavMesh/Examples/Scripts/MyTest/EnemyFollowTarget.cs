using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollowTarget : MonoBehaviour
{
    public Transform target;
    NavMeshAgent m_Agent;

    // TODO: Follow as a group

    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        m_Agent.destination = target.position;
    }
}
