using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GlobalResources : MonoBehaviour
{
    public static int cookiesCount = 0;
    public static int moneyCount = 100;

    public GameObject cookieDisplay;
    public GameObject moneyDisplay;
    public GameObject cookieGeneratingDisplay;
    public GameObject cookieSellingDisplay;
    public GameObject statusDisplay;

    public static bool isCookieGeneratingDisplayActive = false;
    public static bool isCookieSellingDisplayActive = false;

    public GameObject statisticPopup;

    public GameObject sellCookieAudio;  // Note: Gameobject because it has two different sounds
    public AudioSource makeCookieSound;
    public AudioSource buzzSound;

    private static uint madeCookies = 0;
    private static uint madeMoney = 0;
    private static uint spendMoney = 0;
    private static uint hiredBakers = 0;
    private static uint hiredSellManagers = 0;

    void Update()
    {
        DisplayStats();
    }

    public void DisplayStats()
    {
        DisplayCookiesAmount();
        DisplayCashAmount();
        DisplayCookieGeneratingAmount();
        DisplayCookieSellingAmount();
    }

    public void MakeCookie()
    {
        makeCookieSound.Play();
        cookiesCount++;
        madeCookies++;
    }

    public void ShowCookieGeneratingDisplay()
	{
        cookieGeneratingDisplay.SetActive(true);
    }
    
    public void ShowCookieSellingDisplay()
	{
        cookieSellingDisplay.SetActive(true);
    }

    void DisplayCookiesAmount()
    {
        cookieDisplay.GetComponent<Text>().text = "Cookies: " + cookiesCount;
    }

    void DisplayCashAmount()
    {
        moneyDisplay.GetComponent<Text>().text = "$" + moneyCount;
    }

    void DisplayCookieGeneratingAmount()
    {
        cookieGeneratingDisplay.GetComponent<Text>().text = "You making " +
            GlobalEmployees.GetCookiesPerSecond() + " cookies per second";
    }

    void DisplayCookieSellingAmount()
    {
        cookieSellingDisplay.GetComponent<Text>().text = "You selling " + 
            GlobalEmployees.GetCookieSellsPerSecond() + " cookies per second";
    }

    void SellCookies(int amount)
    {
        if (cookiesCount > amount)
        {
            cookiesCount -= amount;
            moneyCount += amount;
            Statistics.madeMoney += amount;
        }
        else
        {
            moneyCount += cookiesCount;
            Statistics.madeMoney += cookiesCount;
            cookiesCount = 0;
        }
    }

    public void ProcessSellCookie()
    {
        if (GlobalResources.cookiesCount > 0)
        {
            ProcessSellClick();
            PlaySellAudio();
        }
        else if (GlobalResources.cookiesCount == 0)
        {
            statusDisplay.GetComponent<Text>().text = "Not enough cookies to sell.";
            statusDisplay.GetComponent<Animation>().Play("StatusAnim");
            buzzSound.Play();
        }
    }

    // TODO: rewrite it for multi purpose
    void ProcessSellClick()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            SellCookies(25);
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            SellCookies(10);
        }
        else if (Input.GetKey(KeyCode.LeftAlt))
        {
            SellCookies(5);
        }
        else
        {
            SellCookies(1);
        }
    }

    void PlaySellAudio()
    {
        sellCookieAudio.GetComponents<AudioSource>()[Random.Range(0, 1)].Play();
    }
}