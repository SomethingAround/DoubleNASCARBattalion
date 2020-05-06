using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundsSystem : MonoBehaviour
{
    float currentTime = 0.0f;
    float startingTime = 5.0f;

    int numberOfRounds = 3;
    int currentRound = 1;
	[HideInInspector]
    public bool gameOver = true;

    [SerializeField] Text countdownText;

    Color textColor;

	public GameObject endRound;

	public GameObject gameActive;

	public PlayerController[] players;

    private void Start()
    {
        currentTime = startingTime;
    }

    private void Update()
    {
		if(!gameOver)
		  currentTime -= 1.0f * Time.deltaTime;
        countdownText.text = currentTime.ToString("0");

        //if (currentTime <= 0.0f)
        //{
        //    currentTime = 0.0f;
        //    textColor = Color.red;
        //    currentRound++;
        //    currentTime = 90.0f;
        //}

        if (currentTime < 90.0f && currentTime > 60.0f)
            textColor = Color.green;

        if (currentTime < 60.0f && currentTime > 0.0f)
            textColor = Color.yellow;

        countdownText.color = textColor;

		if (currentTime <= 0.0f)
		{
			gameOver = true;
			for (int i = 0; i < players.Length; ++i)
			{
				players[i].gameActive = false;
				players[i].turret.m_gameActive = false;
				players[i].gameObject.transform.position = players[i].startPosition;
			}
			endRound.SetActive(true);
			gameActive.SetActive(false);
			ResetTimer();
		}
    }

	public void ResetTimer()
	{
		currentTime = startingTime;
	}
}
