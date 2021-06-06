using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogSystem : MonoBehaviour
{
    public TextMeshProUGUI text;
    public CanvasGroup dialog;

    public AudioSource audioSource;
    public AudioClip[] audios;

    [Header("Start game Texts")]
    public string[] startTexts;
    [Header("Kill Texts")]
    public string[] killTexts;
    [Header("Dead Texts")]
    public string[] deadText;

    public void ShowStartText()
    {
        int i = Random.Range(0, startTexts.Length);
        StartCoroutine(ShowDialog(startTexts[i]));
    }

    public void ShowKillText()
    {
        int i = Random.Range(0, killTexts.Length);
        StartCoroutine(ShowDialog(killTexts[i]));
    }

    public void ShowDeadText()
    {
        int i = Random.Range(0, deadText.Length);
        StartCoroutine(ShowDialog(deadText[i]));
    }

    private IEnumerator ShowDialog (string text)
    {
        this.text.text = text;

        int i = Random.Range(0, audios.Length);
        audioSource.clip = audios[i];
        audioSource.Play();

        while(dialog.alpha != 1)
        {
            dialog.alpha += Time.deltaTime * 2f;
            yield return null;
        }

        yield return new WaitForSeconds(2f);

        while (dialog.alpha != 0)
        {
            dialog.alpha -= Time.deltaTime * 2f;
            yield return null;
        }
    }

}
