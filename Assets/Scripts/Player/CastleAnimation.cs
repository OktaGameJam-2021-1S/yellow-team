using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleAnimation : MonoBehaviour
{
    [SerializeField] Vector3 target;
    [SerializeField] float speed;
    public bool canDrive;
    Action callbackOnEndAniamtion;

    public BoxCollider mycollider;

    private void Update()
    {
        if (!canDrive) return;

        transform.position = Vector3.MoveTowards(transform.position, target, speed);

        if(transform.position == target)
        {
            callbackOnEndAniamtion?.Invoke();
            canDrive = false;
            mycollider.enabled = true;
        }
    }

    public void MoveToTarget(Transform target)
    {
        mycollider.enabled = false;

        this.target = target.position;
        this.target.y = transform.position.y;
        canDrive = true;
    }

}
