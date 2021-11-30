using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

	public static bool game_is_paused = false;
	public GameObject pauseMenuUI;
	public GameObject pauseButtonUI;

	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
		{
			if (game_is_paused)
			{
				Resume();
			}
			else
			{
				Pause();
			}
		}
	}

	public void Resume()
	{
		pauseMenuUI.SetActive(false);
		pauseButtonUI.SetActive(true);		
		Time.timeScale = 1f;
		game_is_paused = false;
	}

	public void Pause()
	{
		pauseMenuUI.SetActive(true);
		pauseButtonUI.SetActive(false);
		Time.timeScale = 0f;
		game_is_paused = true;		
	}

	public void LoadMainMenu()
	{
		Time.timeScale = 1f;
		LevelManager.LoadMenu();
	}
}
