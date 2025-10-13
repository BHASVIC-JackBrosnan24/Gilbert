using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private float speed; //player speed

    [SerializeField]
    private int damage; //hammer damage

    private float hammerSpeed = 10f; //base speed of hammer
    private float exp=0; //starting exp (0)

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float getSpeed() { //returns speed
        return speed;
    }

    public void setSpeed(float aspeed) { //sets speed
        speed = aspeed;
    }

    public int getDamage() //returns damage 
    {
        return damage;
    }

    public void setDamage(int adamage) //sets damage
    {
        damage = adamage;
    }

    public float getHammerSpeed() //returns hammer speed
    {
        return hammerSpeed;
    }
    public void setHammerSpeed(float ahammerSpeed)  //sets hammer speed
    {
        hammerSpeed = ahammerSpeed;
    }

    public float getEXP() //returns exp
    {
        return exp;
    }

    public void setEXP(float EXP) //sets exp
    {
        exp = EXP;
    }
}
