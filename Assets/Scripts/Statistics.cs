using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Statistics : MonoBehaviour
{
    public GameObject statisticPopup;

    public static int madeCookies = 0;
    public static int madeMoney = 0;
    public static int spendMoney = 0;
    public static int hiredBakers = 0;
    public static int hiredSellManagers = 0;

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
}