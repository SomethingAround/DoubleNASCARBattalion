using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject endRound;
	public GameObject gameActive;

	public RoundsSystem roundsSystem;

    public PlayerController[] players;

    public void startGame()
    {
        mainMenu.SetActive(false);
		for (int i = 0; i < players.Length; ++i)
		{
			players[i].gameActive = true;
			players[i].turret.m_gameActive = true;
			players[i].points = 0;
		}
		gameActive.SetActive(true);
		roundsSystem.enabled = true;
		roundsSystem.ResetTimer();
		roundsSystem.gameOver = false;
	}

    // Go back to the main menu
    public void backButton()
    {
		mainMenu.SetActive(true);
		endRound.SetActive(false);
    }

    // Close the game
    public void exitGame()
    {
        Application.Quit();
    }
}
