using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundsSystem : MonoBehaviour
{
    float currentTime = 0.0f;
    float startingTime = 90.0f;

    int numberOfRounds = 3;
    int currentRound = 1;

    bool gameOver = false;

    [SerializeField] Text countdownText;

    Color textColor;

    private void Start()
    {
        currentTime = startingTime;
    }

    private void Update()
    {
        currentTime -= 1.0f * Time.deltaTime;
        countdownText.text = currentTime.ToString("0");

        if (currentTime <= 0.0f)
        {
            currentTime = 0.0f;
            textColor = Color.red;
            currentRound++;
            currentTime = 90.0f;
        }

        if (currentTime < 90.0f && currentTime > 60.0f)
            textColor = Color.green;

        if (currentTime < 60.0f && currentTime > 0.0f)
            textColor = Color.yellow;

        countdownText.color = textColor;

        if (currentRound == 3 && currentTime == 0)
            gameOver = true;
    }
}
