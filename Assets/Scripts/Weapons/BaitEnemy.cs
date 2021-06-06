using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaitEnemy : MonoBehaviour
{
    List<Enemy> enemiesBaited;

    private void Start()
    {
        enemiesBaited = new List<Enemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == Tags.enemyTag)
        {
            Enemy enemy;
            enemy = other.GetComponent<Enemy>();
            if(enemy != null)
            {
                enemy.SetNewProvisoryTarget(transform);
                if(!enemiesBaited.Contains(enemy))
                    enemiesBaited.Add(enemy);
            }
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < enemiesBaited.Count; i++)
        {
            enemiesBaited[i].SetTargetToOriginal();
        }
    }
    
}
