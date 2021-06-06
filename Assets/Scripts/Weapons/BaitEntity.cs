using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaitEntity : Entity
{
    protected override void Death(Entity killer)
    {
        Destroy(transform.parent.gameObject);
    }
}
