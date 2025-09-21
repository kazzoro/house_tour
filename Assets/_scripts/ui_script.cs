using UnityEngine;
using UnityEngine.SceneManagement;

public class ui_script : MonoBehaviour
{
    private Quaternion initialRotation;
    public GameObject camera_obj;
    public MapCameraController MapCameraController;
    void Start()
    {
        // Save the starting orientation
        initialRotation = camera_obj.transform.rotation;
    }

    public void Recenter()
    {
        // Reset back to initial orientation
        camera_obj.transform.rotation = initialRotation;
        MapCameraController.Recenter();
    }

    public void RestartScene()
    {
        MapCameraController.Recenter();
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
