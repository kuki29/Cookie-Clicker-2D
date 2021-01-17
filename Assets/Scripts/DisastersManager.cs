using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using static GlobalResources;
using static GlobalEmployees;

public class DisastersManager : MonoBehaviour
{
    enum DisasterType
    {
        Once,
        Continuos
    }

    enum DisasterTarget
    {
        Cookies,
        Money,
        Bakers
    }

    enum DisasterDamageType
    {
        Percentage,
        Amount
    }

    struct Disaster
    {
        public string Message;
        public DisasterType Type;
        public DisasterTarget Target;
        public DisasterDamageType DamageType;
        public int? MaxAmount;
        public float? MaxPercentage;
        public int? MaxDuration;
    }


    public GameObject statusDisplay;
    [Range(0f, 1f)]
    public float generatingChance = 0.1f;
    [Range(0f, 1f)]
    public float difficulty = 0.5f;

    private bool isDisasterActive = false;
    private List<Disaster> Disasters = new List<Disaster>
        {
            new Disaster
            {
                Message = "There is a fire in the kithen!",
                Type = DisasterType.Continuos,
                Target = DisasterTarget.Cookies,
                DamageType = DisasterDamageType.Amount,
                MaxAmount = 10,
                MaxDuration = 15
            },
            new Disaster
            {
                Message = "Some of your workers decided to retire!",
                Type = DisasterType.Once,
                Target = DisasterTarget.Bakers,
                DamageType = DisasterDamageType.Percentage,
                MaxPercentage = 0.75f,
            },
            new Disaster
            {
                Message = "Someone robbed you!",
                Type = DisasterType.Once,
                Target = DisasterTarget.Money,
                DamageType = DisasterDamageType.Percentage,
                MaxPercentage = 0.99f
            }
        };

    void Update()
    {
        if (!isDisasterActive)
		{
            StartCoroutine(DisastersLogic());
		}
    }

    IEnumerator DisastersLogic()
	{
        isDisasterActive = true;
        if (Random.Range(0f, 1f) < generatingChance)
        {
            int DisasterIndex = Random.Range(0, Disasters.Count);
            StartCoroutine(DoDisaster(Disasters[DisasterIndex]));
        }

        yield return new WaitForSeconds(5);

        statusDisplay.SetActive(false);
        statusDisplay.SetActive(true);
        isDisasterActive = false;
    }

    IEnumerator DoDisaster(Disaster disaster, int? duration = null)
    {
        string message = disaster.Message + "\nYou lost ";
        int damage = 0;

        /* Note: if we didn`t send duration of disaster, we check if it has max duration at all,
         * in case it has we call DoDisaster() recursiwly and in inner calls we skip this statement,
         * because we have duration now */

        if (duration == null)
		{
            if (disaster.Type == DisasterType.Continuos)
		    {
                StartCoroutine(DoDisaster(disaster, Random.Range(Mathf.RoundToInt(disaster.MaxDuration.Value / 3f), disaster.MaxDuration.Value)));
		    }
		}

        /* Note: if we have duration of a disaster we call DoDisaster() recursiwly for every 5 seconds
         * of a duration of it */

        if (duration.HasValue && duration.Value > 0)
		{
            StartCoroutine(DoDisaster(disaster, duration - 5));
		}

        if (disaster.DamageType == DisasterDamageType.Amount)
        {
            if (disaster.MaxAmount.HasValue)
            {
                damage = Random.Range(Mathf.RoundToInt(disaster.MaxAmount.Value / 3f), disaster.MaxAmount.Value);
            }
            else
			{
                Debug.LogError("Disaster wich has an amount type of damage must have a value of max amount of damage!!! ");
                yield break;
            }
        }
        else
		{
            if (disaster.MaxPercentage.HasValue)
            {
                damage = Mathf.RoundToInt(Random.Range(disaster.MaxPercentage.Value / 3f, disaster.MaxPercentage.Value));

                if (disaster.Target == DisasterTarget.Bakers)
				{
                    damage *= bakersCount;
				}
                else if (disaster.Target == DisasterTarget.Cookies)
				{
                    damage *= cookiesCount;
				}
                else if (disaster.Target == DisasterTarget.Money)
				{
                    damage *= moneyCount;
				}
                else
				{
                    Debug.LogError("Unknown type of disaster target!!!");
                    yield break;
                }
            }
            else
            {
                Debug.LogError("Disaster wich has an amount type of damage must have a value of max amount of damage!!! ");
                yield break;
            }
        }

        damage = Mathf.RoundToInt(damage * difficulty);

        if (damage == 0)
		{
            yield break;
        }

        switch (disaster.Target)
		{
            case DisasterTarget.Cookies:
                if (cookiesCount == 0)
                {
                    damage = 0;
                    Debug.Log("Not enough cookies to process disaster");
                    yield break;
                }

                if (damage > cookiesCount)
				{
                    damage = cookiesCount;
                    cookiesCount = 0;
				}
                else
				{
                    cookiesCount -= damage;
				}

                message += damage + " cookies.";
                break;

            case DisasterTarget.Money:
                if (moneyCount == 0)
                {
                    damage = 0;
                    Debug.Log("Not enough money to process disaster");
                    yield break;
                }

                if (damage > moneyCount)
                {
                    damage = moneyCount;
                    moneyCount = 0;
                }
                else
                {
                    moneyCount -= damage;
                }

                message += damage + "$.";
                break;

            case DisasterTarget.Bakers:
                if (bakersCount == 0)
                {
                    damage = 0;
                    Debug.Log("Not enough bakers to process disaster");
                    yield break;
                }

                if (damage > bakersCount)
                {
                    damage = bakersCount;
                    bakersCount = 0;
                }
                else
                {
                    bakersCount -= damage;
                }

                message += damage + " bakers.";
                break;
        }

        if (damage > 0)
		{
            statusDisplay.GetComponent<Text>().text = message;
            statusDisplay.GetComponent<Animation>().PlayQueued("Display1s");
            statusDisplay.GetComponent<Animation>().PlayQueued("StatusAnim");
		}

        yield return new WaitForSeconds(5);
    }
}
