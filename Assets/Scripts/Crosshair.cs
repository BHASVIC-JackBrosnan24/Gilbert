 using UnityEngine;

public class Crosshair : MonoBehaviour
{
    private Camera mainCamera;//gets the camera

    private void Start()
    {
        mainCamera = Camera.main;//sets the camera to the main camera
    }
    void Update()
    {
        Vector2 mouse = mainCamera.ScreenToWorldPoint(Input.mousePosition);//gets the position of the mouse

        transform.position = mouse;//sets this position to the position of the mouseD
    }
}
