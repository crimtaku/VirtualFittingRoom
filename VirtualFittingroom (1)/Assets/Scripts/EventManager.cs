using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EventManager : MonoBehaviour
{
    public GameObject XRRig;
    public GameObject cameraHolder;
    public GameObject vrCanvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            CloseApplication();
        }
    }

    public void toggleXR(bool xr)
    {
        if (xr)
        {
            cameraHolder.SetActive(false);
            XRRig.SetActive(true);
            vrCanvas.SetActive(true);
        }
        else
        {
            cameraHolder.SetActive(true);
            XRRig.SetActive(false);
            vrCanvas.SetActive(false);
        }
    }
    public void CloseApplication()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
