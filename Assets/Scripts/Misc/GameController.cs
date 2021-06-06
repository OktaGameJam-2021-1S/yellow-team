using UnityEngine;
using UnityEngine.SceneManagement;
using ThirteenPixels.Soda;
using System.Collections;
using UnityEngine.AI;
using System.Collections.Generic;
using TMPro;

public enum GameState
{
    INIT,
    STARTED,
    ENDWAVE
}
public class GameController : MonoBehaviour
{
    [SerializeField] GlobalGameState gameState;
    [SerializeField] private GlobalEnemySpawner enemySpawner;
    [SerializeField] private WaveScriptableObject wavesConfig;
    [SerializeField] private BlockMap blockMapPrefab;
    [SerializeField] private BlockMap currentBlockMap;
    [SerializeField] private CastleAnimation castleAnimation;
    [SerializeField] private CountdownUI countdownUI;
    [SerializeField] DialogSystem dialogSystem;

    private BlockMap nextBlockMap;
    private BlockMap oldBlockMap;

    [SerializeField] TextMeshProUGUI bannerText;


    private int waveNumber = 0;

    [SerializeField] GameEvent levelPassed;

    private void Start()
    {
        StartCoroutine(StartNewWave());
    }

    private void ConfigSpawner()
    {
        WaveObejct wave = wavesConfig.waves[waveNumber];
        enemySpawner.componentCache.InitSpawner(wave.enemies, currentBlockMap.SpawnPoints, wave.enemyCount);
    }

    /// <summary>
    /// Restarts the game
    /// </summary>
    public void Restart()
    {
        gameState.value = GameState.INIT;
        SceneManager.LoadScene("Game");
    }

    private void OnEnable()
    {
        levelPassed.onRaise.AddResponse(OnCompleteWave);
    }
    private void OnDisable()
    {
        levelPassed.onRaise.RemoveResponse(OnCompleteWave);
    }

    private void OnCompleteWave()
    {
        gameState.value = GameState.ENDWAVE;

        waveNumber++;

        if (waveNumber >= wavesConfig.waves.Count)
        {
            GameObject ga = new GameObject();
            ga.AddComponent<GameOverData>().Init(true);
            SceneManager.LoadScene("WinLoseCondition");
        }

        oldBlockMap = currentBlockMap;
        currentBlockMap.OpenExitGate();
        currentBlockMap = Instantiate(blockMapPrefab, currentBlockMap.EndMapPoint.position, currentBlockMap.EndMapPoint.rotation);
        currentBlockMap.OpenEntryGate();

        currentBlockMap.GetComponent<NavMeshSurface>().BuildNavMesh();

        castleAnimation.MoveToTarget(currentBlockMap.TruckTarget);

        currentBlockMap.SetActionPlayerEntry(() =>
        {
            StartCoroutine(StartNewWave());
        });

    }

    private IEnumerator StartNewWave()
    {
        ConfigSpawner();

        if (oldBlockMap != null)
        {
            Destroy(oldBlockMap.gameObject);
            yield return new WaitForSecondsRealtime(1f);
        }

        currentBlockMap.CloseEntryGate();


        bannerText.gameObject.SetActive(true);
        bannerText.text = string.Format("Wave {0}", waveNumber + 1);

        yield return new WaitForSecondsRealtime(2f);

        bannerText.gameObject.SetActive(false);

        countdownUI.Config(wavesConfig.waves[waveNumber].timeToStartWave,
            () =>
            {
                dialogSystem.ShowStartText();
                enemySpawner.componentCache.SpawnEnemies();
            });

        gameState.value = GameState.STARTED;
    }
}
