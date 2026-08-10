using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField] private bool isInvert;
    private Transform camTransform;
    void Start()
    {
        camTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        UpdateTransform();
    }

    private void UpdateTransform()
    {
        if (isInvert)
        {
            Vector3 ToCam = (camTransform.position - transform.position).normalized;
            transform.LookAt(transform.position + ToCam * -1);
        }
        else
        {
            transform.LookAt(camTransform);
        }
    }
}
