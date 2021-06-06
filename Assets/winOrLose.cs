using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class winOrLose : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI uiText;

    [SerializeField]
    GameObject[] images;

    [SerializeField]
    string[] text;

    GameOverData data;
    private IEnumerator Start()
    {
        data = GameObject.FindObjectOfType<GameOverData>() as GameOverData;

        while(data == null)
        {
            yield return new WaitForEndOfFrame();
        }

        Started(data.win);

        Destroy(data.gameObject);
    }
    public void Started(bool win)
    {        
        if (win)
        {
            uiText.text = text[0];
            images[0].SetActive(true);
            images[1].SetActive(true);
            images[2].SetActive(false);
            images[3].SetActive(false);
        }
        else
        {
            uiText.text = text[1];
            images[0].SetActive(false);
            images[1].SetActive(false);
            images[2].SetActive(true);
            images[3].SetActive(true);
            
        }  
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene("Game");
    }
}
