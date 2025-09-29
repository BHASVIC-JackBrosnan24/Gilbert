using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform player;
    private Vector3 pos;
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        pos = player.position;
        pos.z = -10;
        transform.position = pos;
    }
}
