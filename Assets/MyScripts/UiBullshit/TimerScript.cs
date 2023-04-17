using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TimerScript : MonoBehaviour
{
    public float totalTime = 60f;
    public TextMeshProUGUI timerText;

    private float timeRemaining;
    private bool isRunning = false;

    void Start()
    {
        timeRemaining = totalTime;
        StartTimer();
        UpdateTimerText();
    }

    void Update()
    {
        if (isRunning)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerText();

            if (timeRemaining <= 10f)
            {
                timerText.color = Color.red;
            }

            if (timeRemaining <= 0f)
            {
                isRunning = false;
                timerText.text = "Time's up!";
            }
        }
    }

    public void StartTimer()
    {
        isRunning = true;
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
