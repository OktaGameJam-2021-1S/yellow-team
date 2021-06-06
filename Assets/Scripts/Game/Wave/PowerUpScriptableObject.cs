using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerUp", menuName = "Game/PowerUp", order = 2)]
public class PowerUpScriptableObject : ScriptableObject
{
    string name;
    int value;
}
