using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hammer : MonoBehaviour
{
    int damage;
    [SerializeField]
    float speed;
    float direction;
    private Camera mainCamera;
    public Hammer(int damage,float direction,float speed)
    {
        this.damage= damage;
        this.direction = direction;
        this.speed = speed;
    }

    public void Update()
    {
        move(direction,speed);
    }
    public void move(float direction,float speed) { 
        Vector2 pos = transform.position;

        pos.x+=speed*Mathf.Cos(direction)*Time.deltaTime;
        pos.y+=speed*Mathf.Sin(direction)*Time.deltaTime;

        transform.position=pos;
    }




}
