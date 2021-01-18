using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
	public AudioMixer mixer;
	public GameObject settingsMenu;

    public void SetVolume(float volume)
	{
		mixer.SetFloat("volume", volume);
	}

	public void Open()
	{
		settingsMenu.SetActive(true);
	}

	public void Close()
	{
		settingsMenu.SetActive(false);
	}
}
