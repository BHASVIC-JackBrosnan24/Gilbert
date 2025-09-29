using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private GameObject Hammer;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) == true) {
            GameObject hammer = Instantiate(Hammer);
            Vector2 hammerPos = transform.position;
            hammer.transform.position = hammerPos;
        }
    }
}
