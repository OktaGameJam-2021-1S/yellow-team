using UnityEngine;
using ThirteenPixels.Soda;
using System.Collections;
using UnityEngine.SceneManagement;

public class Castle : Entity
{
    [SerializeField] GameEvent onPlayerDeath;

    protected override void Death(Entity killer)
    {
        onPlayerDeath.Raise();
        Debug.Log("Castle is DEAD");
        gameObject.SetActive(false);

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
