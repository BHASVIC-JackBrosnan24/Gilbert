using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hammer : MonoBehaviour
{
    [SerializeField]
    int damage;
    [SerializeField]
    float speed;
    float direction;
    private Camera mainCamera;

    private GameObject player;

    private Vector3 directionVector;
    [SerializeField]
    private float timer;

    public void Start()
    {
        mainCamera = Camera.main;
        player = GameObject.Find("Player");
        directionCalc();
    }

    public void Update()
    {
        Vector2 pos = transform.position;

        if (directionVector.x > 0)
        {
            pos.x += speed * Mathf.Cos(direction) * Time.deltaTime;
            pos.y += speed * Mathf.Sin(direction) * Time.deltaTime;
        }
        else {
            pos.x += speed * -Mathf.Cos(direction) * Time.deltaTime;
            pos.y += speed * -Mathf.Sin(direction) * Time.deltaTime;
        }

        timer=timer - 1*Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(this.gameObject);
        }

            transform.position = pos;
    }

    public void directionCalc() {
        Vector3 mouse = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        directionVector = (mouse - player.transform.position).normalized;
        direction=Mathf.Atan(directionVector.y/directionVector.x);
    }

    public int getDamage() {
        return damage;
    }

    public void setDamage(int newDamage) {
        damage = newDamage;
    }


}
