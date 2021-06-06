using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static CraftInventory;

public class PlayerHUDUI : MonoBehaviour
{
    public static Action<int> OnScoreChanged;
    public static Action<int> OnMineChanged;
    public static Action<int> OnTurretChanged;

    [SerializeField] EnemyHandler enemyHandler;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI turretCounter;
    [SerializeField] TextMeshProUGUI mineCounter;

    private void Awake()
    {
        enemyHandler.NewGame();
        OnScoreChanged += UpdateScore;
        OnTurretChanged += UpdateTurret;
        OnMineChanged += UpdateMine;
    }

    private void OnDestroy()
    {
        OnScoreChanged -= UpdateScore;
        OnTurretChanged -= UpdateTurret;
        OnMineChanged -= UpdateMine;
    }

    public static void ScoreChanged(int score)
    {
        OnScoreChanged?.Invoke(score);
    }

    public static void TurretChanged(int score)
    {
        OnTurretChanged?.Invoke(score);
    }

    public static void MineChanged(int score)
    {
        OnMineChanged?.Invoke(score);
    }

    private void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void UpdateTurret(int score)
    {
        turretCounter.text = "x"+score.ToString();
    }

    public void UpdateMine(int score)
    {
        mineCounter.text = "x" + score.ToString();
    }
}
