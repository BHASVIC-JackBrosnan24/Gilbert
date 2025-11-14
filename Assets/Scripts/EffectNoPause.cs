using UnityEngine;

public class EffectNoPauset : MonoBehaviour
{
    [SerializeField]
    private float timer; //timer until it despawns

    [SerializeField]
    private float minOpacity; //minimum opacity

    float maxTimer;
    float opacity = 1; //opacity
    SpriteRenderer spriteRenderer;
    PauseController pauseController;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        pauseController = GameObject.Find("PauseController").GetComponent<PauseController>();
        maxTimer = timer;
    }

    void Update()
    {
        spriteRenderer.color = new Color(1f, 1f, 1f, opacity); //sets the opacity to the new opacity
        if (opacity > minOpacity)
        {
            opacity = opacity - (Time.deltaTime / maxTimer); //decreases opacity
        }
        timer = timer - Time.deltaTime; //decreases timer
        if (timer <= 0)
        {
            Destroy(gameObject); //destroys game object if the timer runs out
        }
    }
}
