using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseChar : MonoBehaviour
{
    #region Enums
    //Character's state
    public enum CharacterState
    {
        None,
        Spawning,
        Alive,
        Dead,
    }
    #endregion

    [Header("Character Settings")]
    [SerializeField] protected float life;
    [SerializeField] protected float movimentSpeed;
    [SerializeField] protected float rotationSpeed;
    [SerializeField] protected BaseSkill[] skills;

    [SerializeField] protected Rigidbody charRigidbody;

    CharacterState State;

    public virtual void HitDamage(float damage)
    {
        if (life - damage < 0)
        {
            life = 0;
            ChangeState(CharacterState.Dead);
        }
        else
        {
            life -= damage;
        }
    }

    public virtual void ChangeState(CharacterState state)
    {
        State = state;
    }
}
