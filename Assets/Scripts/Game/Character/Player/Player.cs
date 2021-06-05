using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private IControllerChar characterController;

    private void Start()
    {
        characterController = GetComponent<IControllerChar>();
    }

    private void FixedUpdate()
    {
        characterController.HandleInput();
    }
}
