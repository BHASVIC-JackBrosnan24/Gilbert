 using UnityEngine;

public class Crosshair : MonoBehaviour
{
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }
    void Update()
    {
        Vector2 mouse = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        transform.position = mouse;
    }

    public Vector2 getPos() { 
        return transform.position;
    }
}
