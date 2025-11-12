using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    [SerializeField]
    int buttonNum;//the number assossiated with this button

    int powerType;
    GameObject ButtonController;
    ButtonController buttonControllerScript;

    GameObject canvas;

    RectTransform buttonRectTransform;
    void Awake()
    {
        canvas = GameObject.FindWithTag("Canvas");
        buttonRectTransform = GetComponent<RectTransform>();
        ButtonController = GameObject.FindWithTag("ButtonController"); //gets the button controller
        buttonControllerScript = ButtonController.GetComponent<ButtonController>(); 
        int[] selection = buttonControllerScript.getSelection(); //gets the selection array from the BC
        powerType = selection[buttonNum]; //gets the power type in the correct index
        Vector3 position = new Vector3(411f,270-(90*buttonNum)); //calcs  correct position for the specific button
        buttonRectTransform.position = position; //sets it to correct position
        this.transform.SetParent(canvas.transform); //makes the canvas its parent
    }


    public int getPowerType() { //getter for powerType
        return powerType;
    }

    public void destruct() //destroys this button
    {
        Destroy(this.gameObject);
    }
}
