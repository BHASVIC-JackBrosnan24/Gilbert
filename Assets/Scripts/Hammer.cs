using UnityEngine;
using UnityEngine.InputSystem;

public class Hammer : MonoBehaviour
{
    int damage;
    float speed;
    float direction;
    private Camera mainCamera;
    public Hammer(int damage)
    {
        this.damage= damage;
        mainCamera = Camera.main;
        Vector2 mouse = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 camera = mainCamera.transform.position;
        direction = Mathf.Atan((mouse.y-camera.y)/(mouse.x-camera.x));
    }

    public void Update()
    {
        move();
    }
    public void move() { 
        Vector2 pos = transform.position;

        pos.x=speed*Mathf.Cos(direction)*Time.deltaTime;
        pos.y=speed*Mathf.Sin(direction)*Time.deltaTime;

        transform.position=pos;
    }




}
