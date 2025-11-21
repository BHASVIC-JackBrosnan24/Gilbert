using UnityEngine;
using System.Collections;
public class OnFire : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    ParentMeleeEnemy melee;
    ParentRangedEnemy ranged;
    PauseController pauseController;

    int damageCount=0; //count for the amount of fire damage the enemy has took

    private void Start()
    {
        pauseController = GameObject.Find("PauseController").GetComponent<PauseController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        melee = GetComponent<ParentMeleeEnemy>();
        ranged = GetComponent<ParentRangedEnemy>();
        spriteRenderer.color = Color.red; //sets the enemy colour to red to signify they are on fire
        StartCoroutine(fireDamage());
    }

    IEnumerator fireDamage()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f); //damages the enemy ever half a second
            if (pauseController.unpaused == 1)
            {
                if (melee != null)
                {
                    melee.damaged(2);
                }
                else if (ranged != null)
                {
                    ranged.damaged(2);
                }
                damageCount += 1;
                if (damageCount >= 5)//if the enemy has taken 5 ticks of fire damage
                {
                    spriteRenderer.color = Color.white; //resets the enemy's colour
                    Destroy(this); //destroys this script
                }
            }
        }
    }
}
