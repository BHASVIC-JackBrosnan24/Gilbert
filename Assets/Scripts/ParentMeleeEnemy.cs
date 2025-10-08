using UnityEngine;

public class ParentMeleeEnemy : MonoBehaviour
{
    [SerializeField]
    private int health;
    [SerializeField]
    private int damage;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float attackRate;

    private Transform player;
    private Vector3 directionVector;
    private float direction;

    void Start()
    {
        player = GameObject.Find("Player").transform;
        directionCalc();
    }

    private void Update()
    {
        if (transform.position != player.transform.position)
        {
            directionCalc();
            movement();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hammer"))
        {
            GameObject hmr = collision.gameObject;
            Hammer hammer = hmr.GetComponent<Hammer>();
            int hDamage = hammer.getDamage();
            damaged(hDamage);
            Destroy(hmr);
        }
    }

    private void damaged(int hDamage)
    {
        health -= hDamage;
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void movement() {
        Vector2 pos = transform.position;

        if (directionVector.x > 0)
        {
            pos.x += speed * Mathf.Cos(direction) * Time.deltaTime;
            pos.y += speed * Mathf.Sin(direction) * Time.deltaTime;
        }
        else
        {
            pos.x += speed * -Mathf.Cos(direction) * Time.deltaTime;
            pos.y += speed * -Mathf.Sin(direction) * Time.deltaTime;
        }

        transform.position = pos;
    }

    private void directionCalc()
    {
        directionVector = (player.transform.position - this.transform.position).normalized;
        direction = Mathf.Atan(directionVector.y / directionVector.x);
    }

    private void attack() { }
}
