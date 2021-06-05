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
    [SerializeField] private float life;
    [SerializeField] private float movimentSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] BaseSkill[] skills;

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
