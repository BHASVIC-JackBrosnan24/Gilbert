using UnityEngine;

public class HealthBar : MonoBehaviour
{

    float maxHealth;
    float currentHealth;

    [SerializeField]
    HealthBarText healthBarText;

    public void setMax(int max)
    {
        maxHealth = max;
        healthBarText.setMax(max);
        if (currentHealth != 0) 
        { 
            changeSize();
        }
    }

    public void setHealth(int health)
    {
        currentHealth = health;
        healthBarText.setHealth(health);
        if (maxHealth != 0)
        {
            changeSize();
        }
    }

    private void changeSize()
    {
        transform.localScale = new Vector3(currentHealth / maxHealth, 1f, 1f); //sets the size
        if (currentHealth < 0) {
            transform.localScale = new Vector3(0f, 1f, 1f); //sets the size
        }
    }
}