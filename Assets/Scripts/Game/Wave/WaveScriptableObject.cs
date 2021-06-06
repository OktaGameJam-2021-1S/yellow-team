using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveConfig", menuName = "Game/WavesConfig", order = 1)]
public class WaveScriptableObject : ScriptableObject
{
    public List<WaveObejct> waves;
}

[Serializable]
public class WaveObejct
{
    public int enemyCount;
    public List<GameObject> enemies;
    public int powerUpAmount;
    public List<PowerUpScriptableObject> powerUp;
}