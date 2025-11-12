using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform player;//the players location
    private Vector3 pos;// a vector 3 for the position of the camera
    bool moving = true;
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;//gets the player's transform value
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null&& moving==true)
        {
            pos = player.position;
            pos.z = -10;//makes sure the z position of the camera never moves
            transform.position = pos;
        }
    }

    public void setMove(bool move)
    {
        moving = move;
    }
}
