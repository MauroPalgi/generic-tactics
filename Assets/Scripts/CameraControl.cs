using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float keyboardInputSensitivity = 10f;
    [SerializeField] float mouseInputSensitivity = 0.4f;
    Vector3 input;
    Vector3 pointOfOrigin;


    private void Update()
    {
        ResetInput();
        MoveCameraInput();

        MoveCamera();

    }

    private void ResetInput()
    {
        input.x = 0;
        input.y = 0;
        input.z = 0;
    }

    private void MoveCamera()
    {

        transform.position += (input * Time.deltaTime);

    }


    private void MoveCameraInput()
    {
        AxisInput();
        MouseInput();

    }

    private void MouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pointOfOrigin = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 mouseInput = Input.mousePosition;
            input.x = (mouseInput.x - pointOfOrigin.x) * mouseInputSensitivity;
            input.z = (mouseInput.y - pointOfOrigin.y) * mouseInputSensitivity;
        }
    }

    private void AxisInput()
    {
        input.x = Input.GetAxisRaw("Horizontal") * keyboardInputSensitivity;
        input.z = Input.GetAxisRaw("Vertical") * keyboardInputSensitivity;
    }
}