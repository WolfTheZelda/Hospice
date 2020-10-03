using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerCamera : MonoBehaviour
{
    public float sensivity = 100.0f, minAngleLimit = -90f, maxAngleLimit = 90f;
    private Transform camera, player;
    private float rotationX = 0f;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main.transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensivity * Time.deltaTime;       

        player.Rotate(Vector3.up * mouseX);
        
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, minAngleLimit, maxAngleLimit);
        camera.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
    }
}
