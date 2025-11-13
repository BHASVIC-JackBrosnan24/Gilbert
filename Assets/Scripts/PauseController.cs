using JetBrains.Annotations;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    public int unpaused = 1; //global variable for everyhting not moving

    public void pause() //stops all movement and timers
    {
        unpaused = 0;
    }

    public void unpause() { //resumes all movement and timers
        unpaused = 1;
    }
}
