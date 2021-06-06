using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXNewObject : MonoBehaviour
{
    public AudioClip audioClip;
    public GameObject sfxDataPrefab;

    private void Start()
    {
        //GameObject go = Instantiate(sfxDataPrefab, transform.parent);
        //go.GetComponent<SFXData>().audioSource.clip = audioClip;


        AudioSource.PlayClipAtPoint(audioClip, transform.position);
    }
}
