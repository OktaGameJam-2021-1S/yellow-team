using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : BaseChar, IControllerChar
{

    private Quaternion targetRotation = new Quaternion();
    public void HandleInput()
    {
        float horizontalMove = Input.GetAxisRaw("LeftJoystickHorizontal");
        float verticalMove = Input.GetAxisRaw("LeftJoystickVertical");

        var inputRotation = new Vector3(Input.GetAxis("RightJoystickHorizontal") , 0, Input.GetAxis("RightJoystickVertical"));
        if (inputRotation != Vector3.zero)
        {
            targetRotation = Quaternion.LookRotation(inputRotation);
        }

        charRigidbody.velocity = new Vector3(horizontalMove * movimentSpeed, 0, verticalMove * rotationSpeed);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed);
    }
}
