using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverData : MonoBehaviour
{
    public int aliensKilled;
    public bool win;

    public void Init(bool win)
    {
        this.win = win;
    }
}
