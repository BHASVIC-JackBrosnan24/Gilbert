using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private float speedV;
    private float speedH;

    private float tempH;
    private float tempV;

    private float dashing = 0f;
    private float dashCooldown = 0f;

    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Sprite dashingSprite;

    [SerializeField]
    private Sprite regularSprite;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        speedV = speed;
        speedH = speed;
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector2 pos = transform.position;

        if (Input.GetKeyDown("space") && dashCooldown<=0f)
        {
            dash();
            print(1);
        }

        if (dashing > 0f)
        {
            dashing = dashing - 1f*Time.deltaTime;
            h = tempH;
            v = tempV;
            spriteRenderer.sprite = dashingSprite;
        }
        else { 
            tempH = h;
            tempV = v;
            spriteRenderer.sprite = regularSprite;
        }

        if (dashCooldown > 0f) {
            dashCooldown = dashCooldown - 1f * Time.deltaTime;
        }

        if ((h ==1 && v ==1) || (h == 1 && v == -1) || (h == -1 && v == 1) || (h == -1 && v == -1))
        {
            speedV = Mathf.Sqrt(speed * speed * 0.5f);
            speedH = Mathf.Sqrt(speed * speed * 0.5f);
        }

        if (dashing > 0f) {
            speedV = speed * 5;
            speedH = speed * 5;
        }

        pos.x += h * speedH * Time.deltaTime;
        pos.y += v * speedV * Time.deltaTime;


        transform.position = pos;
        
    }

    private void dash() {
        dashing = 0.15f;
        dashCooldown = 0.8f;
    }

    public bool getDashInvincibility() { 
        return dashing> 0f;
    }

}
