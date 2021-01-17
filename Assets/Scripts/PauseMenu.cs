using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
		Application.Quit();
	}

	public void SaveGame()
	{
		PlayerPrefs.SetInt("cookies", GlobalResources.cookiesCount);
		PlayerPrefs.SetInt("money", GlobalResources.moneyCount);
		PlayerPrefs.SetInt("bakers", GlobalEmployees.bakersCount);
		PlayerPrefs.SetInt("sellManagers", GlobalEmployees.sellManagersCount);
		PlayerPrefs.SetInt("madeCookies", Statistics.madeCookies);
		PlayerPrefs.SetInt("madeMoney", Statistics.madeMoney);
		PlayerPrefs.SetInt("spendMoney", Statistics.spendMoney);
		PlayerPrefs.SetInt("hiredBakers", Statistics.hiredBakers);
		PlayerPrefs.SetInt("hiredSellManagers", Statistics.hiredSellManagers);
		PlayerPrefs.SetFloat("disastersGeneratingChance", mechanicsObject.GetComponent<DisastersManager>().generatingChance);
		PlayerPrefs.SetFloat("disastersDifficulty", mechanicsObject.GetComponent<DisastersManager>().difficulty);
	}

	public void LoadGame()
	{
		GlobalResources.cookiesCount = PlayerPrefs.GetInt("cookies");
		GlobalResources.moneyCount = PlayerPrefs.GetInt("money");
		GlobalEmployees.bakersCount = PlayerPrefs.GetInt("bakers");
		GlobalEmployees.sellManagersCount = PlayerPrefs.GetInt("sellManagers");
		Statistics.madeCookies = PlayerPrefs.GetInt("madeCookies");
		Statistics.madeMoney = PlayerPrefs.GetInt("madeMoney");
		Statistics.spendMoney = PlayerPrefs.GetInt("spendMoney");
		Statistics.hiredBakers = PlayerPrefs.GetInt("hiredBakers");
		Statistics.hiredSellManagers = PlayerPrefs.GetInt("hiredSellManagers");
		mechanicsObject.GetComponent<DisastersManager>().generatingChance = PlayerPrefs.GetFloat("disastersGeneratingChance");
		mechanicsObject.GetComponent<DisastersManager>().difficulty = PlayerPrefs.GetFloat("disastersDifficulty");
	}
}
