using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public float speedHorizontal = 1.0f;
    public float speedVertical = 1.0f;
    public float zoomSensitivity = 1.0f;
    public GameObject cameraRotationX;
    private float currentZoom = 1.0f;

    float rotationVertical=0.0f;
    float rotationHorizontal=0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            rotationVertical = -Input.GetAxis("Mouse Y");
            rotationHorizontal = -Input.GetAxis("Mouse X");

            //transform.Rotate(rotationVertical*speedVertical*Time.deltaTime, rotationHorizontal * speedHorizontal * Time.deltaTime, 0);
            transform.Rotate(0, rotationHorizontal * speedHorizontal * Time.deltaTime, 0);
            cameraRotationX.transform.Rotate(rotationVertical * speedVertical * Time.deltaTime, 0, 0);
        }
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity;
        if (currentZoom < 0.5f)
        {
            currentZoom = 0.5f;
        }
        cameraRotationX.transform.localScale = new Vector3(1,1,currentZoom);

    }
}
