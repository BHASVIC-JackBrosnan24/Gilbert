using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private float timer;

    private int intTime;

    [SerializeField]
    TextMeshProUGUI m_Object;

    PauseController pauseController;

    void Start()
    {
        intTime = (int) timer;
        pauseController = GameObject.Find("PauseController").GetComponent<PauseController>();
    }

    // Update is called once per frame
    void Update()
    {
        timer=timer-(Time.deltaTime * pauseController.unpaused);
        intTime = (int)timer;
        m_Object.text = intTime.ToString();
        if (timer <= 0) {
            m_Object.fontSize = 0;
        }
    }

    public int getTime() {
        return intTime;
    }
}
