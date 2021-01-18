using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoader : MonoBehaviour
{
    public GameObject mechanicsObject;
 
    void Start()
    {
        if (MainMenu.isLoading)
		{
            Saves.Load(mechanicsObject.GetComponent<DisastersManager>());
		}
    }
}
