using UnityEngine;
using ThirteenPixels.Soda;

public class Castle : Entity
{
    [SerializeField] GameEvent onPlayerDeath;

    protected override void Death(Entity killer)
    {
        onPlayerDeath.Raise();
        Debug.Log("Castle is DEAD");
        gameObject.SetActive(false);
    }

}
