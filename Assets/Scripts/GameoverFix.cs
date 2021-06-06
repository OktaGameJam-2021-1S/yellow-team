using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameoverFix : MonoBehaviour
{
    public void GameOver() {
        StartCoroutine(GameOverScreen());
    }

    private IEnumerator GameOverScreen()
    {
        yield return new WaitForSeconds(5f);

        GameObject ga = new GameObject();
        ga.AddComponent<GameOverData>().Init(false);
        SceneManager.LoadScene("WinLoseCondition");
    }
}
