using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    [SerializeField]
    int buttonNum;//the number assossiated with this button

    int powerType;
    GameObject ButtonController;
    ButtonController buttonControllerScript;
    void Awake()
    {
        ButtonController = GameObject.FindWithTag("ButtonController"); //gets the button controller
        buttonControllerScript = ButtonController.GetComponent<ButtonController>(); 
        int[] selection = buttonControllerScript.getSelection(); //gets the selection array from the BC
        powerType = selection[buttonNum]; //gets the power type in the correct index
    }


    public int getPowerType() { //getter for powerType
        return powerType;
    }
}
