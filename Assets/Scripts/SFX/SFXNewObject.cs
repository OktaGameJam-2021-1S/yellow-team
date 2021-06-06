using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXNewObject : MonoBehaviour
{
    public List<AudioClip> audioClip;
    public bool playOnStart;

    private void Start()
    {
        if (!playOnStart) return;
        AudioSource.PlayClipAtPoint(audioClip[Random.Range(0, audioClip.Count)], transform.position);
    }


    public void Play()
    {
        AudioSource.PlayClipAtPoint(audioClip[Random.Range(0, audioClip.Count)], transform.position);
    }
}
