using System.Collections;
using UnityEngine;

public class PUFire : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.red; //sets the hammer colour to red to signify they are on fire
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) {
            collision.gameObject.AddComponent<OnFire>(); //adds the OnFire component to the enemy
        }
    }


}
