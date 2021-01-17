using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
	public GameObject pauseMenu;

    public void Pause()
	{
		pauseMenu.SetActive(true);
		Time.timeScale = 0f;
	}

	public void Resume()
	{
		pauseMenu.SetActive(false);
		Time.timeScale = 1f;
	}

	public void QuitGame()
	{
		Application.Quit();
	}

	public void SaveGame()
	{

	}

	public void LoadGame()
	{

	}
}
