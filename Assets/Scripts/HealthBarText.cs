using TMPro;
using UnityEngine;

public class HealthBarText : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI m_Object;


    int maxHealth;
    int currentHealth;

    public void setMax(int max)
    {
        maxHealth = max;
        if (currentHealth != 0)
        {
            setText();
        }
    }

    public void setHealth(int health)
    {
        currentHealth = health;
        if (maxHealth != 0)
        {
            setText();
        }
    }

    private void setText() {
        string string1 = currentHealth.ToString();
        string string2 = maxHealth.ToString();
        m_Object.text = string1 + "/" + string2;
    }
}
