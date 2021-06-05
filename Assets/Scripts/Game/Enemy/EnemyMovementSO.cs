using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyMovementData", menuName = "ScriptableObjects/EnemyMovementData", order = 1)]
public class EnemyMovementSO : ScriptableObject
{
    public float meleeDistance;
    public float rangeDistance;
}
