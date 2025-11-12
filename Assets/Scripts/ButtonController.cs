using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{

    private int[] powerUpSelection = new int[3];

    private int choice;


    [SerializeField]
    GameObject b1; //button 1
    [SerializeField]
    GameObject b2; //button 2
    [SerializeField]
    GameObject b3; //button 3



    public void choiceMade()
    {
        GameObject clicked = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject; //gets the button clicked

        ButtonScript button = clicked.GetComponent<ButtonScript>(); 
        choice = button.getPowerType();//gets the PowerType of the button clicked

        GameObject button1 = GameObject.FindWithTag("Button1");
        Destroy(button1);
        GameObject button2 = GameObject.FindWithTag("Button2");
        Destroy(button2);
        GameObject button3 = GameObject.FindWithTag("Button3");
        Destroy(button3);

        GameObject player = GameObject.FindWithTag("Player");
        PlayerStats playerStats = player.GetComponent<PlayerStats>();

        playerStats.addPowerUp(choice); //adds this power-ups powerType to the array

        Camera cam = Camera.main;
        CameraMovement cameraMovement = cam.GetComponent<CameraMovement>();
        cameraMovement.setMove(true); //lets the camera move again
    }

    public void powerUpTime(int[] selection) { //sets the power-up selection
        powerUpSelection[0] = selection[0];
        powerUpSelection[1] = selection[1];
        powerUpSelection[2] = selection[2];

        GameObject bOne = Instantiate(b1); //creates the buttons
        GameObject bTwo = Instantiate(b2);
        GameObject bThree = Instantiate(b3);

        Camera cam = Camera.main;
        CameraMovement cameraMovement = cam.GetComponent<CameraMovement>();
        cameraMovement.setMove(false); //stops camera from moving
        Vector3 pos = cam.transform.position;
        pos.x = 1000;
        pos.y = 0;
        cam.transform.position = pos; //sets camera to the blue background location

    }

    public int[] getSelection() { //getter for powerUpSelection array
        return powerUpSelection;
    }
}
