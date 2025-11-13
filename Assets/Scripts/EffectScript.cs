using UnityEngine;

public class EffectScript : MonoBehaviour
{
    float timer = 1; //timer until it despawns
    float opacity = 1; //opacity
    SpriteRenderer spriteRenderer;
    PauseController pauseController;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        pauseController = GameObject.Find("PauseController").GetComponent<PauseController>();
    }

    void Update()
    {
        spriteRenderer.color = new Color(1f, 1f, 1f, opacity); //sets the opacity to the new opacity
        opacity = opacity-(Time.deltaTime * pauseController.unpaused); //decreases opacity
        timer = timer-(Time.deltaTime * pauseController.unpaused); //decreases timer
        if (timer <= 0) { 
            Destroy(gameObject); //destroys game object if the timer runs out
        }
    }
}
