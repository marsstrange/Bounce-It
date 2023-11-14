using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform cam;
    public Transform player;
    public float CamX;
    public float CamY;
    public float CamZ;
    
    public static bool[] IsCameraOn = new bool[4] { false, false, false, false };
    public string[] CamNames = new string[4] {"FirstCamera", "SecondCamera", "ThirdCamera", "FourthCamera" };
    public static int NumbOfCamFlag = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        Camera cam1 = GameObject.FindWithTag(CamNames[0]).GetComponent<Camera>();
        Camera cam2 = GameObject.FindWithTag(CamNames[1]).GetComponent<Camera>();
        Camera cam3 = GameObject.FindWithTag(CamNames[2]).GetComponent<Camera>();
        Camera cam4 = GameObject.FindWithTag(CamNames[3]).GetComponent<Camera>();
        
        switch (NumbOfCamFlag)
        {
            case 1:
                cam1.rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
                break;
            case 2:
                cam1.rect = new Rect(0.0f, 0.0f, 0.5f, 1.0f);
                cam2.rect = new Rect(0.5f, 0.0f, 0.5f, 1.0f);
                break;
            case 3:
                cam1.rect = new Rect(0.0f, 0.0f, 0.5f, 1.0f);
                cam2.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                cam3.rect = new Rect(0.5f, 0.0f, 0.5f, 0.5f);
                break;
            case 4:
                // cam1.rect = new Rect(0.0f, 0.0f, 0.5f, 0.5f);
                // cam2.rect = new Rect(0.0f, 0.5f, 0.5f, 0.5f);
                // cam3.rect = new Rect(0.5f, 0.0f, 0.5f, 0.5f);
                // cam4.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                
                cam1.rect = new Rect(0.0f, 0.5f, 0.5f, 0.5f);
                cam2.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                cam3.rect = new Rect(0.5f, 0.0f, 0.5f, 0.5f);
                cam4.rect = new Rect(0.0f, 0.0f, 0.5f, 0.5f);
                break;
        }
        
        
        
        for (int i = 0; i < IsCameraOn.Length; i++)
        {
            
            if (IsCameraOn[i]) 
                // Enable the camera or perform any other necessary actions
                GameObject.FindWithTag(CamNames[i]).GetComponent<Camera>().enabled = true;
            else
            
                // Disable the camera or perform other actions
                GameObject.FindWithTag(CamNames[i]).GetComponent<Camera>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        cam.position = player.position + new Vector3(CamX, CamY, CamZ);
    }
}

