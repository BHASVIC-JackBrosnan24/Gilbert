using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private float speedV;
    private float speedH;
    private void Update()
    {
        speedV = speed;
        speedH = speed;
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector2 pos = transform.position;

        if (h ==1 && v ==1)
        {
            speedV = Mathf.Sqrt(speed*speed*0.5f);
            speedH = Mathf.Sqrt(speed * speed * 0.5f);
        }

        pos.x += h * speedH * Time.deltaTime;
        pos.y += v * speedV * Time.deltaTime;

        transform.position = pos;
    }

}
