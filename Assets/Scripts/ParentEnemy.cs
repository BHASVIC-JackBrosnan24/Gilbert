using UnityEngine;

public class ParentEnemy : MonoBehaviour
{
    [SerializeField] 
    private int health;

    private int damage;
    private float speed;
    private float attackSpeed;
    void Start()
    {
        
    }

    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.CompareTag("Hammer")) {
            GameObject hmr = collision.gameObject;
            print("1");
            Hammer hammer = hmr.GetComponent<Hammer>();
            damage = hammer.getDamage();
            damaged(damage);
            Destroy(hmr);
        }
    }

    public void damaged(int damage) {
        print("2");
        health -= damage;
        if (health <= 0) { 
        Destroy(this.gameObject);
        }
    }
}
