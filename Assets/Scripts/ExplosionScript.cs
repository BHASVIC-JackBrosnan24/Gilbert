using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    ParentMeleeEnemy melee;
    ParentRangedEnemy ranged;

    private void Start()
    {
        Invoke("destroyThis", 0.3f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            melee = collision.gameObject.GetComponent<ParentMeleeEnemy>();
            ranged = collision.gameObject.GetComponent<ParentRangedEnemy>();
            if (melee != null)
            {
                melee.damaged(14); //deals 14 damage to a melee enemy
            }
            else if (ranged != null)
            {
                ranged.damaged(14); //deals 14 damage to a ranged enemy
            }
        }
    }

    private void destroyThis()
    {
        Destroy(gameObject);
    }
}