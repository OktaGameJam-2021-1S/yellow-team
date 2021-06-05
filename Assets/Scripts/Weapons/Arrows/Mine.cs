using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : Shell
{
    public float damage;

    protected override void OnEnemyCollision(Entity entity)
    {
        //entity.TakeDamage( damageReport);
        entity.TakeDamage( new DamageReport()
        {
            //attacker = 
            damage = damage
        });
        Destroy(gameObject);
        //shooter.DeactivateShell(gameObject);
    }

    protected override void OnObstacleCollision(Transform obstacle)
    {
        //GetComponent<Collider>().enabled = false;
        //rigidbody.velocity = Vector3.zero;
        //flyingState = FlyingState.STUCK;
        //stuckTime = Time.time;
    }

    private void FixedUpdate()
    {
        //if (flyingState == FlyingState.STUCK && Time.time - stuckTime >= timeToDisappear)
        //{
        //    flyingState = FlyingState.FLYING;
        //    shooter.DeactivateShell(gameObject);
        //}
    }

    protected override void OnPlayerCollision(Entity entity) { }
}
