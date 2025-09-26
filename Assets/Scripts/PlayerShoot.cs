using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private GameObject Hammer;

    [SerializeField]
    private int damage;

    [SerializeField]
    private float speed;

   private float direction;

    private float xDif;
    private float yDif;
    private float angle;
    void Start()
    {
    Crosshair crosshair = GetComponent<Crosshair>();
        Vector2 crosshairPos = crosshair.transform.position;
        Vector2 playerPos = transform.position;
        xDif = crosshairPos.x - playerPos.x;
        yDif = crosshairPos.y - playerPos.y;
        direction = Mathf.Atan2(xDif, yDif);
    } 
     
    
    
    void Update()
    {
        if (Input.GetButtonDown("SpaceBar") == true) {
            Hammer hammer = new Hammer(damage, direction, speed);
            Vector2 hammerPos = transform.position;
            Hammer.transform.position = hammerPos;
        }
    }
}
