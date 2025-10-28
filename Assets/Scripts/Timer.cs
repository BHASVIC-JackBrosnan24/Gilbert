using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private float timer;

    private int intTime;

    [SerializeField]
    TextMeshProUGUI m_Object;

    void Start()
    {
        intTime = (int) timer;
    }

    // Update is called once per frame
    void Update()
    {
        timer=timer-Time.deltaTime;
        intTime = (int)timer;
        m_Object.text = intTime.ToString();
    }

    public int getTime() {
        return intTime;
    }
}
