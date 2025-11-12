using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public static ButtonController instance; //creates a static ButtonController

    private int[] powerUpSelection = new int[3];

    private int sceneSwap = 0;
    private int choice;

    private GameObject player;
    private PlayerStats playerStats;

    [SerializeField]
    GameObject b1; //button 1
    [SerializeField]
    GameObject b2; //button 2
    [SerializeField]
    GameObject b3; //button 3

    GameObject[] buttons = new GameObject[3];

    void Awake()
    {
        if (instance == null) //if there is no button controller instance, it makes one
        { 
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else //if there uus a button controller instance, it destroys this one
        {
            Destroy(gameObject);
        }
        player = GameObject.Find("Player");
        playerStats = player.GetComponent<PlayerStats>();
    }


    private void Update()
    {
        if (sceneSwap == 1)
        {
            if (player != null)
            { 
                playerStats.addPowerUp(choice); //adds this power-ups powerType to the array
                print(2);
                sceneSwap = 0;
            }
        }

    }

    public void choiceMade()
    {
        GameObject clicked = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject; //gets the button clicked

        ButtonScript button = clicked.GetComponent<ButtonScript>(); 
        choice = button.getPowerType();//gets the PowerType of the button clicked
        print(1);

        Destroy(buttons[0]); //removes the buttons from existence
        Destroy(buttons[1]);
        Destroy(buttons[2]);
        buttons[0] = null;
        buttons[1] = null;
        buttons[2] = null;
    }

    public void powerUpTime(int[] selection) { //sets the power-up selection
        powerUpSelection[0] = selection[0];
        powerUpSelection[1] = selection[1];
        powerUpSelection[2] = selection[2];

        GameObject bOne = Instantiate(b1); //creates the buttons
        GameObject bTwo = Instantiate(b2);
        GameObject bThree = Instantiate(b3);

        buttons[0] = bOne; //saves the buttons in an array for later use
        buttons[1] = bTwo;
        buttons[2] = bThree;
    }

    public int[] getSelection() { //getter for powerUpSelection array
        return powerUpSelection;
    }
}
