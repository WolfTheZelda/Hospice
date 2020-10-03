using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCamera : MonoBehaviour
{


    public string MouseXInput;
    public string MouseYInput;

    public float mouseSensitivity;

    public float MinAngles;
    public float MaxAngles;

    

    

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    void Update()
    {

        RotateCamera();

    }


    void RotateCamera()
    {
        float mouseX = Input.GetAxis(MouseXInput) * (mouseSensitivity * Time.deltaTime);
        float mouseY = Input.GetAxis(MouseYInput) * (mouseSensitivity * Time.deltaTime);
        Vector3 eulerRotation = transform.eulerAngles;

    }
}