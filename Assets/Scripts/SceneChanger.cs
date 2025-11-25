using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public static SceneChanger instance;
    void Awake()
    {
        if (instance == null) //checks if any instance has existed before
        {
            instance = this; //if not, this becomes the only instance
            DontDestroyOnLoad(this.gameObject); //makes sure this isn't destroyed when a new scene is loaded
        }
        else
        {
            Destroy(gameObject); //destroys this is an instance already exists
        }
    }

    // Update is called once per frame
    public void startGameplay()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void deathScreen() {
        SceneManager.LoadScene("DeathScreen");
    }

    public void winScreen() {
        Invoke("invokedWin", 2.5f);
    }

    public void invokedWin() {
        SceneManager.LoadScene("WinScreen");
    }
}
