using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Example : MonoBehaviour
{
    //Make sure to attach these Buttons in the Inspector
    public Button firstButton, secondButton, thirdButton, thourthButton;

    void Start()
    {
        firstButton.onClick.AddListener(delegate {ButtonHandlerFB(); });
        secondButton.onClick.AddListener(delegate {ButtonHandlerSB(); });
        thirdButton.onClick.AddListener(delegate {ButtonHandlerTB(); });
        thourthButton.onClick.AddListener(delegate {ButtonHandlerFRB(); });
    }
    
    public void LoadNextScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    void ButtonHandlerFB()
    {
        CameraController.IsCameraOn[0] = true;
        CameraController.NumbOfCamFlag = 1;
        LoadNextScene();
    }
    
    void ButtonHandlerSB()
    {
        CameraController.IsCameraOn[0] = true;
        CameraController.IsCameraOn[1] = true;
        CameraController.NumbOfCamFlag = 2;
        LoadNextScene();
    }
    
    void ButtonHandlerTB()
    {
        CameraController.IsCameraOn[0] = true;
        CameraController.IsCameraOn[1] = true;
        CameraController.IsCameraOn[2] = true;
        CameraController.NumbOfCamFlag = 3;
        LoadNextScene();
    }
    
    void ButtonHandlerFRB()
    {
        CameraController.IsCameraOn[0] = true;
        CameraController.IsCameraOn[1] = true;
        CameraController.IsCameraOn[2] = true;
        CameraController.IsCameraOn[3] = true;
        CameraController.NumbOfCamFlag = 4;
        LoadNextScene();
    }
}