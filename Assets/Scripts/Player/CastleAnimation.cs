using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleAnimation : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float speed;
    public bool canDrive;
    Action callbackOnEndAniamtion;

    private void Update()
    {
        if (!canDrive) return;

        transform.position = Vector3.MoveTowards(transform.position, target.position, speed);

        if(transform.position == target.position)
        {
            callbackOnEndAniamtion?.Invoke();
            canDrive = false;
        }
    }

    private void OnFinishedWave()
    {
        canDrive = true;
    }

}
