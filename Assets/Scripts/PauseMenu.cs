using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	public GameObject pauseMenu;
	public GameObject mechanicsObject;

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
		SceneManager.LoadScene(0);
	}

	public void SaveGame()
	{
		Saves.Save(mechanicsObject.GetComponent<DisastersManager>());
	}

	public void LoadGame()
	{
		Saves.Load(mechanicsObject.GetComponent<DisastersManager>());
	}
}
