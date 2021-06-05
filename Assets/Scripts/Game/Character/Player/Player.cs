using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseChar, IControllerChar
{

    private void FixedUpdate()
    {
        HandleInput();
    }

    public void Attack()
    {
        throw new System.NotImplementedException();
    }

    public void HandleInput()
    {
        throw new System.NotImplementedException();
    }

    public void SwapSkill()
    {
        throw new System.NotImplementedException();
    }
}
