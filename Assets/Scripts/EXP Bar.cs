using UnityEngine;

public class EXPBar : MonoBehaviour
{
    float nextLevelBoundary;
    float currentEXP;

    public void setBoundary(int boundary)
    {
        nextLevelBoundary = boundary;
        if (currentEXP != 0)
        {
            changeSize();
        }
    }

    public void setEXP(int exp)
    {
        currentEXP = exp;
        if (nextLevelBoundary != 0)
        {
            changeSize();
        }
    }

    private void changeSize()
    {
        transform.localScale = new Vector3(currentEXP / nextLevelBoundary, 1f, 1f); //sets the size
        if (currentEXP/nextLevelBoundary > 1)
        {
            transform.localScale = new Vector3(1f, 1f, 1f); //sets the size
        }
    }
}
