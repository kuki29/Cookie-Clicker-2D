using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalResourceManagment : MonoBehaviour
{
    public static int cookieCount = 0;
    public static int cashCount = 0;
    public static int bakersCount = 0;

    public static int bakerPrice = 10;

    public GameObject cookieDisplay;
    public GameObject cashDisplay;
    public GameObject bakersDisplay;
    public GameObject bakersPriceDisplay;
    public GameObject statusDisplay;

    public GameObject bakersButton;

    public GameObject sellCookieAudio;  // Note: Gameobject because it has two different sounds

    public AudioSource buyBakerSound;
    public AudioSource makeCookieSound;
    public AudioSource buzzSound;

    private bool isCreatingCookie = false;
    private float productivity = 1f;

    void Update()
    {
        DisplayStats();
        DisplayBakersPrice();

        CookieGenerator();

        BuyButtonsAvailability();
    }

    public void DisplayStats()
    {
        DisplayCookiesAmount();
        DisplayCashAmount();
        DisplayBakersAmount();
    }
    
    public void BuyBaker()
    {
        if (cashCount >= bakerPrice)
        {
            buyBakerSound.Play();
            AddBaker();
            if (bakersDisplay.active == false)
			{
                bakersDisplay.SetActive(true);
			}
        }
        else
        {
            statusDisplay.GetComponent<Text>().text = "Not enough money to buy auto cookie.";
            statusDisplay.GetComponent<Animation>().Play("StatusAnim");
        }
    }
    
    public void MakeCookie()
	{
        makeCookieSound.Play();
        cookieCount++;
    }

    public void ProcessSellCookie()
	{
        if (cookieCount > 0)
        {
            ProcessSellClick();
            PlaySellAudio();
        }
        else if (cookieCount == 0)
        {
            statusDisplay.GetComponent<Text>().text = "Not enough cookies to sell.";
            statusDisplay.GetComponent<Animation>().Play("StatusAnim");
            buzzSound.Play();
        }
    }


    void DisplayCookiesAmount()
    {
        cookieDisplay.GetComponent<Text>().text = "Cookies: " + cookieCount;
    }

    void DisplayCashAmount()
    {
        cashDisplay.GetComponent<Text>().text = "$" + cashCount;
    }

    void DisplayBakersAmount()
    {
        bakersDisplay.GetComponent<Text>().text = "Bakers: " + bakersCount;
    }

    void DisplayBakersPrice()
    {
        bakersPriceDisplay.GetComponent<Text>().text = "$" + bakerPrice;
    }

    void BuyButtonsAvailability()
    {
        if (cashCount >= bakerPrice)
        {
            bakersButton.GetComponent<Button>().interactable = true;
        }
    }

    void SellCookies(int amount)
    {
        if (cookieCount > amount)
        {
            cookieCount -= amount;
            cashCount += amount;
        }
        else
        {
            cashCount += cookieCount;
            cookieCount = 0;
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
        sellCookieAudio.GetComponents<AudioSource>()[Random.Range(0, 2)].Play();
    }

    void CookieGenerator()
    {
        if (bakersCount > 0)
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
        cookieCount += Mathf.RoundToInt(bakersCount * productivity);
        yield return new WaitForSeconds(1);
        isCreatingCookie = false;
    }

    void AddBaker()
    {
        if (cashCount >= bakerPrice)
		{
            cashCount -= bakerPrice;
            bakersCount++;
            bakerPrice = bakersCount * 7 + 10;
		}
    }
}
