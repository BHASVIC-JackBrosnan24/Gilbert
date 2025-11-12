using UnityEngine;

public class PauseController : MonoBehaviour
{
    public delegate void paused();
    public static event paused pauseAll; 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
