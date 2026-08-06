using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed;
    //public float rotationSpeed;
    //private float pitch;
    //private float yaw;
    private float horizontal;
    private float forward;
    //private Vector3 cursorPos;
    // Start is called before the first frame update
    void Start()
    {
        //cursorPos = Input.mousePosition;
        //rotationSpeed = 20;
        //speed = 10;
    }

    // Update is called once per frame
    void Update()
    {
        float vertical = 0f;
        horizontal = Input.GetAxis("Horizontal");
        forward = Input.GetAxis("Vertical");

        if(Input.GetKey(KeyCode.Q)) 
        {
            vertical = -1f;
        }
        if(Input.GetKey(KeyCode.E)) 
        {
            vertical = 1f;
        }
        Vector3 camTranslation = new Vector3 (horizontal, vertical , forward).normalized;
        //Vector3 camMovement = transform.forward * camTranslation.z + transform.position.z * camTranslation.x;

        transform.position += camTranslation * Time.deltaTime * speed;

        /*yaw = (float)(Input.mousePosition.x - cursorPos.x);
        pitch = (float)(Input.mousePosition.y - cursorPos.y);
        Vector3 cameraRotation = new Vector3(-pitch, yaw, 0); 
        transform.eulerAngles += cameraRotation * Time.deltaTime * rotationSpeed;
        cursorPos = Input.mousePosition;*/
        //Vector3 cameraMove = transform.forward * cameraTranslation.z + transform.right * cameraTranslation.x;
       // Debug.Log(Screen.height * 0.5);
        //transform.position += cameraMove * Time.deltaTime; 
    }
}
