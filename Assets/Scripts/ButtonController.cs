using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public static ButtonController instance; //creates a static ButtonController

    private int[] powerUpSelection = new int[3];

    private int sceneSwap = 0;
    private int choice;

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
    }


    private void Update()
    {
        if (sceneSwap == 1)
        {
            GameObject player = GameObject.Find("Player");
            if (player != null)
            {
                
                PlayerStats playerStats = player.GetComponent<PlayerStats>();
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
        sceneSwap = 1;
        SceneManager.LoadScene("Gameplay"); //sends you back to the gameplay scene
    }

    public void powerUpTime(int[] selection) { //sets the power-up selection
        powerUpSelection[0] = selection[0];
        powerUpSelection[1] = selection[1];
        powerUpSelection[2] = selection[2];

        SceneManager.LoadScene("Power-Up Selection"); //sends you back to the gameplay scene
    }

    public int[] getSelection() { //getter for powerUpSelection array
        return powerUpSelection;
    }
}
