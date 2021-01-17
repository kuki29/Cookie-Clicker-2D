using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalEmployees : MonoBehaviour
{
    public static int bakersCount = 0;
    public static int sellManagersCount = 0;

    public static int bakerPrice = 10;
    public static int sellManagerPrice = 30;

    public GameObject bakersPriceDisplay;
    public GameObject sellManagersPriceDisplay;
    public GameObject statusDisplay;

    public GameObject bakersButton;
    public GameObject sellManagersButton;

    public AudioSource HireBakerSound;

    private bool isCreatingCookie = false;
    private static float bakersProductivity = 1f;
    private bool isSellingCookie = false;
    private static float sellManagersProductivity = 1f;

    void Update()
    {
        DisplayBakersPrice();
        DisplaySellManagersPrice();

        CookieGenerator();
        CookieSeller();

        BuyButtonsAvailability();
    }

    public void HireBaker()
    {
        if (GlobalResources.moneyCount >= bakerPrice)
        {
            HireBakerSound.Play();
            AddBaker();

            if (!GlobalResources.isCookieGeneratingDisplayActive)
            {
                GetComponent<GlobalResources>().ShowCookieGeneratingDisplay();
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
        if (GlobalResources.moneyCount >= sellManagerPrice)
        {
            AddSellManager();

            if (!GlobalResources.isCookieGeneratingDisplayActive)
            {
                GetComponent<GlobalResources>().ShowCookieSellingDisplay();
            }
        }
        else
        {
            statusDisplay.GetComponent<Text>().text = "Not enough money to hire a sell manager.";
            statusDisplay.GetComponent<Animation>().Play("StatusAnim");
        }
    }

    public static int GetCookiesPerSecond()
    {
        return Mathf.RoundToInt(bakersCount * bakersProductivity);
    }

    public static int GetCookieSellsPerSecond()
    {
        return Mathf.RoundToInt(sellManagersCount * sellManagersProductivity);
    }

    void DisplayBakersPrice()
    {
        bakersPriceDisplay.GetComponent<Text>().text = "$" + bakerPrice;
    }

    void DisplaySellManagersPrice()
    {
        sellManagersPriceDisplay.GetComponent<Text>().text = "$" + sellManagerPrice;
    }

    void BuyButtonsAvailability()
    {
        if (GlobalResources.moneyCount >= bakerPrice)
        {
            bakersButton.GetComponent<Button>().interactable = true;
        }
        if (GlobalResources.moneyCount >= sellManagerPrice)
        {
            sellManagersButton.GetComponent<Button>().interactable = true;
        }
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
        GlobalResources.cookiesCount += GetCookiesPerSecond();
        Statistics.madeCookies += GetCookiesPerSecond();
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
        if (GetCookieSellsPerSecond() > GlobalResources.cookiesCount)
        {
            GlobalResources.moneyCount += GlobalResources.cookiesCount;
            Statistics.madeMoney += GlobalResources.cookiesCount;
            GlobalResources.cookiesCount = 0;
        }
        else
        {
            GlobalResources.moneyCount += GetCookieSellsPerSecond();
            GlobalResources.cookiesCount -= GetCookieSellsPerSecond();
            Statistics.madeMoney += GetCookieSellsPerSecond();
        }

        yield return new WaitForSeconds(1);
        isSellingCookie = false;
    }

    void AddBaker()
    {
        if (GlobalResources.moneyCount >= bakerPrice)
        {
            GlobalResources.moneyCount -= bakerPrice;
            Statistics.spendMoney += bakerPrice;
            bakersCount++;
            Statistics.hiredBakers++;
            bakerPrice = bakersCount * 7 + 10;
        }
    }

    void AddSellManager()
    {
        if (GlobalResources.moneyCount >= sellManagerPrice)
        {
            GlobalResources.moneyCount -= sellManagerPrice;
            Statistics.spendMoney += sellManagerPrice;
            sellManagersCount++;
            Statistics.hiredSellManagers++;
            sellManagerPrice = (sellManagersCount * 9) + 30;
        }
    }
}