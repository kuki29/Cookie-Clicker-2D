using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GlobalResourceManagment : MonoBehaviour
{
    public static int cookiesCount = 0;
    public static int moneyCount = 100;
    public static int bakersCount = 0;
    public static int sellManagersCount = 0;

    public static int bakerPrice = 10;
    public static int sellManagerPrice = 30;

    public GameObject cookieDisplay;
    public GameObject moneyDisplay;
    public GameObject cookieProducingDisplay;
    public GameObject cookieSellingDisplay;
    public GameObject bakersPriceDisplay;
    public GameObject sellManagersPriceDisplay;
    public GameObject statusDisplay;

    public GameObject statisticPopup;

    public GameObject bakersButton;
    public GameObject sellManagersButton;

    public GameObject sellCookieAudio;  // Note: Gameobject because it has two different sounds

    public AudioSource HireBakerSound;
    public AudioSource makeCookieSound;
    public AudioSource buzzSound;

    private bool isCreatingCookie = false;
    private float bakersProductivity = 1f;
    private bool isSellingCookie = false;
    private float sellManagersProductivity = 1f;

    private static uint madeCookies = 0;
    private static uint madeMoney = 0;
    private static uint spendMoney = 0;
    private static uint hiredBakers = 0;
    private static uint hiredSellManagers = 0;

    void Update()
    {
        DisplayStats();
        DisplayBakersPrice();
        DisplaySellManagersPrice();

        CookieGenerator();
        CookieSeller();

        BuyButtonsAvailability();
    }

    public void DisplayStats()
    {
        DisplayCookiesAmount();
        DisplayCashAmount();
        DisplayCookieGeneratingAmount();
        DisplayCookieSellingAmount();
    }
    
    public void HireBaker()
    {
        if (moneyCount >= bakerPrice)
        {
            HireBakerSound.Play();
            AddBaker();

            if (cookieProducingDisplay.active == false)
			{
                cookieProducingDisplay.SetActive(true);
			}
        }
        else
        {
            statusDisplay.GetComponent<Text>().text = "Not enough money to hire a baker.";
            statusDisplay.GetComponent<Animation>().Play("StatusAnim");
        }
    }
    
    public void HireSellManager()
	{
        if (moneyCount >= sellManagerPrice)
		{
            AddSellManager();

            if (cookieSellingDisplay.active == false)
			{
                cookieSellingDisplay.SetActive(true);
			}
		}
        else
		{
            statusDisplay.GetComponent<Text>().text = "Not enough money to hire a sell manager.";
            statusDisplay.GetComponent<Animation>().Play("StatusAnim");
        }
	}

    public void MakeCookie()
	{
        makeCookieSound.Play();
        cookiesCount++;
        madeCookies++;
    }

    public void ProcessSellCookie()
	{
        if (cookiesCount > 0)
        {
            ProcessSellClick();
            PlaySellAudio();
        }
        else if (cookiesCount == 0)
        {
            statusDisplay.GetComponent<Text>().text = "Not enough cookies to sell.";
            statusDisplay.GetComponent<Animation>().Play("StatusAnim");
            buzzSound.Play();
        }
    }

    public void ShowStatistic()
	{
        statisticPopup.SetActive(true);

        string statistic = "You made " + madeCookies + " cookies\n" +
                            "You made $" + madeMoney + "\n" +
                            "You spend $" + spendMoney + "\n" +
                            "You hired " + hiredBakers + " bakers\n" +
                            "You hires " + hiredSellManagers + " sell managers";

        statisticPopup.GetComponentInChildren<Text>().text = statistic;
	}

    public void CloseStatistic()
	{
        statisticPopup.SetActive(false);
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
        cookieProducingDisplay.GetComponent<Text>().text = "You making " + GetCookiesPerSecond() + " cookies per second";
    }

    void DisplayCookieSellingAmount()
	{
        cookieSellingDisplay.GetComponent<Text>().text = "You selling " + GetCookieSellsPerSecond() + " cookies per second";
    }

    void DisplayBakersPrice()
    {
        bakersPriceDisplay.GetComponent<Text>().text = "$" + bakerPrice;
    }

    void DisplaySellManagersPrice()
	{
        sellManagersPriceDisplay.GetComponent<Text>().text = "$" + sellManagerPrice;
	}

    int GetCookiesPerSecond()
	{
        return Mathf.RoundToInt(bakersCount * bakersProductivity);
	}

    int GetCookieSellsPerSecond()
	{
        return Mathf.RoundToInt(sellManagersCount * sellManagersProductivity);
	}

    void BuyButtonsAvailability()
    {
        if (moneyCount >= bakerPrice)
        {
            bakersButton.GetComponent<Button>().interactable = true;
        }
        if (moneyCount >= sellManagerPrice)
		{
            sellManagersButton.GetComponent<Button>().interactable = true;
		}
    }

    void SellCookies(int amount)
    {
        if (cookiesCount > amount)
        {
            cookiesCount -= amount;
            moneyCount += amount;
        }
        else
        {
            moneyCount += cookiesCount;
            cookiesCount = 0;
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

    void CookieGenerator()
    {
        if (GetCookiesPerSecond() > 0)
        {
            if (!isCreatingCookie)
            {
                isCreatingCookie = true;
                StartCoroutine(AutoCreateCookie());
            }
        }
    }

    IEnumerator AutoCreateCookie()
    {
        cookiesCount += GetCookiesPerSecond();
        madeCookies += (uint)GetCookiesPerSecond();
        yield return new WaitForSeconds(1);
        isCreatingCookie = false;
    }

    void CookieSeller()
	{
        if (GetCookieSellsPerSecond() > 0)
		{
            if (!isSellingCookie)
			{
                isSellingCookie = true;
                StartCoroutine(AutoSellCookie());
			}                
		}
	}

    IEnumerator AutoSellCookie()
	{
        if (GetCookieSellsPerSecond() > cookiesCount)
		{
            moneyCount += cookiesCount;
            madeMoney += (uint)cookiesCount;
            cookiesCount = 0;
		}
        else
		{
            moneyCount += GetCookieSellsPerSecond();
            cookiesCount -= GetCookieSellsPerSecond();
            madeMoney += (uint)GetCookieSellsPerSecond();
		}

        yield return new WaitForSeconds(1);
        isSellingCookie = false;
	}

    void AddBaker()
    {
        if (moneyCount >= bakerPrice)
		{
            moneyCount -= bakerPrice;
            spendMoney += (uint)bakerPrice;
            bakersCount++;
            hiredBakers++;
            bakerPrice = bakersCount * 7 + 10;
		}
    }

    void AddSellManager()
	{
        if (moneyCount >= sellManagerPrice)
		{
            moneyCount -= sellManagerPrice;
            spendMoney += (uint)sellManagerPrice;
            sellManagersCount++;
            hiredSellManagers++;
            sellManagerPrice = (sellManagersCount * 9) + 30;
		}
	}
}
