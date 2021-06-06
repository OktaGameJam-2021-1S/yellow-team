using UnityEngine;
using System.Collections;
using TMPro;
using ThirteenPixels.Soda;

public class CountdownUI : MonoBehaviour
{
    [SerializeField] GlobalGameState gameState;
    [SerializeField] TextMeshProUGUI countdown;
    [SerializeField] GameObject canvas;
    int time;

    private System.Action onComplete;

    private void OnEnable()
    {
        gameState.onChange.AddResponse(StartTimer);
    }

    private void OnDisable()
    {
        gameState.onChange.RemoveResponse(StartTimer);
    }

    private void StartTimer(GameState gameState)
    {
        if(gameState==GameState.STARTED)
            StartCoroutine(StartTimerCoroutine());
    }

    private IEnumerator StartTimerCoroutine()
    {
        canvas.SetActive(true);
        for (int i = time; i > 0; i--)
        {
            countdown.text = "O jogo irá iniciar em " + i.ToString();
            yield return new WaitForSecondsRealtime(1);
        }
        canvas.SetActive(false);

        onComplete?.Invoke();
    }

    public void Config(int time, System.Action onComplete)
    {
        this.time = time;
        this.onComplete = onComplete;
    }
}
