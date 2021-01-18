using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saves : MonoBehaviour
{
	public static void Save(DisastersManager disastersManager)
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
		PlayerPrefs.SetFloat("disastersGeneratingChance", disastersManager.generatingChance);
		PlayerPrefs.SetFloat("disastersDifficulty", disastersManager.difficulty);
	}

	public static void Load(DisastersManager disastersManager)
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
		disastersManager.generatingChance = PlayerPrefs.GetFloat("disastersGeneratingChance");
		disastersManager.difficulty = PlayerPrefs.GetFloat("disastersDifficulty");
	}
}
