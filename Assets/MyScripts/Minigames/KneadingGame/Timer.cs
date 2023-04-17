using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 0;
    public bool timeIsRunning = true;
    public TMP_Text timeText;
    public GameObject KneadGameManager;


    // Start is called before the first frame update
    void Start()
    {
        timeIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (KneadGameManager.GetComponent<KneadGameManager>().gameStarted == true)
        {
            if (timeIsRunning)
            {
                if (timeRemaining >= 0)
                {
                    timeRemaining += Time.deltaTime;
                    DisplayTime(timeRemaining);
                }
            }
            if (KneadGameManager.GetComponent<KneadGameManager>().gameEnded == true)
            {
                timeIsRunning = false;
                DisplayTime(timeRemaining);
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:0} : {1:00}", minutes, seconds);
    }
}
